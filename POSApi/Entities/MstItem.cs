using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItem
    {
        public Int32 Id { get; set; }
        public String ItemCode { get; set; }
        public String BarCode { get; set; }
        public String ItemDescription { get; set; }
        public String Alias { get; set; }
        public String GenericName { get; set; }
        public String Category { get; set; }
        public Int32 SalesAccountId { get; set; }
        public Int32 AssetAccountId { get; set; }
        public Int32 CostAccountId { get; set; }
        public Int32 InTaxId { get; set; }
        public Int32 OutTaxId { get; set; }
        public Int32 UnitId { get; set; }
        public String Unit { get; set; }
        public Int32 DefaultSupplierId { get; set; }
        public Decimal Cost { get; set; }
        public Decimal MarkUp { get; set; }
        public Decimal Price { get; set; }
        public String ImagePath { get; set; }
        public Decimal ReorderQuantity { get; set; }
        public Decimal OnhandQuantity { get; set; }
        public Boolean IsInventory { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public String LotNumber { get; set; }
        public String Remarks { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Boolean IsLocked { get; set; }
        public String DefaultKitchenReport { get; set; }
        public Boolean IsPackage { get; set; }
        public List<MstItemComponent> Components { get; set; }
    }
}