using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Utils
{
    class Github
    {
        //JSON parsing methods
        struct LinkFields
        {
            public String self;
        }
        struct FileInfo
        {
            public String name;
            public String type;
            public String download_url;
            public LinkFields _links;
        }

        //Structs used to hold file data
        public struct FileData
        {
            public String name;
            public String contents;
        }
        public struct Directory
        {
            public String name;
            public List<Directory> subDirs;
            public List<FileData> files;
        }

        public static async Task<Directory> GetRepo(string owner, string name, string access_token)
        {
            HttpClient client = new HttpClient();
            Directory root = await ReadDirectory("root", client, String.Format("https://api.github.com/repos/{0}/{1}/contents/", owner, name), access_token);
            client.Dispose();
            return root;
        }

        //recursively get the contents of all files and subdirectories within a directory 
        private static async Task<Directory> ReadDirectory(String name, HttpClient client, string uri, string access_token)
        {
            //get the directory contents
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization",
                "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", access_token, "x-oauth-basic"))));
            request.Headers.Add("User-Agent", "lk-github-client");

            //parse result
            HttpResponseMessage response = await client.SendAsync(request);
            String jsonStr = await response.Content.ReadAsStringAsync(); ;
            response.Dispose();
            FileInfo[] dirContents = JsonConvert.DeserializeObject<FileInfo[]>(jsonStr);

            //read in data
            Directory result;
            result.name = name;
            result.subDirs = new List<Directory>();
            result.files = new List<FileData>();
            foreach (FileInfo file in dirContents)
            {
                if (file.type == "dir")
                { //read in the subdirectory
                    Directory sub = await ReadDirectory(file.name, client, file._links.self, access_token);
                    result.subDirs.Add(sub);
                }
                else
                { //get the file contents;
                    HttpRequestMessage downLoadUrl = new HttpRequestMessage(HttpMethod.Get, file.download_url);
                    downLoadUrl.Headers.Add("Authorization",
                        "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", access_token, "x-oauth-basic"))));
                    request.Headers.Add("User-Agent", "lk-github-client");

                    HttpResponseMessage contentResponse = await client.SendAsync(downLoadUrl);
                    String content = await contentResponse.Content.ReadAsStringAsync();
                    contentResponse.Dispose();

                    FileData data;
                    data.name = file.name;
                    data.contents = content;

                    result.files.Add(data);
                }
            }
            return result;
        }
    }
}
