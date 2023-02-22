using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace PDFDownloader
{

    internal class Filedownloader
    {
        //private int currentdownloads = 0;
        readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(100);

        
        /// <summary>
        /// Downloads the PDF specified by the fields in the DLRequest parameter.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>True if the download succeeded. False othervise</returns>
        public async Task<bool> DownloadPDF(DLRequest request)
        {


            if (await request.TryURL())
            {
                while (true)
                {
                    if (request.Usingbackup)
                    {
                        try
                        {
                            using (var client = new WebClient())
                            {
                                //await client.DownloadFileTaskAsync(request.backup, request.Absolute());
                                //client.DownloadFileAsync(new Uri(request.backup), request.Absolute());
                                client.DownloadFile(request.backup,request.Absolute());
                                Debug.WriteLine("Finished downloading:" + request.backup);
                                request.outcome = 2;
                                return true;
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Connection Error" + e);
                            request.outcome = 0;
                            return false;
                        }
                    }
                    try
                    {
                        using (var client = new WebClient())
                        {
                            //await client.DownloadFileTaskAsync(request.url, request.Absolute());
                            //client.DownloadFileAsync(new Uri(request.url), request.Absolute());
                            client.DownloadFile(request.url, request.Absolute());
                            Debug.WriteLine("Finished downloading:" + request.url);
                            request.outcome = 1;
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        //Debug.WriteLine("Failed: " + request.PDFname() + ": " + request.url+ " : " + e.Message);
                        if (request.Validbackup)
                        {
                            request.Usingbackup = true;
                        }
                        else
                        {
                            Debug.WriteLine("Connection Error" + e);
                            request.outcome = 0;
                            return false;
                        }
                    }
                }
            }
            request.outcome = 0;
            return false;
        }

        /// <summary>
        /// Instatiates asyncronous execution of all downloadrequests from the list of DLRequests.
        /// </summary>
        /// <param name="requests"></param>
        /// <returns>A list of booleans, refering to each sucessful download</returns>
        public async Task<bool[]> DownloadAllPDFS(List<DLRequest> requests)
        {
            var PDFDownloadTasks = requests.Select(async request =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    return await DownloadPDF(request);
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }).ToList();

            return await Task.WhenAll(PDFDownloadTasks);
        }
        public async Task<bool[]> DownloadAllPDFS(List<DLRequest> requests, IProgress<int> progress)
        {
            var PDFDownloadTasks = requests.Select(async request =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    return await DownloadPDF(request);
                }
                finally
                {
                    progress.Report(0);
                    semaphoreSlim.Release();
                }
                
            }).ToList();

            return await Task.WhenAll(PDFDownloadTasks);
        }
    }
}
