using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/journal")]
    public class ApiTrnJournalController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST         JOURNAL
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnJournal> listSalesLine()
        {
            var journal = from d in db.TrnJournals
                            select new Entities.TrnJournal
                            {
                                Id = d.Id,
                                JournalDate = d.JournalDate,
                                JournalRefDocument = d.JournalRefDocument,
                                AccountId = d.AccountId,
                                DebitAmount = d.DebitAmount,
                                CreditAmount = d.CreditAmount,
                                SalesId = d.SalesId,
                                StockInId = d.StockInId,
                                StockOutId = d.StockOutId,
                                CollectionId = d.CollectionId,
                                DCMemoId = d.DCMemoId,
                                DisbursementId = d.DisbursementId,
                            };
            return journal.ToList();
        }

        //**********************
        //ADD          JOURNAL
        //**********************
        [HttpPost, Route("post")]
        public Int32 postJournal()
        {
            try
            {

                Data.TrnJournal newJournal = new Data.TrnJournal();
                newJournal.JournalDate = DateTime.Today;
                newJournal.JournalRefDocument = "n/a";
                newJournal.AccountId = AccountId();
                newJournal.DebitAmount = 0;
                newJournal.CreditAmount = 0;
                newJournal.SalesId = SalesId();
                newJournal.StockInId = StockInId();
                newJournal.StockOutId = StockOutId();
                newJournal.CollectionId = CollectionId();
                newJournal.DCMemoId = DebitCreditMemoId();
                newJournal.DisbursementId = DisbursementId();
                db.TrnJournals.InsertOnSubmit(newJournal);
                db.SubmitChanges();

                return newJournal.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE       JOURNAL 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putJournal(String id, Entities.TrnJournal journal)
        {
            try
            {
                var journals = from d in db.TrnJournals where d.Id == Convert.ToInt32(id) select d;
                if (journals.Any())
                {
                    var updateJournal = journals.FirstOrDefault();
                    updateJournal.JournalDate = journal.JournalDate;
                    updateJournal.JournalRefDocument = journal.JournalRefDocument;
                    updateJournal.AccountId = journal.AccountId;
                    updateJournal.DebitAmount = journal.DebitAmount;
                    updateJournal.CreditAmount = journal.CreditAmount;
                    updateJournal.SalesId = journal.SalesId;
                    updateJournal.StockInId = journal.StockInId;
                    updateJournal.StockOutId = journal.StockOutId;
                    updateJournal.CollectionId = journal.CollectionId;
                    updateJournal.DCMemoId = journal.DCMemoId;
                    updateJournal.DisbursementId = journal.DisbursementId;
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
        //DELETE       JOURNAL
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteJournal(String id)
        {
            try
            {
                var delete = from d in db.TrnJournals where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnJournals.DeleteOnSubmit(delete.First());
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
