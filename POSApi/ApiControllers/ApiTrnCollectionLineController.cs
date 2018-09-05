using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/collectionline")]
    public class ApiTrnCollectionLineController : ApiMethod.ApiMethodController
    {
        //********************
        //LIST COLLECTION LINE
        //********************
        [HttpGet, Route("list")]
        public List<Entities.TrnCollectionLine> listCollectionLine()
        {
            var collectionLine = from d in db.TrnCollectionLines
                             select new Entities.TrnCollectionLine
                             {
                                 Id = d.Id,
                                 CollectionId = d.CollectionId,
                                 Amount = d.Amount,
                                 PayTypeId = d.PayTypeId,
                                 CheckNumber = d.CheckNumber,
                                 CheckDate = d.CheckDate,
                                 CheckBank = d.CheckBank,
                                 CreditCardVerificationCode = d.CreditCardVerificationCode,
                                 CreditCardNumber = d.CreditCardNumber,
                                 CreditCardType = d.CreditCardType,
                                 CreditCardBank = d.CreditCardBank,
                                 GiftCertificateNumber = d.GiftCertificateNumber,
                                 OtherInformation = d.OtherInformation,
                                 StockInId = d.StockInId,
                                 AccountId = d.AccountId,
                             };
            return collectionLine.ToList();
        }

        //*******************
        //ADD COLLECTION LINE
        //*******************
        [HttpPost, Route("post")]
        public Int32 postCollectionLine()
        {
            try
            {

                Data.TrnCollectionLine newCollectionLine = new Data.TrnCollectionLine();
                newCollectionLine.CollectionId = CollectionId();
                newCollectionLine.Amount = 0;
                newCollectionLine.PayTypeId = PayTypeId();
                newCollectionLine.CheckNumber = "n/a";
                newCollectionLine.CheckDate = DateTime.Today;
                newCollectionLine.CheckBank = "n/a";
                newCollectionLine.CreditCardVerificationCode = "n/a";
                newCollectionLine.CreditCardNumber = "n/a";
                newCollectionLine.CreditCardType = "n/a";
                newCollectionLine.CreditCardBank = "n/a";
                newCollectionLine.GiftCertificateNumber = "n/a";
                newCollectionLine.OtherInformation = "n/a";
                newCollectionLine.StockInId = StockInId();
                newCollectionLine.AccountId = AccountId();
                db.TrnCollectionLines.InsertOnSubmit(newCollectionLine);
                db.SubmitChanges();

                return newCollectionLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //***********************
        //UPDATE COLLECTION  LINE
        //***********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putCollectionLine(String id, Entities.TrnCollectionLine collectionLine)
        {
            try
            {
                var collectionLines = from d in db.TrnCollectionLines where d.Id == Convert.ToInt32(id) select d;
                if (collectionLines.Any())
                {
                    var updateCollectionLine = collectionLines.FirstOrDefault();
                    updateCollectionLine.CollectionId = collectionLine.CollectionId;
                    updateCollectionLine.Amount = collectionLine.CollectionId;
                    updateCollectionLine.PayTypeId = collectionLine.CollectionId;
                    updateCollectionLine.CheckNumber = collectionLine.CheckNumber;
                    updateCollectionLine.CheckDate = collectionLine.CheckDate;
                    updateCollectionLine.CheckBank = collectionLine.CheckBank;
                    updateCollectionLine.CreditCardVerificationCode = collectionLine.CreditCardVerificationCode;
                    updateCollectionLine.CreditCardNumber = collectionLine.CreditCardNumber;
                    updateCollectionLine.CreditCardType = collectionLine.CreditCardType;
                    updateCollectionLine.CreditCardBank = collectionLine.CreditCardBank;
                    updateCollectionLine.GiftCertificateNumber = collectionLine.GiftCertificateNumber;
                    updateCollectionLine.OtherInformation = collectionLine.OtherInformation;
                    updateCollectionLine.StockInId = collectionLine.StockInId;
                    updateCollectionLine.AccountId = collectionLine.AccountId;
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

        //**********************
        //DELETE COLLECTION LINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteCollectionLine(String id)
        {
            try
            {
                var delete = from d in db.TrnCollectionLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnCollectionLines.DeleteOnSubmit(delete.First());
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
