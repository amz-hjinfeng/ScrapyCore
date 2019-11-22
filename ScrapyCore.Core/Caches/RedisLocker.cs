using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Caches
{
    public class RedisLocker : ICacheLocker
    {
        private readonly IDatabase database;
        private readonly TimeSpan expire;
        private readonly string lockValue = Guid.NewGuid().ToString();

        public RedisLocker(string lockKey, IDatabase database, TimeSpan? expire)
        {
            LockKey = lockKey;
            this.database = database;
            this.expire = expire ?? TimeSpan.FromSeconds(5);
        }

        public string LockKey { get; }

        public void Dispose()
        {
            database.LockRelease(LockKey, lockValue);
        }

        public async Task WithLock()
        {
            await database.LockTakeAsync(LockKey, lockValue, expire);
        }
    }
}
