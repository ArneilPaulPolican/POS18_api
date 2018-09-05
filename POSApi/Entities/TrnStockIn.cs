using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class TrnStockIn
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public String Period { get; set; }
        public DateTime StockInDate { get; set; }
        public String StockInNumber { get; set; }
        public String StockInNumDate { get; set; }
        public Int32 SupplierId { get; set; }
        public String Supplier { get; set; }
        public String Remarks { get; set; }
        public Boolean IsReturn { get; set; }
        public Int32? CollectionId { get; set; }
        public Int32? PurchaseOrderId { get; set; }
        public Int32 PreparedBy { get; set; }
        public String Prepared { get; set; }
        public Int32 CheckedBy { get; set; }
        public Int32 ApprovedBy { get; set; }
        public Int32 IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}