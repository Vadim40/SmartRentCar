namespace AuthService.Models;

public enum RoleIds
{
    User = 1,
    CompanyRepresentative = 2,
    Arbitrator = 3
}
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<UserRole> UserRoles { get; set; }
}
