using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class ConfigureAwaitDemo
    {

        public async Task Run()
        {
            Task res =  MyMethodAsync();
            await res;
        }
        public async Task MyMethodAsync()
        {
            // Code here runs in the original context.
            await Task.Delay(1000);
            // Code here runs in the original context.
            await Task.Delay(1000).ConfigureAwait(
              continueOnCapturedContext: false);
            // Code here runs without the original
            // context (in this case, on the thread pool).
        }

        /// <summary>
        /// 不能使用ConfigureAwait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            //button1.Enabled = false;
            try
            {
                // Can't use ConfigureAwait here ...
                await Task.Delay(1000);
            }
            finally
            {
                // Because we need the context here.
                //button1.Enabled = true;
            }
        }


        private async Task HandleClickAsync()
        {
            // Can use ConfigureAwait here.
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            //button1.Enabled = false;
            try
            {
                // Can't use ConfigureAwait here.
                await HandleClickAsync();
            }
            finally
            {
                // We are back on the original context for this method.
                //button1.Enabled = true;
            }
        }
    }
}
