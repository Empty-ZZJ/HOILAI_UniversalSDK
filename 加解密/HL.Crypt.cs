using System.Security.Cryptography;
using System.Text;


namespace HL.Crypt
{

    /// <summary>
    /// 各种加密类
    /// </summary>
    public static class Crypt
    {
        #region md5

        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>md5加密结果</returns>
        public static string MD5 (this string input)
        {
            // input = "123456z";
            var md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] result = md5.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x2");
            return ret;
        }

        /// <summary>
        /// MD5加密byte
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>md5加密结果</returns>
        public static string MD5 (this byte[] bytes)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bytes, 0, bytes.Length);
            StringBuilder sb = new StringBuilder();
            foreach (byte value in hash)
            {
                sb.AppendFormat("{0:x2}", value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 读取文件MD5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5</returns>
        public static string FileMd5 (string filePath)
        {
            var cfilePath = filePath + "e";
            if (File.Exists(cfilePath))
                File.Delete(cfilePath);
            File.Copy(filePath, cfilePath);//复制一份，防止占用


            if (File.Exists(cfilePath))
            {
                var buffer = File.ReadAllBytes(cfilePath);
                System.IO.File.Delete(cfilePath);
                return MD5(buffer);
            }
            else
            {
                throw new Exception("读取文件MD5出错!");
            }
        }

        #endregion MD5

        #region 16进制 转换

        public static string ByteToHex (this byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public static byte[] HexToByte (this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #endregion byte  16进制 转换

        #region Des

        public static byte[] DesEncrypt (string input, string key,
        CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            if (key.Length > 8) key = key.Substring(0, 8);

            if (key.Length != 8)//必须是8位数的密码 不足我们补全下
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 8 - key.Length; i++)
                {
                    sb.Append("0");
                }

                key = key + sb.ToString();

            }

            try
            {

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(input);
                    des.Mode = mode;
                    des.Padding = padding;

                    des.Key = ASCIIEncoding.UTF8.GetBytes(key);
                    des.IV = ASCIIEncoding.UTF8.GetBytes(key);
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new byte[0];
            }

        }

        public static byte[] DesDecrypt (byte[] input, string key,
         CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {

            if (key.Length > 8) key = key.Substring(0, 8);

            if (key.Length != 8)//必须是8位数的密码 不足我们补全下
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 8 - key.Length; i++)
                {
                    sb.Append("0");
                }

                key = key + sb.ToString();

            }


            try
            {


                using (var des = new DESCryptoServiceProvider())
                {
                    des.Mode = mode;
                    des.Padding = padding;
                    des.Key = ASCIIEncoding.UTF8.GetBytes(key);
                    des.IV = ASCIIEncoding.UTF8.GetBytes(key);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(input, 0, input.Length);
                            cs.FlushFinalBlock();

                            ms.ToArray();
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch { return new byte[0]; }
        }

        #endregion Des


        #region sha1


        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <returns>返回40位UTF8 大写</returns>  
        public static string SHA1 (string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1 (string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        #endregion



        #region Aes

        private static readonly string Default_AES_Key = "@#kim123";

        private static readonly byte[] Keys =
        {
            0x41,
            0x72,
            0x65,
            0x79,
            0x6F,
            0x75,
            0x6D,
            0x79,
            0x53,
            0x6E,
            0x6F,
            0x77,
            0x6D,
            0x61,
            0x6E,
            0x3F
        };

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密结果字符串</returns>
        public static string AESEncrypt (this string encryptString)
        {
            return AESEncrypt(encryptString, Default_AES_Key);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        public static string AESEncrypt (this string encryptString, string encryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged
            {
                Key = encoding.GetBytes(encryptKey.Substring(0, 32)),
                IV = Keys
            };
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
            byte[] inputData = encoding.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string AESDecrypt (this string decryptString)
        {
            return AESDecrypt(decryptString, Default_AES_Key);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public static string AESDecrypt (this string decryptString, string decryptKey, Encoding encoding = null)
        {
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }

                decryptKey = GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');
                var rijndaelProvider = new RijndaelManaged()
                {
                    Key = encoding.GetBytes(decryptKey),
                    IV = Keys
                };
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return encoding.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString (this string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0开始</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString (this string sourceString, int startIndex, int length, string tailString)
        {
            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }

                return sourceString.Substring(startIndex, length + startIndex > sourceString.Length ? (sourceString.Length - startIndex) : length);
            }

            //中文字符，如"中国人民abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }

            byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

            //当字符串长度大于起始位置
            if (bytesSource.Length > startIndex)
            {
                int endIndex = bytesSource.Length;

                //当要截取的长度在字符串的有效长度范围内
                if (bytesSource.Length > (startIndex + length))
                {
                    endIndex = length + startIndex;
                }
                else
                {
                    //当不在有效范围内时,只取到字符串的结尾
                    length = bytesSource.Length - startIndex;
                    tailString = "";
                }

                var anResultFlag = new int[length];
                int nFlag = 0;
                //字节大于127为双字节字符
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (bytesSource[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3)
                        {
                            nFlag = 1;
                        }
                    }
                    else
                    {
                        nFlag = 0;
                    }

                    anResultFlag[i] = nFlag;
                }

                //最后一个字节为双字节字符的一半
                if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                {
                    length++;
                }

                byte[] bsResult = new byte[length];
                Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                var myResult = Encoding.Default.GetString(bsResult);
                myResult += tailString;
                return myResult;
            }

            return string.Empty;
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">需要加密的文件流</param>
        /// <param name="decryptKey">加密密钥</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESEncryptStrream (FileStream fs, string decryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged()
            {
                Key = encoding.GetBytes(decryptKey),
                IV = Keys
            };
            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            return new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">需要解密的文件流</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESDecryptStream (FileStream fs, string decryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged()
            {
                Key = encoding.GetBytes(decryptKey),
                IV = Keys
            };
            ICryptoTransform decrypto = rijndaelProvider.CreateDecryptor();
            return new CryptoStream(fs, decrypto, CryptoStreamMode.Read);
        }

        /// <summary>
        /// 对指定文件AES加密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="outputPath">输出文件路径</param>
        public static void AESEncryptFile (FileStream input, string outputPath)
        {
            using (FileStream fren = new FileStream(outputPath, FileMode.Create))
            {
                CryptoStream enfr = AESEncryptStrream(fren, Default_AES_Key);
                byte[] bytearrayinput = new byte[input.Length];
                input.Read(bytearrayinput, 0, bytearrayinput.Length);
                enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
            }
        }

        /// <summary>
        /// 对指定的文件AES解密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="outputPath">输出文件路径</param>
        public static void AESDecryptFile (FileStream input, string outputPath)
        {
            FileStream frde = new FileStream(outputPath, FileMode.Create);
            CryptoStream defr = AESDecryptStream(input, Default_AES_Key);
            byte[] bytearrayoutput = new byte[1024];
            while (true)
            {
                var count = defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                frde.Write(bytearrayoutput, 0, count);
                if (count < bytearrayoutput.Length)
                {
                    break;
                }
            }
        }


        #endregion


        #region Base64加密解密

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的数据</returns>
        public static string Base64Encrypt (this string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] encbuff = encoding.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <returns>解密后的数据</returns>
        public static string Base64Decrypt (this string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] decbuff = Convert.FromBase64String(str);
            return encoding.GetString(decbuff);
        }

        #endregion



        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果(16进制字节)</returns>
        public static string SHA256 (this string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] sha256Data = encoding.GetBytes(str);
            var sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(sha256Data);

            return ByteToHex(result).ToLower();

            //return Convert.ToBase64String(result); //返回base64
        }

        /// <summary>
        /// 文本_加密 简单加密
        /// </summary>
        /// <param name="str">待加密的文本</param>
        /// <param name="pass">加密的密码</param>
        /// <returns></returns>
        public static string StringEncrypt (string str, string pass, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] bin = encoding.GetBytes(str);
            List<byte> list = new List<byte>();
            for (int i = 0; i < bin.Length; i++)
            {
                var ch = (byte)(bin[i] ^ 3600);
                list.Add(ch);
            }

            string md5 = MD5(pass).Substring(2, 9);

            string hex = ByteToHex(list.ToArray());


            return hex + md5.ToUpper();
        }

        /// <summary>
        /// 文本解密
        /// </summary>
        /// <param name="str">待解密的文本</param>
        /// <param name="pass">解密的密文</param>
        /// <returns></returns>
        public static string StringDecrypt (string str, string pass, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string md5 = MD5(pass).Substring(2, 9).ToUpper();
            if (!str.EndsWith(md5))
            {
                return "";
            }

            string item = str.Substring(0, str.Length - 9);

            byte[] bin = HexToByte(item);
            List<byte> list = new List<byte>();
            for (int i = 0; i < bin.Length; i++)
            {
                var ch = (byte)(bin[i] ^ 3600);
                list.Add(ch);
            }

            string html = encoding.GetString(list.ToArray());

            return html;
        }


    }
}
