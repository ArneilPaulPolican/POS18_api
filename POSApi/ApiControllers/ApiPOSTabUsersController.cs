using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/posTab/users")]
    public class ApiPOSTabUsersController : ApiMethod.ApiMethodController
    {
        [AllowAnonymous, HttpGet, Route("list")]
        public List<Entities.MstUser> ListUser()
        {
            var user = from d in db.AspNetUsers
                       select new Entities.MstUser
                       {
                           UserId = d.Id,
                           UserName = d.UserName,
                       };

            return user.ToList();
        }

        [AllowAnonymous, HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUser(String id)
        {
            try
            {
                var mstUser = from d in db.MstUsers
                              where d.AspNetUserId == id
                              select d;

                if (mstUser.Any())
                {
                    var updateMstUser = mstUser.FirstOrDefault();
                    updateMstUser.AspNetUserId = null;
                    db.SubmitChanges();

                    var aspNetUser = from d in db.AspNetUsers
                                     where d.Id == id
                                     select d;

                    if (aspNetUser.Any())
                    {
                        db.AspNetUsers.DeleteOnSubmit(aspNetUser.FirstOrDefault());
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "POS TAB user not found!");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "iPOS user not found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
