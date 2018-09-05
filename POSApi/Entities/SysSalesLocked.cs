using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class SysSalesLocked
    {
        public Int32 Id { get; set; }
        public Int32 SalesId { get; set; }
        public Int32 UserId { get; set; }
    }
}