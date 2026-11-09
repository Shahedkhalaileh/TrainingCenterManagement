using System;
using System.Collections.Generic;
using System.Text;

using TrainingCenterManagement.Enums;

namespace TrainingCenterManagement.Models;

public class PaymentAttempt
{
    public int Id { get; private set; }
    public int EnrollmentId { get; private set; }
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime AttemptedAt { get; private set; }
    public string? FailureReason { get; private set; }

    public PaymentAttempt(
        int id,
        int enrollmentId,
        decimal amount,
        string paymentMethod,
        PaymentStatus status,
        string? failureReason = null)
    {
        Id = id;
        EnrollmentId = enrollmentId;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Status = status;
        AttemptedAt = DateTime.Now;
        FailureReason = failureReason;
    }

    public void UpdateStatus(
        PaymentStatus status,
        string? failureReason = null)
    {
        Status = status;
        FailureReason = failureReason;
    }
}
