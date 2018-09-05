using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [RoutePrefix("api/salesOrder")]
    public class ApiSysPrintSalesOrderController : ApiController
    {
        // ============
        // Data Context
        // ============
        public Data.posDBDataContext db = new Data.posDBDataContext();

        // ================
        // Global Variables
        // ================
        private Int32 trnSalesId = 0;

        // =================
        // Print Sales Order
        // =================
        [HttpGet, Route("print/{salesId}")]
        public void PrintSalesOrder(String salesId)
        {
            try
            {
                trnSalesId = Convert.ToInt32(salesId);

                PrinterSettings ps = new PrinterSettings
                {
                    //PrinterName = "EPSON TM-T81 Receipt"
                    PrinterName = "EPSON TM-U220 Receipt"
                };

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintSalesOrderPage);
                pd.PrinterSettings = ps;
                pd.Print();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // ==========
        // Print Page
        // ==========
        public void PrintSalesOrderPage(object sender, PrintPageEventArgs ev)
        {
            // =============
            // Font Settings
            // =============
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);

            // ==================
            // Alignment Settings
            // ==================
            StringFormat drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            StringFormat drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };

            float x = 5, y = 5;
            float width = 245.0F, height = 0F;

            // ==============
            // Tools Settings
            // ==============
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black, 1);
            Graphics graphics = ev.Graphics;

            // ==============
            // System Current
            // ==============
            var systemCurrent = from d in db.SysCurrents
                                select d;

            // ======
            // Header
            // ======
            if (systemCurrent.Any())
            {
                // ============
                // Company Name
                // ============
                String companyName = systemCurrent.FirstOrDefault().companyName;
                RectangleF companyNameRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(companyName, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(companyName, fontArial8Regular, Brushes.Black, companyNameRectangle, drawFormatCenter);
                y += companyNameRectangle.Size.Height;

                // ===============
                // Company Address
                // ===============
                String companyAddress = systemCurrent.FirstOrDefault().address;
                RectangleF companyAddressRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(companyAddress, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(companyAddress, fontArial8Regular, Brushes.Black, companyAddressRectangle, drawFormatCenter);
                y += companyAddressRectangle.Size.Height + 5;

                // ==========
                // TIN Number
                // ==========
                String TINLabel = "TIN No.:";
                RectangleF TINLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(TINLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(TINLabel, fontArial8Regular, Brushes.Black, TINLabelRectangle, drawFormatLeft);

                String TINData = systemCurrent.FirstOrDefault().TIN;
                RectangleF TINDataRectangle = new RectangleF
                {
                    X = 120,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(TINData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(TINData, fontArial8Regular, Brushes.Black, TINDataRectangle, drawFormatLeft);
                y += TINLabelRectangle.Size.Height;

                // ============
                // Sales Header
                // ============
                var sales = from d in db.TrnSales
                            where d.Id == Convert.ToInt32(trnSalesId) &&
                                  d.TrnSalesLines.Count(c=>c.IsPrinted==false) > 0
                            select d;

                if (sales.Any())
                {
                    // ==============
                    // Invoice Number
                    // ==============
                    String invoiceLabel = "Invoice No.:";
                    RectangleF invoiceLabelRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(invoiceLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(invoiceLabel, fontArial8Regular, Brushes.Black, invoiceLabelRectangle, drawFormatLeft);

                    String invoiceNoData = sales.FirstOrDefault().SalesNumber;
                    RectangleF invoiceNoDataRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(invoiceNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(invoiceNoData, fontArial8Regular, Brushes.Black, invoiceNoDataRectangle, drawFormatLeft);
                    y += invoiceNoDataRectangle.Size.Height;

                    // ========
                    // Terminal
                    // ========
                    var terminal = from d in db.MstTerminals
                                   select d;

                    if (terminal.Any())
                    {
                        String terminalLabel = "Terminal:";
                        RectangleF terminalLabelRectangle = new RectangleF
                        {
                            X = x,
                            Y = y,
                            Size = new Size(245, ((int)graphics.MeasureString(terminalLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                        };
                        graphics.DrawString(terminalLabel, fontArial8Regular, Brushes.Black, terminalLabelRectangle, drawFormatLeft);

                        String terminalNoData = terminal.FirstOrDefault().Terminal;
                        RectangleF terminalNoDataRectangle = new RectangleF
                        {
                            X = 120,
                            Y = y,
                            Size = new Size(245, ((int)graphics.MeasureString(terminalNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                        };
                        graphics.DrawString(terminalNoData, fontArial8Regular, Brushes.Black, terminalNoDataRectangle, drawFormatLeft);
                        y += terminalNoDataRectangle.Size.Height;
                    }

                    // ===========
                    // Prepared By
                    // ===========
                    String preparedByLabel = "Prepared By.:";
                    RectangleF preparedByLabelRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(preparedByLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(preparedByLabel, fontArial8Regular, Brushes.Black, preparedByLabelRectangle, drawFormatLeft);

                    String preparedByData = sales.FirstOrDefault().MstUser.FullName;
                    RectangleF preparedByDataRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(preparedByData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(preparedByData, fontArial8Regular, Brushes.Black, preparedByDataRectangle, drawFormatLeft);
                    y += preparedByDataRectangle.Size.Height;

                    // ================
                    // Transaction Date
                    // ================
                    String transDateLabel = "Transaction Date.:";
                    RectangleF transDateLabelRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(transDateLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(transDateLabel, fontArial8Regular, Brushes.Black, transDateLabelRectangle, drawFormatLeft);

                    String transactionDateData = sales.FirstOrDefault().SalesDate.ToString("MM-dd-yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    RectangleF transDateRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(transactionDateData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(transactionDateData, fontArial8Regular, Brushes.Black, transDateRectangle, drawFormatLeft);
                    y += transDateRectangle.Size.Height + 20;

                    // ============
                    // Sales Header
                    // ============
                    var salesLines = from d in db.TrnSalesLines
                                     where d.SalesId == Convert.ToInt32(sales.FirstOrDefault().Id) &&
                                           d.IsPrinted == false
                                     select d;

                    if (salesLines.Any())
                    {
                        // ====================
                        // Line Points Settings
                        // ====================
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) - 9);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) - 9);

                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        String itemLabel = "ITEM";
                        graphics.DrawString(itemLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        String amountLabel = "AMOUNT";
                        graphics.DrawString(amountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 5.0F;

                        Decimal totalSales = 0, totalDiscount = 0;
                        if (salesLines.Any())
                        {
                            foreach (var salesLine in salesLines)
                            {
                                String itemData = salesLine.MstItem.ItemDescription + "\n" + "**" + salesLine.Quantity.ToString("#,##0.00") + " @ " + salesLine.Price.ToString("#,##0.00") + " - " + salesLine.MstTax.Tax;
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(150, ((int)graphics.MeasureString(itemData, fontArial8Regular, 150, StringFormat.GenericTypographic).Height))
                                };
                                graphics.DrawString(itemData, fontArial8Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);

                                String itemAmountData = salesLine.Amount.ToString("#,##0.00");
                                graphics.DrawString(itemAmountData, fontArial8Regular, drawBrush, new RectangleF(x, y, 245.0F, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + 5.0F;

                                totalSales += salesLine.Amount;
                                totalDiscount += salesLine.DiscountAmount;

                                salesLine.IsPrinted = true;
                            }

                            db.SubmitChanges();
                        }

                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        String totalSalesLabel = "\nTOTAL SALES";
                        graphics.DrawString(totalSalesLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        String totalSalesAmount = "\n" + totalSales.ToString("#,##0.00");
                        graphics.DrawString(totalSalesAmount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(totalSalesAmount, fontArial8Bold).Height;

                        String totalDiscountLabel = "TOTAL DISCOUNT";
                        graphics.DrawString(totalDiscountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        String totalDiscountAmount = totalDiscount.ToString("#,##0.00");
                        graphics.DrawString(totalDiscountAmount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(totalDiscountAmount, fontArial8Bold).Height;
                    }
                }
            }

            // ====================
            // Line Points Settings
            // ====================
            Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
            Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

            graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

            // ==============
            // Invoice Footer
            // ==============
            if (systemCurrent.Any())
            {
                String invoiceFooter = "\n" + systemCurrent.FirstOrDefault().invoiceFooter;
                RectangleF invoiceFooterRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(invoiceFooter, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(invoiceFooter, fontArial8Regular, Brushes.Black, invoiceFooterRectangle, drawFormatCenter);
                y += invoiceFooterRectangle.Size.Height + 5.0F;
            }
        }
    }
}
