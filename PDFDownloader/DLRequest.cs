using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PDFDownloader
{
    internal class DLRequest
    {
        private static ConcurrentDictionary<string, byte> fileset;
        private readonly string path;
        public readonly string BRnum;
        public readonly string title;
        public readonly string name;
        public string url;
        public string backup;
        public int outcome;
        public bool Validbackup { get; private set; }
        public bool Usingbackup { get; set; }
        

        public static void SetDict (ConcurrentDictionary<string, byte> fileinstance)
        {
            fileset= fileinstance;
        }


        public DLRequest(string path, string BRNum, string name, string title, string url, string backup)
        {
            this.path = path;
            this.BRnum = BRNum;
            this.title = title;
            this.name = name;
            this.url = url;
            this.backup = backup;
            Validbackup = false;
            Usingbackup = false;
        }

        public string PDFname() { return BRnum + ".pdf"; }

        public string Absolute()
        {
            return path + PDFname();
        }

        public string Absolute(bool backup)
        {
            return path + (backup ? "" : "@BACKUP") + PDFname();
        }

        public bool Notduplicate()
        {
            if (fileset.TryAdd(Absolute(), 1))
            {

                return true;
            }
            Debug.WriteLine("Duplicate: " + PDFname());
            return false;
        }
        /// <summary>
        /// Checks whether the PDF-URL or the Backup contains ".pdf" anywhere in the text. 
        /// The outcome is stored in the Class.
        /// </summary>
        /// <returns>Void</returns>
        public bool ValidatePDFURL()
        {
            bool validurl = false;

            int index = url.IndexOf(".pdf", StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                //URL = URL.Substring(0, index+4);
                validurl = true;
            }

            int backupindex = backup.IndexOf(".pdf", StringComparison.OrdinalIgnoreCase);
            if (backupindex >= 0)
            {
                Validbackup = true;
            }
            Usingbackup = Validbackup && !validurl;
            bool result = validurl || Validbackup;
            //if (!result) { Debug.WriteLine("Invalid URL: " + url + "||" + backup); }
            return result;


        }
        /// <summary>
        /// Tries to connect to the URLs previously verified as valid. Checks the HttpStatusCodes and the Content-Type for the response
        /// </summary>
        /// <returns>True if download succedeed. False othervise.</returns>
        public async Task<bool> TryURL()
        {
            bool urlresult = false;

            if (!Usingbackup)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        //client.Timeout = TimeSpan.FromSeconds(25);
                        var response = await client.GetAsync(url);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var contentType = response.Content.Headers.GetValues("Content-Type").First();
                            //Debug.WriteLine("ContentType: " + contentType);
                            urlresult = contentType.StartsWith("application/pdf");
                            //Debug.WriteLine("URL:" + url + " " + (urlresult ? "Valid" : "Not Valid"));
                        }

                        //urlresult = (response.StatusCode == HttpStatusCode.OK);
                    }
                }
                catch
                {
                    urlresult = false;
                }
            }
            if (Validbackup)
            {
                try
                {

                    using (var client = new HttpClient())
                    {
                        //client.Timeout = TimeSpan.FromSeconds(25);
                        var response = await client.GetAsync(backup);
                        //Validbackup = response.StatusCode == HttpStatusCode.OK;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var contentType = response.Content.Headers.GetValues("Content-Type").First();
                            //Debug.WriteLine("ContentType: " + contentType);
                            urlresult = contentType.StartsWith("application/pdf");
                            //Debug.WriteLine("URL:" + backup + " " + (urlresult ? "Valid" : "Not Valid"));
                        }
                    }
                }
                catch
                {
                    Validbackup = false;
                }
            }
            Usingbackup = !urlresult && Validbackup;
            bool result = urlresult || Validbackup;
            //if (!result) { Debug.WriteLine("No response: " + url + "||" + backup); }
            return result;
        }
    }
}
