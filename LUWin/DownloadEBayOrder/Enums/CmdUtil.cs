using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.Enums
{
    public class CmdUtil
    {
        public static Enums.Cmd GetCmd(string cmd, string cmdContent)
        {
            var cmdstr = cmd.Trim().ToLower();
            int cmdInt;
            int.TryParse(cmdstr, out cmdInt);
            if (cmdInt > 0)
            {
                return (Enums.Cmd)Enum.Parse(typeof(Enums.Cmd), cmdInt.ToString());
            }
            if (cmdstr == "eBayPriceOrder".ToLower())
            {
                if (cmdContent.Trim().ToLower() == "macthebayproduct")
                {
                    return Cmd.DowneBayPrice;
                }
                if (cmdContent.Trim().ToLower() == "ModifyPartEbayPrice".ToLower())
                {
                    return Cmd.ModifyPartEbayPrice;
                }
                if (cmdContent.Trim().ToLower() == "ModifyEbayPricePartAndSys".ToLower())
                {
                    return Cmd.ModifyEbayPricePartAndSys;
                }
                if (cmdContent.Trim().ToLower() == "ReadPriceFile".ToLower())
                {
                    return Cmd.ReadPriceFile;
                }

                int oid;
                int.TryParse(cmdContent, out oid);
                if (oid > 0)
                {
                    return Enums.Cmd.DownSingleOrder;
                }

                return Enums.Cmd.DownOrder;
            }
            return Enums.Cmd.None;
        }
    }
}
