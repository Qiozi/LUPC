// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-15 0:15:24
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_track")]
public class TrackModel : ActiveRecordBase<TrackModel>
{
    int _track_id;
    DateTime _track_regdate;
    string _track_comment;
    int _track_staff_id;
    string _track_url;
    string _track_ip;
    int _track_type_id;

    public TrackModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int track_id
    {
        get { return _track_id; }
        set { _track_id = value; }
    }
    public static TrackModel GetTrackModel(int _track_id)
    {
        TrackModel[] models = TrackModel.FindAllByProperty("track_id", _track_id);
        if (models.Length == 1)
            return models[0];
        else
            return new TrackModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime track_regdate
    {
        get { return _track_regdate; }
        set { _track_regdate = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string track_comment
    {
        get { return _track_comment; }
        set { _track_comment = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int track_staff_id
    {
        get { return _track_staff_id; }
        set { _track_staff_id = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string track_url
    {
        get { return _track_url; }
        set { _track_url = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string track_ip
    {
        get { return _track_ip; }
        set { _track_ip = value; }
    }
[Property]
    public int track_type_id
    {
        get { return _track_type_id; }
        set { _track_type_id = value; }
    }
    public static void Insert(string comment, int staff_id, string ip, string url)
    {
        TrackModel tm = new TrackModel();
        tm.track_comment = comment;
        tm.track_ip = ip;
        tm.track_regdate = DateTime.Now;
        tm.track_staff_id = staff_id;
        tm.track_url = url;
        tm.Create();
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


    public static void InsertInfo(string comment, int staff_id)
    {
        // 1. 程序跟踪纪录， 2. 系统错误纪录
        InsertInfo(comment, staff_id, 1);
    }

    public static void InsertInfo(string comment, int staff_id, int track_type_id)
    {
        try
        {
            TrackModel tm = new TrackModel();
            tm.track_comment = comment;
            tm.track_url = System.Web.HttpContext.Current.Request.Url.ToString();
            tm.track_staff_id = staff_id;
            tm.track_regdate = DateTime.Now;
            tm.track_ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            tm.track_type_id = track_type_id;
            tm.Create();
        }
        catch { }
    }
}
