﻿using System;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;
using StackExchange.Redis;
using ScrapyCore.Core.External;
using ScrapyCore.Core.External.Conventor;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ScrapyCore.Core.Caches
{
    public class RedisCache : CommonCache
    {
        private ConnectionMultiplexer connection;
        private IDatabase database;
        private IServer server;

        public RedisCache(ICachingConfigure cachingConfigure)
            : base(cachingConfigure)
        {
            var configureOptions = new ConfigurationOptions()
            {
                Password = cachingConfigure.ConfigureDetail.DefaultValue("password"),
                AbortOnConnectFail = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("abortConnect", StringToBoolConvertor.Instance),
                AllowAdmin = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("allowAdmin", StringToBoolConvertor.Instance),
                ChannelPrefix = cachingConfigure.ConfigureDetail.DefaultValue("channelPrefix"),
                ConnectRetry = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("connectRetry", StringToInt32Conventor.Instance),
                ConnectTimeout = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("connectTimeout", StringToInt32Conventor.Instance),
                ConfigurationChannel = cachingConfigure.ConfigureDetail.DefaultValue("configChannel"),
                DefaultDatabase = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("defaultDatabase", StringToInt32Conventor.Instance),
                KeepAlive = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("keepAlive", StringToInt32Conventor.Instance),
                Proxy = Proxy.None,
                ResolveDns = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("resolveDns", StringToBoolConvertor.Instance),
                Ssl = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("ssl", StringToBoolConvertor.Instance),
            };
            cachingConfigure.ConfigureDetail.DefaultValue("end-points").Split(';').ToList().ForEach(x => configureOptions.EndPoints.Add(x));

            connection = ConnectionMultiplexer.Connect(configureOptions);
            server = connection.GetServer(connection.GetEndPoints()[0]);
            database = connection.GetDatabase();
        }

        public override bool IsKeyExist(string key)
        {
            var isExist = database.KeyExists(new RedisKey[] { key }, CommandFlags.PreferMaster);
            Logger.Debug($"Is Key Exist:{isExist}");
            return isExist > 0;
        }

        public override Task<bool> IsKeyExistAsync(string key)
        {
            Logger.Debug($"Is Key Exist async:" + key);
            return database.KeyExistsAsync(key);
        }

        public override bool Remove(string key)
        {
            Logger.Debug($"Remove key:{key}");
            if (IsKeyExist(key))
            {
                return database.KeyDelete(key);
            }
            return false;
        }

        public async override Task<bool> RemoveAsync(string key)
        {
            if (await IsKeyExistAsync(key))
            {
                return await database.KeyDeleteAsync(key);
            }
            return false;
        }

        public override T Restore<T>(string key)
        {
            T obj = JsonConvert.DeserializeObject<T>(database.StringGet(key));
            Logger.Debug($"Restore key:{key}");
            return obj;
        }

        public override async Task<T> RestoreAsync<T>(string key)
        {
            T obj = JsonConvert.DeserializeObject<T>(
                await database.StringGetAsync(key)
                );
            Logger.Debug($"Restore key:{key}");
            return obj;
        }

        public override void Store<T>(string key, T model, TimeSpan? timeSpan = null)
        {
            Logger.Debug($"Store key:{key}");
            string json = JsonConvert.SerializeObject(model);
            var result = database.StringSet(key, json, expiry: timeSpan);

        }

        public override async Task StoreAsync<T>(string key, T model, TimeSpan? timeSpan = null)
        {
            Logger.Debug($"Store key:{key}");
            string json = JsonConvert.SerializeObject(model);
            await database.StringSetAsync(key, json, expiry: timeSpan);

        }

        public override Task<IEnumerable<string>> SearchKeys(string keyPatten)
        {
            return Task.FromResult(server.Keys(pattern: keyPatten).Select(x => x.ToString()));
        }
    }
}
