using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstPayType
    {
        public Int32 Id { get; set; }
        public String PayType { get; set; }
        public Int32? AccountId { get; set; }
    }
}