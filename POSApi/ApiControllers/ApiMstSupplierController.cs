using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/supplier")]
    public class ApiMstSupplierController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Supplier
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstSupplier> listTable()
        {
            var supplier = from d in db.MstSuppliers
                        select new Entities.MstSupplier
                        {
                            Id = d.Id,
                            Supplier = d.Supplier,
                            Address = d.Address,
                            TelephoneNumber = d.TelephoneNumber,
                            CellphoneNumber = d.CellphoneNumber,
                            FaxNumber = d.FaxNumber,
                            TermId = d.TermId,
                            TIN = d.TIN,
                            AccountId = d.AccountId,
                            EntryUserId = d.EntryUserId,
                            EntryDateTime = d.EntryDateTime,
                            UpdateUserId = d.UpdateUserId,
                            UpdateDateTime = d.UpdateDateTime,
                            IsLocked = d.IsLocked,
                        };
            return supplier.ToList();
        }

        //************
        //ADD Supplier 
        //************
        [HttpPost, Route("post")]
        public Int32 postSupplier()
        {
            try
            {

                Data.MstSupplier newSupplier = new Data.MstSupplier();
                newSupplier.Supplier = "n/a";
                newSupplier.Address = "n/a";
                newSupplier.TelephoneNumber = "n/a";
                newSupplier.CellphoneNumber = "n/a";
                newSupplier.FaxNumber = "n/a";
                newSupplier.TermId = TermId();
                newSupplier.TIN = "n/a";
                newSupplier.AccountId = AccountId();
                newSupplier.EntryUserId = UserId();
                newSupplier.EntryDateTime = DateTime.Today;
                newSupplier.UpdateUserId = UserId();
                newSupplier.UpdateDateTime = DateTime.Today;
                newSupplier.IsLocked = false;
                db.MstSuppliers.InsertOnSubmit(newSupplier);
                db.SubmitChanges();

                return newSupplier.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE Supplier 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage updateSupplier(String id, Entities.MstSupplier supplier)
        {
            try
            {
                var suppliers = from d in db.MstSuppliers where d.Id == Convert.ToInt32(id) select d;
                if (suppliers.Any())
                {
                    var updateSupplier = suppliers.FirstOrDefault();
                    updateSupplier.Supplier = supplier.Supplier;
                    updateSupplier.Address = supplier.Address;
                    updateSupplier.TelephoneNumber = supplier.TelephoneNumber;
                    updateSupplier.CellphoneNumber = supplier.CellphoneNumber;
                    updateSupplier.FaxNumber = supplier.FaxNumber;
                    updateSupplier.TermId = supplier.TermId;
                    updateSupplier.TIN = supplier.TIN;
                    updateSupplier.AccountId = supplier.AccountId;
                    updateSupplier.EntryUserId = UserId();
                    updateSupplier.EntryDateTime = DateTime.Today;
                    updateSupplier.UpdateUserId = UserId();
                    updateSupplier.UpdateDateTime = DateTime.Today;
                    updateSupplier.IsLocked = true;
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
        //DELETE Supplier
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteTable(String id)
        {
            try
            {
                var delete = from d in db.MstSuppliers where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstSuppliers.DeleteOnSubmit(delete.First());
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
