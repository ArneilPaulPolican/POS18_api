using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/term")]
    public class ApiMstTermController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Term
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstTerm> listTerm()
        {
            var term = from d in db.MstTerms
                       select new Entities.MstTerm
                           {
                               Id = d.Id,
                               Term = d.Term,
                               NumberOfDays = d.NumberOfDays
                       };
            return term.ToList();
        }

        //************
        //ADD Term 
        //************
        [HttpPost, Route("post")]
        public Int32 postTerm()
        {
            try
            {

                Data.MstTerm newTerm = new Data.MstTerm();
                newTerm.Term = "n/a";
                newTerm.NumberOfDays = 0;
                db.MstTerms.InsertOnSubmit(newTerm);
                db.SubmitChanges();

                return newTerm.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //**************
        //UPDATE Terminal 
        //**************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putTerm(String id, Entities.MstTerm term)
        {
            try
            {
                var terms = from d in db.MstTerms where d.Id == Convert.ToInt32(id) select d;
                if (terms.Any())
                {
                    var updateTerm = terms.FirstOrDefault();
                    updateTerm.Term = term.Term;
                    updateTerm.NumberOfDays = term.NumberOfDays;
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
        //DELETE Terminal
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteTerm(String id)
        {
            try
            {
                var delete = from d in db.MstTerms where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstTerms.DeleteOnSubmit(delete.First());
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
