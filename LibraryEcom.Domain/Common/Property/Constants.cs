namespace LibraryEcom.Domain.Common.Property;

public abstract class Constants
{
    public abstract class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Staff = "Staff";
    }
    
    public abstract class DbProviderKeys
    {
        public const string Npgsql = "Npgsql";
    }
    
    public abstract class Cors
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    }
    
}