using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockcount")]
    public class ApiTrnStockCountController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST         STOCKCOUNT
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockCount> listStockCount()
        {
            var stockCount = from d in db.TrnStockCounts
                          select new Entities.TrnStockCount
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              StockCountDate = d.StockCountDate,
                              StockCountNumber = d.StockCountNumber,
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
            return stockCount.ToList();
        }

        //**********************
        //ADD          STOCKCOUNT
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockCount()
        {
            try
            {

                Data.TrnStockCount newStockCount = new Data.TrnStockCount();
                newStockCount.PeriodId = SalesId();
                newStockCount.StockCountDate = DateTime.Today;
                newStockCount.StockCountNumber = "n/a";
                newStockCount.Remarks = "n/a";
                newStockCount.PreparedBy = UserId();
                newStockCount.CheckedBy = UserId();
                newStockCount.ApprovedBy = UserId();
                newStockCount.IsLocked = 0;
                newStockCount.EntryUserId = UserId();
                newStockCount.EntryDateTime = DateTime.Today;
                newStockCount.UpdateUserId = UserId();                         
                newStockCount.UpdateDateTime = DateTime.Today;
                db.TrnStockCounts.InsertOnSubmit(newStockCount);
                db.SubmitChanges();

                return newStockCount.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE       STOCKCOUNT 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putStockCount(String id, Entities.TrnStockCount stockCount)
        {
            try
            {
                var stockCounts = from d in db.TrnStockCounts where d.Id == Convert.ToInt32(id) select d;
                if (stockCounts.Any())
                {
                    var updateStockCount = stockCounts.FirstOrDefault();
                    updateStockCount.PeriodId = stockCount.PeriodId;
                    updateStockCount.StockCountDate = stockCount.StockCountDate;
                    updateStockCount.StockCountNumber = stockCount.StockCountNumber;
                    updateStockCount.Remarks = stockCount.Remarks;
                    updateStockCount.PreparedBy = stockCount.PreparedBy;
                    updateStockCount.CheckedBy = stockCount.CheckedBy;
                    updateStockCount.ApprovedBy = stockCount.ApprovedBy;
                    updateStockCount.IsLocked = -1;
                    updateStockCount.EntryUserId = UserId();
                    updateStockCount.EntryDateTime = DateTime.Today;
                    updateStockCount.UpdateUserId = UserId();
                    updateStockCount.UpdateDateTime = DateTime.Today;
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
        //DELETE       STOCKCOUNT
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteStockCount(String id)
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
