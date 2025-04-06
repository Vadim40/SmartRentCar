using Newtonsoft.Json.Linq;

namespace ContractService.ContractConfig
{
public static class ContractLoader
{
    public static ContractDefinition Load(string path)
    {
        var json = File.ReadAllText(path);
        var jObject = JObject.Parse(json);

        var abi = jObject["abi"]?.ToString() ?? throw new Exception("ABI not found.");
        var bytecode = jObject["bytecode"]?.ToString() ?? throw new Exception("Bytecode not found.");

        return new ContractDefinition
        {
            Abi = abi,
            Bytecode = bytecode
        };
    }
}
}