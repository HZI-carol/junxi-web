using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using vgo.partner.frontsite.Models;

namespace vgo.partner.frontsite
{
    /// <summary>
    /// 接口返回结果模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonApiResult<T>
    {
        public int code { get; set; }

        public string msg { get; set; }

        public T data { get; set; }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        internal bool success => code == 100;
    }

    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public IEnumerable<T> list { get; set; }

        public int count { get; set; }
    }

    /// <summary>
    /// 接口提供类
    /// </summary>
    public static class ApiProvider
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        static async Task<T> HandleRequestAsync<T>(HttpMethod method, string uri, object param = null)
        {
            T data = default(T);
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = null;

                    if (method == HttpMethod.Get)
                    {
                        List<string> paramPairs = null;
                        if (param != null)
                        {
                            paramPairs = new List<string>();
                            foreach (var p in param.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                            {
                                paramPairs.Add(p.Name + "=" + p.GetValue(param));
                            }
                            if (uri.IndexOf("?") > -1)
                            {
                                uri += "&" + string.Join("&", paramPairs);
                            }
                            else
                            {
                                uri += "?" + string.Join("&", paramPairs);
                            }
                        }
                        response = await client.GetAsync(WebConfigs.ApiUrl + uri);
                    }
                    else if (method == HttpMethod.Post)
                    {
                        var dict = new Dictionary<string, string>();
                        if (param != null)
                        {
                            foreach (var p in param.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                            {
                                dict.Add(p.Name, p.GetValue(param) + "");
                            }
                        }

                        response = await client.PostAsync(WebConfigs.ApiUrl + uri, new FormUrlEncodedContent(dict));
                    }

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsAsync<JsonApiResult<T>>();
                            if (result.success)
                            {
                                return result.data;
                            }
                            else
                            {
                                throw new Exception(result.msg);
                            }
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
                catch (Exception e)
                {
                    LogProvider.Error.Error(e, $"请求接口【{uri}】出现错误");
                    throw e;
                }
            }

            return data;
        }

        /// <summary>
        /// 获取单页网站配置
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static async Task<SiteConfig> GetSiteConfigAsync(string domain)
        {
            string key = $"siteconfig:{domain}";
            if (!CacheProvider.TryGet<SiteConfig>(key, out SiteConfig config))
            {
                config = await HandleRequestAsync<SiteConfig>(HttpMethod.Get, "api/partnersite/front/siteconfig", new { domain });
                if (config != null)
                {
                    CacheProvider.Set(key, config, TimeSpan.FromMinutes(5));
                }
            }

            return config;
        }

        #region 基础数据

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns></returns>
        public static Task<SysConfig> GetSysConfigAsync()
        {
            return HandleRequestAsync<SysConfig>(HttpMethod.Get, "api/frontsite/configs");
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Tags>> GetTagsListAsync()
        {
            string path = "api/frontsite/tags";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<Tags> list))
            {
                list = await HandleRequestAsync<IEnumerable<Tags>>(HttpMethod.Get, path);
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        #endregion

        #region 全景相关

        /// <summary>
        /// 获取首页精选全景作品列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Pano>> GetIndexListAsync(int count)
        {
            string path = "api/frontsite/indexpanos";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<Pano> list))
            {
                list = await HandleRequestAsync<IEnumerable<Pano>>(HttpMethod.Get, path, new { count });
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取全景分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fk_partner_id"></param>
        /// <param name="user_id"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="tag_id"></param>
        /// <param name="orderBy">默认：sort_DESC</param>
        /// <returns></returns>
        public static Task<PagedList<Pano>> GetPanoPagedListAsync(int page, int pageSize, int fk_partner_id = 0, int user_id = 0, string keyword = "", string tag_id = "", string orderBy = "")
            => HandleRequestAsync<PagedList<Pano>>(HttpMethod.Get, $"api/frontsite/panos/{page}/{pageSize}", new
            {
                fk_partner_id,
                user_id,
                keyword,
                tag_id,
                orderBy
            });

        #endregion

        #region 用户相关

        /// <summary>
        /// 获取指定用户详情
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static Task<User> GetUserAsync(int user_id)
            => HandleRequestAsync<User>(HttpMethod.Get, $"api/frontsite/users/{user_id}");

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="fk_partner_id"></param>
        /// <param name="orderBy">默认：seecount_DESC</param>
        /// <returns></returns>
        public static Task<PagedList<User>> GetUserPagedListAsync(int page, int pageSize, string keyword = "", int fk_partner_id = 0, string orderBy = "")
            => HandleRequestAsync<PagedList<User>>(HttpMethod.Get, $"api/frontsite/users/{page}/{pageSize}", new
            {
                fk_partner_id,
                keyword,
                orderBy
            });

        #endregion

        /// <summary>
        /// 获取数据缓存键名
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        static string GetCacheKey(string shortKey)
        {
            return $"{typeof(ApiProvider).FullName}:{shortKey}";
        }

        #region 资讯相关

        /// <summary>
        /// 获取资讯类型列表
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<NewsType>> GetNewsTypeListAsync()
        {
            string path = "api/frontsite/newstypes";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<NewsType> list))
            {
                list = await HandleRequestAsync<IEnumerable<NewsType>>(HttpMethod.Get, path);
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取资讯详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Task<News> GetNewsAsync(int id)
            => HandleRequestAsync<News>(HttpMethod.Get, $"api/frontsite/news/{id}");

        /// <summary>
        /// 获取新闻分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="type_id"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static Task<PagedList<News>> GetNewsPagedListAsync(int page, int pageSize, int type_id = 0, string orderBy = "")
            => HandleRequestAsync<PagedList<News>>(HttpMethod.Get, $"api/frontsite/news/{page}/{pageSize}", new
            {
                type_id,
                orderBy
            });

        #endregion
    }
}