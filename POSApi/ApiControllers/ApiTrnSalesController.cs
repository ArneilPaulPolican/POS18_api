using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/sales")]
    public class ApiTrnSalesController : ApiMethod.ApiMethodController
    {
        // ===================
        // Fill Leading Zeroes
        // ===================
        public String FillLeadingZeroes(Int32 number, Int32 length)
        {
            var result = number.ToString();
            var pad = length - result.Length;
            while (pad > 0)
            {
                result = '0' + result;
                pad--;
            }

            return result;
        }

        //************************
        //LIST BY DATE RANGE SALES
        //************************
        [HttpGet, Route("getSalesPerDateRange/{dateStart}/{dateEnd}")]
        public List<Entities.TrnSales> getSalesPerDateRange(String dateStart, String dateEnd)
        {
            var sales = from d in db.TrnSales
                        where d.SalesDate >= Convert.ToDateTime(dateStart)
                        && d.SalesDate <= Convert.ToDateTime(dateEnd)
                        select new Entities.TrnSales
                        {
                            Id = d.Id,
                            PeriodId = d.PeriodId,
                            SalesDate = d.SalesDate,
                            SalesNumber = d.SalesNumber,
                            ManualInvoiceNumber = d.ManualInvoiceNumber,
                            Amount = d.Amount,
                            TableId = d.TableId,
                            CustomerId = d.CustomerId,
                            AccountId = d.AccountId,
                            TermId = d.TermId,
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge,
                            Remarks = d.Remarks,
                            SalesAgent = d.SalesAgent,
                            TerminalId = d.TerminalId,
                            PreparedBy = d.PreparedBy,
                            CheckedBy = d.CheckedBy,
                            ApprovedBy = d.ApprovedBy,
                            IsLocked = d.IsLocked,
                            IsCancelled = d.IsCancelled,
                            PaidAmount = d.PaidAmount,
                            CreditAmount = d.CreditAmount,
                            DebitAmount = d.DebitAmount,
                            BalanceAmount = d.BalanceAmount,
                            EntryUserId = d.EntryUserId,
                            EntryDateTime = d.EntryDateTime,
                            UpdateUserId = d.UpdateUserId,
                            UpdateDateTime = d.UpdateDateTime,
                            Pax = d.Pax,
                            CollectionNumber = d.TrnCollections != null ? d.TrnCollections.Where(c => c.SalesId == d.Id).FirstOrDefault().CollectionNumber : " ",
                            DiscountedPax = d.DiscountedPax
                        };

            return sales.ToList();
        }

        //*****************
        //GET BY ID - SALES
        //*****************
        [HttpGet, Route("getSalesPerSalesId/{id}")]
        public Entities.TrnSales getSalesPerSalesId(String id)
        {
            var sales = from d in db.TrnSales
                        where d.Id == Convert.ToInt32(id)
                        select new Entities.TrnSales
                        {
                            Id = d.Id,
                            PeriodId = d.PeriodId,
                            SalesDate = d.SalesDate,
                            SalesNumber = d.SalesNumber,
                            ManualInvoiceNumber = d.ManualInvoiceNumber,
                            Amount = d.Amount,
                            TableId = d.TableId,
                            CustomerId = d.CustomerId,
                            AccountId = d.AccountId,
                            TermId = d.TermId,
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge,
                            Remarks = d.Remarks,
                            SalesAgent = d.SalesAgent,
                            TerminalId = d.TerminalId,
                            PreparedBy = d.PreparedBy,
                            CheckedBy = d.CheckedBy,
                            ApprovedBy = d.ApprovedBy,
                            IsLocked = d.IsLocked,
                            IsCancelled = d.IsCancelled,
                            PaidAmount = d.PaidAmount,
                            CreditAmount = d.CreditAmount,
                            DebitAmount = d.DebitAmount,
                            BalanceAmount = d.BalanceAmount,
                            EntryUserId = d.EntryUserId,
                            EntryDateTime = d.EntryDateTime,
                            UpdateUserId = d.UpdateUserId,
                            UpdateDateTime = d.UpdateDateTime,
                            Pax = d.Pax,
                            CollectionNumber = d.TrnCollections != null ? d.TrnCollections.Where(c => c.SalesId == d.Id).FirstOrDefault().CollectionNumber : " ",
                            DiscountedPax = d.DiscountedPax
                        };

            return sales.FirstOrDefault();
        }

        //**********************
        //LIST             SALES
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnSales> listSales()
        {
            var sales = from d in db.TrnSales
                        select new Entities.TrnSales
                        {
                            Id = d.Id,
                            PeriodId = d.PeriodId,
                            SalesDate = d.SalesDate,
                            SalesNumber = d.SalesNumber,
                            ManualInvoiceNumber = d.ManualInvoiceNumber,
                            Amount = d.Amount,
                            TableId = d.TableId,
                            CustomerId = d.CustomerId,
                            AccountId = d.AccountId,
                            TermId = d.TermId,
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge,
                            Remarks = d.Remarks,
                            SalesAgent = d.SalesAgent,
                            TerminalId = d.TerminalId,
                            PreparedBy = d.PreparedBy,
                            CheckedBy = d.CheckedBy,
                            ApprovedBy = d.ApprovedBy,
                            IsLocked = d.IsLocked,
                            IsCancelled = d.IsCancelled,
                            PaidAmount = d.PaidAmount,
                            CreditAmount = d.CreditAmount,
                            DebitAmount = d.DebitAmount,
                            BalanceAmount = d.BalanceAmount,
                            EntryUserId = d.EntryUserId,
                            EntryDateTime = d.EntryDateTime,
                            UpdateUserId = d.UpdateUserId,
                            UpdateDateTime = d.UpdateDateTime,
                            Pax = d.Pax,
                            CollectionNumber = d.TrnCollections != null ? d.TrnCollections.Where(c => c.SalesId == d.Id).FirstOrDefault().CollectionNumber : " ",
                            DiscountedPax = d.DiscountedPax
                        };

            return sales.ToList();
        }

        //**********************
        //ADD              SALES
        //**********************
        [HttpPost, Route("post")]
        public HttpResponseMessage postSales(Entities.TrnSales objSales)
        {
            var defaultPeriod = from d in db.MstPeriods select d;

            var lastSINumber = from d in db.TrnSales.OrderByDescending(d => d.Id) select d;
            var salesInNumberResult = defaultPeriod.FirstOrDefault().Period + "-000001";

            if (lastSINumber.Any())
            {
                var salesInNumberSplitStrings = lastSINumber.FirstOrDefault().SalesNumber;
                Int32 secondIndex = salesInNumberSplitStrings.IndexOf('-', salesInNumberSplitStrings.IndexOf('-'));
                var salesInNumberSplitStringValue = salesInNumberSplitStrings.Substring(secondIndex + 1);
                var salesInNumber = Convert.ToInt32(salesInNumberSplitStringValue) + 000001;
                salesInNumberResult = defaultPeriod.FirstOrDefault().Period + "-" + FillLeadingZeroes(salesInNumber, 6);
            }

            try
            {
                Data.TrnSale newSales = new Data.TrnSale();
                newSales.PeriodId = objSales.PeriodId;
                newSales.SalesDate = objSales.SalesDate;
                newSales.SalesNumber = salesInNumberResult;
                newSales.ManualInvoiceNumber = objSales.ManualInvoiceNumber;
                newSales.Amount = objSales.Amount;
                newSales.TableId = objSales.TableId;
                newSales.CustomerId = objSales.CustomerId;
                newSales.AccountId = objSales.AccountId;
                newSales.TermId = objSales.TermId;
                newSales.DiscountId = objSales.DiscountId;
                newSales.SeniorCitizenId = objSales.SeniorCitizenId;
                newSales.SeniorCitizenName = objSales.SeniorCitizenName;
                newSales.SeniorCitizenAge = objSales.SeniorCitizenAge;
                newSales.Remarks = "NA";
                newSales.SalesAgent = objSales.SalesAgent;
                newSales.TerminalId = objSales.TerminalId;
                newSales.PreparedBy = objSales.PreparedBy;
                newSales.CheckedBy = objSales.CheckedBy;
                newSales.ApprovedBy = objSales.ApprovedBy;
                newSales.IsLocked = objSales.IsLocked;
                newSales.IsCancelled = objSales.IsCancelled;
                newSales.PaidAmount = objSales.PaidAmount;
                newSales.CreditAmount = objSales.CreditAmount;
                newSales.DebitAmount = objSales.DebitAmount;
                newSales.BalanceAmount = objSales.BalanceAmount;
                newSales.EntryUserId = UserId();
                newSales.EntryDateTime = DateTime.Today;
                newSales.UpdateUserId = UserId();
                newSales.UpdateDateTime = DateTime.Today;
                newSales.Pax = objSales.Pax;
                newSales.TableStatus = objSales.TableStatus;
                newSales.DiscountedPax = objSales.DiscountedPax;
                db.TrnSales.InsertOnSubmit(newSales);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, newSales.Id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }


        //**********************
        //UPDATE           SALES 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSales(String id, Entities.TrnSales sale)
        {
            try
            {
                var sales = from d in db.TrnSales where d.Id == Convert.ToInt32(id) select d;
                if (sales.Any())
                {
                    var updateSales = sales.FirstOrDefault();
                    updateSales.PeriodId = sale.PeriodId;
                    updateSales.SalesDate = sale.SalesDate;
                    updateSales.SalesNumber = sale.SalesNumber;
                    updateSales.ManualInvoiceNumber = sale.ManualInvoiceNumber;
                    updateSales.Amount = sale.Amount;
                    updateSales.TableId = sale.TableId;
                    updateSales.CustomerId = sale.CustomerId;
                    updateSales.AccountId = sale.AccountId;
                    updateSales.TermId = sale.TermId;
                    updateSales.DiscountId = sale.DiscountId;
                    updateSales.SeniorCitizenId = sale.SeniorCitizenId;
                    updateSales.SeniorCitizenName = sale.SeniorCitizenName;
                    updateSales.SeniorCitizenAge = sale.SeniorCitizenAge;
                    updateSales.Remarks = "NA";
                    updateSales.SalesAgent = sale.SalesAgent;
                    updateSales.TerminalId = sale.TerminalId;
                    updateSales.PreparedBy = sale.PreparedBy;
                    updateSales.CheckedBy = sale.CheckedBy;
                    updateSales.ApprovedBy = sale.ApprovedBy;
                    updateSales.IsLocked = sale.IsLocked;
                    updateSales.IsCancelled = sale.IsCancelled;
                    updateSales.PaidAmount = sale.PaidAmount;
                    updateSales.CreditAmount = sale.CreditAmount;
                    updateSales.DebitAmount = sale.DebitAmount;
                    updateSales.BalanceAmount = sale.BalanceAmount;
                    updateSales.EntryUserId = UserId();
                    updateSales.EntryDateTime = DateTime.Today;
                    updateSales.UpdateUserId = UserId();
                    updateSales.UpdateDateTime = DateTime.Today;
                    updateSales.Pax = sale.Pax;
                    updateSales.TableStatus = sale.TableStatus;
                    updateSales.DiscountedPax = sale.DiscountedPax;
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }

        //**********************
        //DELETE           SALES
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSales(String id)
        {
            try
            {
                var delete = from d in db.TrnSales where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnSales.DeleteOnSubmit(delete.First());
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
