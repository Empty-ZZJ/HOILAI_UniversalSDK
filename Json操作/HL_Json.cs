using Newtonsoft.Json;
using System.Collections;
using System.Reflection;

namespace HL.Json
{
    public class HL_Json
    {
        public static Dictionary<string, object> JsonToDic (string jsonString)
        {
            try
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                return dic;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }


        #region 对象转换为Json
        /// <summary> 
        /// 对象转换为Json 
        /// </summary> 
        /// <param T="t">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson<T> (T t)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = t.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(t, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "\"" + objectValue.ToString() + "\"";
                }
                else if (objectValue is string)
                {
                    value = "\"" + objectValue.ToString() + "\"";
                }
                else if (objectValue is IEnumerable)//待修改
                {
                    value = objectValue.ToString();
                }
                else
                {
                    value = objectValue.ToString().ToLower();//Flase 报错
                }
                jsonString += "\"" + propertyInfo[i].Name + "\":" + value + ",";
            }

            return jsonString.TrimEnd(',') + "}";
        }
        #endregion


    }
}
