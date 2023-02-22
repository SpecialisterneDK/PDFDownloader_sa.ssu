using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PDFDownloader
{
    internal class PDFDownloader
    {
        public async Task<bool[]> Run()
        {
            //Testing
            Debug.WriteLine("Testing");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //Path for my system. Might differ on others
            string inputPath = "C:\\Users\\KOM\\Documents\\PDFDownload\\GRI_2017_2020 (1).xlsx";
            string outputPath = "C:\\Users\\KOM\\Documents\\PDFDownload\\Test\\";
            var results = await Run(inputPath, outputPath);
            stopwatch.Stop();
            Debug.WriteLine("Took " + stopwatch.ElapsedMilliseconds / 1000 + "Seconds. Press enter to end");
            Console.ReadLine();
            return results;
        }

        public async Task<bool[]> Run(string inputPath, string outputPath)
        {


            Excelreader xlreader = new Excelreader(inputPath);


            Filedownloader fd = new Filedownloader();



            string[] BRNums = xlreader.GetCollomnArray("BRnum");
            string[] URLS = xlreader.GetCollomnArray("Pdf_URL");
            string[] names = xlreader.GetCollomnArray("Name");
            string[] titles = xlreader.GetCollomnArray("Title");
            string[] backups = xlreader.GetCollomnArray("Report Html Address");


            string[] filesarray = Directory.GetFiles(outputPath);

            ConcurrentDictionary<string, byte> fileset = new ConcurrentDictionary<string, byte>();
            DLRequest.SetDict(fileset);

            foreach (var file in filesarray)
            {
                fileset.TryAdd(file, 1);
            }

            //For testing, this is a useful place to limit it to say the first 5000 entries
            DLRequest[] requestArray = new DLRequest[BRNums.Length];
            List<DLRequest> validRequests = new List<DLRequest>();


            for (int i = 0; i < requestArray.Length; i++)
            {
                var request = new DLRequest(outputPath, BRNums[i], names[i], titles[i], URLS[i], backups[i]);
                requestArray[i] = request;
                if (request.ValidatePDFURL() && request.Notduplicate() )
                {
                    validRequests.Add(request);
                }
                else
                {
                    request.outcome = 0;
                }
            }

            bool[] results = await fd.DownloadAllPDFS(validRequests);

            try
            {
                Excelwriter xlw = new Excelwriter(outputPath + "Report.xlsx");
                xlw.Reportresults(requestArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Excelerror: " + e);
            }

            return results;
        }

        public async Task<bool[]> Run(string inputPath, string outputPath, IProgress<int> progress)
        {





            Filedownloader fd = new Filedownloader();
            string[] BRNums;
            string[] URLS;
            string[] names;
            string[] titles;
            string[] backups;


            using (var xlreader = new Excelreader(inputPath))
            {
                BRNums = xlreader.GetCollomnArray("BRnum");
                URLS = xlreader.GetCollomnArray("Pdf_URL");
                names = xlreader.GetCollomnArray("Name");
                titles = xlreader.GetCollomnArray("Title");
                backups = xlreader.GetCollomnArray("Report Html Address");
            }



            string[] filesarray = Directory.GetFiles(outputPath);

            ConcurrentDictionary<string, byte> fileset = new ConcurrentDictionary<string, byte>();
            DLRequest.SetDict(fileset);

            foreach (var file in filesarray)
            {
                fileset.TryAdd(file, 1);
            }

            //For testing, this is a useful place to limit it to say the first 5000 entries
            DLRequest[] requestArray = new DLRequest[URLS.Length];
            List<DLRequest> validRequests = new List<DLRequest>();


            for (int i = 0; i < requestArray.Length; i++)
            {
                var request = new DLRequest(outputPath, BRNums[i], names[i], titles[i], URLS[i], backups[i]);
                requestArray[i] = request;
                if (request.ValidatePDFURL())
                {
                    validRequests.Add(request);
                }
                else
                {
                    request.outcome = 0;
                }
            }

            progress.Report(validRequests.Count);
            var results = await fd.DownloadAllPDFS(validRequests, progress);

            try
            {
                using (var xlw = new Excelwriter(outputPath + "Report.xlsx"))
                {
                    ;
                    xlw.Reportresults(requestArray);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Excelerror: " + e);
            }
            progress.Report(0);

            Debug.WriteLine("Downloaded " + results.Count(c => c)+ " out of " + results.Length);

            return results;

        }
    } 
}

