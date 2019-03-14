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
            var wbFeature4AdsFedPStr = Regex.Replace("{\"2785\":\"50\",\"940\":\"19\",\"942\":\"50\"}", @"[\""\{\}]", "");
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
