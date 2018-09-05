using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItemInventory
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public DateTime InventoryDate { get; set; }
        public Decimal Quantity { get; set; }
    }
}