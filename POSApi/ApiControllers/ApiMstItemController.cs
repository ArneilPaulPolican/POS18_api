using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    //[Authorize]
    [RoutePrefix("api/item")]
    public class ApiMstItemController : ApiMethod.ApiMethodController
    {
        //*************
        //LIST ITEM 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstItem> listItem()
        {
            var item = from d in db.MstItems
                       select new Entities.MstItem
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Alias = d.Alias,
                           GenericName = d.GenericName,
                           Category = d.Category,
                           SalesAccountId = d.SalesAccountId,
                           AssetAccountId = d.AssetAccountId,
                           CostAccountId = d.CostAccountId,
                           InTaxId = d.InTaxId,
                           OutTaxId = d.OutTaxId,
                           UnitId = d.UnitId,
                           DefaultSupplierId = d.DefaultSupplierId,
                           Cost = d.Cost,
                           MarkUp = d.MarkUp,
                           Price = d.Price,
                           ImagePath = d.ImagePath,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           ExpiryDate = d.ExpiryDate,
                           LotNumber = d.LotNumber,
                           Remarks = d.Remarks,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           DefaultKitchenReport = d.DefaultKitchenReport,
                           IsPackage = d.IsPackage,
                       };
            return item.ToList();
        }

        //*************
        //DETAIL ITEM 
        //*************
        [HttpGet, Route("detail/{id}")]
        public Entities.MstItem detailItem(String id)
        {
            var item = from d in db.MstItems
                       where d.Id == Convert.ToInt32(id)
                       select new Entities.MstItem
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Alias = d.Alias,
                           GenericName = d.GenericName,
                           Category = d.Category,
                           SalesAccountId = d.SalesAccountId,
                           AssetAccountId = d.AssetAccountId,
                           CostAccountId = d.CostAccountId,
                           InTaxId = d.InTaxId,
                           OutTaxId = d.OutTaxId,
                           UnitId = d.UnitId,
                           DefaultSupplierId = d.DefaultSupplierId,
                           Cost = d.Cost,
                           MarkUp = d.MarkUp,
                           Price = d.Price,
                           ImagePath = d.ImagePath,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           ExpiryDate = d.ExpiryDate,
                           LotNumber = d.LotNumber,
                           Remarks = d.Remarks,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           DefaultKitchenReport = d.DefaultKitchenReport,
                           IsPackage = d.IsPackage,
                           Components = getItemComponents(d.Id)
                       };
            return (Entities.MstItem)item.FirstOrDefault();
        }

        private List<Entities.MstItemComponent> getItemComponents(Int32 itemId)
        {
            List<Entities.MstItemComponent> components = new List<Entities.MstItemComponent>();

            var itemComponents = from d in db.MstItemComponents where d.ItemId == itemId select d;

            if(itemComponents.Any())
            {
                foreach (var itemComponent in itemComponents)
                {
                    components.Add(new Entities.MstItemComponent
                    {
                        ComponentItem = itemComponent.MstItem1.ItemDescription,
                        Unit = itemComponent.MstUnit.Unit,
                        Quantity = itemComponent.Quantity
                    });
                }
            }

            return components;
        }

        //************
        //ADD ITEM 
        //************
        [HttpPost, Route("post")]
        public Int32 postItem()
        {
            try
            {

                Data.MstItem newItem = new Data.MstItem();
                newItem.ItemCode = "n/a";
                newItem.BarCode = "n/a";
                newItem.ItemDescription = "n/a";
                newItem.Alias = "n/a";
                newItem.GenericName = "n/a";
                newItem.Category = "n/a";
                newItem.SalesAccountId = AccountId();
                newItem.AssetAccountId = AccountId();
                newItem.CostAccountId = AccountId();
                newItem.InTaxId = TaxId();
                newItem.OutTaxId = TaxId();
                newItem.UnitId = UnitId();
                newItem.DefaultSupplierId = SupplierId();
                newItem.Cost = 0;
                newItem.MarkUp = 0;
                newItem.Price = 0;
                newItem.ImagePath = "n/a";
                newItem.ReorderQuantity = 0;
                newItem.OnhandQuantity = 0;
                newItem.IsInventory = false;
                newItem.ExpiryDate = DateTime.Today;
                newItem.LotNumber = "n/a";
                newItem.Remarks = "n/a";
                newItem.EntryUserId = UserId();
                newItem.EntryDateTime = DateTime.Today;
                newItem.UpdateUserId = UserId();
                newItem.UpdateDateTime = DateTime.Today;
                newItem.IsLocked = false;
                newItem.DefaultKitchenReport = "n/a";
                newItem.IsPackage = false;
                db.MstItems.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return newItem.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //***************
        //UPDATE ITEM
        //***************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItem(String id, Entities.MstItem item)
        {
            try
            {
                var items = from d in db.MstItems where d.Id == Convert.ToInt32(id) select d;
                if (items.Any())
                {
                    var updateItem = items.FirstOrDefault();
                    updateItem.ItemCode = item.ItemCode;
                    updateItem.BarCode = item.BarCode;
                    updateItem.ItemDescription = item.ItemDescription;
                    updateItem.Alias = item.Alias;
                    updateItem.GenericName = item.GenericName;
                    updateItem.Category = item.Category;
                    updateItem.SalesAccountId = item.SalesAccountId;
                    updateItem.AssetAccountId = item.AssetAccountId;
                    updateItem.CostAccountId = item.CostAccountId;
                    updateItem.InTaxId = item.InTaxId;
                    updateItem.OutTaxId = item.OutTaxId;
                    updateItem.UnitId = item.UnitId;
                    updateItem.DefaultSupplierId = item.DefaultSupplierId;
                    updateItem.Cost = item.Cost;
                    updateItem.MarkUp = item.MarkUp;
                    updateItem.Price = item.Price;
                    updateItem.ImagePath = item.ImagePath;
                    updateItem.ReorderQuantity = item.ReorderQuantity;
                    updateItem.OnhandQuantity = item.OnhandQuantity;
                    updateItem.IsInventory = item.IsInventory;
                    updateItem.ExpiryDate = item.ExpiryDate;
                    updateItem.LotNumber = item.LotNumber;
                    updateItem.Remarks = item.Remarks;
                    updateItem.EntryUserId = UserId();
                    updateItem.EntryDateTime = DateTime.Today;
                    updateItem.UpdateUserId = UserId();
                    updateItem.UpdateDateTime = DateTime.Today;
                    updateItem.IsLocked = true;
                    updateItem.DefaultKitchenReport = item.DefaultKitchenReport;
                    updateItem.IsPackage = item.IsPackage;
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
        //DELETE ITEM
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItem(String id)
        {
            try
            {
                var delete = from d in db.MstItems where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstItems.DeleteOnSubmit(delete.First());
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
