using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/iteminventory")]
    public class ApiMstItemInventoryController : ApiMethod.ApiMethodController
    {
        //******************
        //LIST ItemInventory
        //******************
        [HttpGet, Route("list")]
        public List<Entities.MstItemInventory> listItemInventory()
        {
            var itemInventory = from d in db.MstItemInventories
                            select new Entities.MstItemInventory
                            {
                                Id = d.Id,
                                ItemId = d.ItemId,
                                InventoryDate = d.InventoryDate,
                                Quantity = d.Quantity,
                            };
            return itemInventory.ToList();
        }

        //************
        //ADD ItemInventory 
        //************
        [HttpPost, Route("post")]
        public Int32 postItemInventory()
        {
            try
            {

                Data.MstItemInventory newItemInventory = new Data.MstItemInventory();
                newItemInventory.ItemId = ItemId();
                newItemInventory.InventoryDate = DateTime.Today;
                newItemInventory.Quantity = 0;
                db.MstItemInventories.InsertOnSubmit(newItemInventory);
                db.SubmitChanges();

                return newItemInventory.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE ItemInventory 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemInventory(String id, Entities.MstItemInventory itemInventory)
        {
            try
            {
                var itemInventories = from d in db.MstItemInventories where d.Id == Convert.ToInt32(id) select d;
                if (itemInventories.Any())
                {
                    var updateItemInventory = itemInventories.FirstOrDefault();
                    updateItemInventory.ItemId = itemInventory.ItemId;
                    updateItemInventory.InventoryDate = itemInventory.InventoryDate;
                    updateItemInventory.Quantity = itemInventory.Quantity;
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
        //DELETE ItemInventory
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItemInventory(String id)
        {
            try
            {
                var delete = from d in db.MstItemInventories where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemInventories.DeleteOnSubmit(delete.First());
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
