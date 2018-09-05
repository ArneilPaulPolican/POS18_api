using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/purchaseorder")]
    public class ApiTrnPurchaseOrderController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     PURCHASEORDER
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnPurchaseOrder> listPurchaseOrder()
        {
            var purchaseOrder = from d in db.TrnPurchaseOrders
                          select new Entities.TrnPurchaseOrder
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              PurchaseOrderDate = d.PurchaseOrderDate,
                              PurchaseOrderNumber = d.PurchaseOrderNumber,
                              Amount = d.Amount,
                              SupplierId = d.SupplierId,
                              Remarks = d.Remarks,
                              PreparedBy = d.PreparedBy,
                              CheckedBy = d.CheckedBy,
                              ApprovedBy = d.ApprovedBy,
                              IsLocked = d.IsLocked,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                          };
            return purchaseOrder.ToList();
        }

        //**********************
        //ADD       PURCHASEORDER
        //**********************
        [HttpPost, Route("post")]
        public Int32 postPurchaseOrder()
        {
            try
            {

                Data.TrnPurchaseOrder newPurchaseOrder = new Data.TrnPurchaseOrder();
                newPurchaseOrder.PeriodId = PeriodId();
                newPurchaseOrder.PurchaseOrderDate = DateTime.Today;
                newPurchaseOrder.PurchaseOrderNumber = "n/a";
                newPurchaseOrder.Amount = 0;
                newPurchaseOrder.SupplierId = SupplierId();
                newPurchaseOrder.Remarks = "n/a";
                newPurchaseOrder.PreparedBy = UserId();
                newPurchaseOrder.CheckedBy = UserId();
                newPurchaseOrder.ApprovedBy = UserId();
                newPurchaseOrder.IsLocked = false;
                newPurchaseOrder.EntryUserId = UserId(); 
                newPurchaseOrder.EntryDateTime = DateTime.Today;
                newPurchaseOrder.UpdateUserId = UserId();
                newPurchaseOrder.UpdateDateTime = DateTime.Today;
                db.TrnPurchaseOrders.InsertOnSubmit(newPurchaseOrder);
                db.SubmitChanges();

                return newPurchaseOrder.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    PURCHASEORDER 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putPurchaseOrder(String id, Entities.TrnPurchaseOrder purchaseOrder)
        {
            try
            {
                var purchaseOrders = from d in db.TrnPurchaseOrders where d.Id == Convert.ToInt32(id) select d;
                if (purchaseOrders.Any())
                {
                    var updatePurchaseOrder = purchaseOrders.FirstOrDefault();
                    updatePurchaseOrder.PeriodId = purchaseOrder.PeriodId;
                    updatePurchaseOrder.PurchaseOrderDate = purchaseOrder.PurchaseOrderDate;
                    updatePurchaseOrder.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    updatePurchaseOrder.Amount = purchaseOrder.Amount;
                    updatePurchaseOrder.SupplierId = purchaseOrder.SupplierId;
                    updatePurchaseOrder.Remarks = purchaseOrder.Remarks;
                    updatePurchaseOrder.PreparedBy = purchaseOrder.PreparedBy;
                    updatePurchaseOrder.CheckedBy = purchaseOrder.CheckedBy;
                    updatePurchaseOrder.ApprovedBy = purchaseOrder.ApprovedBy;
                    updatePurchaseOrder.IsLocked = true;
                    updatePurchaseOrder.EntryUserId = UserId();
                    updatePurchaseOrder.EntryDateTime = DateTime.Today;
                    updatePurchaseOrder.UpdateUserId = UserId();
                    updatePurchaseOrder.UpdateDateTime = DateTime.Today;
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
        //DELETE    PURCHASEORDER
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deletePurchaseOrder(String id)
        {
            try
            {
                var delete = from d in db.TrnPurchaseOrders where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnPurchaseOrders.DeleteOnSubmit(delete.First());
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
