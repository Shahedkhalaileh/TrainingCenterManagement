using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingCenterManagement.Interfaces
{
    public interface IPaymentMethod
    {
        string Name { get; }
        bool ProcessPayment(decimal amount);
    }
}
