using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstTable
    {
        public Int32 Id { get; set; }
        public String TableCode { get; set; }
        public Int32 TableGroupId { get; set; }
        public Int32? TopLocation { get; set; }
        public Int32? LeftLocation { get; set; }
    }
}