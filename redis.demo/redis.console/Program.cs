using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace redis.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Redis !");

            ICacheOps cache = new RedisOps();


            var redisConfig = "127.0.0.1";
            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { "127.0.0.1", 6379 },
                    { "127.0.0.1", 6380 }
                },
                CommandMap = CommandMap.Create(new HashSet<string>
                { // EXCLUDE a few commands
                    "INFO", "CONFIG", "CLUSTER",
                    "PING", "ECHO", "CLIENT"
                }, available: false),
                KeepAlive = 180,
                DefaultVersion = new Version(2, 8, 8),
                Password = "changeme"
            };


            ConnectionMultiplexer redis = ConnectionMultiplexer.ConnectAsync(redisConfig).Result;

            IDatabase db = redis.GetDatabase();


            string value = "abcdefg";
            db.StringSet("mykey", value);
            string value2 = db.StringGet("mykey");
            Console.WriteLine(value2); // writes: "abcdefg"

            ISubscriber sub = redis.GetSubscriber();

            sub.Subscribe("messages", (channel, message) =>
            {
                Console.WriteLine((string)message);
            });

            sub.Publish("messages", "hello");

        }
    }
}
