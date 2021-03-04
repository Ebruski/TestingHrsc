using HRSC.Core.Data;
using HRSC.Core.Extensions;
using HRSC.Core.Managers.Interfaces;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRSC.Core.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly DataContext<Payment> _paymentContext;

        public PaymentManager()
        {
            _paymentContext = new DataContext<Payment>();
        }

        public Result<Guid> Create(Payment payment)
        {
            if (string.IsNullOrEmpty(payment.Month)) return Result<Guid>.Failure("Month is required");

            //Validation
            var pay = _paymentContext.GetAll().FirstOrDefault(x => x.EmployeeId == payment.EmployeeId && x.Month.ToUpper() == payment.Month.ToUpper());

            if (pay == null)
            {
                var rsp = _paymentContext.Add(payment);
                return Result<Guid>.Success(rsp, "Payment Saved Successfuly");
            }
            else
            {
                pay.Days = payment.Days;
                pay.SalaryPaid = payment.SalaryPaid;

                var rsp = _paymentContext.Update(pay);
                return Result<Guid>.Success(pay.Id, "Payment Updated Successfully");
            }
        }

        public Result<Payment> Get(Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAll(Guid employeeId)
        {
            return _paymentContext.GetAll().Where(x => x.EmployeeId == employeeId).OrderByDescending(x => x.Month);
        }

        public Result<Payment> Update(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
