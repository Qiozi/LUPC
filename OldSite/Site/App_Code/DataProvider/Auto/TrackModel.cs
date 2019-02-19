// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-15 0:15:24
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;


public class TrackModel 
{
    
    public static void Insert(nicklu2Entities context, string comment, int staff_id, string ip, string url)
    {
        //TrackModel tm = new TrackModel();
        //tm.track_comment = comment;
        //tm.track_ip = ip;
        //tm.track_regdate = DateTime.Now;
        //tm.track_staff_id = staff_id;
        //tm.track_url = url;
        //tm.Create();
        var model = new tb_track
        {
            track_comment = comment,
            track_ip = ip,
            track_regdate = DateTime.Now,
            track_staff_id = staff_id,
            track_url = url
        };
        context.tb_track.Add(model);
        context.SaveChanges();
    }

    public static DataTable FindModelsByCount(int count)
    {
        // 1. 程序跟踪纪录， 2. 系统错误纪录
        return FindModelsByCount(count, 1);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    /// <param name="track_type_id">1. 程序跟踪纪录， 2. 系统错误纪录</param>
    /// <returns></returns>
    public static DataTable FindModelsByCount(int count, int track_type_id)
    {
        return Config.ExecuteDataTable("select track_regdate, staff_realname, track_comment,track_ip from tb_track tt inner join tb_staff ts on ts.staff_serial_no=tt.track_staff_id where track_type_id="+ track_type_id.ToString() +" order by track_id desc limit 0, " + count.ToString());
    }


    public static void InsertInfo(nicklu2Entities context, string comment, int staff_id)
    {
        // 1. 程序跟踪纪录， 2. 系统错误纪录
        InsertInfo(context, comment, staff_id, 1);
    }

    public static void InsertInfo(nicklu2Entities context, string comment, int staff_id, int track_type_id)
    {
        try
        {
            //TrackModel tm = new TrackModel();
            //tm.track_comment = comment;
            //tm.track_url = System.Web.HttpContext.Current.Request.Url.ToString();
            //tm.track_staff_id = staff_id;
            //tm.track_regdate = DateTime.Now;
            //tm.track_ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //tm.track_type_id = track_type_id;
            //tm.Create();
            var model = new tb_track
            {
                track_comment = comment,
                track_url = System.Web.HttpContext.Current.Request.Url.ToString(),
                track_staff_id = staff_id,
                track_regdate = DateTime.Now,
                track_ip = System.Web.HttpContext.Current.Request.UserHostAddress,
                track_type_id = track_type_id
            };
            context.tb_track.Add(model);
            context.SaveChanges();
        }
        catch { }
    }
}
