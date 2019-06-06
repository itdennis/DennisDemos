using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class OtherDemo : DemoBase
    {
        public override void Run()
        {
            Dictionary<string, string> iiu = new Dictionary<string, string>();
            iiu["001"] = "";
           

            //ModelName[ \t]*=[ \t]*Woodblocks(?<FormatVersion>V\\d)?\\.US\\.Slot(?<SlotId>[0-9]+)
            Regex test = new Regex("{\"FeatureId\":\"(?<FeatureId>[0-9]+)\",\"SinkNodeId\":\"[0-9]+\",\"SlotId\":\"[0-9]+\"}", RegexOptions.Compiled);
            string target = "[{\"FeatureId\":\"941\",\"SinkNodeId\":\"0\",\"SlotId\":\"51\"},{\"FeatureId\":\"909\",\"SinkNodeId\":\"1\",\"SlotId\":\"51\"}]";
            var hits = test.Matches(target);
            if (hits.Count>0)
            {
               var res = hits[0].Groups["FeatureId"].Value;
            }
            var wbFeature4AdsFedPStr = Regex.Replace("[{\"FeatureId\":\"941\",\"SinkNodeId\":\"0\",\"SlotId\":\"51\"},{\"FeatureId\":\"909\",\"SinkNodeId\":\"1\",\"SlotId\":\"51\"}]", @"[\""\{\}\[\]]", "");
            var featureModels = wbFeature4AdsFedPStr.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var featureModel in featureModels)
            {
                var fields = featureModel.Split(':');
                if (fields.Count() != 2)
                {
                    continue;
                }
            }
        }
    }
}
