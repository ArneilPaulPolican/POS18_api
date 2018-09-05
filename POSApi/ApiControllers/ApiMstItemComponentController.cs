using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/itemcomponent")]
    public class ApiMstItemComponentController : ApiMethod.ApiMethodController
    {
        //*******************
        //LIST ITEM COMPONENT
        //*******************
        [HttpGet, Route("list")]
        public List<Entities.MstItemComponent> listItemComponent()
        {
            var itemComponent = from d in db.MstItemComponents
                                select new Entities.MstItemComponent
                                {
                                    Id = d.Id,
                                    ItemId = d.ItemId,
                                    ComponentItemId = d.ComponentItemId,
                                    UnitId = d.UnitId,
                                    Quantity = d.Quantity,
                                    Cost = d.Cost,
                                    Amount = d.Amount,
                                    IsPrinted = d.IsPrinted,

                                };
            return itemComponent.ToList();
        }

        //*******************
        //ADD ITEM  COMPONENT
        //********************
        [HttpPost, Route("post")]
        public Int32 postItemComponent()
        {
            try
            {

                Data.MstItemComponent newItemComponent = new Data.MstItemComponent();
                newItemComponent.ItemId = ItemId();
                newItemComponent.ComponentItemId = ComponentItemId();
                newItemComponent.UnitId = UnitId();
                newItemComponent.Quantity = 0;
                newItemComponent.Cost = 0;
                newItemComponent.Amount = 0;
                newItemComponent.IsPrinted = false;
                db.MstItemComponents.InsertOnSubmit(newItemComponent);
                db.SubmitChanges();

                return newItemComponent.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //*********************
        //UPDATE ITEM COMPONENT
        //*********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemComponent(String id, Entities.MstItemComponent itemComponent)
        {
            try
            {
                var itemComponents = from d in db.MstItemComponents where d.Id == Convert.ToInt32(id) select d;
                if (itemComponents.Any())
                {
                    var updateitemComponent = itemComponents.FirstOrDefault();
                    updateitemComponent.ItemId = itemComponent.ItemId;
                    updateitemComponent.ComponentItemId = itemComponent.ComponentItemId;
                    updateitemComponent.UnitId = itemComponent.UnitId;
                    updateitemComponent.Quantity = itemComponent.Quantity;
                    updateitemComponent.Cost = itemComponent.Cost;
                    updateitemComponent.Amount = itemComponent.Amount;
                    updateitemComponent.IsPrinted = itemComponent.IsPrinted;
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

        //***************
        //DELETE ITEM COMPONENT
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItemComponent(String id)
        {
            try
            {
                var delete = from d in db.MstItemComponents where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItemComponents.DeleteOnSubmit(delete.First());
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
    }
}
