using AutoMapper;
using ContractService.DTOs;
using ContractService.Models;
using ContractService.Repositories;
using Confluent.Kafka;
using System.Text.Json;
using ContractService.Kafka;

namespace ContractService.Services.Impl
{
    public class RentalServiceImpl : IRentalService
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;
        private readonly IBlockchainService _blockchainService;
        private readonly IKafkaProducer _kafkaProducer;
        private const string KafkaTopic = "rental-events";

        public RentalServiceImpl(
            IMapper mapper,
            IRentalRepository rentalRepository,
            IBlockchainService blockchainService,
            IKafkaProducer kafkaProducer)
        {
            _mapper = mapper;
            _rentalRepository = rentalRepository;
            _blockchainService = blockchainService;
            _kafkaProducer = kafkaProducer;
        }

    

        public async Task ApproveRental(int rentalId)
        {
            int statusId = (int)DTOs.RentalStatus.Completed;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }

        public async Task ConfirmEnd(int rentalId)
        {
            var confirmations = await _rentalRepository.GetStartConfirmations(rentalId);
            if (confirmations.Company)
                throw new InvalidOperationException("Уже подтверждено");

            string contractAddress = await _rentalRepository.GetContractAddress(rentalId);
            await _blockchainService.ConfirmEnd(contractAddress);

            await _rentalRepository.ConfirmEndByCompany(rentalId);
            await _kafkaProducer.SendMessageAsync("rental-events", rentalId.ToString(), new RentalEvent
            {
                RentalId = rentalId,
                EventType = "RentalEnd",
                Timestamp = DateTime.UtcNow
            });

            if (confirmations.Renter)
            {
                int statusId = (int)DTOs.RentalStatus.Completed;
                await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
              
            }
        }

        public async Task ConfirmEarlyEnd(int rentalId)
        {
            var confirmations = await _rentalRepository.GetStartConfirmations(rentalId);
            if (confirmations.Company)
                throw new InvalidOperationException("Уже подтверждено");

            await _rentalRepository.ConfirmEarlyEndByCompany(rentalId);

            string contractAddress = await _rentalRepository.GetContractAddress(rentalId);
            await _blockchainService.ConfirmEarlyEnd(contractAddress);

                      await _kafkaProducer.SendMessageAsync("rental-events", rentalId.ToString(), new RentalEvent
            {
                RentalId = rentalId,
                EventType = "RentalEarlyEnd",
                Timestamp = DateTime.UtcNow
            });

            int statusId = (int)DTOs.RentalStatus.Completed;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
          
        }

        public async Task ConfirmStart(int rentalId)
        {
            var confirmations = await _rentalRepository.GetStartConfirmations(rentalId);
            if (confirmations.Company)
                throw new InvalidOperationException("Уже подтверждено");

            await _rentalRepository.ConfirmStartByCompany(rentalId);

            string contractAddress = await _rentalRepository.GetContractAddress(rentalId);
            await _blockchainService.ConfirmStart(contractAddress);

                      await _kafkaProducer.SendMessageAsync("rental-events", rentalId.ToString(), new RentalEvent
            {
                RentalId = rentalId,
                EventType = "RentalStart",
                Timestamp = DateTime.UtcNow
            });

            if (confirmations.Renter)
            {
                int statusId = (int)DTOs.RentalStatus.Active;
                await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
              
            }
        }

        public async Task<RentalDTO> GetRental(int rentalId)
        {
            var rental = await _rentalRepository.GetRental(rentalId);
            return _mapper.Map<RentalDTO>(rental);
        }

        public async Task<List<RentalDTO>> GetRentals(FilterToRents filter, string role, int companyId)
        {
            List<Rental> rentals = new();

            if (role == "intermediary")
                rentals = await _rentalRepository.GetRentalsToArbiter(filter);
            else if (role == "company_rep")
                rentals = await _rentalRepository.GetRentalsByCompany(filter, companyId);
            else
                rentals = await _rentalRepository.GetRentalsToArbiter(filter);

            return _mapper.Map<List<RentalDTO>>(rentals);
        }

        public async Task<List<RentalStatusDTO>> GetRentalStatuses()
        {
            var statuses = await _rentalRepository.GetRentalStatuses();
            return _mapper.Map<List<RentalStatusDTO>>(statuses);
        }

        public async Task InitiateDispute(int rentalId)
        {
            int statusId = (int)DTOs.RentalStatus.PendingArbitration;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
         
        }

        public async Task SendToArbitration(int rentalId)
        {
            var isDisputeRequested = await _rentalRepository.GetDisputeRequest(rentalId);
            if (isDisputeRequested)
                throw new InvalidOperationException("Уже отправлено");

            string contractAddress = await _rentalRepository.GetContractAddress(rentalId);
            await _blockchainService.RaiseDispute(contractAddress);
            await _kafkaProducer.SendMessageAsync("rental-events", rentalId.ToString(), new RentalEvent
            {
                RentalId = rentalId,
                EventType = "DepositRaise",
                Timestamp = DateTime.UtcNow
            });
            await _rentalRepository.BeginDispute(rentalId);
            int statusId = (int)DTOs.RentalStatus.PendingArbitration;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);

          
        }
    }
}
