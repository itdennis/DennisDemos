using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DennisDemos.Utils
{
    public class AzureDevOpsServices
    {
		public static async Task GetProjects()
		{
			try
			{
				var personalaccesstoken = "jixx6ujugmxpr63piio4vzgmdnzh7vsmzf5wflw27uv5ptr7daxq";

				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Add(
						new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
						Convert.ToBase64String(
							System.Text.ASCIIEncoding.ASCII.GetBytes(
								string.Format("{0}:{1}", "", personalaccesstoken))));

					using (HttpResponseMessage response = await client.GetAsync(
								"https://dev.azure.com/msasg/Bing_Ads/_apis/git/repositories/a1c17068-b146-4d6b-b799-1de19b02f569/items?path=/private/De.Snr.Listing.Config.Schemas/Config/ListingConfig.config&includeContent=true&api-version=5.1"))
					{
						response.EnsureSuccessStatusCode();
						string responseBody = await response.Content.ReadAsStringAsync();
						JObject jResponseBody = JObject.Parse(responseBody);
						JToken jContent = jResponseBody["content"];
						Console.WriteLine(responseBody);
						XmlDocument doc = new XmlDocument();
						doc.LoadXml(jContent.Value<string>());
						var node = doc.SelectSingleNode("/ListingConfig/WaaSWoodblocksModelNames");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
