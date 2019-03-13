using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.WeiXin.Models
{

    #region 微信响应信息类
    /// <summary>
    /// 微信响应信息类
    /// </summary>
    public class PassTicketXmlInfo
    {
        //<error><ret>0</ret><message>OK</message><skey>@crypt_314e74f5_5a08c21544fb296d214a5dac330f4b93</skey><wxsid>uI7iuJqWAGO30JQd</wxsid><wxuin>1630516940</wxuin><pass_ticket>spJedq60h%2FSzM0zGFaRkJVyUnqS3ElLhfIxMKnHbPMcIX3pZ0hzy%2Fw0jLSal12WR</pass_ticket><isgrayscale>1</isgrayscale></error>

        public int ret { get; set; }

        public string message { get; set; }

        public string skey { get; set; }

        public string wxsid { get; set; }

        public string wxuin { get; set; }

        public string pass_ticket { get; set; }

        public string isgrayscale { get; set; }
    }
    #endregion
}
