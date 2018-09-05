using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstItemGroup
    {
        public Int32 Id { get; set; }
        public String ItemGroup { get; set; }
        public Int32 ItemGroupId { get; set; }
        public Int32 ItemId { get; set; }
        public String ImagePath { get; set; }
        public String KitchenReport { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Boolean IsLocked { get; set; }
    }
}