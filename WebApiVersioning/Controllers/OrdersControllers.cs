using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiVersioning.Models;

namespace WebApiVersioning.Controllers
{
    public class OrdersController : ApiController
    {
        [VersionedRoute("api/Orders")]
        public IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CreatedDate = DateTime.UtcNow
                },
                new Order()
                {
                    Id = 2,
                    CreatedDate = DateTime.UtcNow
                }
            };
        }
    }
}
