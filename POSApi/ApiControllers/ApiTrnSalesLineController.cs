using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSApi.Entities;

namespace POSApi.ApiControllers
{
    //[Authorize]
    [RoutePrefix("api/salesline")]
    public class ApiTrnSalesLineController : ApiMethod.ApiMethodController
    {
        //****************************
        //LIST BY DATE RANGE SALESLINE
        //****************************
        [HttpGet, Route("listByDateRange/{dateStart}/{dateEnd}")]
        public List<Entities.TrnSalesLine> listSalesLineByDateRange(String dateStart, String dateEnd)
        {
            var salesLine = from d in db.TrnSalesLines
                            where d.TrnSale.SalesDate >= Convert.ToDateTime(dateStart)
                            && d.TrnSale.SalesDate <= Convert.ToDateTime(dateEnd)
                            select new Entities.TrnSalesLine
                            {
                                Id = d.Id,
                                SalesId = d.SalesId,
                                ItemId = d.ItemId,
                                UnitId = d.UnitId,
                                Price = d.Price,
                                DiscountId = d.DiscountId,
                                DiscountRate = d.DiscountRate,
                                DiscountAmount = d.DiscountAmount,
                                NetPrice = d.NetPrice,
                                Quantity = d.Quantity,
                                Amount = d.Amount,
                                TaxId = d.TaxId,
                                TaxRate = d.TaxRate,
                                TaxAmount = d.TaxAmount,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                TaxAccountId = d.TaxAccountId,
                                SalesLineTimeStamp = d.SalesLineTimeStamp,
                                UserId = d.UserId,
                                Preparation = d.Preparation,
                            };
            return salesLine.ToList();
        }

        //******************
        //DETAIL SALES LINE
        //******************
        [HttpGet, Route("listSalesLineBySalesId/{salesId}")]
        public List<Entities.TrnSalesLine> listSalesLineBySalesId(String salesId)
        {
            var sales = from d in db.TrnSalesLines
                        where d.SalesId == Convert.ToInt32(salesId)
                        select new Entities.TrnSalesLine
                        {
                            Id = d.Id,
                            SalesId = d.SalesId,
                            ItemId = d.ItemId,
                            UnitId = d.UnitId,
                            Price = d.Price,
                            DiscountId = d.DiscountId,
                            DiscountRate = d.DiscountRate,
                            DiscountAmount = d.DiscountAmount,
                            NetPrice = d.NetPrice,
                            Quantity = d.Quantity,
                            Amount = d.Amount,
                            TaxId = d.TaxId,
                            TaxRate = d.TaxRate,
                            TaxAmount = d.TaxAmount,
                            SalesAccountId = d.SalesAccountId,
                            AssetAccountId = d.AssetAccountId,
                            CostAccountId = d.CostAccountId,
                            TaxAccountId = d.TaxAccountId,
                            SalesLineTimeStamp = d.SalesLineTimeStamp,
                            UserId = d.UserId,
                            Preparation = d.Preparation,
                        };

            return sales.ToList();
        }

        //******************
        //DETAIL SALES LINE
        //******************
        [HttpGet, Route("detail/{id}")]
        public TrnSalesLine detailSalesLine(String id)
        {
            var sales = from d in db.TrnSalesLines
                        where d.Id == Convert.ToInt32(id)
                        select new Entities.TrnSalesLine
                        {
                            Id = d.Id,
                            SalesId = d.SalesId,
                            ItemId = d.ItemId,
                            UnitId = d.UnitId,
                            Price = d.Price,
                            DiscountId = d.DiscountId,
                            DiscountRate = d.DiscountRate,
                            DiscountAmount = d.DiscountAmount,
                            NetPrice = d.NetPrice,
                            Quantity = d.Quantity,
                            Amount = d.Amount,
                            TaxId = d.TaxId,
                            TaxRate = d.TaxRate,
                            TaxAmount = d.TaxAmount,
                            SalesAccountId = d.SalesAccountId,
                            AssetAccountId = d.AssetAccountId,
                            CostAccountId = d.CostAccountId,
                            TaxAccountId = d.TaxAccountId,
                            SalesLineTimeStamp = d.SalesLineTimeStamp,
                            UserId = d.UserId,
                            Preparation = d.Preparation,

                        };
            return (TrnSalesLine)sales.FirstOrDefault();
        }

        //**********************
        //LIST         SALESLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnSalesLine> listSalesLine()
        {
            var salesLine = from d in db.TrnSalesLines
                            select new Entities.TrnSalesLine
                            {
                                Id = d.Id,
                                SalesId = d.SalesId,
                                ItemId = d.ItemId,
                                UnitId = d.UnitId,
                                Price = d.Price,
                                DiscountId = d.DiscountId,
                                DiscountRate = d.DiscountRate,
                                DiscountAmount = d.DiscountAmount,
                                NetPrice = d.NetPrice,
                                Quantity = d.Quantity,
                                Amount = d.Amount,
                                TaxId = d.TaxId,
                                TaxRate = d.TaxRate,
                                TaxAmount = d.TaxAmount,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                TaxAccountId = d.TaxAccountId,
                                SalesLineTimeStamp = d.SalesLineTimeStamp,
                                UserId = d.UserId,
                                Preparation = d.Preparation,
                            };
            return salesLine.ToList();
        }

        //**********************
        //ADD          SALESLINE
        //**********************
        [HttpPost, Route("post")]
        public HttpResponseMessage postSalesLine(Entities.TrnSalesLine objSalesLine)
        {
            try
            {
                Data.TrnSalesLine newSalesLine = new Data.TrnSalesLine();
                newSalesLine.SalesId = objSalesLine.SalesId;
                newSalesLine.ItemId = objSalesLine.ItemId;
                newSalesLine.UnitId = objSalesLine.UnitId;
                newSalesLine.Price = objSalesLine.Price;
                newSalesLine.DiscountId = objSalesLine.DiscountId;
                newSalesLine.DiscountRate = objSalesLine.DiscountRate;
                newSalesLine.DiscountAmount = objSalesLine.DiscountAmount;
                newSalesLine.NetPrice = objSalesLine.NetPrice;
                newSalesLine.Quantity = objSalesLine.Quantity;
                newSalesLine.Amount = objSalesLine.Amount;
                newSalesLine.TaxId = objSalesLine.TaxId;
                newSalesLine.TaxRate = TaxRate(TaxId());
                newSalesLine.TaxAmount = objSalesLine.TaxAmount;
                newSalesLine.SalesAccountId = objSalesLine.SalesAccountId;
                newSalesLine.AssetAccountId = objSalesLine.AssetAccountId;
                newSalesLine.CostAccountId = objSalesLine.CostAccountId;
                newSalesLine.TaxAccountId = objSalesLine.TaxAccountId;
                newSalesLine.SalesLineTimeStamp = DateTime.Today;
                newSalesLine.UserId = UserId();
                newSalesLine.Preparation = objSalesLine.Preparation;
                db.TrnSalesLines.InsertOnSubmit(newSalesLine);
                db.SubmitChanges();
                return Request.CreateResponse(HttpStatusCode.OK, newSalesLine.Id);
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }

        //**********************
        //UPDATE       SALESLINE 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSalesLine(String id, Entities.TrnSalesLine salesLine)
        {
            try
            {
                var salesLines = from d in db.TrnSalesLines where d.Id == Convert.ToInt32(id) select d;
                if (salesLines.Any())
                {
                    var updateSalesLine = salesLines.FirstOrDefault();
                    updateSalesLine.SalesId = salesLine.SalesId;
                    updateSalesLine.ItemId = salesLine.ItemId;
                    updateSalesLine.UnitId = salesLine.UnitId;
                    updateSalesLine.Price = salesLine.Price;
                    updateSalesLine.DiscountId = salesLine.DiscountId;
                    updateSalesLine.DiscountRate = salesLine.DiscountRate;
                    updateSalesLine.DiscountAmount = salesLine.DiscountAmount;
                    updateSalesLine.NetPrice = salesLine.NetPrice;
                    updateSalesLine.Quantity = salesLine.Quantity;
                    updateSalesLine.Amount = salesLine.Amount;
                    updateSalesLine.TaxId = salesLine.TaxId;
                    updateSalesLine.TaxRate = salesLine.TaxRate;
                    updateSalesLine.TaxAmount = salesLine.TaxAmount;
                    updateSalesLine.SalesAccountId = salesLine.SalesAccountId;
                    updateSalesLine.AssetAccountId = salesLine.AssetAccountId;
                    updateSalesLine.CostAccountId = salesLine.CostAccountId;
                    updateSalesLine.TaxAccountId = salesLine.TaxAccountId;
                    updateSalesLine.SalesLineTimeStamp = salesLine.SalesLineTimeStamp;
                    updateSalesLine.UserId = salesLine.UserId;
                    updateSalesLine.Preparation = salesLine.Preparation;
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
        //DELETE       SALESLINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSalesLine(String id)
        {
            try
            {
                var delete = from d in db.TrnSalesLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    if (delete.FirstOrDefault().IsPrinted == false)
                    {
                        db.TrnSalesLines.DeleteOnSubmit(delete.First());
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Item is already Printed");
                    }
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
