using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redis4test.Cache
{
    public class RedisCache : IValueCache
    {
        private ILogger logger;

        private IConnectionMultiplexer connection;

        public RedisCache(ILogger<RedisCache> logger, IConnectionMultiplexer connectionMultiplexer)
        {
            this.logger = logger;
            this.connection = connectionMultiplexer;

            logger.LogInformation($"Using redis location cache - {connectionMultiplexer.Configuration}");
        }

        public RedisCache(ILogger<RedisCache> logger,
            ConnectionMultiplexer connectionMultiplexer) : this(logger, (IConnectionMultiplexer)connectionMultiplexer)
        {

        }
        public IList<string> GetValuesByKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
