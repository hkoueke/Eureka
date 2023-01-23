using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class Role
    {
        public static readonly Error NotFound =
            Error.NotFound(
                "Role.NotFound", 
                "Role was not found.");

        public static readonly Error NotAssigned =
            Error.NotFound(
                "Role.NotAssigned", 
                "Role is not assigned.");

        public static readonly Error AlreadyAssigned =
            Error.NotFound(
                "Role.AlreadyAssigned", 
                "Role is already assigned.");
    }
}
