using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/itemgroupitem")]
    public class ApiMstItemGroupItemController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ItemGroupItem 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstItemGroupItem> listItemGroupItem()
        {
            var itemGroupItem = from d in db.MstItemGroupItems
                            select new Entities.MstItemGroupItem
                            {
                                Id = d.Id,
                                ItemId = d.ItemId,
                                Item = d.MstItem.ItemDescription,
                                ItemGroupId = d.ItemGroupId,
                                ItemGroup = d.MstItemGroup.ItemGroup
                            };
            return itemGroupItem.ToList();
        }

        //************
        //ADD ItemGroupItem 
        //************
        [HttpPost, Route("post")]
        public Int32 postItemGroupItem()
        {
            try
            {

                Data.MstItemGroupItem newItemGroupItem = new Data.MstItemGroupItem();
                newItemGroupItem.ItemId = ItemId();
                newItemGroupItem.ItemGroupId = ItemGroupId();
                db.MstItemGroupItems.InsertOnSubmit(newItemGroupItem);
                db.SubmitChanges();

                return newItemGroupItem.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE ItemGroupItem 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemGroup(String id, Entities.MstItemGroupItem itemGroupItem)
        {
            try
            {
                var itemGroupItems = from d in db.MstItemGroupItems where d.Id == Convert.ToInt32(id) select d;
                if (itemGroupItems.Any())
                {
                    var updateItemGroupItem = itemGroupItems.FirstOrDefault();
                    updateItemGroupItem.ItemId = itemGroupItem.ItemId;
                    updateItemGroupItem.ItemGroupId = itemGroupItem.ItemGroupId;
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
        //DELETE ItemGroupItem
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItemGroupItem(String id)
        {
            try
            {
                var delete = from d in db.MstItemGroupItems where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemGroupItems.DeleteOnSubmit(delete.First());
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
