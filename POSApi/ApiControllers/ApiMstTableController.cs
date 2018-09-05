using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/table")]
    public class ApiMstTableController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Table
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstTable> listTable()
        {
            var table = from d in db.MstTables
                             select new Entities.MstTable
                             {
                                 Id = d.Id,
                                 TableCode = d.TableCode,
                                 TableGroupId = d.TableGroupId,
                                 TopLocation = d.TopLocation,
                                 LeftLocation = d.LeftLocation,
                             };
            return table.ToList();
        }

        //************
        //ADD Table 
        //************
        [HttpPost, Route("post")]
        public Int32 postTable()
        {
            try
            {

                Data.MstTable newTable = new Data.MstTable();
                newTable.TableCode = "n/a";
                newTable.TableGroupId= TableGroupId();
                newTable.TopLocation = 0;
                newTable.LeftLocation = 0;
                db.MstTables.InsertOnSubmit(newTable);
                db.SubmitChanges();

                return newTable.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE Table 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage updateTable(String id, Entities.MstTable table)
        {
            try
            {
                var tables = from d in db.MstTables where d.Id == Convert.ToInt32(id) select d;
                if (tables.Any())
                {
                    var updateTable = tables.FirstOrDefault();
                    updateTable.TableCode = table.TableCode;
                    updateTable.TableGroupId = table.TableGroupId;
                    updateTable.TopLocation = table.TopLocation;
                    updateTable.LeftLocation = table.LeftLocation;
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
        //DELETE Table
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteTable(String id)
        {
            try
            {
                var delete = from d in db.MstTables where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstTables.DeleteOnSubmit(delete.First());
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
