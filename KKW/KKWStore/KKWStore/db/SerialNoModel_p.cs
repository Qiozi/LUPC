using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KKWStore.db
{
    public class SerialNoModel_p 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        public static bool ExistBySerialNO(qstoreEntities context, string serialNo, ref bool used)
        {
            if (db.SqlExec.ExecuteScalarInt("Select count(*) from tb_serial_no where SerialNo='" + serialNo + "'") == 1)
            {
                // 是否已被使用过
                used = db.SerialNoAndPCodeModel_p.ExistSN(context, serialNo);
                return !used;
            }
            return false;
        }

        /// <summary>
        /// 取个未用的serialno
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetSerialNo(qstoreEntities context)
        {
            var query = context.tb_serial_no.FirstOrDefault(me => me.is_print.HasValue && me.is_print.Value.Equals(false));
            if(query  == null)
            {
                throw new Exception("serial no store is null.");
            }
            query.is_print = true;
            context.SaveChanges();
            return query.SerialNo;
        }
    }
}
