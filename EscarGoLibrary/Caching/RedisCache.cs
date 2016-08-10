﻿#region using
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
        public static T Get<T>(string key, Func<T> loadingFunction)
        {
            IDatabase cache = Connection.GetDatabase();
            RedisValue fromCache = cache.StringGet(key);

            if (fromCache.HasValue)
            {
                T retour = Deserialize<T>(fromCache);
                return retour;
            }

            lock(cacheLock)
            {
                fromCache = cache.StringGet(key);
                if (fromCache.HasValue)
                {
                    T retour = Deserialize<T>(fromCache);
                    return retour;
                }

                T obj = loadingFunction();
                Set(key, obj);

                return obj;
            }
        }
        #endregion

        #region Set
        public static void Set<T>(string key, T value)
        {
            IDatabase cache = Connection.GetDatabase();
            TimeSpan timeSpan = new TimeSpan(0, 3, 0);

            byte[] bytes = Serialize(value);
            cache.StringSet(key, bytes);
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