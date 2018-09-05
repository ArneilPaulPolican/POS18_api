using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/debitcredit")]
    public class ApiTrnDebitCreditMemoController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST DEBIT CREDIT MEMO 
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnDebitCreditMemo> listDebitCredit()
        {
            var debitCredit = from d in db.TrnDebitCreditMemos
                          select new Entities.TrnDebitCreditMemo
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              DCMemoNumber = d.DCMemoNumber,
                              DCMemoDate = d.DCMemoDate,
                              Particulars = d.Particulars,
                              PreparedBy = d.PreparedBy,
                              CheckedBy = d.CheckedBy,
                              ApprovedBy = d.ApprovedBy,
                              IsLocked = d.IsLocked,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                          };
            return debitCredit.ToList();
        }


        //**********************
        //ADD DEBIT CREDIT MEMO 
        //**********************
        [HttpPost, Route("post")]
        public Int32 postDebitCredit()
        {
            try
            {

                Data.TrnDebitCreditMemo newDebitCredit = new Data.TrnDebitCreditMemo();
                newDebitCredit.PeriodId = PeriodId();
                newDebitCredit.DCMemoNumber = "n/a";
                newDebitCredit.DCMemoDate = DateTime.Today;
                newDebitCredit.Particulars = "n/a";
                newDebitCredit.PreparedBy = UserId();
                newDebitCredit.CheckedBy = UserId();
                newDebitCredit.ApprovedBy = UserId();
                newDebitCredit.IsLocked = false;
                newDebitCredit.EntryUserId = UserId();
                newDebitCredit.EntryDateTime = DateTime.Today;
                newDebitCredit.UpdateUserId = UserId();
                newDebitCredit.UpdateDateTime = DateTime.Today;
                db.TrnDebitCreditMemos.InsertOnSubmit(newDebitCredit);
                db.SubmitChanges();

                return newDebitCredit.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE DEBIT CREDIT MEMO 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDebitCredit(String id, Entities.TrnDebitCreditMemo debitCredit)
        {
            try
            {
                var debitCredits = from d in db.TrnDebitCreditMemos where d.Id == Convert.ToInt32(id) select d;
                if (debitCredits.Any())
                {
                    var updateDebitCredit = debitCredits.FirstOrDefault();
                    updateDebitCredit.PeriodId = debitCredit.PeriodId;
                    updateDebitCredit.DCMemoNumber = debitCredit.DCMemoNumber;
                    updateDebitCredit.DCMemoDate = debitCredit.DCMemoDate;
                    updateDebitCredit.Particulars = debitCredit.Particulars;
                    updateDebitCredit.PreparedBy = debitCredit.PreparedBy;
                    updateDebitCredit.CheckedBy = debitCredit.CheckedBy;
                    updateDebitCredit.ApprovedBy = debitCredit.ApprovedBy;
                    updateDebitCredit.IsLocked = true;
                    updateDebitCredit.EntryUserId = UserId();
                    updateDebitCredit.EntryDateTime = DateTime.Today;
                    updateDebitCredit.UpdateUserId = UserId();
                    updateDebitCredit.UpdateDateTime = DateTime.Today;
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
        //DELETE DEBIT CREDIT MEMO 
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDebitCredit(String id)
        {
            try
            {
                var delete = from d in db.TrnDebitCreditMemos where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnDebitCreditMemos.DeleteOnSubmit(delete.First());
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
