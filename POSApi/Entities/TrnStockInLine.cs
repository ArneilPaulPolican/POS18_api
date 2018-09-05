using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class TrnStockInLine
    {
        public Int32 Id { get; set; }
        public Int32 StockInId { get; set; }
        public Int32 ItemId { get; set; }
        public String Item { get; set; }
        public Int32 UnitId { get; set; }
        public String Unit { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Amount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public String LotNumber { get; set; }
        public Int32 AssetAccountId { get; set; }
        public String AssetAccount { get; set; }
    }
}