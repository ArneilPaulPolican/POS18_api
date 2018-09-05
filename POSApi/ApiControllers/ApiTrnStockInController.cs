using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockin")]
    public class ApiTrnStockInController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     STOCKIN
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockIn> listStockIn()
        {
            var stockIn = from d in db.TrnStockIns
                                 select new Entities.TrnStockIn
                                 {
                                     Id = d.Id,
                                     PeriodId = d.PeriodId,
                                     StockInDate = d.StockInDate,
                                     StockInNumber = d.StockInNumber,
                                     SupplierId = d.SupplierId,
                                     Remarks = d.Remarks,
                                     IsReturn = d.IsReturn,
                                     CollectionId = d.CollectionId,
                                     PurchaseOrderId = d.PurchaseOrderId,
                                     PreparedBy = d.PreparedBy,
                                     CheckedBy = d.CheckedBy,
                                     ApprovedBy = d.ApprovedBy,
                                     IsLocked = d.IsLocked,
                                     EntryUserId = d.EntryUserId,
                                     EntryDateTime = d.EntryDateTime,
                                     UpdateUserId = d.UpdateUserId,
                                     UpdateDateTime = d.UpdateDateTime,
                                 };
            return stockIn.ToList();
        }

        //**********************
        //ADD       STOCKIN
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockIn()
        {
            try
            {
                Data.TrnStockIn newStockIn = new Data.TrnStockIn();
                newStockIn.PeriodId = PeriodId();
                newStockIn.StockInDate = DateTime.Today;
                newStockIn.StockInNumber = "n/a";
                newStockIn.SupplierId = SupplierId();
                newStockIn.Remarks = "n/a";
                newStockIn.IsReturn = false;      
                newStockIn.CollectionId = CollectionId();
                newStockIn.PurchaseOrderId = PurchaseOrderId();
                newStockIn.PreparedBy = UserId();
                newStockIn.CheckedBy = UserId();
                newStockIn.ApprovedBy = UserId();
                newStockIn.IsLocked = 0;
                newStockIn.EntryUserId = UserId();
                newStockIn.EntryDateTime = DateTime.Today;
                newStockIn.UpdateUserId = UserId();
                newStockIn.UpdateDateTime = DateTime.Today;
                db.TrnStockIns.InsertOnSubmit(newStockIn);
                db.SubmitChanges();

                return newStockIn.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    STOCKIN 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putStockIn(String id, Entities.TrnStockIn stockIn)
        {
            try
            {
                var stockIns = from d in db.TrnStockIns where d.Id == Convert.ToInt32(id) select d;
                if (stockIns.Any())
                {
                    var updateStockIn = stockIns.FirstOrDefault();
                    updateStockIn.PeriodId = stockIn.PeriodId;
                    updateStockIn.StockInDate = stockIn.StockInDate;
                    updateStockIn.StockInNumber = stockIn.StockInNumber;
                    updateStockIn.SupplierId = stockIn.SupplierId;
                    updateStockIn.Remarks = stockIn.Remarks;
                    updateStockIn.IsReturn = stockIn.IsReturn;
                    updateStockIn.CollectionId = stockIn.CollectionId;
                    updateStockIn.PurchaseOrderId = stockIn.PurchaseOrderId;
                    updateStockIn.PreparedBy = stockIn.PreparedBy;
                    updateStockIn.CheckedBy = stockIn.CheckedBy;
                    updateStockIn.ApprovedBy = stockIn.ApprovedBy;
                    updateStockIn.IsLocked = -1;
                    updateStockIn.EntryUserId = UserId();
                    updateStockIn.EntryDateTime = DateTime.Today;
                    updateStockIn.UpdateUserId = UserId();
                    updateStockIn.UpdateDateTime = DateTime.Today;
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
        //DELETE    STOCKIN
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteStockIn(String id)
        {
            try
            {
                var delete = from d in db.TrnStockIns where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnStockIns.DeleteOnSubmit(delete.First());
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
