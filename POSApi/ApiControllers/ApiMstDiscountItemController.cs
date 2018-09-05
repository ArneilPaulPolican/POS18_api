using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/discountitem")]
    public class ApiMstDiscountItemController : ApiMethod.ApiMethodController
    {
        //*******************
        //LIST DISCOUNT ITEM
        //*******************
        [HttpGet, Route("list")]
        public List<Entities.MstDiscountItem> listDiscountItem()
        {
            var discount = from d in db.MstDiscountItems
                           select new Entities.MstDiscountItem
                           {
                               Id = d.Id,
                               ItemId = d.ItemId,
                               DiscountId = d.DiscountId,
                           };
            return discount.ToList();
        }

        //*****************
        //ADD DISCOUNT ITEM 
        //*****************
        [HttpPost, Route("post")]
        public Int32 postDiscountItem()
        {
            try
            {

                Data.MstDiscountItem newDiscountItem = new Data.MstDiscountItem();
                newDiscountItem.ItemId = ItemId();
                newDiscountItem.DiscountId = DiscountId();
                db.MstDiscountItems.InsertOnSubmit(newDiscountItem);
                db.SubmitChanges();

                return newDiscountItem.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //*********************
        //UPDATE DISCOUNT ITEM
        //*********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDiscountItem(String id, Entities.MstDiscountItem discountItem)
        {
            try
            {
                var discountItems = from d in db.MstDiscountItems where d.Id == Convert.ToInt32(id) select d;
                if (discountItems.Any())
                {
                    var updateDiscountItem = discountItems.FirstOrDefault();
                    updateDiscountItem.ItemId = discountItem.ItemId;
                    updateDiscountItem.DiscountId = discountItem.DiscountId;
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

        //*********************
        //DELETE DISCOUNT ITEM
        //*********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDiscountItem(String id)
        {
            try
            {
                var delete = from d in db.MstDiscountItems where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstDiscountItems.DeleteOnSubmit(delete.First());
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
