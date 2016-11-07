using System.Collections.Generic;
using System.Web.Http;
using WebApiVersioning.Models;

namespace WebApiVersioning.Controllers
{
    public class CustomersController : ApiController
    {
        [VersionedRoute("api/Customers", 1)]
        public IEnumerable<Customer> GetCustomers1()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = 1,
                    CustomerName = "Customer 1 v1"
                },
                new Customer()
                {
                    Id = 2,
                    CustomerName = "Customer 2 v1"
                }
            };
        }

        [VersionedRoute("api/Customers2", 2)]
        public IEnumerable<Customer2> GetCustomers2()
        {
            return new List<Customer2>()
            {
                new Customer2()
                {
                    Id = 1,
                    Name = "Customer name 1 v2"
                },
                new Customer2()
                {
                    Id = 2,
                    Name = "Customer name 2 v2"
                }
            };
        }
    }
}
