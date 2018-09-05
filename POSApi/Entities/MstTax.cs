using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstTax
    {
        public Int32 Id { get; set; }
        public String Code { get; set; }
        public String Tax { get; set; }
        public Decimal Rate { get; set; }
        public Int32 AccountId { get; set; }
    }
}