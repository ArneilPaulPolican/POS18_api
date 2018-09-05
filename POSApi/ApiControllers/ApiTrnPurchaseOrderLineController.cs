using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/purchaseorderline")]
    public class ApiTrnPurchaseOrderLineController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     PURCHASEORDERLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnPurchaseOrderLine> listPurchaseOrderLine()
        {
            var purchaseOrderLine = from d in db.TrnPurchaseOrderLines
                                    select new Entities.TrnPurchaseOrderLine
                                {
                                    Id = d.Id,
                                    PurchaseOrderId = d.PurchaseOrderId,
                                    ItemId = d.ItemId,
                                    UnitId = d.UnitId,
                                    Quantity = d.Quantity,
                                    Cost = d.Cost,
                                    Amount = d.Amount,
                                };
            return purchaseOrderLine.ToList();
        }

        //**********************
        //ADD       PURCHASEORDERLINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postPurchaseOrderLine()
        {
            try
            {

                Data.TrnPurchaseOrderLine newPurchaseOrderLine = new Data.TrnPurchaseOrderLine();
                newPurchaseOrderLine.PurchaseOrderId = PurchaseOrderId();
                newPurchaseOrderLine.ItemId = ItemId();
                newPurchaseOrderLine.UnitId = UnitId();
                newPurchaseOrderLine.Quantity = 0;
                newPurchaseOrderLine.Cost = 0;
                newPurchaseOrderLine.Amount = 0;
                db.TrnPurchaseOrderLines.InsertOnSubmit(newPurchaseOrderLine);
                db.SubmitChanges();

                return newPurchaseOrderLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    PURCHASEORDERLINE 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putPurchaseOrderLine(String id, Entities.TrnPurchaseOrderLine purchaseOrderLine)
        {
            try
            {
                var purchaseOrderLines = from d in db.TrnPurchaseOrderLines where d.Id == Convert.ToInt32(id) select d;
                if (purchaseOrderLines.Any())
                {
                    var updatePurchaseOrderLine = purchaseOrderLines.FirstOrDefault();
                    updatePurchaseOrderLine.PurchaseOrderId = purchaseOrderLine.PurchaseOrderId;
                    updatePurchaseOrderLine.ItemId = purchaseOrderLine.ItemId;
                    updatePurchaseOrderLine.UnitId = purchaseOrderLine.UnitId;
                    updatePurchaseOrderLine.Quantity = purchaseOrderLine.Quantity;
                    updatePurchaseOrderLine.Cost = purchaseOrderLine.Cost;
                    updatePurchaseOrderLine.Amount = purchaseOrderLine.Amount;
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

        //**********************
        //DELETE    PURCHASEORDERLINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deletePurchaseOrderLine(String id)
        {
            try
            {
                var delete = from d in db.TrnPurchaseOrderLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnPurchaseOrderLines.DeleteOnSubmit(delete.First());
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
