using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/discount")]
    public class ApiMstDiscountController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST DISCOUNT 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstDiscount> listDiscount()
        {
            var discount = from d in db.MstDiscounts
                           select new Entities.MstDiscount
                           {
                               Id = d.Id,
                               Discount = d.Discount,
                               DiscountRate = d.DiscountRate,
                               IsVatExempt = d.IsVatExempt,
                               IsDateScheduled = d.IsDateScheduled,
                               DateStart = d.DateStart,
                               DateEnd = d.DateEnd,
                               IsTimeScheduled = d.IsTimeScheduled,
                               TimeStart = d.TimeStart,
                               TimeEnd = d.TimeEnd,
                               IsDayScheduled = d.IsDayScheduled,
                               DayMon = d.DayMon,
                               DayTue = d.DayTue,
                               DayWed = d.DayWed,
                               DayThu = d.DayThu,
                               DayFri = d.DayFri,
                               DaySat = d.DaySat,
                               DaySun = d.DaySun,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked,
                           };
            return discount.ToList();
        }

        //************
        //ADD DISCOUNT 
        //************
        [HttpPost, Route("post")]
        public Int32 postDiscount()
        {
            try
            {

                Data.MstDiscount newDiscount = new Data.MstDiscount();
                newDiscount.Discount = "n/a";
                newDiscount.DiscountRate = 0;
                newDiscount.IsVatExempt = false;
                newDiscount.IsDateScheduled = false;
                newDiscount.DateStart = DateTime.Today;
                newDiscount.DateEnd = DateTime.Today;
                newDiscount.IsTimeScheduled = false;
                newDiscount.TimeStart = DateTime.Now;
                newDiscount.TimeEnd = DateTime.Now;
                newDiscount.IsDayScheduled = false;
                newDiscount.DayMon = false;
                newDiscount.DayTue = false;
                newDiscount.DayWed = false;
                newDiscount.DayThu = false;
                newDiscount.DayFri = false;
                newDiscount.DaySat = false;
                newDiscount.DaySun = false;
                newDiscount.EntryUserId = UserId();
                newDiscount.EntryDateTime = DateTime.Today;
                newDiscount.UpdateUserId = UserId();
                newDiscount.UpdateDateTime = DateTime.Today;
                newDiscount.IsLocked = false;
                db.MstDiscounts.InsertOnSubmit(newDiscount);
                db.SubmitChanges();

                return newDiscount.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //***************
        //UPDATE DISCOUNT
        //***************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDiscount(String id, Entities.MstDiscount discount)
        {
            try
            {
                var discounts = from d in db.MstDiscounts where d.Id == Convert.ToInt32(id) select d;
                if (discounts.Any())
                {
                    var updateDiscount = discounts.FirstOrDefault();
                    updateDiscount.Discount = discount.Discount;
                    updateDiscount.DiscountRate = discount.DiscountRate;
                    updateDiscount.IsVatExempt = discount.IsVatExempt;
                    updateDiscount.IsDateScheduled = discount.IsDateScheduled;
                    updateDiscount.DateStart = discount.DateStart;
                    updateDiscount.DateEnd = discount.DateEnd;
                    updateDiscount.IsTimeScheduled = discount.IsTimeScheduled;
                    updateDiscount.TimeStart = discount.TimeStart;
                    updateDiscount.TimeEnd = discount.TimeEnd;
                    updateDiscount.IsDayScheduled = discount.IsDayScheduled;
                    updateDiscount.DayMon = discount.DayMon;
                    updateDiscount.DayTue = discount.DayTue;
                    updateDiscount.DayWed = discount.DayWed;
                    updateDiscount.DayThu = discount.DayThu;
                    updateDiscount.DayFri = discount.DayFri;
                    updateDiscount.DaySat = discount.DaySat;
                    updateDiscount.DaySun = discount.DaySun;
                    updateDiscount.EntryUserId = UserId();
                    updateDiscount.EntryDateTime = DateTime.Today;
                    updateDiscount.UpdateUserId = UserId();
                    updateDiscount.UpdateDateTime = DateTime.Today;
                    updateDiscount.IsLocked = discount.IsLocked;
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

        //***************
        //DELETE DISCOUNT
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDiscount(String id)
        {
            try
            {
                var delete = from d in db.MstDiscounts where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstDiscounts.DeleteOnSubmit(delete.First());
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
    }
}
