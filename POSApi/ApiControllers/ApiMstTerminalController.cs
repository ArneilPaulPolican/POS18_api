using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/terminal")]
    public class ApiMstTerminalController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST Terminal 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstTerminal> listTerminal()
        {
            var terminal = from d in db.MstTerminals
                       select new Entities.MstTerminal
                       {
                           Id = d.Id,
                           Terminal = d.Terminal
                       };
            return terminal.ToList();
        }

        //************
        //ADD Terminal 
        //************
        [HttpPost, Route("post")]
        public Int32 postTerminal()
        {
            try
            {

                Data.MstTerminal newTerminal = new Data.MstTerminal();
                newTerminal.Terminal = "n/a";
                db.MstTerminals.InsertOnSubmit(newTerminal);
                db.SubmitChanges();

                return newTerminal.Id;

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
        public HttpResponseMessage putTerminal(String id, Entities.MstTerminal terminal)
        {
            try
            {
                var terminals = from d in db.MstTerminals where d.Id == Convert.ToInt32(id) select d;
                if (terminals.Any())
                {
                    var updateTerminal = terminals.FirstOrDefault();
                    updateTerminal.Terminal = terminal.Terminal;
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
        public HttpResponseMessage deleteTerminal(String id)
        {
            try
            {
                var delete = from d in db.MstTerminals where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstTerminals.DeleteOnSubmit(delete.First());
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
