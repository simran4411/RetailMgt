using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailMgt.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public List<DeliveryToCustomer> DeliveryToCustomers { get; set; }
    }
}
