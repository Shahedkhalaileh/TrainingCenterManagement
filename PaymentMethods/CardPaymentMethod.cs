using TrainingCenterManagement.Interfaces;

namespace TrainingCenterManagement.PaymentMethods;

public class CardPaymentMethod : IPaymentMethod
{
    private readonly bool _shouldSucceed;

    public CardPaymentMethod(bool shouldSucceed)
    {
        _shouldSucceed = shouldSucceed;
    }

    public string Name => "Card";

    public bool ProcessPayment(decimal amount)
    {
        return _shouldSucceed;
    }
}