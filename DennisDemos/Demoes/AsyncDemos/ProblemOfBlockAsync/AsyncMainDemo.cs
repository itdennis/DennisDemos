﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.AsyncDemos
{
    class AsyncMainDemo
    {
        //static void Main()
        //{
        //    MainAsync().Wait();
        //}
        static async Task MainAsync()
        {
            try
            {
                // Asynchronous implementation.
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                // Handle exceptions.
            }
        }
    }
}
