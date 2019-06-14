using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace vgo.frontsite004
{
    public static class CacheProvider
    {
        static readonly MemoryCache m_Cache = new MemoryCache(Guid.NewGuid().ToString("N"));

        /// <summary>
        /// 向缓存中插入缓存项，而不会覆盖任何现有的缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        /// <returns> 如果插入成功，则为 true；如果缓存中已存在具有与 key 相同的键的项，则为 false</returns>
        public static bool Add(string key, object value, TimeSpan? expiration = null)
        {
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                var offset = DateTimeOffset.MinValue;
                if (expiration.HasValue && expiration.Value > TimeSpan.Zero)
                {
                    offset = DateTimeOffset.Now.Add(expiration.Value);
                }
                else
                {
                    offset = DateTimeOffset.MaxValue;
                }

                return m_Cache.Add(key, value, offset);
            }

            return false;
        }

        /// <summary>
        /// 使用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        public static void Set(string key, object value, TimeSpan? expiration = null)
        {
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                var offset = DateTimeOffset.MinValue;
                if (expiration.HasValue && expiration.Value > TimeSpan.Zero)
                {
                    offset = DateTimeOffset.Now.Add(expiration.Value);
                }
                else
                {
                    offset = DateTimeOffset.MaxValue;
                }

                m_Cache.Set(key, value, offset);
            }
        }

        /// <summary>
        /// 获取指定名称的缓存对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet<TSource>(string key, out TSource value)
        {
            var item = m_Cache.Get(key);
            if (item != null && typeof(TSource).IsAssignableFrom(item.GetType()))
            {
                value = (TSource)item;
                return true;
            }
            else
            {
                value = default(TSource);
                return false;
            }
        }

        public static bool Contians(string key)
        {
            return m_Cache.Contains(key);
        }

        /// <summary>
        /// 删除指定名称的缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            var obj = m_Cache.Remove(key);

            return obj != null;
        }
    }
}
