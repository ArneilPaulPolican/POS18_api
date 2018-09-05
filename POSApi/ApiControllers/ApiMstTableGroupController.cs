using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/tablegroup")]
    public class ApiMstTableGroupController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST TableGroup
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstTableGroup> listTableGroup()
        {
            var tableGroup = from d in db.MstTableGroups
                      select new Entities.MstTableGroup
                      {
                          Id = d.Id,
                          TableGroup = d.TableGroup,
                          EntryUserId = d.EntryUserId,
                          EntryDateTime = d.EntryDateTime,
                          UpdateUserId = d.UpdateUserId,
                          UpdateDateTime = d.UpdateDateTime,
                          IsLocked = d.IsLocked,
                      };
            return tableGroup.ToList();
        }

        //************
        //ADD TableGroup 
        //************
        [HttpPost, Route("post")]
        public Int32 postTableGroup()
        {
            try
            {

                Data.MstTableGroup newTableGroup = new Data.MstTableGroup();
                newTableGroup.TableGroup = "n/a";
                newTableGroup.EntryUserId = UserId();
                newTableGroup.EntryDateTime = DateTime.Today;
                newTableGroup.UpdateUserId = UserId();
                newTableGroup.UpdateDateTime = DateTime.Today;
                newTableGroup.IsLocked = false;
                db.MstTableGroups.InsertOnSubmit(newTableGroup);
                db.SubmitChanges();

                return newTableGroup.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE TableGroup 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage updateTableGroup(String id, Entities.MstTableGroup tableGroup)
        {
            try
            {
                var tableGroups = from d in db.MstTableGroups where d.Id == Convert.ToInt32(id) select d;
                if (tableGroups.Any())
                {
                    var updateTableGroup = tableGroups.FirstOrDefault();
                    updateTableGroup.TableGroup = tableGroup.TableGroup;
                    updateTableGroup.EntryUserId = UserId();
                    updateTableGroup.EntryDateTime = DateTime.Today;
                    updateTableGroup.UpdateUserId = UserId();
                    updateTableGroup.UpdateDateTime = DateTime.Today;
                    updateTableGroup.IsLocked = true;
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
        //DELETE TableGroup
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteTableGroups(String id)
        {
            try
            {
                var delete = from d in db.MstTableGroups where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstTableGroups.DeleteOnSubmit(delete.First());
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
