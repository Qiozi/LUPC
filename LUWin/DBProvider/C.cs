using System.Data;

namespace LUComputers.DBProvider
{
    public class C
    {
        static DataTable _partSize = null;

        /// <summary>
        /// 远程尺寸列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPartSize()
        {
            if(_partSize == null)
            _partSize = Config.RemoteExecuteDateTable(@"select product_size_id, concat(product_size_name, '(low:$',(select round( charge, 2 ) from tb_account a where shipping_company_id=1 and a.product_size_id=p.product_size_id) ,')') c
 from tb_product_size p where product_type=1");
            return _partSize;
        }
    }
}
