using System;

namespace Infrastructure
{
    public class TimeHelper
    {
        #region 获取时间戳

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            UInt32 uiStamp = Convert.ToUInt32(ts.TotalSeconds);
            return uiStamp;
        }
        #endregion
    }
}
