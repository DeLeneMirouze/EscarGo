using System;
using System.Runtime.Caching;

namespace EscarGoLibrary.Caching
{
    public static class LocalCache
    {
        #region Constructeur
        private static readonly MemoryCache _cache;

        static LocalCache()
        {
            _cache = MemoryCache.Default;
        }
        #endregion

        #region Get
        /// <summary>
        /// Obtient un objet du cache
        /// </summary>
        /// <typeparam name="T">Type de l'objet réclamé</typeparam>
        /// <param name="key">Clef de l'objet pour la catégorie fournie</param>
        /// <param name="loadingFunction">Fonction qui sait comment retrouver un objet</param>
        /// <returns></returns>
        public static T Get<T>(string key, Func<T> loadingFunction)
        {
            return AddOrGetExisting(key, loadingFunction);
        }
        #endregion

        #region AddOrGetExisting (private)
        private static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = new TimeSpan(10, 0, 0);
            var oldValue = _cache.AddOrGetExisting(key, newValue, policy) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                // si valueFactory à renvoyé une exception, celle-ci sera rejouée à chaque lecture ultérieure
                // le mieux est donc de sortir du cache l'objet qui pose problème
                _cache.Remove(key);
                throw;
            }
        } 
        #endregion
    }
}

