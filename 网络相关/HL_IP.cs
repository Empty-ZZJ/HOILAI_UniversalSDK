using HL.String;
using System.Text.RegularExpressions;

namespace HL.Network
{
    public static class Ip
    {

        /// <summary>
        /// 检测是否是代理格式 177.36.42.92:8080 成功返回真
        /// </summary>
        /// <param name="ip">代理ip</param>
        /// <returns></returns>
        public static bool IsValidProxy (string ip)
        {
            if (Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:[0-9]{1,5}"))
            {

                return true;

            }
            else
                return false;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsValidIP (string ip)
        {
            return Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
        }


        /// <summary>
        ///     获取本地外网 IP
        /// </summary>
        /// <returns></returns>
        public static string GetExternalIp ()
        {
            var mc = Regex.Match(
                new System.Net.Http.HttpClient().GetStringAsync("http://www.net.cn/static/customercare/yourip.asp").Result,
                @"您的本地上网IP是：<h2>(\d+\.\d+\.\d+\.\d+)</h2>");
            if (mc.Success && mc.Groups.Count > 1)
            {
                return mc.Groups[1].Value;
            }

            throw new Exception("获取IP失败");
        }





        /// <summary>
        /// 校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchInetAddress (string s)
        {
            MatchInetAddress(s, out bool success);
            return success;
        }
        // <summary>
        /// 校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static Match? MatchInetAddress (string s, out bool isMatch)
        {
            Match match;
            if (s.Contains(":"))
            {
                //IPv6
                match = Regex.Match(s, @"^([\da-fA-F]{0,4}:){1,7}[\da-fA-F]{1,4}$");
                isMatch = match.Success;
            }
            else
            {
                //IPv4
                match = Regex.Match(s, @"^(\d+)\.(\d+)\.(\d+)\.(\d+)$");
                isMatch = match.Success;
                foreach (Group m in match.Groups)
                {
                    if (m.Value.ToInt32() is < 0 or > 255)
                    {
                        isMatch = false;
                        break;
                    }
                }
            }

            return isMatch ? match : null;
        }

    }
}
