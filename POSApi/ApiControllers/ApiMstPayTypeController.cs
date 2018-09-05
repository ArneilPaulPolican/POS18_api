using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/paytype")]
    public class ApiMstPayTypeController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST PayType 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstPayType> listPayType()
        {
            var payType = from d in db.MstPayTypes
                         select new Entities.MstPayType
                         {
                             Id = d.Id,
                             PayType = d.PayType,
                             AccountId = d.AccountId
                         };
            return payType.ToList();
        }

        //************
        //ADD PayType 
        //************
        [HttpPost, Route("post")]
        public Int32 postPeriod()
        {
            try
            {

                Data.MstPayType newPayType = new Data.MstPayType();
                newPayType.PayType = "n/a";
                newPayType.AccountId = AccountId();
                db.MstPayTypes.InsertOnSubmit(newPayType);
                db.SubmitChanges();

                return newPayType.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE PayType 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putPayType(String id, Entities.MstPayType payType)
        {
            try
            {
                var payTypes = from d in db.MstPayTypes where d.Id == Convert.ToInt32(id) select d;
                if (payTypes.Any())
                {
                    var updatePayType = payTypes.FirstOrDefault();
                    updatePayType.PayType = payType.PayType;
                    updatePayType.AccountId = payType.AccountId;
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
        //DELETE PayType
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deletePayType(String id)
        {
            try
            {
                var delete = from d in db.MstPayTypes where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstPayTypes.DeleteOnSubmit(delete.First());
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
