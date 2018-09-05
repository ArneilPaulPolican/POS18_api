using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/disbursement")]
    public class ApiTrnDisbursementController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST      DISBURSEMENT
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnDisbursement> listDisbursement()
        {
            var debitCredit = from d in db.TrnDisbursements
                              select new Entities.TrnDisbursement
                              {
                                  Id = d.Id,
                                  PeriodId = d.PeriodId,
                                  DisbursementDate = d.DisbursementDate,
                                  DisbursementNumber = d.DisbursementNumber,
                                  DisbursementType = d.DisbursementType,
                                  AccountId = d.AccountId,
                                  Amount = d.Amount,
                                  PayTypeId = d.PayTypeId,
                                  TerminalId = d.TerminalId,
                                  Remarks = d.Remarks,
                                  IsReturn = d.IsReturn,
                                  StockInId = d.StockInId,
                                  PreparedBy = d.PreparedBy,
                                  CheckedBy = d.CheckedBy,
                                  ApprovedBy = d.ApprovedBy,
                                  IsLocked = d.IsLocked,
                                  EntryUserId = d.EntryUserId,
                                  EntryDateTime = d.EntryDateTime,
                                  UpdateUserId = d.UpdateUserId,
                                  UpdateDateTime = d.UpdateDateTime,
                                  Amount1000 = d.Amount1000,
                                  Amount500 = d.Amount500,
                                  Amount200 = d.Amount200,
                                  Amount100 = d.Amount100,
                                  Amount50 = d.Amount50,
                                  Amount20 = d.Amount20,
                                  Amount10 = d.Amount10,
                                  Amount5 = d.Amount5,
                                  Amount1 = d.Amount1,
                                  Amount025 = d.Amount025,
                                  Amount010 = d.Amount010,
                                  Amount005 = d.Amount005,
                                  Amount001 = d.Amount001,
                                  Payee = d.Payee,
                              };
            return debitCredit.ToList();
        }

        //**********************
        //ADD       DISBURSEMENT
        //**********************
        [HttpPost, Route("post")]
        public Int32 postDisbursement()
        {
            try
            {

                Data.TrnDisbursement newDisbursement = new Data.TrnDisbursement();
                newDisbursement.PeriodId = PeriodId();
                newDisbursement.DisbursementDate = DateTime.Today;
                newDisbursement.DisbursementNumber = "n/a";
                newDisbursement.DisbursementType = "n/a";
                newDisbursement.Amount = 0;
                newDisbursement.PayTypeId = PayTypeId();
                newDisbursement.TerminalId = TerminalId();
                newDisbursement.Remarks = "n/a";
                newDisbursement.IsReturn = false;
                newDisbursement.StockInId = StockInId();
                newDisbursement.PreparedBy = UserId();
                newDisbursement.CheckedBy = UserId();
                newDisbursement.CheckedBy = UserId();
                newDisbursement.ApprovedBy = UserId();
                newDisbursement.IsLocked = false;
                newDisbursement.EntryUserId = UserId();
                newDisbursement.EntryDateTime = DateTime.Today;
                newDisbursement.UpdateUserId = UserId();
                newDisbursement.UpdateDateTime = DateTime.Today;
                newDisbursement.Amount1000 = 0;
                newDisbursement.Amount500 = 0;
                newDisbursement.Amount200 = 0;
                newDisbursement.Amount100 = 0;
                newDisbursement.Amount50 = 0;
                newDisbursement.Amount20 = 0;
                newDisbursement.Amount10 = 0;
                newDisbursement.Amount5 = 0;
                newDisbursement.Amount1 = 0;
                newDisbursement.Amount025 = 0;
                newDisbursement.Amount010 = 0;
                newDisbursement.Amount005 = 0;
                newDisbursement.Amount001 = 0;
                newDisbursement.Payee = "n/a";
                db.TrnDisbursements.InsertOnSubmit(newDisbursement);
                db.SubmitChanges();

                return newDisbursement.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE    DISBURSEMENT 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDisbursement(String id, Entities.TrnDisbursement disbursement)
        {
            try
            {
                var disbursements = from d in db.TrnDisbursements where d.Id == Convert.ToInt32(id) select d;
                if (disbursements.Any())
                {
                    var updateDisbursement = disbursements.FirstOrDefault();
                    updateDisbursement.PeriodId = disbursement.PeriodId;
                    updateDisbursement.DisbursementDate = disbursement.DisbursementDate;
                    updateDisbursement.DisbursementNumber = disbursement.DisbursementNumber;
                    updateDisbursement.DisbursementType = disbursement.DisbursementType;
                    updateDisbursement.Amount = disbursement.Amount;
                    updateDisbursement.PayTypeId = disbursement.PayTypeId;
                    updateDisbursement.TerminalId = disbursement.TerminalId;
                    updateDisbursement.Remarks = disbursement.Remarks;
                    updateDisbursement.IsReturn = disbursement.IsReturn;
                    updateDisbursement.StockInId = disbursement.StockInId;
                    updateDisbursement.PreparedBy = disbursement.PreparedBy;
                    updateDisbursement.CheckedBy = disbursement.CheckedBy;
                    updateDisbursement.CheckedBy = disbursement.CheckedBy;
                    updateDisbursement.ApprovedBy = disbursement.ApprovedBy;
                    updateDisbursement.IsLocked = true;
                    updateDisbursement.EntryUserId = UserId();
                    updateDisbursement.EntryDateTime = DateTime.Today;
                    updateDisbursement.UpdateUserId = UserId();
                    updateDisbursement.UpdateDateTime = DateTime.Today;
                    updateDisbursement.Amount1000 = disbursement.Amount1000;
                    updateDisbursement.Amount500 = disbursement.Amount500;
                    updateDisbursement.Amount200 = disbursement.Amount200;
                    updateDisbursement.Amount100 = disbursement.Amount100;
                    updateDisbursement.Amount50 = disbursement.Amount50;
                    updateDisbursement.Amount20 = disbursement.Amount20;
                    updateDisbursement.Amount10 = disbursement.Amount10;
                    updateDisbursement.Amount5 = disbursement.Amount5;
                    updateDisbursement.Amount1 = disbursement.Amount1;
                    updateDisbursement.Amount025 = disbursement.Amount025;
                    updateDisbursement.Amount010 = disbursement.Amount010;
                    updateDisbursement.Amount005 = disbursement.Amount005;
                    updateDisbursement.Amount001 = disbursement.Amount001;
                    updateDisbursement.Payee = disbursement.Payee;
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
        //DELETE    DISBURSEMENT
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDisbursement(String id)
        {
            try
            {
                var delete = from d in db.TrnDisbursements where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnDisbursements.DeleteOnSubmit(delete.First());
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
