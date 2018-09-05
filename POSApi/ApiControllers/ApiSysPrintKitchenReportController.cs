using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Printing;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Web.Http;
using System.Net.Http;

namespace POSApi.ApiControllers
{
    [RoutePrefix("api/kitchenReport")]
    public class ApiSysPrintKitchenReportController : ApiController
    {
        // ============
        // Data Context
        // ============
        public Data.posDBDataContext db = new Data.posDBDataContext();

        // ================
        // Global Variables
        // ================
        private Int32 trnSalesLineId = 0;

        // ==========
        // Print Page
        // ==========
        [HttpGet, Route("print/{salesLineId}")]
        public void PrintKitchenReport(String salesLineId)
        {
            try
            {
                trnSalesLineId = Convert.ToInt32(salesLineId);

                PrinterSettings ps = new PrinterSettings
                {
                    //PrinterName = "EPSON TM-T81 Receipt"
                    PrinterName = "EPSON TM-U220 Receipt"
                };
                
                //var SalesLine = from d in db.TrnSalesLines where d.Id == Convert.ToInt32(salesLineId) select d;
                //if (SalesLine.FirstOrDefault().IsPrinted == false)
                //{
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(PrintKitchenReportPage);
                    pd.PrinterSettings = ps;
                    pd.Print();
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // ==========
        // Print Page
        // ==========
        public void PrintKitchenReportPage(object sender, PrintPageEventArgs ev)
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

            if (systemCurrent.Any())
            {
                // ============
                // Sales Header
                // ============
                var salesLine = from d in db.TrnSalesLines
                                where d.Id == Convert.ToInt32(trnSalesLineId) && d.IsPrinted==false
                                select d;

                // ======
                // Header
                // ======
                if (salesLine.Any())
                {
                    //var UpdateSalesLineIsPrinted = salesLine.FirstOrDefault();
                    //UpdateSalesLineIsPrinted.IsPrinted = true;
                    //db.SubmitChanges();

                    // ============
                    // Order Number
                    // ============
                    String orderNoLabelData = "Order No: " + salesLine.FirstOrDefault().TrnSale.SalesNumber;
                    graphics.DrawString(orderNoLabelData, fontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(orderNoLabelData, fontArial12Bold).Height + 5.0F;

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

                    String preparedByData = salesLine.FirstOrDefault().TrnSale.MstUser.FullName;
                    RectangleF preparedByDataRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(preparedByData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(preparedByData, fontArial8Regular, Brushes.Black, preparedByDataRectangle, drawFormatLeft);
                    y += preparedByDataRectangle.Size.Height;

                    // ========
                    // Customer
                    // ========
                    String customerLabel = "Customer:";
                    RectangleF customerLabelRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(customerLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(customerLabel, fontArial8Regular, Brushes.Black, customerLabelRectangle, drawFormatLeft);

                    String customerData = salesLine.FirstOrDefault().TrnSale.MstCustomer.Customer;
                    RectangleF customerDataRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(customerData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(customerData, fontArial8Regular, Brushes.Black, customerDataRectangle, drawFormatLeft);
                    y += customerDataRectangle.Size.Height;

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

                    // =====
                    // Table
                    // =====
                    String tableLabel = "Table:";
                    RectangleF tableLabelRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(tableLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(tableLabel, fontArial8Regular, Brushes.Black, tableLabelRectangle, drawFormatLeft);

                    String tableData = salesLine.FirstOrDefault().TrnSale.MstTable.TableCode;
                    RectangleF tableDataRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(tableData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(tableData, fontArial8Regular, Brushes.Black, tableDataRectangle, drawFormatLeft);
                    y += tableDataRectangle.Size.Height;

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

                    String transactionDateData = salesLine.FirstOrDefault().TrnSale.SalesDate.ToString("MM-dd-yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    RectangleF transDateRectangle = new RectangleF
                    {
                        X = 120,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(transactionDateData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                    };
                    graphics.DrawString(transactionDateData, fontArial8Regular, Brushes.Black, transDateRectangle, drawFormatLeft);
                    y += transDateRectangle.Size.Height + 20;
                }

                // ====================
                // Line Points Settings
                // ====================
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) - 9);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) - 9);

                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                String itemLabel = "ITEM";
                graphics.DrawString(itemLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String amountLabel = "Unit";
                graphics.DrawString(amountLabel, fontArial8Regular, drawBrush, new RectangleF(x + 150.0F, y, width, height), drawFormatLeft);

                String qtyLabel = "Qty.";
                graphics.DrawString(qtyLabel, fontArial8Regular, drawBrush, new RectangleF(x + 220.0F, y, width, height), drawFormatLeft);
                y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 5.0F;

                String itemData = salesLine.FirstOrDefault().MstItem.ItemDescription;
                RectangleF itemDataRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(150, ((int)graphics.MeasureString(itemData, fontArial8Regular, 150, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(itemData, fontArial8Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);

                String unitData = salesLine.FirstOrDefault().MstItem.MstUnit.Unit;
                RectangleF unitDataRectangle = new RectangleF
                {
                    X = x + 150.0F,
                    Y = y,
                    Size = new Size(150, ((int)graphics.MeasureString(unitData, fontArial8Regular, 150, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(unitData, fontArial8Regular, Brushes.Black, unitDataRectangle, drawFormatLeft);

                String qtyData = salesLine.FirstOrDefault().Quantity.ToString("#,##0.00");
                RectangleF qtyDataRectangle = new RectangleF
                {
                    X = x + 90.0F,
                    Y = y,
                    Size = new Size(150, ((int)graphics.MeasureString(qtyData, fontArial8Regular, 150, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(qtyData, fontArial8Regular, Brushes.Black, qtyDataRectangle, drawFormatRight);
                y += qtyDataRectangle.Size.Height + 15.0F;

                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                String preparationLabel = "Preparation:";
                graphics.DrawString(preparationLabel, fontArial8Regular, drawBrush, new RectangleF(x, y + 15F, width, height), drawFormatLeft);
                y += graphics.MeasureString(preparationLabel, fontArial8Regular).Height + 15.0F;

                String preparationData = salesLine.FirstOrDefault().Preparation;
                graphics.DrawString(preparationData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += graphics.MeasureString(preparationData, fontArial8Regular).Height + 5.0F;

                Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

                graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                // ========
                // Order By
                // ========
                String orderByLabel = "Order By:";
                RectangleF orderByLabelRectangle = new RectangleF
                {
                    X = x,
                    Y = y + 15F,
                    Size = new Size(245, ((int)graphics.MeasureString(orderByLabel, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(orderByLabel, fontArial8Regular, Brushes.Black, orderByLabelRectangle, drawFormatLeft);

                String orderByData = salesLine.FirstOrDefault().TrnSale.MstUser.FullName;
                RectangleF orderByDataRectangle = new RectangleF
                {
                    X = 120,
                    Y = y + 15F,
                    Size = new Size(245, ((int)graphics.MeasureString(orderByData, fontArial8Regular, 245, StringFormat.GenericTypographic).Height))
                };
                graphics.DrawString(orderByData, fontArial8Regular, Brushes.Black, orderByDataRectangle, drawFormatLeft);
                y += orderByDataRectangle.Size.Height;
            }
        }

        // ==========
        // Print Page
        // ==========
        //public void PrinterPage(String id)
        //{
        //    try
        //    {
        //        salesLineId = Convert.ToInt32(id);

        //        String defaultPrinter = "EPSON TM-T81 Receipt";
        //        String alternativePrinter = "Microsoft XPS Document Writer";

        //        PrinterSettings defaultPrinterSettings = new PrinterSettings
        //        {
        //            PrinterName = alternativePrinter
        //        };

        //        //foreach(var avp in GetAvailablePrinters())
        //        //{
        //        //    Debug.WriteLine(avp + " wawawa");
        //        //}


        //        CheckPrinter();
        //        //  GetStatus();

        //        //PrintDocument pd = new PrintDocument();
        //        //pd.PrintPage += new PrintPageEventHandler(PrintPage);
        //        //pd.PrinterSettings = defaultPrinterSettings;
        //        //pd.Print();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex + "sasas");
        //    }
        //}

        //// ==========
        //// Check Available Printer
        //// ==========
        //public void PrinterStatus(String id)
        //{

        //    string printer1 = "EPSON TM-T81 Receipt";
        //    string printer2 = "Microsoft XPS Document Writer";
        //    try

        //    {
        //        salesLineId = Convert.ToInt32(id);

        //        //PrinterSettings printer = new PrinterSettings();

        //        //printer.PrinterName = printer2;
        //        //printer.PrinterName = printer1;

        //        //if (printer.IsValid) {
        //        //    PrintDocument pd = new PrintDocument();
        //        //          pd.PrintPage += new PrintPageEventHandler(PrintPage);
        //        //           pd.PrinterSettings = printer;
        //        //           pd.Print();
        //        //}


        //        foreach (string printername in PrinterSettings.InstalledPrinters)
        //        {
        //            PrinterSettings printer = new PrinterSettings();

        //            printer.PrinterName = printername;
        //            printer1 = printername;
        //            printer2 = printername;
        //            if (printer.IsValid)
        //            {
        //                PrintDocument pd = new PrintDocument();
        //                pd.PrintPage += new PrintPageEventHandler(PrintPage);
        //                pd.PrinterSettings = printer;
        //                pd.Print();

        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }


        //}

        //private void GetStatus()
        //{
        //    string printerName = "";
        //    string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);

        //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
        //    using (ManagementObjectCollection coll = searcher.Get())
        //    {
        //        try
        //        {
        //            foreach (ManagementObject printer in coll)
        //            {
        //                foreach (PropertyData property in printer.Properties)
        //                {


        //                    Debug.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
        //                }
        //            }
        //        }
        //        catch (ManagementException ex)
        //        {
        //            Debug.WriteLine(ex.Message);
        //        }
        //    }
        //}

        //[STAThread]
        //public void CheckPrinter()
        //{

        //    String defaultPrinter = "EPSON TM-T81 Receipt";
        //    String alternativePrinter = "Microsoft XPS Document Writer";

        //    var server = new LocalPrintServer();
        //    PrintQueue queue = server.DefaultPrintQueue;
        //    var name = queue.Name;
        //    name = defaultPrinter;
        //    var isDefault = queue.Name;
        //    //   var offline = queue.IsOffline;
        //    //  Debug.WriteLine("Default Printer is " + name);
        //    //Debug.WriteLine("Is printer Offline? " + offline);
        //    if ((isDefault.Equals(defaultPrinter)) && (queue.IsOffline))
        //    {
        //        Debug.WriteLine("Default Printer is " + name);
        //        Debug.WriteLine("Your printer " + name + "  is Offline");

        //        LocalPrintServer printServer = new LocalPrintServer();
        //        PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
        //        List<string> myPrinters = new List<string>();

        //        foreach (PrintQueue printer in printQueuesOnLocalServer)
        //        {
        //            myPrinters.Add(printer.FullName);
        //        }

        //        if (myPrinters.Contains(alternativePrinter) == true)
        //        {
        //            PrinterSettings defaultPrinterSettings = new PrinterSettings
        //            {
        //                PrinterName = alternativePrinter
        //            };
        //            PrintDocument pd = new PrintDocument();
        //            pd.PrintPage += new PrintPageEventHandler(PrintPage);
        //            pd.PrinterSettings = defaultPrinterSettings;
        //            pd.Print();

        //        }


        //    }
        //    else
        //    {
        //        Debug.WriteLine("Default Printer is " + name);
        //        Debug.WriteLine("Your printer " + name + "  is Online");

        //        PrinterSettings defaultPrinterSettings = new PrinterSettings
        //        {
        //            PrinterName = defaultPrinter
        //        };
        //        PrintDocument pd = new PrintDocument();
        //        pd.PrintPage += new PrintPageEventHandler(PrintPage);
        //        pd.PrinterSettings = defaultPrinterSettings;
        //        pd.Print();
        //    }
        //}
    }
}
