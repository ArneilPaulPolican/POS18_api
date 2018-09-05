using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItemGroupItem
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public String Item { get; set; }
        public Int32 ItemGroupId { get; set; }
        public String ItemGroup { get; set; }
    }
}