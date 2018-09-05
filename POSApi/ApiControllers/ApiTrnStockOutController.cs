using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockout")]
    public class ApiTrnStockOutController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     STOCKOUT
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockOut> listStockOut()
        {
            var stockOut = from d in db.TrnStockOuts
                          select new Entities.TrnStockOut
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              StockOutDate = d.StockOutDate,
                              StockOutNumber = d.StockOutNumber,
                              AccountId = d.AccountId,
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
            return stockOut.ToList();
        }

        //**********************
        //ADD       STOCKOUT
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockOut()
        {
            try
            {
                Data.TrnStockOut newStockOut = new Data.TrnStockOut();
                newStockOut.PeriodId = PeriodId();
                newStockOut.StockOutDate = DateTime.Today;
                newStockOut.StockOutNumber = "n/a";
                newStockOut.AccountId = AccountId();
                newStockOut.Remarks = "n/a";
                newStockOut.PreparedBy = UserId();
                newStockOut.CheckedBy = UserId();
                newStockOut.ApprovedBy = UserId();
                newStockOut.IsLocked = false;
                newStockOut.EntryUserId = UserId();
                newStockOut.EntryDateTime = DateTime.Today;
                newStockOut.UpdateUserId = UserId();
                newStockOut.UpdateDateTime = DateTime.Today;
                db.TrnStockOuts.InsertOnSubmit(newStockOut);
                db.SubmitChanges();

                return newStockOut.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    STOCKOUT 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putStockOut(String id, Entities.TrnStockOut stockOut)
        {
            try
            {
                var stockOuts = from d in db.TrnStockOuts where d.Id == Convert.ToInt32(id) select d;
                if (stockOuts.Any())
                {
                    var updateStockOut = stockOuts.FirstOrDefault();
                    updateStockOut.PeriodId = stockOut.PeriodId;
                    updateStockOut.StockOutDate = stockOut.StockOutDate;
                    updateStockOut.StockOutNumber = stockOut.StockOutNumber;
                    updateStockOut.AccountId = stockOut.AccountId;
                    updateStockOut.Remarks = stockOut.Remarks;
                    updateStockOut.PreparedBy = stockOut.PreparedBy;
                    updateStockOut.CheckedBy = stockOut.CheckedBy;
                    updateStockOut.ApprovedBy = stockOut.ApprovedBy;
                    updateStockOut.IsLocked = true;
                    updateStockOut.EntryUserId = UserId();
                    updateStockOut.EntryDateTime = DateTime.Today;
                    updateStockOut.UpdateUserId = UserId();
                    updateStockOut.UpdateDateTime = DateTime.Today;
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
        //DELETE    STOCKOUT
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteStockOut(String id)
        {
            try
            {
                var delete = from d in db.TrnStockOuts where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnStockOuts.DeleteOnSubmit(delete.First());
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
