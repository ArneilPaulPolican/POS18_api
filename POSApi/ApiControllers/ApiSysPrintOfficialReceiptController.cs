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
    [RoutePrefix("api/officialReceipt")]
    public class ApiSysPrintOfficialReceiptController : ApiController
    {
        // ============
        // Data Context
        // ============
        public Data.posDBDataContext db = new Data.posDBDataContext();

        // ================
        // Global Variables
        // ================
        private Int32 trnSalesId = 0;

        // =============
        // Print Receipt
        // =============
        [HttpGet, Route("print/{salesId}")]
        public void PrintOfficialReceipt(String salesId)
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
                pd.PrintPage += new PrintPageEventHandler(PrintOfficialReceiptPage);
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
        public void PrintOfficialReceiptPage(object sender, PrintPageEventArgs ev)
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

            // ==============
            // Defining Receipt Footer
            // ==============
            String receiptFooter = systemCurrent.FirstOrDefault().receiptFooter;

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
                y += companyAddressRectangle.Size.Height;

                // ===========
                // Operated By
                // ===========
                String operatedBy = "Operated by: " + systemCurrent.FirstOrDefault().operatedBy;
                RectangleF operatedByRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(operatedBy, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(operatedBy, fontArial8Regular, Brushes.Black, operatedByRectangle, drawFormatCenter);
                y += operatedByRectangle.Size.Height + 5.0F;

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
                    X = 80,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(TINData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(TINData, fontArial8Regular, Brushes.Black, TINDataRectangle, drawFormatLeft);
                y += TINLabelRectangle.Size.Height;

                // =============
                // Permit Number
                // =============
                String permitNoLabel = "Permit No.:";
                RectangleF permitNoLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(permitNoLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(permitNoLabel, fontArial8Regular, Brushes.Black, permitNoLabelRectangle, drawFormatLeft);

                String permitNoData = systemCurrent.FirstOrDefault().permitNo;
                RectangleF permitNoDataRectangle = new RectangleF
                {
                    X = 80,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(permitNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(permitNoData, fontArial8Regular, Brushes.Black, permitNoDataRectangle, drawFormatLeft);
                y += permitNoLabelRectangle.Size.Height;

                // ====================
                // Accreditation Number
                // ====================
                String accrdNoLabel = "Accrd. No.:";
                RectangleF accrdNoLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(accrdNoLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(accrdNoLabel, fontArial8Regular, Brushes.Black, accrdNoLabelRectangle, drawFormatLeft);

                String accrdNoData = systemCurrent.FirstOrDefault().accreditationNo;
                RectangleF accrdNoDataRectangle = new RectangleF
                {
                    X = 80,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(accrdNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(accrdNoData, fontArial8Regular, Brushes.Black, accrdNoDataRectangle, drawFormatLeft);
                y += accrdNoLabelRectangle.Size.Height;

                // =============
                // Serial Number
                // =============
                String serialNoLabel = "Serial No.:";
                RectangleF serialNoLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(serialNoLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(serialNoLabel, fontArial8Regular, Brushes.Black, serialNoLabelRectangle, drawFormatLeft);

                String serialNoData = systemCurrent.FirstOrDefault().serialNo;
                RectangleF serialNoDataRectangle = new RectangleF
                {
                    X = 80,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(serialNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(serialNoData, fontArial8Regular, Brushes.Black, serialNoDataRectangle, drawFormatLeft);
                y += serialNoLabelRectangle.Size.Height;

                // ==============
                // Machine Number
                // ==============
                String machineNoLabel = "Machine No.:";
                RectangleF machineNoLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(machineNoLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(machineNoLabel, fontArial8Regular, Brushes.Black, machineNoLabelRectangle, drawFormatLeft);

                String machineNoData = systemCurrent.FirstOrDefault().machineNo;
                RectangleF machineNoDataRectangle = new RectangleF
                {
                    X = 80,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(machineNoData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(machineNoData, fontArial8Regular, Brushes.Black, machineNoDataRectangle, drawFormatLeft);
                y += machineNoLabelRectangle.Size.Height + 5.0F;

                // ======================
                // Official Receipt Title
                // ======================
                String officialReceiptTitle = systemCurrent.FirstOrDefault().ORPrintTitle;
                RectangleF officialReceiptTitleRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(officialReceiptTitle, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(officialReceiptTitle, fontArial8Regular, Brushes.Black, officialReceiptTitleRectangle, drawFormatCenter);
                y += officialReceiptTitleRectangle.Size.Height + 5.0F;
            }

            // ============
            // Sales Header
            // ============
            var sales = from d in db.TrnSales
                        where d.Id == Convert.ToInt32(trnSalesId)
                        select d;

            if (sales.Any())
            {
                var period = from d in db.MstPeriods
                             select d;

                if (period.Any())
                {
                    String salesNumberText = period.FirstOrDefault().Period + "-" + sales.FirstOrDefault().SalesNumber;
                    graphics.DrawString(salesNumberText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(salesNumberText, fontArial8Regular).Height;
                }
                else
                {
                    String salesNumberText = sales.FirstOrDefault().SalesNumber;
                    graphics.DrawString(salesNumberText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(salesNumberText, fontArial8Regular).Height;
                }

                String salesDateText = sales.FirstOrDefault().SalesDate.ToString("MM-dd-yyyy h:mm:ss tt", CultureInfo.InvariantCulture) + "\n\n";
                graphics.DrawString(salesDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(salesDateText, fontArial8Regular).Height;

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

                // ==========
                // Sales Item
                // ==========
                var salesLines = from d in db.TrnSalesLines
                                 where d.SalesId == sales.FirstOrDefault().Id
                                 select d;

                Decimal totalSales = 0, totalDiscount = 0, cash = 0, change = 0;
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
                    }
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

                String cashLabel = "CASH";
                graphics.DrawString(cashLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String cashAmount = cash.ToString("#,##0.00");
                graphics.DrawString(cashAmount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashAmount, fontArial8Bold).Height;

                String changelabel = "CHANGE";
                graphics.DrawString(changelabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String changeAmount = change.ToString("#,##0.00");
                graphics.DrawString(changeAmount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(changeAmount, fontArial8Bold).Height;

                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);
            }

            // ====================
            // Line Points Settings
            // ====================
            Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
            Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

            graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

            // ======================
            // Receipt Footer
            // ======================
            if (systemCurrent.Any())
            {
                receiptFooter = "\n" + systemCurrent.FirstOrDefault().receiptFooter;
                RectangleF receiptFooterRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(245, ((int)graphics.MeasureString(receiptFooter, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(receiptFooter, fontArial8Regular, Brushes.Black, receiptFooterRectangle, drawFormatCenter);
                y += receiptFooterRectangle.Size.Height + 5.0F;
            }
        }
    }
}
