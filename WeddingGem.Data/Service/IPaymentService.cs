using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Service
{
    public interface IPaymentService
    {
        Task<CustomerBusket> CreateOrUpdateInetent(string UserId);
    }
}
