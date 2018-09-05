using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/collection")]
    public class ApiTrnCollectionController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST COLLECTION 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.TrnCollection> listCollection()
        {
            var collection = from d in db.TrnCollections
                          select new Entities.TrnCollection
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              CollectionDate = d.CollectionDate,
                              CollectionNumber = d.CollectionNumber,
                              TerminalId = d.TerminalId,
                              ManualORNumber = d.ManualORNumber,
                              CustomerId = d.CustomerId,
                              Remarks = d.Remarks,
                              SalesId = d.SalesId,
                              SalesBalanceAmount = d.SalesBalanceAmount,
                              Amount = d.Amount,
                              TenderAmount = d.TenderAmount,
                              ChangeAmount = d.ChangeAmount,
                              PreparedBy = d.PreparedBy,
                              CheckedBy = d.CheckedBy,
                              ApprovedBy = d.ApprovedBy,
                              IsCancelled = d.IsCancelled,
                              IsLocked = d.IsLocked,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                          };
            return collection.ToList();
        }

        //************
        //ADD COLLECTION 
        //************
        [HttpPost, Route("post")]
        public Int32 postCollection()
        {
            try
            {

                Data.TrnCollection newCollection = new Data.TrnCollection();
                newCollection.PeriodId = PeriodId();
                newCollection.CollectionDate = DateTime.Today;
                newCollection.CollectionNumber = "n/a";
                newCollection.TerminalId = TerminalId();
                newCollection.ManualORNumber = "n/a";
                newCollection.CustomerId = CustomerId();
                newCollection.Remarks = "n/a";
                newCollection.SalesId = 0;
                newCollection.SalesBalanceAmount = 0;
                newCollection.Amount = 0;
                newCollection.TenderAmount = 0;
                newCollection.ChangeAmount = 0;
                newCollection.PreparedBy = UserId();
                newCollection.CheckedBy = UserId();
                newCollection.ApprovedBy = UserId();
                newCollection.IsCancelled = false;
                newCollection.IsLocked = false;
                newCollection.EntryUserId = UserId();
                newCollection.EntryDateTime = DateTime.Today;
                newCollection.UpdateUserId = UserId();
                newCollection.UpdateDateTime = DateTime.Today;
                db.TrnCollections.InsertOnSubmit(newCollection);
                db.SubmitChanges();

                return newCollection.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE COLLECTION 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putCollection(String id, Entities.TrnCollection collection)
        {
            try
            {
                var collections = from d in db.TrnCollections where d.Id == Convert.ToInt32(id) select d;
                if (collections.Any())
                {
                    var updateCollection = collections.FirstOrDefault();
                    updateCollection.PeriodId = collection.PeriodId;
                    updateCollection.CollectionDate = collection.CollectionDate;
                    updateCollection.CollectionNumber = collection.CollectionNumber;
                    updateCollection.TerminalId = collection.TerminalId;
                    updateCollection.ManualORNumber = collection.ManualORNumber;
                    updateCollection.CustomerId = collection.CustomerId;
                    updateCollection.Remarks = collection.Remarks;
                    updateCollection.SalesId = collection.SalesId;
                    updateCollection.SalesBalanceAmount = collection.SalesBalanceAmount;
                    updateCollection.Amount = collection.Amount;
                    updateCollection.TenderAmount = collection.TenderAmount;
                    updateCollection.ChangeAmount = collection.ChangeAmount;
                    updateCollection.PreparedBy = UserId();
                    updateCollection.CheckedBy = UserId();
                    updateCollection.ApprovedBy = UserId();
                    updateCollection.IsCancelled = collection.IsCancelled;
                    updateCollection.IsLocked = true;
                    updateCollection.EntryUserId = UserId();
                    updateCollection.EntryDateTime = DateTime.Today;
                    updateCollection.UpdateUserId = UserId();
                    updateCollection.UpdateDateTime = DateTime.Today;
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

        //***************
        //DELETE COLLECTION
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteCollection(String id)
        {
            try
            {
                var delete = from d in db.TrnCollections where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnCollections.DeleteOnSubmit(delete.First());
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
