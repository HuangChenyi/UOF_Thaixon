using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            string url = "http://211.22.211.211:8080/api/file/ext/Download/404d00d1-ede4-8941-6a64-14484c74b7cb?p=eyJVc2VySWQiOiJiMWIxN2VhMy0wNDZjLTRhNTEtZjVmMS0wOGRjYmQ5MzFmYzIiLCJFeHByaWVkVGltZSI6MTczMDg2MjU4MX0=&h=37c397780c57a5c36caa752ace4ff8d4";

            string base64String = "eyJVc2VySWQiOiJiMWIxN2VhMy0wNDZjLTRhNTEtZjVmMS0wOGRjYmQ5MzFmYzIiLCJFeHByaWVkVGltZSI6MTczMDg2MjU4MX0=";
            //base64 To string
            byte[] data = Convert.FromBase64String(base64String);
            string decodedString = Encoding.UTF8.GetString(data);
            Console.WriteLine(decodedString);
            
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {

                        //上面的Base64不像是有檔名，GUID也不是檔名，先存成是ABC.PDF

                        using (var fileStream = System.IO.File.Create("ABC.PDF"))
                        {
                            // 將檔案寫入到 MemoryStream
                            using (var memoryStream = new MemoryStream())
                            {
                                fileStream.CopyTo(memoryStream);

                            }
                        }

                        
                    }
                }
            }


  
        }
    }
}
