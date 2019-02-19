// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-22 23:15:02
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_computer_case")]
[Serializable]
public class ComputerCaseModel : ActiveRecordBase<ComputerCaseModel>
{
    int _computer_cases_id;
    int _computer_case_category;

    public ComputerCaseModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int computer_cases_id
    {
        get { return _computer_cases_id; }
        set { _computer_cases_id = value; }
    }
    public static ComputerCaseModel GetComputerCaseModel(int _computer_cases_id)
    {
        ComputerCaseModel[] models = ComputerCaseModel.FindAllByProperty("computer_cases_id", _computer_cases_id);
        if (models.Length == 1)
            return models[0];
        else
            return new ComputerCaseModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int computer_case_category
    {
        get { return _computer_case_category; }
        set { _computer_case_category = value; }
    }
}
