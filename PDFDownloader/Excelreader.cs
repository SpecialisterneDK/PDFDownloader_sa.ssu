using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace PDFDownloader
{

    internal class Excelreader : IDisposable
    {

        private readonly string _filename;
        private readonly Workbook wb;
        private readonly Worksheet ws;
        readonly Excel.Application xl;

        public Excelreader(string path)
        {
            xl = new Excel.Application();
            _filename = path;
            wb = xl.Workbooks.Open(_filename);
            ws = wb.Worksheets[1];
        }

        public void Dispose()
        {
            wb.Close(0);
            Marshal.FinalReleaseComObject(ws);

            xl.Quit();
            Marshal.FinalReleaseComObject(xl);
            GC.SuppressFinalize(this);
        }

        ~Excelreader()
        {
            wb.Close(0);
            Marshal.FinalReleaseComObject(ws);

            xl.Quit();
            Marshal.FinalReleaseComObject(xl);
        }

        public int GetRowCount()
        {
            Range range = ws.UsedRange;

            return range.Rows.Count;
        }

        public Range ReadRange(string range)
        {
            return ws.Range[range];
        }

        public Range GetCellData(string coloumName)
        {
            int colNumber = 0;
            Range maxrange = ws.UsedRange;

            for (int i = 1; i < maxrange.Columns.Count; i++)
            {
                string colNameValue = Convert.ToString((maxrange.Cells[1,i] as Range).Value2);

                if(colNameValue.ToLower() == coloumName.ToLower())
                {
                    colNumber = i;
                    break;
                }
                
            }

            //Leet coding converting a number to it's capital letter
            string colLetter = colNumber > 25 ? "A"+ ((char)(colNumber + 38)).ToString() : ((char)(colNumber+64)).ToString();
            
             

            //Can be hardcoded to max 10 here
            string range =  colLetter + "2:" + colLetter + maxrange.Rows.Count;
            Range cellvalues = ws.Range[range];

            //Useful printing statement for testing
            //foreach (string result in cellvalues.Value)
            //{
            //    value += result + " ";
            //}


            return cellvalues;
        }

        public string[] GetCollomnArray(string colomnname)
        {
            var range = GetCellData(colomnname);
            return ConvertRangeToArray(range);
        }

        private string[] ConvertRangeToArray(Range range)
        {
            return ((Array)range.Value).OfType<object>().Select(o => o.ToString()).ToArray();
        }

    }

    
}
