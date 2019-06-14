using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using vgo.frontsite004.Models;

namespace vgo.frontsite004
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

        #region 注册登录相关

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        static long GetTimeStamp()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(DateTime.Now - startTime).TotalSeconds;
        }

        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <param name="appSecret"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        static string SignatureString(string appSecret, string timestamp, string nonce)
        {
            string[] ArrTmp = { appSecret, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = HashAlgorithmProvider.ComputeHash("SHA1", tmpStr, true);
            return tmpStr.ToLower();
        }

        /// <summary>
        /// 获取写入Cookie的信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static HttpCookie GetWriteCookie(UserCookie user)
        {
            var cookie = new HttpCookie(WebConfigs.CookieName);
            cookie.Domain = WebConfigs.CookieDomain;
            cookie.Values["access_token"] = HttpUtility.UrlEncode(user.access_token);
            cookie.Values["expires_in"] = HttpUtility.UrlEncode(user.expires_in + "");
            cookie.Values["nickname"] = HttpUtility.UrlEncode(user.nickname, Encoding.GetEncoding("UTF-8"));
            cookie.Values["avatar"] = HttpUtility.UrlEncode(user.avatar, Encoding.GetEncoding("UTF-8"));
            cookie.Values["id"] = HttpUtility.UrlEncode(user.id + "");
            cookie.Values["isads"] = HttpUtility.UrlEncode(user.isads);
            cookie.Values["isadz"] = HttpUtility.UrlEncode(user.isadz);
            cookie.Values["packageid"] = HttpUtility.UrlEncode(user.packageid);
            cookie.Values["overtdt"] = HttpUtility.UrlEncode(user.overtdt);
            cookie.Values["packagename"] = HttpUtility.UrlEncode(user.packagename);
            cookie.Values["panonum"] = user.panonum;
            cookie.Values["integral"] = user.integral;
            cookie.Expires = DateTime.Now.AddHours(2);

            return cookie;
        }

        /// <summary>
        /// 获取cookie信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static UserCookie GetHttpCookie(HttpCookie cookie)
        {
            UserCookie user = null;
            if (cookie != null)
            {
                user = new UserCookie();
                user.access_token = HttpUtility.UrlDecode(cookie["access_token"]);
                if (int.TryParse(cookie["expires_in"], out int expires_in))
                {
                    user.expires_in = expires_in;
                }
                user.nickname = HttpUtility.UrlDecode(cookie["nickname"]);
                user.avatar = HttpUtility.UrlDecode(cookie["avatar"]);
                if (int.TryParse(cookie["id"], out int id))
                {
                    user.id = id;
                }
                user.packageid = cookie["packageid"];
                user.overtdt = cookie["overtdt"];
                user.panonum = cookie["panonum"];
            }

            return user;
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="response"></param>
        public static void ClearCookie(HttpResponseBase response)
        {
            response.AppendCookie(new HttpCookie(WebConfigs.CookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            //设置指定域名cookie设置过期
            response.AppendCookie(new HttpCookie(WebConfigs.CookieName)
            {
                Domain = WebConfigs.CookieDomain,
                Expires = DateTime.Now.AddDays(-1)
            });
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<JsonApiResult<UserCookie>> LoginAsync(string username, string password)
        {
            var result = new JsonApiResult<UserCookie>();

            try
            {
                string timestamp = GetTimeStamp() + "";
                string nonce = Guid.NewGuid().ToString("N");
                string signature = SignatureString(WebConfigs.SharedKey, timestamp, nonce);

                using (var client = new HttpClient())
                {
                    var dict = new Dictionary<string, string>();
                    dict.Add("username", username);
                    dict.Add("password", password);
                    dict.Add("signature", signature);
                    dict.Add("timestamp", timestamp);
                    dict.Add("nonce", nonce);

                    var res = await client.PostAsync(WebConfigs.ApiUrl + "api/userlogin/Login", new FormUrlEncodedContent(dict));
                    if (res.IsSuccessStatusCode)
                    {
                        result = await res.Content.ReadAsAsync<JsonApiResult<UserCookie>>();
                    }
                    else
                    {
                        res.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception e)
            {
                result.code = 1;
                if (e is ArgumentException)
                {
                    result.msg = e.Message;
                }
                else
                {
                    result.code = -99;
                    result.msg = "系统出错，请稍后再试～";
                }
            }

            return result;
        }

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
        /// <param name="user_id"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="tag_id"></param>
        /// <param name="orderBy">默认：sort_DESC</param>
        /// <returns></returns>
        public static Task<PagedList<Pano>> GetPanoPagedListAsync(int page, int pageSize, int user_id = 0, string keyword = "", string tag_id = "", string orderBy = "")
            => HandleRequestAsync<PagedList<Pano>>(HttpMethod.Get, $"api/frontsite/panos/{page}/{pageSize}", new
            {
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
        /// <param name="orderBy">默认：seecount_DESC</param>
        /// <returns></returns>
        public static Task<PagedList<User>> GetUserPagedListAsync(int page, int pageSize, string keyword = "", string orderBy = "")
            => HandleRequestAsync<PagedList<User>>(HttpMethod.Get, $"api/frontsite/users/{page}/{pageSize}", new
            {
                keyword,
                orderBy
            });

        #endregion

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

        #region 作品市场相关

        /// <summary>
        /// 获取作品市场分类列表
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<MarketPanoType>> GetPanoMarketTypeListAsync()
        {
            string path = "api/pmarket/types";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<MarketPanoType> list))
            {
                list = await HandleRequestAsync<IEnumerable<MarketPanoType>>(HttpMethod.Get, path);
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取作品详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<MarketPano> GetMarketPanoAsync(int id, string token)
            => HandleRequestAsync<MarketPano>(HttpMethod.Get, $"api/frontsite/pmarket/panos/{id}?token={token}");

        /// <summary>
        /// 获取作品市场作品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<PagedList<MarketPano>> GetMarketPanoListAsync(int page, int pageSize, int type, string token)
            => HandleRequestAsync<PagedList<MarketPano>>(HttpMethod.Get, $"api/frontsite/pmarket/panos/{page}/{pageSize}?token={token}", new
            {
                type
            });

        #endregion
    }
}