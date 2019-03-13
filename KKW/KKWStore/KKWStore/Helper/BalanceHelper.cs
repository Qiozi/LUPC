using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KKWStore.Helper
{
    public class BalanceHelper
    {

        public static bool CurrMonthHaveAutoStat(db.qstoreEntities context, DateTime currDate)
        {
            var date = DateTime.Parse(currDate.ToString("yyyy-MM-01"));
            var y = date.Year;
            var m = date.Month;
            var d = date.Day;

            var lastAutoInsert = context.tb_balance_cash_record
                    .OrderByDescending(p => p.PayDate)
                    .FirstOrDefault(p => p.CreatorId.Equals(Helper.Config.SysAdminId) && !p.IncomeCash.Equals(0) &&
                        p.PayDate.Year.Equals(y) &&
                        p.PayDate.Month.Equals(m) &&
                        p.PayDate.Day.Equals(d));

            if (lastAutoInsert == null)
            {
                return false;
            }
            else if (currDate.Year == lastAutoInsert.PayDate.Year &&
               currDate.Month > lastAutoInsert.PayDate.Month)
            {
                return false;
            }
            return true;
        }

        public static bool SaveMemory(db.qstoreEntities context, DateTime currDate)
        {
            var monthTxt = currDate.AddMonths(-1).ToString("yyyy-MM");

            //SaveStatBalance(context, Helper.ProfitStat.StatMonthExclude(currDate.AddMonths(-1)), GetBalance(context), currDate, "系统自动计算，重复扣除的成本(" + monthTxt + ")。", "系统");


            decimal saleTotal = 0M;
            decimal partCost = 0M;
            decimal proxyCost = 0M;
            decimal payCost = 0M;
            decimal freeCost = 0M;
            decimal profit = 0M;
            profit = Helper.ProfitStat.StatMonthProfit(currDate.AddMonths(-1), profit, out saleTotal, out partCost, out proxyCost, out payCost, out freeCost);

            SaveStatBalance(context, saleTotal, GetBalance(context), currDate, "系统自动计算(" + monthTxt + " 营业额)", "系统");
            SaveBalance(context, 0, freeCost, GetBalance(context), currDate, "系统自动计算(" + monthTxt + " 赠品金额)", "系统", Helper.Config.SysAdminId, enums.PayType.Fee);
            //SaveStatBalance(context, freeCost, GetBalance(context), freeCost, "系统自动计算(" + monthTxt + " 赠品金额)", "系统");
            //SaveStatBalance(context, profit, GetBalance(context), currDate, "系统自动计算(" + monthTxt + " 利润)", "系统");
            //SaveStatBalance(context, partCost, GetBalance(context), currDate, "系统自动计算(" + monthTxt + " 货物成本)", "系统");
            //SaveStatBalance(context, proxyCost, GetBalance(context), currDate, "系统自动计算(" + monthTxt + " 代付成本)", "系统");

            return true;
            //SaveStatBalance(context, profit, last == null ? 0M : last.AfterBalance, currDate, "系统自动计算结余", "系统");
            //SaveStatBalance(context, Helper.ProfitStat.StatMonthExclude(currDate.AddMonths(-1)), GetBalance(context), currDate, "系统自动计算，重复扣除的成本。", "系统");
        }

        /// <summary>
        /// currDate , 统计的月份数据。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="currDate"></param>
        /// <returns></returns>
        public static bool StatBalance(db.qstoreEntities context, DateTime currDate, bool IsExec = false)
        {
            // currDate = currDate.AddMonths(1);

            if (currDate.Year >= 2016 && (currDate.Year > 2016 || currDate.Month >= 8) &&
                currDate.Year <= DateTime.Now.Year && currDate.Month <= DateTime.Now.Month) // 2016-08 开始计算结余
            {
                var lastAutoInsert = context.tb_balance_cash_record
                    .OrderByDescending(p => p.PayDate)
                    .FirstOrDefault(p => p.CreatorId.Equals(Helper.Config.SysAdminId) && !p.IncomeCash.Equals(0));

                if (lastAutoInsert == null)
                {
                    //SaveStatBalance(context, profit, last == null ? 0M : last.AfterBalance, currDate, "系统自动计算结余", "系统");
                    //SaveStatBalance(context, Helper.ProfitStat.StatMonthExclude(currDate.AddMonths(-1)), GetBalance(context), currDate, "系统自动计算，重复扣除的成本。", "系统");

                    SaveMemory(context, currDate);
                }
                else if (IsExec || currDate.Month > lastAutoInsert.PayDate.Month)
                {
                    SaveMemory(context, currDate);
                }
            }
            return true;
        }

       public static bool SaveBalance(db.qstoreEntities context, decimal profit, decimal payCash
             , decimal currBalance, DateTime currDate
             , string payNote, string creatorName, int createorId, enums.PayType payType)
        {
            if (payNote.IndexOf("系统") > -1)
            {
                var query = context.tb_balance_cash_record.Where(me => me.PayNote.Equals(payNote) &&
                                                                       me.PayType.Equals((int)payType) &&
                                                                       me.PayDate.Equals(currDate)).ToList();
                foreach(var item in query)
                {
                    context.tb_balance_cash_record.Remove(item);
                }
            }

            currBalance = GetBalance(context);
            var balance = new db.tb_balance_cash_record
            {
                PayDate = currDate,// payCash > 0 ? currDate : DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")),
                CreatorId = createorId,
                CreateTime = DateTime.Now,
                IncomeCash = profit,
                AfterBalance = payCash > 0M ? currBalance - payCash : currBalance + profit,
                CreatorName = creatorName,
                PayCash = payCash,
                PayNote = payNote,
                PreBalance = currBalance,
                IsExclude = true,
                PayType = (int)payType
            };
            context.tb_balance_cash_record.Add(balance);
            context.SaveChanges();
            return true;
        }
        
        public static bool SavePayBalance(db.qstoreEntities context,
             decimal payCash, DateTime payDate, string payNote, int userId
            , string userName, enums.PayType payType)
        {
            return SaveBalance(context, 0M, payCash, GetBalance(context), payDate, payNote
                , userName, userId, payType);
        }

        /// <summary>
        /// for 保存上月结余
        /// </summary>
        /// <param name="context"></param>
        /// <param name="profit"></param>
        /// <param name="currBalance"></param>
        /// <param name="currDate"></param>
        /// <param name="payNote"></param>
        /// <param name="creatorName"></param>
        /// <returns></returns>
        public static bool SaveStatBalance(db.qstoreEntities context
            , decimal profit
            , decimal currBalance
            , DateTime currDate
            , string payNote, string creatorName)
        {
            return SaveBalance(context, profit, 0, currBalance, currDate, payNote, creatorName, Helper.Config.SysAdminId, enums.PayType.Comein);// )
        }

        public static decimal GetBalance(db.qstoreEntities context)
        {
            var inCome = context.tb_balance_cash_record.Sum(p => (decimal?)p.IncomeCash).GetValueOrDefault();
            var pay = context.tb_balance_cash_record.Sum(p => (decimal?)p.PayCash).GetValueOrDefault();
            return inCome - pay;
        }

        //public static decimal GetBalanceStat(db.qstoreEntities context, DateTime dateMonth, bool isMonth)
        //{
        //    var year = dateMonth.Year;
        //    var month = dateMonth.Month;
        //    var day = dateMonth.Day;
        //    if (isMonth)
        //    {
        //        var query = context.tb_balance_cash_record
        //            .Where(p => p.PayDate.Year.Equals(year) && p.PayDate.Month.Equals(month))
        //            .Sum(p => (decimal?)p.PayCash).GetValueOrDefault();
        //        return query;
        //    }
        //    else
        //    {
        //        var query = context.tb_balance_cash_record
        //        .Where(p => p.PayDate.Year.Equals(year) &&
        //            p.PayDate.Month.Equals(month) && p.PayDate.Day.Equals(day))
        //            .Sum(p => (decimal?)p.PayCash).GetValueOrDefault();
        //        return query;
        //    }
        //}
    }
}
