using Ardalis.SmartEnum;

namespace SiteYonetimApp.Domain.Entities;


public sealed class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole Admin = new UserRole(nameof(Admin), 1);
    public static readonly UserRole Resident = new UserRole(nameof(Resident), 2);
    private UserRole(string name, int value) : base(name, value) { }
}


