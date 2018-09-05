using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/itemgroup")]
    public class ApiMstItemGroupController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ItemGroup 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstItemGroup> listItemGroup()
        {
            var itemGroup = from d in db.MstItemGroups
                          select new Entities.MstItemGroup
                          {
                              Id = d.Id,
                              ItemGroup = d.ItemGroup,
                              ImagePath = d.ImagePath,
                              KitchenReport = d.KitchenReport,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                              IsLocked = d.IsLocked,
                          };
            return itemGroup.ToList();
        }

        //************
        //ADD ItemGroup 
        //************
        [HttpPost, Route("post")]
        public Int32 postItemGroup()
        {
            try
            {

                Data.MstItemGroup newItemGroup = new Data.MstItemGroup();
                newItemGroup.ItemGroup = "n/a";
                newItemGroup.ImagePath = "n/a";
                newItemGroup.KitchenReport = "n/a";
                newItemGroup.EntryUserId = UserId();
                newItemGroup.EntryDateTime = DateTime.Today;
                newItemGroup.UpdateUserId = UserId();
                newItemGroup.UpdateDateTime = DateTime.Today;
                newItemGroup.IsLocked = false;
                db.MstItemGroups.InsertOnSubmit(newItemGroup);
                db.SubmitChanges();

                return newItemGroup.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE ItemGroup 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemGroup(String id, Entities.MstItemGroup itemGroup)
        {
            try
            {
                var itemGroups = from d in db.MstItemGroups where d.Id == Convert.ToInt32(id) select d;
                if (itemGroups.Any())
                {
                    var updateItemGroup = itemGroups.FirstOrDefault();
                    updateItemGroup.ItemGroup = itemGroup.ItemGroup;
                    updateItemGroup.ImagePath = itemGroup.ImagePath;
                    updateItemGroup.KitchenReport = itemGroup.KitchenReport;
                    updateItemGroup.EntryUserId = UserId();
                    updateItemGroup.EntryDateTime = DateTime.Today;
                    updateItemGroup.UpdateUserId = UserId();
                    updateItemGroup.UpdateDateTime = DateTime.Today;
                    updateItemGroup.IsLocked = true;
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
        //DELETE ItemGroup
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItemGroup(String id)
        {
            try
            {
                var delete = from d in db.MstItemGroups where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemGroups.DeleteOnSubmit(delete.First());
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
