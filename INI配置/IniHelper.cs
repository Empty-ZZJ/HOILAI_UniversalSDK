using System.Runtime.InteropServices;
using System.Text;

namespace HL.IO
{
    public class InI
    {
        private string FilePath;
        public string Path
        {

            get { return this.FilePath; }
            set
            {
                if (value.Substring(0, 1) == "\\" || value.Substring(0, 1) == "/")
                {
                    this.FilePath = AppDomain.CurrentDomain + value;
                }
                else
                {
                    this.FilePath = value;
                }

            }
        }
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString (string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString (string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="_Path">首个字符为\\或/则自动前面加路径</param>
        public InI (string _Path)
        {
            this.Path = _Path;

        }

        /// <summary>
        /// 写入INI文件指定KEY的值
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">Key</param>
        /// <param name="Value">Value</param>
        public void WriteValue (string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.FilePath);
        }

        /// <summary>
        /// 读取INI文件指定KEY的值
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">KEY</param>
        public string ReadValue (string Section, string Key)
        {
            try
            {

                StringBuilder temp = new StringBuilder(204800);
                int i = GetPrivateProfileString(Section, Key, "", temp, 204800, this.FilePath);

                return temp.ToString();
            }
            catch { return ""; }


        }

        ///   删除指定Section。 
        ///   <param   name= "Section ">Section</param> 
        ///   <returns> 返回删除是否成功</returns> 
        public bool RemoveSection (string Section)
        {
            return WritePrivateProfileString(Section, null, null, this.FilePath);
        }

        /// 验证文件是否存在
        public bool Exists ()
        {
            return System.IO.File.Exists(this.FilePath);
        }
    }
}
