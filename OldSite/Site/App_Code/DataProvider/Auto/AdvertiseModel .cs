
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2/8/2009 4:13:54 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class AdvertiseModel 
{

    //public AdvertiseModel()
    //{

    //}

    public static tb_advertise GetAdvertiseModel(nicklu2Entities context, int _advertise_serial_no)
    {
        //AdvertiseModel[] models = AdvertiseModel.FindAllByProperty("advertise_serial_no", _advertise_serial_no);
        //if (models.Length == 1)
        //    return models[0];
        //else
        //    return new AdvertiseModel();
        return context.tb_advertise.Single(me => me.advertise_serial_no.Equals(_advertise_serial_no));
    }

    //int _advertise_serial_no;
    //[PrimaryKey(PrimaryKeyType.Identity)]
    //public int advertise_serial_no
    //{
    //    get { return _advertise_serial_no; }
    //    set { _advertise_serial_no = value; }
    //}

    //string _image_file_name;
    //[Property]
    //public string image_file_name
    //{
    //    get { return _image_file_name; }
    //    set { _image_file_name = value; }
    //}

    //string _advertise_link_url;

    //[Property]
    //public string advertise_link_url
    //{
    //    get { return _advertise_link_url; }
    //    set { _advertise_link_url = value; }
    //}

    //int _advertise_type;

    //[Property]
    //public int advertise_type
    //{
    //    get { return _advertise_type; }
    //    set { _advertise_type = value; }
    //}
}

