using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YunStore.Model;

namespace YunStore.Toolkits
{
    public static class DescriptionHelper
    {
        public static string Get(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumeration.ToString();
        }

        public static List<CommomItem> GetAllItems(this Type enumName)
        {
            List<CommomItem> list = new List<CommomItem>();
            // get enum fileds
            FieldInfo[] fields = enumName.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }
                // get enum value
                int value = (int)enumName.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                string text = field.Name;
                string description = string.Empty;
                object[] array = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (array.Length > 0)
                {
                    description = ((DescriptionAttribute)array[0]).Description;
                }
                else
                {
                    description = ""; //none description,set empty
                }

                //add to list
                var obj = new CommomItem();
                obj.Sid = value;
                obj.Name = description;
                list.Add(obj);
            }
            return list;
        }
    }
}
