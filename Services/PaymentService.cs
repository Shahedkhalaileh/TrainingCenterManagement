using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Enums;
using TrainingCenterManagement.Interfaces;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Services
{
    public class PaymentService
    {
        private readonly TrainingCenterData _data;

        public PaymentService(TrainingCenterData data)
        {
            _data = data;
        }
        public PaymentResult ProcessPayment(int enrollmentId, decimal amount,IPaymentMethod paymentMethod)
        {
            // 1. Find enrollment
            Enrollment? enrollment = null;

            foreach (Enrollment item in _data.Enrollments)
            {
                if (item.Id == enrollmentId)
                {
                    enrollment = item;
                    break;
                }
            }

            if (enrollment == null)
            {
                return PaymentResult.EnrollmentNotFound;
            }

            // 2. Check enrollment status
            if (enrollment.Status != EnrollmentStatus.Active)
            {
                return PaymentResult.InvalidEnrollmentStatus;
            }

            // 3. Check amount
            if (amount <= 0 ||
                amount != _data.Courses[enrollment.CourseId].Price)
            {
                return PaymentResult.InvalidAmount;
            }

            // 4. Check previous successful payment
            foreach (PaymentAttempt attempt in _data.PaymentAttempts)
            {
                if (attempt.EnrollmentId == enrollmentId &&
                    attempt.Status == PaymentStatus.Paid)
                {
                    return PaymentResult.AlreadyPaid;
                }
            }

            // 5. Create pending payment attempt
            int paymentId = _data.PaymentAttempts.Count + 1;

            PaymentAttempt paymentAttempt = new PaymentAttempt(
                paymentId,
                enrollmentId,
                amount,
                paymentMethod.Name,
                PaymentStatus.Pending
            );

            _data.PaymentAttempts.Add(paymentAttempt);

            // 6. Process payment
            bool isSuccessful = paymentMethod.ProcessPayment(amount);

            // 7. Update payment status
            if (isSuccessful)
            {
                paymentAttempt.UpdateStatus(PaymentStatus.Paid);

                return PaymentResult.PaymentSuccessful;
            }

            paymentAttempt.UpdateStatus(
                PaymentStatus.Failed,
                "Payment was declined."
            );

            return PaymentResult.PaymentFailed;
        }
    }
}
