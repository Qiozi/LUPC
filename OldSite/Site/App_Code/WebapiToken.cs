using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebapiToken
/// </summary>
public class WebapiToken
{
    public WebapiToken()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetToken()
    {
        var token = Guid.NewGuid().ToString();
        // ChangeeBayStockQuantityOnline = 1
        // CreateEbayonSlae = 2
        Config.ExecuteNonQuery(string.Format(@"insert into tb_exchange 
	( Pwd, ExchangeType, Source, Regdate)
	values
	( '{0}', 1, 'WebManage', now())", token));
        return token;
    }
}