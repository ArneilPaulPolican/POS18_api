using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/stockoutline")]
    public class ApiTrnStockOutLineController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST     STOCKINLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnStockOutLine> listStockOutLine()
        {
            var stockOutLine = from d in db.TrnStockOutLines
                              select new Entities.TrnStockOutLine
                              {
                                  Id = d.Id,
                                  StockOutId = d.StockOutId,
                                  ItemId = d.ItemId,
                                  UnitId = d.UnitId,
                                  Quantity = d.Quantity,
                                  Cost = d.Cost,
                                  Amount = d.Amount,
                                  AssetAccountId = d.AssetAccountId,
                              };
            return stockOutLine.ToList();
        }

        //**********************
        //ADD       STOCKINLINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postStockOutLine()
        {
            try
            {
                Data.TrnStockOutLine newStockOutLine = new Data.TrnStockOutLine();
                newStockOutLine.StockOutId = StockOutId();
                newStockOutLine.ItemId = ItemId();
                newStockOutLine.UnitId = UnitId();
                newStockOutLine.Quantity = 0;
                newStockOutLine.Cost = 0;
                newStockOutLine.Amount = 0;
                newStockOutLine.AssetAccountId = AccountId();
                db.TrnStockOutLines.InsertOnSubmit(newStockOutLine);
                db.SubmitChanges();

                return newStockOutLine.Id;

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
        public HttpResponseMessage putStockOutLine(String id, Entities.TrnStockOutLine stockOutLine)
        {
            try
            {
                var stockOutLines = from d in db.TrnStockOutLines where d.Id == Convert.ToInt32(id) select d;
                if (stockOutLines.Any())
                {
                    var updateStockOutLine = stockOutLines.FirstOrDefault();
                    updateStockOutLine.StockOutId = stockOutLine.StockOutId;
                    updateStockOutLine.ItemId = stockOutLine.ItemId;
                    updateStockOutLine.UnitId = stockOutLine.UnitId;
                    updateStockOutLine.Quantity = stockOutLine.Quantity;
                    updateStockOutLine.Cost = stockOutLine.Cost;
                    updateStockOutLine.Amount = stockOutLine.Amount;
                    updateStockOutLine.AssetAccountId = AccountId();
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
        public HttpResponseMessage deleteStockOutLine(String id)
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
