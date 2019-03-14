using Infrastructure.Data;
using Infrastructure.WeiXin.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Infrastructure.WeiXin
{
    public class WeiXinHelper
    {
        #region 登录地址
        //获取会话ID的URL
        public const string _session_id_url = "https://login.wx.qq.com/jslogin";

        //微信登录二维码地址
        public static string WxQrCodeUrl(string Uid) => String.Format("{0}/{1}", "https://login.weixin.qq.com/qrcode", Uid);

        //判断二维码扫描情况   200表示扫描登录  201表示已扫描未登录  其它表示未扫描
        public const string _login_check_url = "https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login";

        //获取Uid使用重定向Uri
        public const string _redirect_uri = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage";
        #endregion

        #region 操作地址

        //获取微信好友
        public static string WxFriendsUrl(string hosts) => String.Format("https://{0}{1}", hosts, "/cgi-bin/mmwebwx-bin/webwxgetcontact");

        //发送微信消息
        public static string WxSendMsgUrl(string hosts) => String.Format("https://{0}{1}", hosts, "/cgi-bin/mmwebwx-bin/webwxsendmsg");

        //获取微信信息
        public static string InitUrl(string hosts,string passTicket) =>
            String.Format("https://{0}{1}?r={2}&pass_ticket={3}", hosts, "/cgi-bin/mmwebwx-bin/webwxinit", TimeHelper.GetTimeStamp(), passTicket);

        //设置备注
        public static string WxSetRemarkName(string hosts, string passTicket) =>
             String.Format("https://{0}{1}?lang=zh_CN&pass_ticket={2}", hosts, "/cgi-bin/mmwebwx-bin/webwxoplog", passTicket);
        
        //开启状态通知
        public static string WxStatusNotify(string hosts) => 
            String.Format("https://{0}{1}", hosts, "/cgi-bin/mmwebwx-bin/webwxstatusnotify");

        //获取微信状态
        public static string WxStatus(string hosts) =>
            String.Format("https://webpush.{0}{1}", hosts, "/cgi-bin/mmwebwx-bin/synccheck");

        //获取微信状态
        public static string WxNews(string hosts, string sid, string skey) =>
            String.Format("https://{0}{1}?sid={2}&skey={3}", hosts, "/cgi-bin/mmwebwx-bin/webwxsync", sid, skey);
        #endregion

        #region 获取Uid

        /// <summary>
        /// 获取Uid
        /// </summary>
        /// <returns></returns>
        public static string GetWxUid()
        {
            var param = new
            {
                appid = "wx782c26e4c19acffb",
                redirect_uri = _redirect_uri,
                fun = "new",
                lang = "zh_CN",
                _ = TimeHelper.GetTimeStamp()
            };
            var request = new HttpHelper(param).HttpGet(_session_id_url);
            Regex reg = new Regex("window.QRLogin.code = (\\d+); window.QRLogin.uuid = \"(.+)\"");
            Match match = reg.Match(request);
            var uid = match.Groups[2].Value;
            return uid;
        }
        #endregion

        #region 获取登录状态

        /// <summary>
        /// 获取登录状态
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public static int GetLoginStatus(string uuid,string tip,ref string url)
        {
            //创建参数
            var param = new
            {
                loginicon = "true",
                uuid = uuid,
                tip = tip,//未扫描:1,已扫描:0
                _ = TimeHelper.GetTimeStamp()
            };
            //获取信息
            var returnStr = new HttpHelper(param).HttpGet(_login_check_url);

            Regex reg = new Regex("window.code=(\\d+);");
            Match match = reg.Match(returnStr);

            if (match.Success)
            {
                var code = Convert.ToInt32(match.Groups[1].Value);//登录状态Code
                if (code == 201)
                {
                    //已扫描
                    url = returnStr.Split(new string[] { "\'" }, StringSplitOptions.None)[1];
                }
                else if (code == 200)
                {
                    //已登录
                    url = returnStr.Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                    url = String.Format("{0}{1}", url, "&fun=new&version=v2&lang=zh_CN");
                }
                return code;
            }
            //未登录成功
            return -1;
        }
        #endregion
        
        #region 获取用户相关Keys
        /// <summary>
        /// 获取用户相关Keys
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static PassTicketXmlInfo GetUserKeys(string url,ref string cookie)
        {
            var httpreuest = new HttpHelper();
            var responseXml = httpreuest.HttpGet(url);

            if (!String.IsNullOrEmpty(responseXml))
            {
                //返回Cookie
                cookie = httpreuest.ReturnCookie;
                //解析返回值
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(responseXml);
                var ret = Convert.ToInt32(xdoc.SelectSingleNode("//ret").InnerText);
                var message = xdoc.SelectSingleNode("//message").InnerText;
                if (ret != 0)
                {
                    ///限制登陆
                    throw new CustomerException(message,500);
                }
                PassTicketXmlInfo Keys = new PassTicketXmlInfo
                {
                    ret = ret,
                    message = message,
                    skey = xdoc.SelectSingleNode("//skey").InnerText,
                    wxsid = xdoc.SelectSingleNode("//wxsid").InnerText,
                    wxuin = xdoc.SelectSingleNode("//wxuin").InnerText,
                    pass_ticket = xdoc.SelectSingleNode("//pass_ticket").InnerText,
                    isgrayscale = xdoc.SelectSingleNode("//isgrayscale").InnerText
                };//返回对象
                return Keys;
            }
            return null;
        }
        #endregion
        
        #region 转换微信状态Cookie
        public static CookieCollection GetWxCookie(string cookiestr)
        {
            var setCookie = cookiestr.Split("Secure,");
            CookieCollection cookieCollection = new CookieCollection();
            foreach (var itemCookie in setCookie)
            {
                try
                {
                    var itemCookieList = itemCookie.Trim().Split(";");
                    cookieCollection.Add(new Cookie
                    {
                        Name = itemCookieList[0].Split("=")[0],
                        Value = itemCookieList[0].Split("=")[1],
                        Domain = itemCookieList[1].Split("=")[1],
                        Path = itemCookieList[2].Split("=")[1],
                        Expires = Convert.ToDateTime(itemCookieList[3].Split("=")[1]),
                    });
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return cookieCollection;
        }
        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="wxHosts"></param>
        /// <param name="passTicket"></param>
        /// <param name="wxSid"></param>
        /// <param name="skey"></param>
        /// <param name="wxUin"></param>
        /// <returns></returns>
        public static WxUserInitInfo GetWxUserInfo(string wxHosts,PassTicketXmlInfo xml)
        {
            var param = new
            {
                BaseRequest = new
                {
                    DeviceID = GetWxDeviceId(),
                    Sid = xml.wxsid,
                    Skey = xml.skey,
                    Uin = xml.wxuin
                }
            };
            var userJson = new HttpHelper(param).HttpPost(InitUrl(wxHosts,xml.pass_ticket));
            var userModel = JsonConvert.DeserializeObject<WxUserInitInfo>(userJson);
            return userModel;
        }
        #endregion

        #region 获取微信好友

        /// <summary>
        /// 获取微信好友
        /// </summary>
        /// <param name="Skey"></param>
        /// <returns></returns>
        public static WxContact GetWxFriends(string wxHosts,string Skey, string wxCookie)
        {
            var prarm = new
            {
                seq = "0",
                lang = "zh_CN",
                skey = Skey
            };

            var friendsJson = new HttpHelper(prarm, GetWxCookie(wxCookie)).HttpGet(WxFriendsUrl(wxHosts));
            var friends = JsonConvert.DeserializeObject<WxContact>(friendsJson);
            return friends;
        }
        #endregion

        #region 开启微信状态通知

        /// <summary>
        /// 开启微信状态通知
        /// </summary>
        public static void StartWxStatusNotify(string wxHosts, string wxCookie, PassTicketXmlInfo keys,string userName)
        {
            var param = new
            {
                BaseRequest = new
                {
                    DeviceID = GetWxDeviceId(),
                    Sid = keys.wxsid,
                    Skey = keys.skey,
                    Uin = keys.wxuin
                },
                Code=3,
                FromUserName=userName,
                ToUserName=userName,
                ClientMsgId=TimeHelper.GetTimeStamp()
            };
            var result=new HttpHelper(param, GetWxCookie(wxCookie)).HttpPost(WxStatusNotify(wxHosts));
            //TODO 检测开启状态
            //if()
        }
        #endregion

        #region 检测微信状态
        public static bool GetWxStatus(string wxHosts, string wxCookie, PassTicketXmlInfo keys, SyncKey syncKey)
        {
            var param = new
            {
                r = TimeHelper.GetTimeStamp(),
                skey = keys.skey,
                sid = keys.wxsid,
                uin = keys.wxuin,
                deviceid = GetWxDeviceId(),
                synckey = String.Join("|", syncKey.List.Select(m => String.Format("{0}_{1}", m.Key, m.Val)))
            };
            var result = new HttpHelper(param, GetWxCookie(wxCookie)).HttpGet(WxStatus(wxHosts));
            var statusJson = result.Replace("window.synccheck=", "");
            var status = JsonConvert.DeserializeObject<WxStatus>(statusJson);
            return status.RetCode.Equals("0");
        }
        #endregion

        #region 获取新消息,延长Cookie时间
        /// <summary>
        /// 获取新消息,延长Cookie时间
        /// </summary>
        /// <param name="wxHosts"></param>
        /// <param name="wxCookie"></param>
        /// <param name="keys"></param>
        /// <param name="syncKey"></param>
        public static string GetNews(string wxHosts, string wxCookie, PassTicketXmlInfo keys, SyncKey syncKey)
        {
            var param = new
            {
                BaseRequest = new
                {
                    skey = keys.skey,
                    sid = keys.wxsid,
                    uin = keys.wxuin,
                    deviceid = GetWxDeviceId(),
                },
                SyncKey= syncKey
            };
            var httpHelper = new HttpHelper(param, GetWxCookie(wxCookie));
            var result = httpHelper.HttpPost(WxNews(wxHosts, keys.wxsid, keys.skey));
            var setcookie = httpHelper.ReturnCookie;
            return result;
        }
        #endregion

        #region 发送微信消息

        /// <summary>
        /// SendMsg
        /// </summary>
        /// <param name="wxHosts">微信域名</param>
        /// <param name="msg">发送信息 TODO 处理信息类型</param>
        /// <param name="toUserName">接收人</param>
        /// <param name="userName">发送人</param>
        /// <param name="wxUin">Uin</param>
        /// <param name="wxSid">Sid</param>
        /// <param name="skey">Skey</param>
        /// <returns></returns>
        public static bool SendMsg(string wxHosts,string msg, string toUserName,string userName, PassTicketXmlInfo keys, string wxCookie)
        {
            var param = new
            {
                BaseRequest = new
                {
                    Uin = keys.wxuin,
                    Sid = keys.wxsid,
                    Skey = keys.skey,
                    DeviceID = GetWxDeviceId(),
                },
                Msg = new
                {
                    Type = 1,
                    Content = msg,
                    FromUserName = userName,
                    ToUserName = toUserName,
                    LocalID = TimeHelper.GetTimeStamp(),
                    ClientMsgId = TimeHelper.GetTimeStamp()
                }
            };
            var responseJson = new HttpHelper(param, GetWxCookie(wxCookie)).HttpPost(WxSendMsgUrl(wxHosts));
            var responseModel = JsonConvert.DeserializeObject<dynamic>(responseJson);
            return responseModel.BaseResponse.Ret == 0;
        }
        #endregion

        #region 获取DeviceID

        /// <summary>
        /// 获得DeviceId
        /// </summary>
        /// <returns></returns>
        public static string GetWxDeviceId()
        {
            Random r = new Random();
            return "e" + 179213476367295 + r.Next(9999999);
        }
        #endregion

        #region 设置备注
        public static bool SetRemarkName(string wxHosts, string wxCookie,PassTicketXmlInfo keys, string userName, string remarkName)
        {
            var param = new
            {
                BaseRequest = new
                {
                    Uin = keys.wxuin,
                    Sid = keys.wxsid,
                    Skey = keys.skey,
                    DeviceID = GetWxDeviceId(),
                },
                CmdId=2,
                RemarkName = remarkName,
                UserName= userName
            };
            var responseJson = new HttpHelper(param, GetWxCookie(wxCookie)).HttpPost(WxSetRemarkName(wxHosts,keys.pass_ticket));
            var responseModel = JsonConvert.DeserializeObject<dynamic>(responseJson);
            return responseModel.BaseResponse.Ret == 0;
        }
        #endregion
    }
}
