using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/itempackage")]
    public class ApiMstItemPackageController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ITEMPACKAGE 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstItemPackage> listItemPackage()
        {
            var itemPackage = from d in db.MstItemPackages
                          select new Entities.MstItemPackage
                          {
                              Id = d.Id,
                              ItemId = d.ItemId,
                              PackageItemId = d.PackageItemId,
                              UnitId = d.UnitId,
                              Quantity = d.Quantity,
                              IsOptional = d.IsOptional,
                          };
            return itemPackage.ToList();
        }

        //************
        //ADD ITEMPACKAGE 
        //************
        [HttpPost, Route("post")]
        public Int32 postItemPackage()
        {
            try
            {

                Data.MstItemPackage newItemPackage = new Data.MstItemPackage();
                newItemPackage.ItemId = ItemId();
                newItemPackage.PackageItemId = ItemId();
                newItemPackage.UnitId = UnitId();
                newItemPackage.Quantity = 0;
                newItemPackage.IsOptional = false;
                db.MstItemPackages.InsertOnSubmit(newItemPackage);
                db.SubmitChanges();

                return newItemPackage.Id;

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
        public HttpResponseMessage putItemPackage(String id, Entities.MstItemPackage itemPackage)
        {
            try
            {
                var itemPackages = from d in db.MstItemPackages where d.Id == Convert.ToInt32(id) select d;
                if (itemPackages.Any())
                {
                    var updateItemPackage = itemPackages.FirstOrDefault();
                    updateItemPackage.ItemId = ItemId();
                    updateItemPackage.PackageItemId = ItemId();
                    updateItemPackage.UnitId = UnitId();
                    updateItemPackage.Quantity = 0;
                    updateItemPackage.IsOptional = false;
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
                var delete = from d in db.MstItemPackages where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemPackages.DeleteOnSubmit(delete.First());
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
