using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading;

using System.Configuration;

using System.Security.Principal;
namespace MoneyExchanger
{
    public class PrintTransaction
    {
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string TaxRef { get; set; }
        public string TranNo { get; set; }
        public string Date { get; set; }
        public string CurCode { get; set; }
        public string TranType { get; set; }
        public string Rate { get; set; }
        public string ForeignAmt { get; set; }
        public string LocalAmt { get; set; }
        PrintDocument pdoc = null;

        public void print()
        {

            using (pdoc = new PrintDocument())
            {
                PrinterSettings ps = new PrinterSettings();
                Font font = new Font("Courier New", 15);
                PaperSize psize = new PaperSize("Custom", 100, 200);
                pdoc.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 820;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;

                pdoc.PrinterSettings.PrinterName = System.Configuration.ConfigurationManager.AppSettings["PrinterName"];
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

                pdoc.Print();

            }

        }


        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Verdana", 8);
            Font fontBold = new Font("Verdana", 8, FontStyle.Bold);

            Pen boldPen = new Pen(Color.Black, 3);
            Pen lightPen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            int startX = 3;
            int startY = 2;
            int Offset = 5;
            graphics.DrawString(Description, new Font("Courier New", 13, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("Authorised money changer",
                 font, new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(DateTime.Now.ToShortDateString(),
            font,
            new SolidBrush(Color.Black), startX + 180, startY + Offset);
            Offset = Offset + 18;
            graphics.DrawString(Address1,
                 font, new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 18;
            graphics.DrawString(Address2,
            font, new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 18;
            graphics.DrawString(Address3 + " - " + PostalCode,
            font, new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 18;
            graphics.DrawString(string.Format("Phone: {0}   LIC No : {1}", Phone,TaxRef),
               fontBold,
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 18;
            graphics.DrawString(string.Format("Receipt NO.:{0}    Cash Customer", TranNo),
               fontBold,
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 18;

            string TranFullName = (TranType == "B") ? "Buy" : "Sell";

            RectangleF rf = new RectangleF(startX, startY + Offset, 68, 30);
            graphics.DrawRectangle(lightPen, rf.X, rf.Y, rf.Width, rf.Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            graphics.DrawString(CurCode, new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf, stringFormat);

            RectangleF rf1 = new RectangleF(startX + 68, startY + Offset, 66, 30);
            graphics.DrawRectangle(lightPen, rf1.X, rf1.Y, rf1.Width, rf1.Height);
            StringFormat stringFormat1 = new StringFormat();
            stringFormat1.Alignment = StringAlignment.Center;
            stringFormat1.LineAlignment = StringAlignment.Center;
            graphics.DrawString(TranFullName, new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf1, stringFormat1);

            Offset = Offset + 10 + 30;

            RectangleF rf2 = new RectangleF(startX, startY + Offset, 135, 30);
            graphics.DrawRectangle(lightPen, rf2.X, rf2.Y, rf2.Width, rf2.Height);
            StringFormat stringFormat2 = new StringFormat();
            stringFormat2.Alignment = StringAlignment.Far;
            stringFormat2.LineAlignment = StringAlignment.Center;
            graphics.DrawString("FC Amt", new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf2, stringFormat2);

            RectangleF rf3 = new RectangleF(startX + 135, startY + Offset, 146, 30);
            graphics.DrawRectangle(lightPen, rf3.X, rf3.Y, rf3.Width, rf3.Height);
            StringFormat stringFormat3 = new StringFormat();
            stringFormat3.Alignment = StringAlignment.Far;
            stringFormat3.LineAlignment = StringAlignment.Center;
            graphics.DrawString("Local Amt", new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf3, stringFormat3);

            Offset = Offset  + 30;

            RectangleF rf4 = new RectangleF(startX, startY + Offset, 135, 30);
            graphics.DrawRectangle(lightPen, rf4.X, rf4.Y, rf4.Width, rf4.Height);
            StringFormat stringFormat4 = new StringFormat();
            stringFormat4.Alignment = StringAlignment.Far;
            stringFormat4.LineAlignment = StringAlignment.Center;
            graphics.DrawString(ForeignAmt, new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf4, stringFormat4);

            RectangleF rf5 = new RectangleF(startX + 135, startY + Offset, 146, 30);
            graphics.DrawRectangle(lightPen, rf5.X, rf5.Y, rf5.Width, rf5.Height);
            StringFormat stringFormat5 = new StringFormat();
            stringFormat5.Alignment = StringAlignment.Far;
            stringFormat5.LineAlignment = StringAlignment.Center;
            graphics.DrawString(LocalAmt, new Font("Verdana", 12, FontStyle.Bold), Brushes.Blue, rf5, stringFormat5);

            string txt = "";
            if (TranType == "B")
                txt = "Please Pay: " + LocalAmt;
            else
                txt = "Please Collect: " + ForeignAmt;

            Offset = Offset + 35;
            graphics.DrawString(txt,
            fontBold,
                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 18;
            graphics.DrawString("Pls. Count $ check notes before leaving ",
            font,
                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 18;
            graphics.DrawString("the counter as we shall not entertain",
            font,
                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 18;
            graphics.DrawString("any complaints thereafter. Thank you.",
            font,
                new SolidBrush(Color.Black), startX, startY + Offset);
        }

        void DrawRow(int startX, int startY, Graphics graphics, Pen lightPen, string[] txts, Font font)
        {
            int celltWidth = 0;

            var rectObjects = new[] { 
                new { rect = new RectangleF (startX , startY , celltWidth = 25, 20), txt = txts[0], alignment = StringAlignment.Far },
                new { rect = new RectangleF (startX = startX + celltWidth, startY, celltWidth =  105, 20), txt = txts[1] ,alignment = StringAlignment.Near},
                new { rect = new RectangleF (startX = startX + celltWidth, startY, celltWidth =  30, 20), txt = txts[2] ,alignment = StringAlignment.Far},
                new { rect = new RectangleF (startX =startX + celltWidth, startY, celltWidth =  60, 20), txt = txts[3],alignment = StringAlignment.Far },
                new { rect = new RectangleF (startX =startX + celltWidth, startY, celltWidth = 60, 20), txt = txts[4] ,alignment = StringAlignment.Far}
            };



            foreach (var r in rectObjects)
            {
                DrawColumn(r.rect, graphics, lightPen, r.txt, font, r.alignment);
            }



        }
        void DrawColumn(RectangleF cell, Graphics g, Pen lightPen, string text, Font font, StringAlignment alignment)
        {
            g.DrawRectangle(lightPen, cell.X, cell.Y, cell.Width, cell.Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = alignment;
            stringFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, Brushes.Blue, cell, stringFormat);
        }
    }
}