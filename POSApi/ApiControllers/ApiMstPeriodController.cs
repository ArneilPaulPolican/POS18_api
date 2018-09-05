using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/period")]
    public class ApiMstPeriodController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Period 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstPeriod> listPeriod()
        {
            var period = from d in db.MstPeriods
                           select new Entities.MstPeriod
                           {
                               Id = d.Id,
                               Period = d.Period
                           };
            return period.ToList();
        }

        //************
        //ADD Period 
        //************
        [HttpPost, Route("post")]
        public Int32 postPeriod()
        {
            try
            {

                Data.MstPeriod newPeriod = new Data.MstPeriod();
                newPeriod.Period = "n/a";
                db.MstPeriods.InsertOnSubmit(newPeriod);
                db.SubmitChanges();

                return newPeriod.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE Period 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putPeriod(String id, Entities.MstPeriod period)
        {
            try
            {
                var periods = from d in db.MstPeriods where d.Id == Convert.ToInt32(id) select d;
                if (periods.Any())
                {
                    var updatePeriod = periods.FirstOrDefault();
                    updatePeriod.Period = "n/a";
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
        //DELETE Period
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deletePeriod(String id)
        {
            try
            {
                var delete = from d in db.MstPeriods where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstPeriods.DeleteOnSubmit(delete.First());
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
