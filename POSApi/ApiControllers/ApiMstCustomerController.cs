using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/customer")]
    public class ApiMstCustomerController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST CUSTOMER 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstCustomer> listCustomer()
        {
            var customer = from d in db.MstCustomers
                           select new Entities.MstCustomer
                           {
                               Id = d.Id,
                               Customer = d.Customer,
                               Address = d.Address,
                               ContactPerson = d.ContactPerson,
                               ContactNumber = d.ContactNumber,
                               CreditLimit = d.CreditLimit,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               WithReward = d.WithReward,
                               RewardNumber = d.RewardNumber,
                               RewardConversion = d.RewardConversion,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked
                           };
            return customer.ToList();
        }

        //************
        //ADD CUSTOMER 
        //************
        [HttpPost, Route("post")]
        public Int32 postCustomer()
        {
            try
            {

                Data.MstCustomer newCustomer = new Data.MstCustomer();
                newCustomer.Customer = "n/a";
                newCustomer.Address = "n/a";
                newCustomer.ContactPerson = "n/a";
                newCustomer.ContactNumber = "n/a";
                newCustomer.CreditLimit = 0;
                newCustomer.TermId = TermId();
                newCustomer.TIN = "n/a";
                newCustomer.WithReward = false;
                newCustomer.RewardNumber = "n/a";
                newCustomer.RewardConversion = 0;
                newCustomer.AccountId = AccountId();
                newCustomer.EntryUserId = UserId();
                newCustomer.EntryDateTime = DateTime.Today;
                newCustomer.UpdateUserId = UserId();
                newCustomer.UpdateDateTime = DateTime.Today;
                newCustomer.IsLocked = false;
                db.MstCustomers.InsertOnSubmit(newCustomer);
                db.SubmitChanges();

                return newCustomer.Id;

            }
            catch (Exception e)
            {   
                return 0;
            }
        }

        //*************
        //EDIT CUSTOMER 
        //*************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putCustomer(String id, Entities.MstCustomer customer)
        {
            try
            {
                var customers = from d in db.MstCustomers where d.Id == Convert.ToInt32(id) select d;
                if (customers.Any())
                {
                    var updateCustomers = customers.FirstOrDefault();
                    updateCustomers.Customer = customer.Customer;
                    updateCustomers.Address = customer.Address;
                    updateCustomers.ContactPerson = customer.ContactPerson;
                    updateCustomers.ContactNumber = customer.ContactNumber;
                    updateCustomers.CreditLimit = customer.CreditLimit;
                    updateCustomers.TermId = customer.TermId;
                    updateCustomers.TIN = customer.TIN;
                    updateCustomers.WithReward = customer.WithReward;
                    updateCustomers.RewardNumber = customer.RewardNumber;
                    updateCustomers.RewardConversion = customer.RewardConversion;
                    updateCustomers.AccountId = customer.AccountId;
                    updateCustomers.EntryUserId = customer.EntryUserId;
                    updateCustomers.EntryDateTime = DateTime.Now;
                    updateCustomers.UpdateUserId = customer.UpdateUserId;
                    updateCustomers.UpdateDateTime = DateTime.Now;
                    updateCustomers.IsLocked = customer.IsLocked;
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
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //***************
        //DELETE CUSTOMER
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteCustomer(String id)
        {
            try
            {
                var delete = from d in db.MstCustomers where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstCustomers.DeleteOnSubmit(delete.First());
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
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
