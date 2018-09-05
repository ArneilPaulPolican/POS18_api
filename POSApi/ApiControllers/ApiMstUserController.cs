using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class ApiMstUserController : ApiMethod.ApiMethodController
    {
        //*********************
        //GET USER PER USERNAME
        //*********************
        [HttpGet, Route("getUserInfoPerUsername/{username}")]
        public Entities.MstUser getUserInfoPerUsername(String username)
        {
            var user = from d in db.MstUsers
                       where d.UserName.Equals(username)
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           FullName = d.FullName,
                           UserCardNumber = d.UserCardNumber,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                       };

            return user.FirstOrDefault();
        }

        //*************
        //LIST USER 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstUser> listUser()
        {
            var user = from d in db.MstUsers
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           Password = d.Password,
                           FullName = d.FullName,
                           UserCardNumber = d.UserCardNumber,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                       };
            return user.ToList();
        }

        //************
        //ADD USER 
        //************
        [HttpPost, Route("post")]
        public Int32 postUser()
        {
            try
            {

                Data.MstUser newUser = new Data.MstUser();
                newUser.UserName = "n/a";
                newUser.Password = "n/a";
                newUser.FullName = "n/a";
                newUser.UserCardNumber = "n/a";
                newUser.EntryUserId = UserId();
                newUser.EntryDateTime = DateTime.Today;
                newUser.UpdateUserId = UserId();
                newUser.UpdateDateTime = DateTime.Today;
                newUser.IsLocked = false;
                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return newUser.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE USER 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.MstUser user)
        {
            try
            {
                var users = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (users.Any())
                {
                    var updateUser = users.FirstOrDefault();
                    updateUser.UserName = user.UserName;
                    updateUser.Password = user.Password;
                    updateUser.FullName = user.FullName;
                    updateUser.UserCardNumber = user.UserName;
                    updateUser.EntryUserId = UserId();
                    updateUser.EntryDateTime = DateTime.Today;
                    updateUser.UpdateUserId = UserId();
                    updateUser.UpdateDateTime = DateTime.Today;
                    updateUser.IsLocked = true;
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
        //DELETE USER
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUser(String id)
        {
            try
            {
                var delete = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstUsers.DeleteOnSubmit(delete.First());
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
