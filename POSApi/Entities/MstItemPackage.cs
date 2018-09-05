using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItemPackage
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public Int32 PackageItemId { get; set; }
        public Int32 UnitId { get; set; }
        public Decimal Quantity { get; set; }
        public Boolean IsOptional { get; set; }
    }
}