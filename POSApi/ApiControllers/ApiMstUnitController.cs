using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/unit")]
    public class ApiMstUnitController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST UNIT 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstUnit> listUnit()
        {
            var unit = from d in db.MstUnits
                          select new Entities.MstUnit
                          {
                              Id = d.Id,
                              Unit = d.Unit
                          };
            return unit.ToList();
        }

        //************
        //ADD UNIT 
        //************
        [HttpPost, Route("post")]
        public Int32 postUnit()
        {
            try
            {

                Data.MstUnit newUnit = new Data.MstUnit();
                newUnit.Unit = "n/a";
                db.MstUnits.InsertOnSubmit(newUnit);
                db.SubmitChanges();

                return newUnit.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE UNIT 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage updateUnit(String id, Entities.MstUnit unit)
        {
            try
            {
                var units = from d in db.MstUnits where d.Id == Convert.ToInt32(id) select d;
                if (units.Any())
                {
                    var updateUnit = units.FirstOrDefault();
                    updateUnit.Unit = unit.Unit;
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
        //DELETE UNIT
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUnit(String id)
        {
            try
            {
                var delete = from d in db.MstUnits where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstUnits.DeleteOnSubmit(delete.First());
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
