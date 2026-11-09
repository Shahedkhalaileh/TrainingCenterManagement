using TrainingCenterManagement.Interfaces;

namespace TrainingCenterManagement.PaymentMethods;

public class CashPaymentMethod : IPaymentMethod
{
    public string Name => "Cash";

    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}