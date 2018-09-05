using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiMethod
{
    public class ApiMethodController : ApiController
    {
        //*************
        // DATA CONTEXT
        //*************
        public Data.posDBDataContext db = new Data.posDBDataContext();

        //**********************
        //LIST   METHOD      MST
        //**********************

        //MSTUSER
        public Int32 UserId()
        {
            var userId = from d in db.MstUsers select d.Id;
            return userId.FirstOrDefault();
        }

        //MSTACCOUNT
        public Int32 AccountId()
        {
            var accountId = from d in db.MstAccounts select d.Id;
            return accountId.FirstOrDefault();
        }

        //MSTCUSTOMER
        public Int32 CustomerId()
        {
            var customerId = from d in db.MstCustomers select d.Id;
            return customerId.FirstOrDefault();
        }

        //MSTDISCOUNT
        public Int32 DiscountId()
        {
            var discountId = from d in db.MstDiscounts select d.Id;
            return discountId.FirstOrDefault();
        }

        //MSTITEM
        public Int32 ItemId()
        {
            var itemId = from d in db.MstItems select d.Id;
            return itemId.FirstOrDefault();
        }

        //MSTITEM GROUP
        public Int32 ItemGroupId()
        {
            var itemGroupId = from d in db.MstItemGroups select d.Id;
            return itemGroupId.FirstOrDefault();
        }

        //MSTCOMPONENT ITEM
        public Int32 ComponentItemId()
        {
            var componentId = from d in db.MstItemComponents select d.Id;
            return componentId.FirstOrDefault();
        }


        //MSTPAYTYPE
        public Int32 PayTypeId()
        {
            var payTypeId = from d in db.MstPayTypes select d.Id;
            return payTypeId.FirstOrDefault();
        }

        //PERIOD
        public Int32 PeriodId()
        {
            var periodId = from d in db.MstPeriods select d.Id;
            return periodId.FirstOrDefault();
        }

        //MSTSUPPLIER
        public Int32 SupplierId()
        {
            var supplierId = from d in db.MstSuppliers select d.Id;
            return supplierId.FirstOrDefault();
        }

        //MSTTABLE
        public Int32 TableId()
        {
            var tableId = from d in db.MstTables select d.Id;
            return tableId.FirstOrDefault();
        }

        //MSTTABLE GROUP
        public Int32 TableGroupId()
        {
            var tableGroupId = from d in db.MstTableGroups select d.Id;
            return tableGroupId.FirstOrDefault();
        }

        //MSTTAX
        public Int32 TaxId()
        {
            var taxId = from d in db.MstTaxes select d.Id;
            return taxId.FirstOrDefault();
        }

        public Decimal TaxRate(int id)
        {
            var rate = from d in db.MstTaxes where d.Id == id select d.Rate;
            return rate.FirstOrDefault();
        }
        //MSTTERM
        public Int32 TermId()
        {
            var termId = from d in db.MstTerms select d.Id;
            return termId.FirstOrDefault();
        }
        //TERMINAL
        public Int32 TerminalId()
        {
            var terminalId = from d in db.MstTerminals select d.Id;
            return terminalId.FirstOrDefault();
        }
        //MSTUNIT
        public Int32 UnitId()
        {
            var unitId = from d in db.MstUnits select d.Id;
            return unitId.FirstOrDefault();
        }

        //**********************
        //LIST   METHOD      SYS
        //**********************

        //SYSFORM
        public Int32 FormId()
        {
            var formId = from d in db.SysForms select d.Id;
            return formId.FirstOrDefault();
        }

        //**********************
        //LIST   METHOD      TRN
        //**********************
        
        //TRNSALES
        public Int32 SalesId()
        {
            var salesId = from d in db.TrnSales select d.Id;
            return salesId.FirstOrDefault();
        }

        //TRNCOLLECTION
        public Int32 CollectionId()
        {
            var collectionId = from d in db.TrnSales select d.Id;
            return collectionId.FirstOrDefault();
        }

        //TRNDEBITCREDIT
        public Int32 DebitCreditMemoId()
        {
            var debitCreditMemoId = from d in db.TrnDebitCreditMemos select d.Id;
            return debitCreditMemoId.FirstOrDefault();
        }

        //TRNDISBURSEMENT
        public Int32 DisbursementId()
        {
            var disbursementId = from d in db.TrnDisbursements select d.Id;
            return disbursementId.FirstOrDefault();
        }

        //TRNPURCHASEORDER
        public Int32 PurchaseOrderId()
        {
            var purchaseOrderId = from d in db.TrnPurchaseOrders select d.Id;
            return purchaseOrderId.FirstOrDefault();
        }

        //TRNSTOCKCOUNT
        public Int32 StockCountId()
        {
            var stockCountId = from d in db.TrnStockCounts select d.Id;
            return stockCountId.FirstOrDefault();
        }

        //TRNSTOCKIN
        public Int32 StockInId()
        {
            var stockInId = from d in db.TrnStockIns select d.Id;
            return stockInId.FirstOrDefault();
        }

        //TRNSTOCKOUT
        public Int32 StockOutId()
        {
            var stockOutId = from d in db.TrnStockOuts select d.Id;
            return stockOutId.FirstOrDefault();
        }
    }
}
