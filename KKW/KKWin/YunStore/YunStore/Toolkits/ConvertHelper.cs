using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using YunStore.Model;

namespace YunStore.Toolkits
{
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        ///  日期转字符转
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string DataRowDateString(object txt)
        {
            var str = DataRowString(txt);

            try
            {
                return string.IsNullOrEmpty(str)
                    ? string.Empty
                    : DateTime.Parse(str).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string DataRowString(object txt)
        {
            if (txt == null)
            {
                return string.Empty;
            }
            return txt.ToString();
        }


        #region 旧版转化
        public static DateTime? ConvertStringToDate(string text)
        {
            DateTime result = new DateTime();
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }
                result = Convert.ToDateTime(text);
            }
            catch
            {
                return null;
            }
            return result;
        }

        /// <summary>
        /// 日期型转为字符串
        /// </summary>
        /// <param name="text">待转换对象</param>
        /// <param name="type">转换后格式：默认:"yyyy-MM-dd HH:mm:ss",1:"yyyy-MM-dd" 2:"yyyy.MM.dd"</param>
        /// <returns></returns>
        public static string ConvertDateToString(DateTime? text, int format = 0)
        {
            string result = "";
            try
            {
                if (text == null)
                {
                    return null;
                }
                if (format == 1)
                {
                    result = Convert.ToDateTime(text).ToString("yyyy-MM-dd");
                }
                else if (format == 2)
                {
                    result = Convert.ToDateTime(text).ToString("yyyy.MM.dd");
                }
                else
                {
                    result = Convert.ToDateTime(text).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch
            {
                return "";
            }
            return result;
        }


        /// <summary>
        /// 日期型转为字符串
        /// </summary>
        /// <param name="text">待转换对象</param>
        /// <param name="type">转换后格式：默认:"yyyy-MM-dd HH:mm:ss",1:"yyyy-MM-dd"</param>
        /// <returns></returns>
        public static string ConvertDateToString(DateTime text, int format = 0)
        {
            string result = "";
            try
            {
                if (text == null)
                {
                    return null;
                }
                if (format == 1)
                {
                    result = Convert.ToDateTime(text).ToString("yyyy-MM-dd");
                }
                else if (format == 2)
                {
                    result = Convert.ToDateTime(text).ToString("yyyy年MM月dd日");
                }
                else
                {
                    result = Convert.ToDateTime(text).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch
            {
                return "";
            }
            return result;
        }
        #endregion

        /// <summary>
        ///把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CustomConvert<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                {
                    var temp = Convert.ChangeType(value, typeof(decimal)); ;
                    result = Convert.ChangeType(temp, type);
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CustomConvert<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }

        /// <summary>
        /// 字符串用指定的分隔符拆分成列表
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要拆分的字符串</param>
        /// <param name="split">拆分符，默认分号</param>
        /// <param name="distinct">是否过滤重复数据，默认是</param>
        /// <returns>返回拆分后的列表</returns>
        public static List<T> ConvertStringToList<T>(string value, char split = ';', bool distinct = true)
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return result;
                }
                char[] separator = new char[] { split };
                string[] arr = value.Split(separator);
                foreach (string item in arr)
                {
                    T item1 = CustomConvert<T>(item);
                    if (!distinct)
                    {
                        result.Add(item1);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item) && !result.Contains(item1))
                        {
                            result.Add(item1);
                        }
                    }
                }
            }
            catch
            {
                result = default(List<T>);
            }
            return result;
        }

        /// <summary>    
        /// DataTable 转换为List 集合    
        /// </summary>    
        /// <typeparam name="TResult">类型</typeparam>    
        /// <param name="dt">DataTable</param>    
        /// <returns></returns>    
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            Type t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合    

            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                T ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// 数字转中文大写（货币）
        /// </summary>
        /// <param name="value">待转换的数字</param>
        /// <returns>返回数字对应的大写中文</returns>
        public static string ConvertNumToChMax(decimal value)
        {
            StringBuilder chBuilder = new StringBuilder();
            if (value == 0)
            {
                chBuilder.Append("零圆");
                return chBuilder.ToString();
            }
            string valueString = ConvertHelper.CustomConvert<Int64>((Math.Round(value, 2) * 100)).ToString().PadLeft(14, '0');
            string digit = "零壹贰叁肆伍陆柒捌玖";//大写数字数组 
            string dom = "仟佰拾亿仟佰拾万仟佰拾圆角分";//大写数字单位数组
            if (value < 0)
            {
                chBuilder.Append("（负数）");
            }
            string indexStr = "";
            string indexStr2 = "";
            for (int i = 0; i < valueString.Length; i++)
            {
                indexStr = valueString.Substring(i, 1);
                if (i < valueString.Length - 1)
                {
                    indexStr2 = valueString.Substring(i, 2);
                }
                if (!(indexStr2 == "00" || (indexStr == "0" && (i == 3 || i == 7 || i == 11 || i == 13))))
                {
                    chBuilder.Append(digit[ConvertHelper.CustomConvert<int>(indexStr)]);
                }
                if (!(indexStr == "0" && i != 3 && i != 7 && i != 11))
                {
                    chBuilder.Append(dom[i]);
                }
                if (chBuilder.Length == 1)
                {
                    if (chBuilder[0].ToString() == "零" || chBuilder[0].ToString() == "万" || chBuilder[0].ToString() == "亿")
                    {
                        chBuilder.Remove(0, 1);
                    }
                }
                else if (chBuilder.Length > 1)
                {

                    if (chBuilder[chBuilder.Length - 2].ToString() == "亿" && chBuilder[chBuilder.Length - 1].ToString() == "万")
                    {
                        chBuilder.Remove(chBuilder.Length - 1, 1);
                    }
                }
            }
            return chBuilder.ToString();
        }

        /// <summary>
        /// 枚举转列表
        /// </summary>
        /// <typeparam name="T">待转换的枚举</typeparam>
        /// <returns>返回列表</returns>
        public static List<DropDownModel> ConvertEnumToList<T>()
        {
            List<DropDownModel> list = new List<DropDownModel>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                DropDownModel m = new DropDownModel();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    m.Text = da.Description;
                }
                m.Value = e;
                list.Add(m);
            }
            return list;
        }

        /// <summary>
        /// json字符串转对象
        /// </summary>
        /// <typeparam name="T">待转换的对象</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns></returns>
        public static T ConvertJsonStringToObject<T>(string jsonString) where T : class
        {
            T result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }
    }
}
