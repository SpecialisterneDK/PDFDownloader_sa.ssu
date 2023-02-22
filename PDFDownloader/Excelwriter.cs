using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;

namespace PDFDownloader
{
    internal class Excelwriter : IDisposable
    {
        readonly Application xl;
        readonly Workbook wb;
        private bool disposed = false;
        public Excelwriter(string path)
        {
            xl = new Application();
            wb = xl.Workbooks.Add();
            wb.SaveAs(path);

        }

        ~Excelwriter()
        {
            wb.Save();
            xl.ActiveWorkbook.Saved = true;
            wb.Close(0);
            xl.Quit();
            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(xl);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                wb.Save();
                xl.ActiveWorkbook.Saved = true;
                wb.Close(0);
                xl.Quit();
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(xl);
                GC.SuppressFinalize(this);
            }
        }

        public void Reportresults(DLRequest[] requests)
        {
            Worksheet ws;
            ws = (Worksheet)wb.Worksheets[1];
            ws.Cells[1, 1] = "BRnum";
            ws.Cells[1, 2] = "Name";
            ws.Cells[1, 3] = "Title";
            ws.Cells[1, 4] = "URL";
            ws.Cells[1, 5] = "Secondary";
            ws.Cells[1, 6] = "Outcome(0 = No Download, 1 = Primary URL, 2 = Secondary URL)";
            for (int i = 0; i < requests.Length; i++)
            {
                ws.Cells[i + 2, 1] = requests[i].BRnum;
                ws.Cells[i + 2, 2] = requests[i].name;
                ws.Cells[i + 2, 3] = requests[i].title;
                ws.Cells[i + 2, 4] = requests[i].url;
                ws.Cells[i + 2, 5] = requests[i].backup;
                ws.Cells[i + 2, 6] = requests[i].outcome;
            }

            wb.Save();
        }

    }
}
