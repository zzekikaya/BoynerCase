using Boyner.Core.CacheManagement.Concrete;
using Boyner.Data;
using Boyner.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace Boyner.Service
{
    public class ConfigurationReader : IConfigurationReader
    {
        private string _applicationName;
        private int _refreshTimerIntervalInMs;
        private string _connectionString;
        private string _cacheValue;
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {

            _applicationName = applicationName;
            _connectionString = connectionString;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }

        public T GetValue<T>(string key)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            _cacheValue = GlobalCachingProvider.Instance.GetItem(_applicationName) as string;
            //database'e başlanmazsa Cache'den Config bilgisini dön.
            if (!string.IsNullOrEmpty(_cacheValue))
            {
                return (T)(object)(_cacheValue);
            }
            try
            {
                // config', cache'den sadece db'ye bağlanamama durumlarında okuyacağız.
                //if (string.IsNullOrEmpty(_cacheValue))
                //{
                using (var _context = new ConfigContext(optionsBuilder.Options))
                {

                    var type = typeof(T).Name.ToLower();
                    var config = _context.Config
                        .Where(x => x.ApplicationName == key && x.ApplicationName == _applicationName && x.IsActive == true).FirstOrDefault()
                        .ApplicationName;

                    if (type != config.GetType().Name.ToLower())
                        throw new Exception(
                            "Type is not the same with the configured type.");
                    //cache'e config'i kaydeder.
                    GlobalCachingProvider.Instance.AddItem(_applicationName, config, _refreshTimerIntervalInMs);

                    switch (config.GetType().Name.ToLower())
                    {
                        case "string": return (T)(object)config;
                        case "int": return (T)(object)int.Parse(config);
                        case "bool": return (T)(object)(config == "True");
                        default: return default(T);
                    }
                }
                //}
                //else
                //{
                //    //cache değerini dön.
                //    return (T)(object)(_cacheValue);
                //}
            }
            catch (Exception ex)
            {
                _cacheValue = GlobalCachingProvider.Instance.GetItem(_applicationName) as string;
                //database'e başlanmazsa Cache'den Config bilgisini dön.
                return (T)(object)(_cacheValue);
            }
        }


    }
}
