using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Infrastructure
{
    public class HttpHelper
    {
        #region 构造函数

        public HttpHelper() { }
        public HttpHelper(object param) => Param = param;
        public HttpHelper(object param, CookieCollection cookie): this(param) => SetCookie = cookie;
        #endregion
        
        #region 参数
        /// <summary>
        /// 添加Cookie模拟
        /// </summary>
        public CookieCollection SetCookie { get; set; }
        /// <summary>
        /// 返回Cookie
        /// </summary>
        public string ReturnCookie { get; private set; }
        /// <summary>
        /// 参数
        /// </summary>
        public object Param { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public WebProxy Agent { get; set; }
        #endregion

        #region HttpPost
        public string HttpPost(string url)
        {
            string result = string.Empty;
            try
            {
                var paramData = JsonConvert.SerializeObject(Param);
                //设置Request
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/json;charset=UTF-8;";
                wbRequest.ContentLength = Encoding.UTF8.GetByteCount(paramData);

                //设置参数
                using (Stream requestStream = wbRequest.GetRequestStream())
                {
                    using (StreamWriter swrite = new StreamWriter(requestStream))
                    {
                        swrite.Write(paramData);
                    }
                }
                //设置代理
                if (Agent != null)
                {
                    wbRequest.Proxy = Agent;
                }

                //添加模拟Cookie
                if (SetCookie != null)
                {
                    if (wbRequest.CookieContainer == null) wbRequest.CookieContainer = new CookieContainer();
                    wbRequest.CookieContainer.Add(SetCookie as CookieCollection);
                }
                //提交
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sread = new StreamReader(responseStream))
                    {
                        //设置返回Cookie
                        ReturnCookie=wbResponse.Headers.Get("Set-Cookie");
                        //读取返回内容
                        result = sread.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
        #endregion

        #region HttpGet

        /// <summary>
        /// 模拟Http请求
        /// </summary>
        /// <param name="httpUrl">请求地址 http://www.baidu.com/</param>
        /// <param name="body">参数:id=123@name=admin </param>
        /// <returns></returns>
        public string HttpGet(string url)
        {
            var result = String.Empty;
            try
            {
                //解析参数
                var prarmStr = String.Empty;
                if (Param != null)
                {
                    var prarmType = Param.GetType();
                    var prarmProperties = prarmType.GetProperties();
                    List<string> prarmSb = new List<string>();
                    foreach (var itemProp in prarmProperties)
                    {
                        var itemPrarm = String.Format("{0}={1}", itemProp.Name, itemProp.GetValue(Param));
                        prarmSb.Add(itemPrarm);
                    }
                    prarmStr = String.Join('&', prarmSb);
                }
                //设置Request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (prarmStr == "" ? "" : "?") + prarmStr);
                request.Method = "GET";

                //添加模拟Cookie
                if (SetCookie != null)
                {
                    if (request.CookieContainer == null) request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(SetCookie);
                }
                //设置代理
                if (Agent != null)
                {
                    request.Proxy = Agent;
                }
                //提交
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //读取返回内容
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        //设置返回Cookie
                        ReturnCookie = response.Headers.Get("Set-Cookie");
                        return sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
