using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockcountline")]
    public class ApiTrnStockCountLinesController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     STOCKCOUNTLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockCountLine> listStockCountLine()
        {
            var stockCountLine = from d in db.TrnStockCountLines
                          select new Entities.TrnStockCountLine
                          {
                              Id = d.Id,
                              StockCountId = d.StockCountId,
                              ItemId = d.ItemId,
                              UnitId = d.UnitId,
                              Quantity = d.Quantity,
                              Cost = d.Cost,
                              Amount = d.Amount,
                          };
            return stockCountLine.ToList();
        }

        //**********************
        //ADD       STOCKCOUNTLINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockCountLine()
        {
            try
            {

                Data.TrnStockCountLine newStockCountLine = new Data.TrnStockCountLine();
                newStockCountLine.StockCountId = StockCountId();
                newStockCountLine.ItemId = ItemId();
                newStockCountLine.UnitId = UnitId();
                newStockCountLine.Quantity = 0;
                newStockCountLine.Cost = 0;
                newStockCountLine.Amount = 0;
                db.TrnStockCountLines.InsertOnSubmit(newStockCountLine);
                db.SubmitChanges();

                return newStockCountLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    STOCKCOUNTLINE 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putStockCountLine(String id, Entities.TrnStockCountLine stockCountLine)
        {
            try
            {
                var stockCountLines = from d in db.TrnStockCountLines where d.Id == Convert.ToInt32(id) select d;
                if (stockCountLines.Any())
                {
                    var updateStockCountLine = stockCountLines.FirstOrDefault();
                    updateStockCountLine.StockCountId = stockCountLine.StockCountId;
                    updateStockCountLine.ItemId = stockCountLine.ItemId;
                    updateStockCountLine.UnitId = stockCountLine.UnitId;
                    updateStockCountLine.Quantity = stockCountLine.Quantity;
                    updateStockCountLine.Cost = stockCountLine.Cost;
                    updateStockCountLine.Amount = stockCountLine.Amount;
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
        //DELETE    STOCKCOUNTLINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteStockCountLine(String id)
        {
            try
            {
                var delete = from d in db.TrnStockOutLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnStockOutLines.DeleteOnSubmit(delete.First());
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
