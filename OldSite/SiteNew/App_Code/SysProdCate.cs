using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SysProdCate
/// </summary>
public class SysProdCate
{
	public SysProdCate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 取得系统所在的目录， 
    /// 默认413 Gaming, 
    /// 按ebay价格匹分， 大于1000块的为gaming (413), 小于1000块的是 home pc (414), 
    /// Barebone pc (412)
    /// </summary>
    /// <param name="sysSku"></param>
    /// <returns></returns>
    public static int GetSysCate(int sysSku, bool IsBarebone, nicklu2Entities db)
    {
        if (IsBarebone)
            return 412;

        var ebayonline = db.tb_ebay_selling.FirstOrDefault(p => p.sys_sku.HasValue
            && p.sys_sku.Value.Equals(sysSku));
        if (ebayonline == null)
            return 413;
        else
            return ebayonline.BuyItNowPrice.Value >= 1000 ? 413 : 414;
    }
}