using System;

namespace Restaurant.DataAccess.Entities
{
    public class Order
    {
        public int IdOrder { get; set; }
        public string NameClient { get; set; }
        public DateTime DateOrder { get; set; }
        public string PayMethod { get; set; }
        public string AsyncState { get; set; }
        public decimal Total { get; set; }
    }
}
