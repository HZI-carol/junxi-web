using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace zg.frontsite
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


        #region 宅构相关

        /// <summary>
        /// 获取首页Banner
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="type">0、首页Banner;1、首页广告位</param>
        /// <returns></returns>
        public static async Task<PagedList<Banners>> GetBannerListAsync(int page, int pageSize, int type = -1)
        {
            string path = $"api/zaigou/banners/{page}/{pageSize}";
            string key = GetCacheKey(path + "/" + type);
            if (!CacheProvider.TryGet(key, out PagedList<Banners> list))
            {
                list = await HandleRequestAsync<PagedList<Banners>>(HttpMethod.Get, path, new { type_id = type });
                if (list.count > 0)
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        #region 公司相关

        /// <summary>
        /// 获取公司标签列表
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Types>> GetTypeListAsync()
        {
            string path = "api/zaigou/types";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<Types> list))
            {
                list = await HandleRequestAsync<IEnumerable<Types>>(HttpMethod.Get, path);
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取装企公司详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Task<Companys> GetCompanyAsync(int id)
            => HandleRequestAsync<Companys>(HttpMethod.Get, $"api/zaigou/front/companys/{id}");

        /// <summary>
        /// 获取装企公司列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="province"></param>
        /// <param name="isrzcert"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static Task<PagedList<Companys>> GetCompanyPagedListAsync(int page, int pageSize, string keyword = "", string province = "", string orderBy = "")
           => HandleRequestAsync<PagedList<Companys>>(HttpMethod.Get, $"api/zaigou/front/frontcompanys/{page}/{pageSize}", new
           {
               keyword,
               province,
               is_contains_project = true,
               isrzcert = false,
               orderBy
           });

        #endregion

        #region 项目相关

        /// <summary>
        /// 获取项目装修风格标签列表
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<ProjectDecorationStyle>> GetDecorationStyleListAsync()
        {
            string path = "api/zaigou/decorationstyles";
            string key = GetCacheKey(path);
            if (!CacheProvider.TryGet(key, out IEnumerable<ProjectDecorationStyle> list))
            {
                list = await HandleRequestAsync<IEnumerable<ProjectDecorationStyle>>(HttpMethod.Get, path);
                if (list.Any())
                {
                    CacheProvider.Set(key, list, TimeSpan.FromMinutes(5));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取公司信息及项目信息
        /// </summary>
        /// <param name="pano_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static Task<Project> GetCompanyAndProjectAsync(int fk_pano_id, int user_id)
            => HandleRequestAsync<Project>(HttpMethod.Get, $"api/zaigou/front/panos/{fk_pano_id}", new { user_id });

        /// <summary>
        /// 获取当前作品相关或相似案例
        /// </summary>
        /// <param name="pano_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static Task<PagedList<Project>> GetProjectPagedListAsync(int page, int pageSize, string tag_id, string orderBy = "")
            => HandleRequestAsync<PagedList<Project>>(HttpMethod.Get, $"api/zaigou/front/panos/{page}/{pageSize}", new { tag_id, orderBy });

        /// <summary>
        /// 获取前端精选项目列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword">关键字搜索 项目名称、姓名、手机号码</param>
        /// <param name="orderBy">默认：created_DESC</param>
        /// <returns></returns>
        public static Task<PagedList<Project>> GetProjectPagedListAsync(int page, int pageSize, string keyword = "", int isrecommond = 1, string orderBy = "")
            => HandleRequestAsync<PagedList<Project>>(HttpMethod.Get, $"api/zaigou/front/projects/{page}/{pageSize}", new
            {
                keyword,
                isrecommond,
                orderBy
            });


        #endregion

        #endregion
    }
}