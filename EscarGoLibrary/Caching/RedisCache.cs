#region using
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#endregion


namespace EscarGoLibrary.Caching
{
    public static class RedisCache
    {
        static readonly object cacheLock = new object();

        #region Connection
        private static Lazy<ConnectionMultiplexer> lazyConnection =
            new Lazy<ConnectionMultiplexer>(() =>
  {
      return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisCnx"]);
  });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        #endregion

        #region Get
        public static T Get<T>(string key, Func<T> loadingFunction, TimeSpan sliding)
        {
            IDatabase cache = Connection.GetDatabase();
            RedisValue fromCache = cache.StringGet(key);

            if (fromCache.HasValue)
            {
                T retour = JsonConvert.DeserializeObject<T>(fromCache);
                return retour;
            }

            lock (cacheLock)
            {
                fromCache = cache.StringGet(key);
                if (fromCache.HasValue)
                {
                    T retour = JsonConvert.DeserializeObject<T>(fromCache);
                    return retour;
                }

                T obj = loadingFunction();
                Set(key, obj, sliding);

                return obj;
            }
        }
        #endregion

        #region Set
        public static void Set<T>(string key, T value, TimeSpan sliding)
        {
            IDatabase cache = Connection.GetDatabase();

            string json = JsonConvert.SerializeObject(value);
            cache.StringSet(key, json, sliding);
        }
        #endregion

        #region CreateRaceKey
        public static string CreateRaceKey(int key)
        {
            return string.Format("race-{0}", key);
        }
        #endregion

        #region CreateCompetitorKey
        public static string CreateCompetitorKey(int key)
        {
            return string.Format("competitor-{0}", key);
        }
        #endregion

        #region Serialize (private)
        static byte[] Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, value);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }
        #endregion

        #region Deserialize (private)
        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
        #endregion
    }
}
