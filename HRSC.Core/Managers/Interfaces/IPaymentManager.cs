using HRSC.Core.Extensions;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;

namespace HRSC.Core.Managers.Interfaces
{
    public interface IPaymentManager
    {
        Result<Payment> Get(Guid paymentId);
        IEnumerable<Payment> GetAll(Guid employeeId);
        Result<Guid> Create(Payment payment);
        Result<Payment> Update(Payment payment);
    }
}
