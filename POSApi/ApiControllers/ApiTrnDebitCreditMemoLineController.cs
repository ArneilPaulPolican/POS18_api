using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/debitcreditline")]
    public class ApiTrnDebitCreditMemoLineController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST DEBIT CREDIT MEMO LINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnDebitCreditMemoLine> listDebitCreditLine()
        {
            var debitCreditLine = from d in db.TrnDebitCreditMemoLines
                              select new Entities.TrnDebitCreditMemoLine
                              {
                                  Id = d.Id,
                                  DCMemoId = d.DCMemoId,
                                  SalesId = d.SalesId,
                                  AccountId = d.AccountId,
                                  Particulars = d.Particulars,
                                  DebitAmount = d.DebitAmount,
                                  CreditAmount = d.CreditAmount,
                              };
            return debitCreditLine.ToList();
        }


        //**********************
        //ADD DEBIT CREDIT MEMO LINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postDebitCreditLine()
        {
            try
            {

                Data.TrnDebitCreditMemoLine newDebitCreditLine = new Data.TrnDebitCreditMemoLine();
                newDebitCreditLine.DCMemoId = DebitCreditMemoId();
                newDebitCreditLine.SalesId = SalesId();
                newDebitCreditLine.AccountId = AccountId();
                newDebitCreditLine.Particulars = "n/a";
                newDebitCreditLine.DebitAmount = 0;
                newDebitCreditLine.CreditAmount = 0;
                db.TrnDebitCreditMemoLines.InsertOnSubmit(newDebitCreditLine);
                db.SubmitChanges();

                return newDebitCreditLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE DEBIT CREDIT MEMO LINE
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDebitCreditLine(String id, Entities.TrnDebitCreditMemoLine debitCreditLine)
        {
            try
            {
                var debitCreditLines = from d in db.TrnDebitCreditMemoLines where d.Id == Convert.ToInt32(id) select d;
                if (debitCreditLines.Any())
                {
                    var updateDebitCreditLine = debitCreditLines.FirstOrDefault();
                    updateDebitCreditLine.DCMemoId = debitCreditLine.DCMemoId;
                    updateDebitCreditLine.SalesId = debitCreditLine.SalesId;
                    updateDebitCreditLine.AccountId = debitCreditLine.AccountId;
                    updateDebitCreditLine.Particulars = debitCreditLine.Particulars;
                    updateDebitCreditLine.DebitAmount = debitCreditLine.DebitAmount;
                    updateDebitCreditLine.CreditAmount = debitCreditLine.CreditAmount;
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
        //DELETE DEBIT CREDIT MEMO LINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDebitCreditLine(String id)
        {
            try
            {
                var delete = from d in db.TrnDebitCreditMemoLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnDebitCreditMemoLines.DeleteOnSubmit(delete.First());
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
