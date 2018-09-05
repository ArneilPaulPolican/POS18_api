using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/userform")]
    public class ApiMstUserFormController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST USERFORM 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstUserForm> listUserForm()
        {
            var userForm = from d in db.MstUserForms
                       select new Entities.MstUserForm
                       {
                           Id = d.Id,
                           FormId = d.FormId,
                           UserId = d.UserId,
                           CanDelete = d.CanDelete,
                           CanAdd = d.CanAdd,
                           CanLock = d.CanLock,
                           CanUnlock = d.CanUnlock,
                           CanPrint = d.CanPrint,
                           CanPreview = d.CanPreview,
                           CanEdit = d.CanEdit,
                           CanTender = d.CanTender,
                           CanDiscount = d.CanDiscount,
                           CanView = d.CanView,
                           CanSplit = d.CanSplit,
                           CanCancel = d.CanCancel,
                           CanReturn = d.CanReturn,
                       };
            return userForm.ToList();
        }

        //************
        //ADD USERFORM
        //************
        [HttpPost, Route("post")]
        public Int32 postUserForm()
        {
            try
            {

                Data.MstUserForm newUserForm = new Data.MstUserForm();
                newUserForm.FormId = FormId();
                newUserForm.UserId = UserId();
                newUserForm.CanDelete = false;
                newUserForm.CanAdd = false;
                newUserForm.CanLock = false;
                newUserForm.CanUnlock = false;
                newUserForm.CanPrint = false;
                newUserForm.CanPreview = false;
                newUserForm.CanEdit = false;
                newUserForm.CanTender = false;
                newUserForm.CanDiscount = false;
                newUserForm.CanView = false;
                newUserForm.CanSplit = false;
                newUserForm.CanCancel = false;
                newUserForm.CanReturn = false;
                db.MstUserForms.InsertOnSubmit(newUserForm);
                db.SubmitChanges();

                return newUserForm.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE USERFORM 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUserForm(String id, Entities.MstUserForm userForm)
        {
            try
            {
                var userForms = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (userForms.Any())
                {
                    var updateUserForm = userForms.FirstOrDefault();
                    updateUserForm.FormId = userForm.FormId;
                    updateUserForm.UserId = userForm.FormId;
                    updateUserForm.CanDelete = userForm.CanDelete;
                    updateUserForm.CanAdd = userForm.CanAdd;
                    updateUserForm.CanLock = userForm.CanLock;
                    updateUserForm.CanUnlock = userForm.CanUnlock;
                    updateUserForm.CanPrint = userForm.CanPrint;
                    updateUserForm.CanPreview = userForm.CanPreview;
                    updateUserForm.CanEdit = userForm.CanEdit;
                    updateUserForm.CanTender = userForm.CanTender;
                    updateUserForm.CanDiscount = userForm.CanDiscount;
                    updateUserForm.CanView = userForm.CanView;
                    updateUserForm.CanSplit = userForm.CanSplit;
                    updateUserForm.CanCancel = userForm.CanCancel;
                    updateUserForm.CanReturn = userForm.CanReturn;
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
        //DELETE USERFORM
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUserForms(String id)
        {
            try
            {
                var delete = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstUserForms.DeleteOnSubmit(delete.First());
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
