﻿// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-22 23:15:02
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System.Linq;
using System;

[Serializable]
public class ComputerCpuModel 
{
    //int _computer_cpu_id;
    //int _computer_cpu_category;

    //public ComputerCpuModel()
    //{

    //}


    ///// <summary>
    ///// 
    ///// </summary>
    //[PrimaryKey(PrimaryKeyType.Identity)]
    //public int computer_cpu_id
    //{
    //    get { return _computer_cpu_id; }
    //    set { _computer_cpu_id = value; }
    //}
    public static tb_computer_cpu GetComputerCpuModel(nicklu2Entities context,int computer_cpu_id)
    {
        //ComputerCpuModel[] models = ComputerCpuModel.FindAllByProperty("computer_cpu_id", computer_cpu_id);
        //if (models.Length == 1)
        //    return models[0];
        //else
        //    return new ComputerCpuModel();
        return context.tb_computer_cpu.Single(me => me.computer_cpu_id.Equals(computer_cpu_id));
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public int computer_cpu_category
    //{
    //    get { return _computer_cpu_category; }
    //    set { _computer_cpu_category = value; }
    //}
}
