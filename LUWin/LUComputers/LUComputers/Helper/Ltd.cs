using LUComputers.DBProvider;
using System;
using System.Data;

/// <summary>
/// PayMethod 的摘要说明
/// </summary>
namespace LUComputers
{
    public class LtdHelper
    {
        const string WHOLESALER_TAG = "wholesaler_";
        const string RIVAL_TAG = "Rival_";

        public LtdHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        
        public DataTable LtdHelperToDataTable()
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("id");
            dt.Columns.Add("text");
            foreach (int i in Enum.GetValues(typeof(Ltd)))
            {
                DataRow dr = dt.NewRow();
                dr["id"] = i;
                dr["text"] = FilterText(Enum.GetName(typeof(Ltd), i));
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable LtdHelperValidToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("text");
            foreach (int i in Enum.GetValues(typeof(Ltd)))
            {
                if (Config.valid_other_inc_id.IndexOf("[" + i.ToString() + "]") != -1)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = i;
                    dr["text"] = FilterText(Enum.GetName(typeof(Ltd), i));
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        public DataTable LtdHelperWholesalerToDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("text");
            foreach (int i in Enum.GetValues(typeof(Ltd)))
            {
                string text = Enum.GetName(typeof(Ltd), i);
                if (text.IndexOf(WHOLESALER_TAG) != -1)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = i;
                    dr["text"] = text.Replace(WHOLESALER_TAG, "");
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public DataTable LtdHelperRivalToDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("text");
            foreach (int i in Enum.GetValues(typeof(Ltd)))
            {
                string text = Enum.GetName(typeof(Ltd), i);
                if (text.IndexOf(RIVAL_TAG) != -1)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = i;
                    dr["text"] = text.Replace(RIVAL_TAG, "");
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        public int LtdHelperValue(Ltd ct)
        {
            foreach (int i in Enum.GetValues(typeof(Ltd)))
            {
                if (Enum.GetName(typeof(Ltd), i) == ct.ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        public Ltd LtdModelByValue(int v)
        {
            try
            {
                return (Ltd)Enum.Parse(typeof(Ltd), Enum.GetName(typeof(Ltd), v));
            }
            catch (Exception ex) { throw ex; }
        }

        public string FilterText(string text)
        {
            if (text.Trim().Length > 2)
                return text.Replace(WHOLESALER_TAG, "").Replace(RIVAL_TAG, "");
            else
                return text;
        }


        public string GetLastStoreTableName(Ltd ltd)
        {
            DataTable dt = Config.ExecuteDateTable(string.Format(@"select db_table_name 
from tb_other_inc_run_date where other_inc_id='{0}' order by id desc limit 0,1", LtdHelperValue(ltd)));
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        public string GetLastStoreTableNameGroup(Ltd ltd)
        {
            return DBProvider.Find.LastTableName(DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(ltd.ToString())));
        }

        public int GetLtdIdByText(string str)
        {
            DataTable dt = LtdHelperToDataTable();
            int ltd_id = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (str == dt.Rows[i]["text"].ToString())
                {
                    ltd_id = int.Parse(dt.Rows[i]["id"].ToString());
                    break;
                }
            }
            return ltd_id;
        }
        
        public string[] GetLtdStoreDBTableNames(Ltd[] ltds)
        {
            string[] Names = new string[ltds.Length];


            for (int i = 0; i < ltds.Length; i++)
            {
                if (ltds[i] == Ltd.wholesaler_asi)
                {
                    Names[i] = DBProvider.Find.LastTableName(DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_asi.ToString())));
                }
                else if (ltds[i] == Ltd.wholesaler_Synnex)
                {
                    Names[i] = DBProvider.Find.LastTableName(DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_Synnex.ToString())));
                }
                else
                {
                    int ltd_id = LtdHelperValue(ltds[i]);

                    DataTable dt = Config.ExecuteDateTable("select db_table_name from tb_other_inc_run_date where other_inc_id='" + ltd_id.ToString() + "' order by id desc limit 0,1");
                    if (dt.Rows.Count == 1)
                    {
                        Names[i] = dt.Rows[0][0].ToString();
                    }
                    else
                        Names[i] = "";
                }
            }
            return Names;
        }

        public string GetLtdIds(Ltd[] ltds)
        {
            string ltdids = "";
            for (int i = 0; i < ltds.Length; i++)
            {
                int ltd_id = LtdHelperValue(ltds[i]);
                ltdids += ltd_id.ToString() + ",";
            }
            return ltdids.TrimEnd(new char[] { ',' });
        }
    }


    public enum Ltd
    {
        lu = 1,
        wholesaler_supercom = 2,
        wholesaler_asi = 3,
        //wholesaler_EPROM = 4,
        wholesaler_DAIWA = 5,
        wholesaler_MUTUAL = 6,
        wholesaler_OCZ = 7,
        wholesaler_COMTRONIX = 8,
        wholesaler_SINOTECH = 9,
        wholesaler_MINIMICRO = 10,
        wholesaler_ALC = 11,
        wholesaler_SAMTACH = 12,
        wholesaler_MMAX = 13,

        wholesaler_CanadaComputers = 15,
       // wholesaler_dandh = 16,
        wholesaler_d2a =17,
        wholesaler_BellMicroproducts = 18,
        wholesaler_Smartvision_Direct = 19,
        wholesaler_Synnex = 20,
        Rival_ETC = 100,
        Rival_Ncix = 101,
        Rival_PcvOnline = 103,
        Rival_DirectDial = 104,
        Rival_TigerDirect = 105,
        Rival_NewEgg = 106,
        Rival_PC_Canada=107,
        Rival_BestDirect =108,
        Rival_HookBag=109,
        Rival_Ashlin = 110,
        Rival_Amazon = 111,
        
        supercom_all = 222,
        eBay = 999,
        MatchStore = 1000,
        ALLPublic = 9999
        
    } 
}