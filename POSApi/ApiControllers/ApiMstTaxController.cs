using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSApi.Entities;

namespace POSApi.ApiControllers
{
    //[Authorize]
    [RoutePrefix("api/tax")]
    public class ApiMstTaxController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Tax
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstTax> listTax()
        {
            var tax = from d in db.MstTaxes
                       select new Entities.MstTax
                       {
                           Id = d.Id,
                           Code = d.Code,
                           Tax = d.Tax,
                           Rate = d.Rate,
                           AccountId = d.AccountId
                       };
            return tax.ToList();
        }

        //*************
        //DETAIL TAXTYPE
        //*************
        [HttpGet, Route("detail/{id}")]
        public Entities.MstTax detailTaxType(String id)
        {
            var tax = from d in db.MstTaxes
                       where d.Id == Convert.ToInt32(id)
                       select new Entities.MstTax
                       {
                           Id = d.Id,
                           Code = d.Code,
                           Tax = d.Tax,
                           Rate = d.Rate,
                           AccountId = d.AccountId

                       };
            return (Entities.MstTax)tax.FirstOrDefault();
        }

        //************
        //ADD Term 
        //************
        [HttpPost, Route("post")]
        public Int32 postTax()
        {
            try
            {

                Data.MstTax newTax = new Data.MstTax();
                newTax.Code = "n/a";
                newTax.Tax = "n/a";
                newTax.Rate = 0;
                newTax.AccountId = AccountId();
                db.MstTaxes.InsertOnSubmit(newTax);
                db.SubmitChanges();

                return newTax.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE Terminal 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage updateTax(String id, Entities.MstTax tax)
        {
            try
            {
                var taxes = from d in db.MstTaxes where d.Id == Convert.ToInt32(id) select d;
                if (taxes.Any())
                {
                    var updateTax = taxes.FirstOrDefault();
                    updateTax.Code = tax.Code;
                    updateTax.Tax = tax.Tax;
                    updateTax.Rate = tax.Rate;
                    updateTax.AccountId = tax.AccountId;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //***************
        //DELETE Terminal
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteTax(String id)
        {
            try
            {
                var delete = from d in db.MstTaxes where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstTaxes.DeleteOnSubmit(delete.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
