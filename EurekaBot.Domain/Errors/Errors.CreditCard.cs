using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class CreditCard
    {
        public static readonly Error CardNumberInvalid = Error.Validation(
           "CreditCard.InvalidCardNumber",
           "Credit card number is invalid.");
    }
}
