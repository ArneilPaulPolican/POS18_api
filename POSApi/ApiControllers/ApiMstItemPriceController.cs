using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/itemPrice")]
    public class ApiMstItemPriceController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ITEMPACKAGE 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstItemPrice> listItemPrice()
        {
            var itemPrice = from d in db.MstItemPrices
                              select new Entities.MstItemPrice
                              {
                                  Id = d.Id,
                                  ItemId = d.ItemId,
                                  PriceDescription = d.PriceDescription,
                                  Price = d.Price,
                                  TriggerQuantity = d.TriggerQuantity,
                              };
            return itemPrice.ToList();
        }

        //************
        //ADD ITEMPACKAGE 
        //************
        [HttpPost, Route("post")]
        public Int32 postItemPrice()
        {
            try
            {

                Data.MstItemPrice newItemPrice = new Data.MstItemPrice();
                newItemPrice.ItemId = ItemId();
                newItemPrice.PriceDescription = "n/a";
                newItemPrice.Price = 0;
                newItemPrice.TriggerQuantity = 0;
                db.MstItemPrices.InsertOnSubmit(newItemPrice);
                db.SubmitChanges();

                return newItemPrice.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE ITEMPACKAGE 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemPrice(String id, Entities.MstItemPrice itemPrice)
        {
            try
            {
                var itemPrices = from d in db.MstItemPrices where d.Id == Convert.ToInt32(id) select d;
                if (itemPrices.Any())
                {
                    var updateItemPrice = itemPrices.FirstOrDefault();
                    updateItemPrice.ItemId = itemPrice.ItemId;
                    updateItemPrice.PriceDescription = itemPrice.PriceDescription;
                    updateItemPrice.Price = itemPrice.Price;
                    updateItemPrice.TriggerQuantity = itemPrice.TriggerQuantity;
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
        //DELETE ITEMPACKAGE
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteAccount(String id)
        {
            try
            {
                var delete = from d in db.MstItemPrices where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemPrices.DeleteOnSubmit(delete.First());
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
