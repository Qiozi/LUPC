using LU.Data;
using System;
using System.Linq;

/// <summary>
/// Summary description for TempleteReplaceParameterModel
/// </summary>
[Serializable]
public class TempleteReplaceParameterModel
{
    public static LU.Data.tb_templete_replace_parameter[] FindModelsByPageName(nicklu2Entities context, string page_name)
    {
        return context.tb_templete_replace_parameter.Where(me => me.page_name.Equals(page_name)).ToList().ToArray();
    }
}
