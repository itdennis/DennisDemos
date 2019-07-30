using DennisDemos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    public class TraceLogDemo
    {
        public void Run()
        {
            long a = 0;
            while (true)
            {

                Logger.GetInstance().LogInformation("[JobMonitor] : JobDesc = [PA OneDNN Daily Pipeline 2019-07-30][BN][600x300][k=12][Slot58], Experiment 99696629-6459-4213-9c05-a0dc34a620e8 status is Running, skip download model.");
                Logger.GetInstance().LogInformation($"index : {a}");
                a++;
            }
            
        }
    }
}
