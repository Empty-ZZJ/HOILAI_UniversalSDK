using System.Text;
using System.Text.RegularExpressions;

namespace HL.String
{
    public static class StringByte
    {
        /// <summary>
        /// 取中间文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <param name="ignoreCase">忽略大小写 则为true 默认区分大小写</param>
        /// <returns></returns>
        public static string Between (this string str, string leftStr, string rightStr, bool ignoreCase = false)
        {
            StringComparison comparison = StringComparison.CurrentCulture;

            if (ignoreCase)
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }


            int index = str.IndexOf(leftStr, comparison);
            if (index == -1) return "";


            int i = index + leftStr.Length;

            int last = str.IndexOf(rightStr, i, comparison);

            if (last == -1) return "";

            return str.Substring(i, last - i);

        }

        /// <summary>
        /// 取左边文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="left"></param>
        /// <param name="ignoreCase">忽略大小写 则为true 默认区分大小写</param>
        /// <returns></returns>
        public static string GetLeft (this string str, string left, bool ignoreCase = false)
        {
            StringComparison comparison = StringComparison.CurrentCulture;

            if (ignoreCase)
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            int index = str.IndexOf(left, comparison);
            if (index == -1) return "";

            return str.Substring(0, index);
        }

        /// <summary>
        /// 取文本右边
        /// </summary>
        /// <param name="str"></param>
        /// <param name="right"></param>
        /// <param name="ignoreCase">忽略大小写 则为true 默认区分大小写</param>
        /// <returns></returns>
        public static string GetRight (this string str, string right, bool ignoreCase = false)
        {
            StringComparison comparison = StringComparison.CurrentCulture;

            if (ignoreCase)
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            int index = str.LastIndexOf(right, comparison);
            if (index == -1) return "";

            int index_start = index + right.Length;

            int end_len = str.Length - index_start;
            string temp = str.Substring(index_start, end_len);
            return temp;


        }

        /// <summary>
        /// 匹配文本 
        /// </summary>
        /// <param name="regStr">正则</param>
        /// <param name="text">源方本</param>
        /// <param name="lable">返回的标签</param>
        /// <returns></returns>
        public static string RegMatch (string regStr, string text, string lable)
        {
            Regex r = new Regex(regStr);
            Match m = r.Match(text);
            if (!m.Success) return "";
            return m.Groups[lable].ToString();
        }

        #region 随机
        /// <summary>
        /// 返回两个数字之间的随机数
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        public static int GetRandNumber (int min, int max)
        {
            Random random = new Random(GetRandomGuid());

            if (min > max)//交换变量 防止用户写反了
            {
                int temp = min;
                min = max;
                max = temp;
            }
            max++;//max 不会大于或等于 这个值 我们这里 加一下

            return random.Next(min, max);


        }

        /// <summary>
        /// 返回0.8991527960220353 16-18位随机小数
        /// </summary>
        /// <returns></returns>
        public static string GetRandJs ()
        {
            Random rand = new Random(GetRandomGuid());
            return rand.NextDouble().ToString();
        }


        /// <summary>
        /// 随机生成英文字母 首字母不是数字
        /// </summary>
        ///<param name="count">长度</param>
        ///<param name="char_type">0=小写 1=大写 2=大小写混合</param>
        /// <returns></returns>
        public static string GetRandstr (int count, int char_type = 0)
        {
            char[] constant =
             {
                '0','1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
              };

            StringBuilder sb = new StringBuilder(62);
            Random rd = new Random(GetRandomGuid());
            for (int i = 0; i < count; i++)
            {
                char ch = ' ';

                if (i == 0)
                {
                    ch = constant[rd.Next(9, constant.Length)];
                }
                else
                {
                    ch = constant[rd.Next(constant.Length)];

                }

                if (char_type == 2)
                {
                    int num = GetRandNumber(1, 2);

                    if (num == 2)
                    {
                        sb.Append(ch.ToString().ToUpper());
                    }
                    else
                    {
                        sb.Append(ch);
                    }



                }
                else
                {
                    sb.Append(ch);
                }



            }

            if (char_type == 1)
            {
                return sb.ToString().ToUpper();
            }


            return sb.ToString();
        }


        /// <summary>
        /// 生成随机中文名字
        /// </summary>
        /// <returns></returns>
        public static string GetChineseName ()
        {
            /// <summary>
            /// 生成中文名字的姓
            /// </summary>
            string[] _crabofirstName = new string[] {  "白", "毕", "卞", "蔡", "曹", "岑", "常", "车", "陈", "成" , "程", "池", "邓", "丁", "范", "方", "樊", "费", "冯", "符"
        , "傅", "甘", "高", "葛", "龚", "古", "关", "郭", "韩", "何" , "贺", "洪", "侯", "胡", "华", "黄", "霍", "姬", "简", "江"
        , "姜", "蒋", "金", "康", "柯", "孔", "赖", "郎", "乐", "雷" , "黎", "李", "连", "廉", "梁", "廖", "林", "凌", "刘", "柳"
        , "龙", "卢", "鲁", "陆", "路", "吕", "罗", "骆", "马", "梅" , "孟", "莫", "母", "穆", "倪", "宁", "欧", "区", "潘", "彭"
        , "蒲", "皮", "齐", "戚", "钱", "强", "秦", "丘", "邱", "饶" , "任", "沈", "盛", "施", "石", "时", "史", "司徒", "苏", "孙"
        , "谭", "汤", "唐", "陶", "田", "童", "涂", "王", "危", "韦" , "卫", "魏", "温", "文", "翁", "巫", "邬", "吴", "伍", "武"
        , "席", "夏", "萧", "谢", "辛", "邢", "徐", "许", "薛", "严" , "颜", "杨", "叶", "易", "殷", "尤", "于", "余", "俞", "虞"
        , "元", "袁", "岳", "云", "曾", "詹", "张", "章", "赵", "郑" , "钟", "周", "邹", "朱", "褚", "庄", "卓"
                                        };

            /// <summary>
            /// 用于生成中文名字的后面两位
            /// </summary>
            string _lastName = "匕刁丐歹戈夭仑讥冗邓艾夯凸卢叭叽皿凹囚矢乍尔冯玄邦迂邢芋芍吏夷吁吕吆" +

                               "屹廷迄臼仲伦伊肋旭匈凫妆亥汛讳讶讹讼诀弛阱驮驯纫玖玛韧抠扼汞扳抡坎坞抑拟抒芙芜苇芥芯芭杖杉巫" +

                               "杈甫匣轩卤肖吱吠呕呐吟呛吻吭邑囤吮岖牡佑佃伺囱肛肘甸狈鸠彤灸刨庇吝庐闰兑灼沐沛汰沥沦汹沧沪忱" +

                               "诅诈罕屁坠妓姊妒纬玫卦坷坯拓坪坤拄拧拂拙拇拗茉昔苛苫苟苞茁苔枉枢枚枫杭郁矾奈奄殴歧卓昙哎咕呵" +

                               "咙呻咒咆咖帕账贬贮氛秉岳侠侥侣侈卑刽刹肴觅忿瓮肮肪狞庞疟疙疚卒氓炬沽沮泣泞泌沼怔怯宠宛衩祈诡" +

                               "帚屉弧弥陋陌函姆虱叁绅驹绊绎契贰玷玲珊拭拷拱挟垢垛拯荆茸茬荚茵茴荞荠荤荧荔栈柑栅柠枷勃柬砂泵" +

                               "砚鸥轴韭虐昧盹咧昵昭盅勋哆咪哟幽钙钝钠钦钧钮毡氢秕俏俄俐侯徊衍胚胧胎狰饵峦奕咨飒闺闽籽娄烁炫" +

                               "洼柒涎洛恃恍恬恤宦诫诬祠诲屏屎逊陨姚娜蚤骇耘耙秦匿埂捂捍袁捌挫挚捣捅埃耿聂荸莽莱莉莹莺梆栖桦" +

                               "栓桅桩贾酌砸砰砾殉逞哮唠哺剔蚌蚜畔蚣蚪蚓哩圃鸯唁哼唆峭唧峻赂赃钾铆氨秫笆俺赁倔殷耸舀豺豹颁胯" +

                               "胰脐脓逛卿鸵鸳馁凌凄衷郭斋疹紊瓷羔烙浦涡涣涤涧涕涩悍悯窍诺诽袒谆祟恕娩骏琐麸琉琅措捺捶赦埠捻" +

                               "掐掂掖掷掸掺勘聊娶菱菲萎菩萤乾萧萨菇彬梗梧梭曹酝酗厢硅硕奢盔匾颅彪眶晤曼晦冕啡畦趾啃蛆蚯蛉蛀" +

                               "唬啰唾啤啥啸崎逻崔崩婴赊铐铛铝铡铣铭矫秸秽笙笤偎傀躯兜衅徘徙舶舷舵敛翎脯逸凰猖祭烹庶庵痊阎阐" +

                               "眷焊焕鸿涯淑淌淮淆渊淫淳淤淀涮涵惦悴惋寂窒谍谐裆袱祷谒谓谚尉堕隅婉颇绰绷综绽缀巢琳琢琼揍堰揩" +

                               "揽揖彭揣搀搓壹搔葫募蒋蒂韩棱椰焚椎棺榔椭粟棘酣酥硝硫颊雳翘凿棠晰鼎喳遏晾畴跋跛蛔蜒蛤鹃喻啼喧" +

                               "嵌赋赎赐锉锌甥掰氮氯黍筏牍粤逾腌腋腕猩猬惫敦痘痢痪竣翔奠遂焙滞湘渤渺溃溅湃愕惶寓窖窘雇谤犀隘" +

                               "媒媚婿缅缆缔缕骚瑟鹉瑰搪聘斟靴靶蓖蒿蒲蓉楔椿楷榄楞楣酪碘硼碉辐辑频睹睦瞄嗜嗦暇畸跷跺蜈蜗蜕蛹" +

                               "嗅嗡嗤署蜀幌锚锥锨锭锰稚颓筷魁衙腻腮腺鹏肄猿颖煞雏馍馏禀痹廓痴靖誊漓溢溯溶滓溺寞窥窟寝褂裸谬" +

                               "媳嫉缚缤剿赘熬赫蔫摹蔓蔗蔼熙蔚兢榛榕酵碟碴碱碳辕辖雌墅嘁踊蝉嘀幔镀舔熏箍箕箫舆僧孵瘩瘟彰粹漱" +

                               "漩漾慷寡寥谭褐褪隧嫡缨撵撩撮撬擒墩撰鞍蕊蕴樊樟橄敷豌醇磕磅碾憋嘶嘲嘹蝠蝎蝌蝗蝙嘿幢镊镐稽篓膘" +

                               "鲤鲫褒瘪瘤瘫凛澎潭潦澳潘澈澜澄憔懊憎翩褥谴鹤憨履嬉豫缭撼擂擅蕾薛薇擎翰噩橱橙瓢蟥霍霎辙冀踱蹂" +

                               "蟆螃螟噪鹦黔穆篡篷篙篱儒膳鲸瘾瘸糙燎濒憾懈窿缰壕藐檬檐檩檀礁磷瞭瞬瞳瞪曙蹋蟋蟀嚎赡镣魏簇儡徽" +

                               "爵朦臊鳄糜癌懦豁臀藕藤瞻嚣鳍癞瀑襟璧戳攒孽蘑藻鳖蹭蹬簸簿蟹靡癣羹鬓攘蠕巍鳞糯譬霹躏髓蘸镶瓤矗";


            Random rnd = new Random(GetRandomGuid());
            return string.Format("{0}{1}{2}", _crabofirstName[rnd.Next(_crabofirstName.Length - 1)], _lastName.Substring(rnd.Next(0, _lastName.Length - 1), 1), _lastName.Substring(rnd.Next(0, _lastName.Length - 1), 1));
        }


        /// <summary>
        /// 随机生成英文名字
        /// </summary>
        /// <returns></returns>
        public static string GenerateSurname ()
        {
            string name = string.Empty;
            string[] currentConsonant;
            string[] vowels = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
            string[] commonConsonants = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
            string[] averageConsonants = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
            string[] middleConsonants = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(',');
            string[] rareConsonants = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
            Random rng = new Random(GetRandomGuid());
            int[] lengthArray = new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 4 };
            int length = lengthArray[rng.Next(lengthArray.Length)];
            for (int i = 0; i < length; i++)
            {
                int letterType = rng.Next(1000);
                if (letterType < 775) currentConsonant = commonConsonants;
                else if (letterType < 875 && i > 0) currentConsonant = middleConsonants;
                else if (letterType < 985) currentConsonant = averageConsonants;
                else currentConsonant = rareConsonants;
                name += currentConsonant[rng.Next(currentConsonant.Length)];
                name += vowels[rng.Next(vowels.Length)];
                if (name.Length > 4 && rng.Next(1000) < 800) break;
                if (name.Length > 6 && rng.Next(1000) < 950) break;
                if (name.Length > 7) break;
            }
            int endingType = rng.Next(1000);
            if (name.Length > 6)
                endingType -= (name.Length * 25);
            else
                endingType += (name.Length * 10);
            if (endingType < 400) { }
            else if (endingType < 775) name += commonConsonants[rng.Next(commonConsonants.Length)];
            else if (endingType < 825) name += averageConsonants[rng.Next(averageConsonants.Length)];
            else if (endingType < 840) name += "ski";
            else if (endingType < 860) name += "son";
            else if (Regex.IsMatch(name, "(.+)(ay|e|ee|ea|oo)$") || name.Length < 5)
            {
                name = "Mc" + name.Substring(0, 1).ToUpper() + name.Substring(1);
                return name;
            }
            else name += "ez";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            return name;
        }

        /// <summary>
        /// Guid 获取随机数种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomGuid ()
        {
            return Guid.NewGuid().GetHashCode();
        }
        #endregion 随机 结束




        /// <summary>
        /// 是否为json内容判断的是{} 简单的判断
        /// </summary>
        /// <param name="input">判断的内容</param>
        /// <returns>true是json false非json</returns>
        public static bool IsJson (this string input)
        {
            if (IsNull(input))
                return false;
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        /// <summary>
        /// 判断文本是否为null 空 或仅由空白组成,自动删除\r\n\t
        /// </summary>
        public static bool IsNull (this string text)
        {
            if (text == null) return true;
            text = Regex.Replace(text, "[\r\n]|[\t]", "");
            if (string.IsNullOrWhiteSpace(text))
                return true;
            return false;
        }






        /// <summary>
        /// 判断文本是否是大写 成功返回true
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUpper (string str)
        {
            Regex reg = new Regex(@"^[A-Z]+$");
            if (reg.IsMatch(str))
            {
                //大写
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 判断文本是否为小写 成功返回true
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLower (string str)
        {

            Regex reg = new Regex(@"^[a-z]+$");
            if (reg.IsMatch(str))
            {
                //小写
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 判断文本是为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt (string str)
        {

            Regex reg = new Regex(@"^[0-9]+$");
            if (reg.IsMatch(str))
            {

                return true;
            }
            else
            {
                return false;
            }

        }



        #region 数字互转

        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>int类型的数字</returns>
        public static int ToInt32 (this string s)
        {
            Int32.TryParse(s, out int result);
            return result;
        }

        /// <summary>
        /// 字符串转long
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>int类型的数字</returns>
        public static long ToInt64 (this string s)
        {
            Int64.TryParse(s, out var result);
            return result;
        }

        /// <summary>
        /// 字符串转double
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>double类型的数据</returns>
        public static double ToDouble (this string s)
        {
            Double.TryParse(s, out var result);
            return result;
        }

        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>int类型的数字</returns>
        public static decimal ToDecimal (this string s)
        {
            Decimal.TryParse(s, out var result);
            return result;
        }

        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>int类型的数字</returns>
        public static decimal ToDecimal (this double s)
        {
            return new decimal(s);
        }

        /// <summary>
        /// 字符串转double
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>double类型的数据</returns>
        public static double ToDouble (this decimal s)
        {
            return (double)s;
        }

        /// <summary>
        /// 将double转换成int
        /// </summary>
        /// <param name="num">double类型</param>
        /// <returns>int类型</returns>
        public static int ToInt32 (this double num)
        {
            return (int)Math.Floor(num);
        }

        /// <summary>
        /// 将double转换成int
        /// </summary>
        /// <param name="num">double类型</param>
        /// <returns>int类型</returns>
        public static int ToInt32 (this decimal num)
        {
            return (int)Math.Floor(num);
        }

        /// <summary>
        /// 字符串转long类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultResult">转换失败的默认值</param>
        /// <returns></returns>
        public static long ToLong (this string str, long defaultResult)
        {
            if (!Int64.TryParse(str, out var result))
            {
                result = defaultResult;
            }
            return result;
        }

        /// <summary>
        /// 将int转换成double
        /// </summary>
        /// <param name="num">int类型</param>
        /// <returns>int类型</returns>
        public static double ToDouble (this int num)
        {
            return num * 1.0;
        }

        /// <summary>
        /// 将int转换成decimal
        /// </summary>
        /// <param name="num">int类型</param>
        /// <returns>int类型</returns>
        public static decimal ToDecimal (this int num)
        {
            return (decimal)(num * 1.0);
        }

        #endregion


        #region 转义
        private static string Escape (string arg)
        {
            return arg.Replace(":", "\\:").Replace(";", "\\;");
        }

        private static string UnEscape (string arg)
        {
            return arg.Replace("\\:", ":").Replace("\\;", ";");
        }
        #endregion

    }
}
