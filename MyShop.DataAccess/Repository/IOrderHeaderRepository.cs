using Microsoft.EntityFrameworkCore.Update.Internal;
using MyShop.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Repository
{
    public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
    {
        public void Edit(OrderHeader orderHeader);
        public void updateOrderStatus(int id, string orderStatus, string paymentStatus);
    }
}
