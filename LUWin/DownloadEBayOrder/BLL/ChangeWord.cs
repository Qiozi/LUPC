using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadEBayOrder.BLL
{
    public class ChangeWord : Events.EventBase
    {
        public void Do()
        {
            nicklu2Entities context = new nicklu2Entities();
            var words = context.tb_keyword_change.ToList();
            try
            {
                for (int j = 0; j < words.Count; j++)
                {
                    SetStatus("change word:" + words[j].OldWord);
                    Config.ExecuteNonQuery(string.Format(@"
update tb_product set producter_serial_no = replace(producter_serial_no, '{0}','{1}')
, product_short_name = replace(product_short_name,'{0}','{1}')
, product_name = replace(product_name,'{0}','{1}')
, product_ebay_name = replace(product_ebay_name,'{0}','{1}')
, manufacturer_part_number = replace(manufacturer_part_number,'{0}','{1}')
, keywords = replace(keywords,'{0}','{1}');
"
                        , words[j].OldWord
                        , words[j].NewWord));
                }
                SetStatus("change word OK");
            }
            catch (Exception ex)
            {
                new Logs(Application.StartupPath).WriteErrorLog(ex);
            }
        }
    }
}
