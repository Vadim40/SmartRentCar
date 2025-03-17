export interface Company {
    companyId: number;
    name: string;
    email: string;
    phoneNumber: string;
    image: string;
}

export const Company: Company = {
    companyId: 1,
    name: 'google',
    email: 'gog@gmail.com',
    phoneNumber: "+111111111",
    image: 'assets/company.jpg'
}