using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockinline")]
    public class ApiTrnStockInLinesController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     STOCKINLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockInLine> listStockInLine()
        {
            var stockInLine = from d in db.TrnStockInLines
                          select new Entities.TrnStockInLine
                          {
                              Id = d.Id,
                              StockInId = d.StockInId,
                              ItemId = d.ItemId,
                              UnitId = d.UnitId,
                              Quantity = d.Quantity,
                              Cost = d.Cost,
                              Amount = d.Amount,
                              ExpiryDate = d.ExpiryDate,
                              LotNumber = d.LotNumber,
                              AssetAccountId = d.AssetAccountId,
                          };
            return stockInLine.ToList();
        }

        //**********************
        //ADD       STOCKINLINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockInLine()
        {
            try
            {
                Data.TrnStockInLine newStockInLine = new Data.TrnStockInLine();
                newStockInLine.StockInId = StockInId();
                newStockInLine.ItemId = ItemId();
                newStockInLine.UnitId = UnitId();
                newStockInLine.Quantity = 0;
                newStockInLine.Cost = 0;
                newStockInLine.Amount = 0;
                newStockInLine.ExpiryDate = DateTime.Today;
                newStockInLine.LotNumber = "n/a";
                newStockInLine.AssetAccountId = AccountId();
                db.TrnStockInLines.InsertOnSubmit(newStockInLine);
                db.SubmitChanges();

                return newStockInLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    STOCKINLINE 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putStockInLine(String id, Entities.TrnStockInLine stockInLine)
        {
            try
            {
                var stockInLines = from d in db.TrnStockInLines where d.Id == Convert.ToInt32(id) select d;
                if (stockInLines.Any())
                {
                    var updateStockInLine = stockInLines.FirstOrDefault();
                    updateStockInLine.StockInId = stockInLine.StockInId;
                    updateStockInLine.ItemId = stockInLine.ItemId;
                    updateStockInLine.UnitId = stockInLine.UnitId;
                    updateStockInLine.Quantity = stockInLine.Quantity;
                    updateStockInLine.Cost = stockInLine.Cost;
                    updateStockInLine.Amount = stockInLine.Amount;
                    updateStockInLine.ExpiryDate = stockInLine.ExpiryDate;
                    updateStockInLine.LotNumber = stockInLine.LotNumber;
                    updateStockInLine.AssetAccountId = stockInLine.AssetAccountId;
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
        //DELETE    STOCKINLINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteStockInLine(String id)
        {
            try
            {
                var delete = from d in db.TrnStockInLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnStockInLines.DeleteOnSubmit(delete.First());
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
