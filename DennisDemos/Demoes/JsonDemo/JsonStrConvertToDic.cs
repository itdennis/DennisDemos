using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.JsonDemo
{
    class JsonStrConvertToDic
    {

        public static void Run()
        {
            string json = @"{""key1"":""value1"",""key2"":""value2""}";

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            string parameterList = @"{
                                       ""SearchPair"": [
                                        {
                                         ""FilterColumn"": ""Interested Party Name"",
                                         ""FilterValue"": ""JP Morgan""
                                        },
                                        {
                                         ""FilterColumn"": ""Reporting Entity Code"",
                                         ""FilterValue"": ""abrom""
                                        }],
                                       ""Report"": [
                                        {
                                         ""FilterColumn"": ""Name"",
                                         ""FilterValue"": ""PortfolioAppraisal"",
                                         ""ReportType"":""SSRS"",
                                         ""ExpectedResult"":""abrom,abromfi,""
                                        },
                                        {
                                         ""FilterColumn"": ""Name"",
                                         ""FilterValue"": ""Appraisal"",
                                         ""ReportType"":""REP"",
                                         ""ExpectedResult"":""abrom,abromfi,""
                                        }]
                                    }";

            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(parameterList);
        }
    }
}
