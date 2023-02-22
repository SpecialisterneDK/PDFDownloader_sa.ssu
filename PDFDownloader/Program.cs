using System;
using System.Windows.Forms;


namespace PDFDownloader
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Code for testing in the console.Projest must be marked as "Console Application" in settings for this to work
            //string inputPath = "C:\\Users\\KOM\\Documents\\PDFDownload\\GRI_2017_2020 (1).xlsx";
            //string outputPath = "C:\\Users\\KOM\\Documents\\PDFDownload\\Test\\";
            //PDFDownloader consoleTest = new PDFDownloader();
            //await consoleTest.Run(inputPath,outputPath);

            //Code to run the form. DOES NOT WORK. Deadlocks after a while

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
