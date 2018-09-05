using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class ApiMstAccountController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ACCOUNT 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstAccount> listAccount()
        {
            var account = from d in db.MstAccounts
                          select new Entities.MstAccount
                          {
                              Id = d.Id,
                              Code = d.Code,
                              Account = d.Account,
                              IsLocked = d.IsLocked,
                              AccountType = d.AccountType,
                          };
            return account.ToList();
        }

        //************
        //ADD ACCOUNT 
        //************
        [HttpPost, Route("post")]
        public Int32 postAccount()
        {
            try
            {

                Data.MstAccount newAccount = new Data.MstAccount();
                newAccount.Code = "n/a";
                newAccount.Account = "n/a";
                newAccount.IsLocked = false;
                newAccount.AccountType = "n/a";
                db.MstAccounts.InsertOnSubmit(newAccount);
                db.SubmitChanges();

                return newAccount.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE ACCOUNT 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putAccount(String id, Entities.MstAccount account)
        {
            try
            {
                var accounts = from d in db.MstAccounts where d.Id == Convert.ToInt32(id) select d;
                if (accounts.Any())
                {
                    var updateAccount = accounts.FirstOrDefault();
                    updateAccount.Code = account.Code;
                    updateAccount.Account = account.Account;
                    updateAccount.IsLocked = account.IsLocked;
                    updateAccount.AccountType = account.AccountType;
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
        //DELETE ACCOUNT
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteAccount(String id)
        {
            try
            {
                var delete = from d in db.MstAccounts where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstAccounts.DeleteOnSubmit(delete.First());
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
