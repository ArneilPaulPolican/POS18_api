using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItemPrice
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public String PriceDescription { get; set; }
        public Decimal Price { get; set; }
        public Decimal TriggerQuantity { get; set; }
    }
}