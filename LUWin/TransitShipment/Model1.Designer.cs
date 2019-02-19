﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace TransitShipment
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class nicklu2Entities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new nicklu2Entities object using the connection string found in the 'nicklu2Entities' section of the application configuration file.
        /// </summary>
        public nicklu2Entities() : base("name=nicklu2Entities", "nicklu2Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new nicklu2Entities object.
        /// </summary>
        public nicklu2Entities(string connectionString) : base(connectionString, "nicklu2Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new nicklu2Entities object.
        /// </summary>
        public nicklu2Entities(EntityConnection connection) : base(connection, "nicklu2Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<tb_timer> tb_timer
        {
            get
            {
                if ((_tb_timer == null))
                {
                    _tb_timer = base.CreateObjectSet<tb_timer>("tb_timer");
                }
                return _tb_timer;
            }
        }
        private ObjectSet<tb_timer> _tb_timer;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tb_timer EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotb_timer(tb_timer tb_timer)
        {
            base.AddObject("tb_timer", tb_timer);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="nicklu2Model", Name="tb_timer")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tb_timer : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new tb_timer object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="cmdContent">Initial value of the CmdContent property.</param>
        /// <param name="cmd">Initial value of the Cmd property.</param>
        /// <param name="regdate">Initial value of the regdate property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        public static tb_timer Createtb_timer(global::System.Int32 id, global::System.String cmdContent, global::System.String cmd, global::System.DateTime regdate, global::System.Int32 status)
        {
            tb_timer tb_timer = new tb_timer();
            tb_timer.ID = id;
            tb_timer.CmdContent = cmdContent;
            tb_timer.Cmd = cmd;
            tb_timer.regdate = regdate;
            tb_timer.Status = status;
            return tb_timer;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value, "ID");
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CmdContent
        {
            get
            {
                return _CmdContent;
            }
            set
            {
                OnCmdContentChanging(value);
                ReportPropertyChanging("CmdContent");
                _CmdContent = StructuralObject.SetValidValue(value, false, "CmdContent");
                ReportPropertyChanged("CmdContent");
                OnCmdContentChanged();
            }
        }
        private global::System.String _CmdContent;
        partial void OnCmdContentChanging(global::System.String value);
        partial void OnCmdContentChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Cmd
        {
            get
            {
                return _Cmd;
            }
            set
            {
                OnCmdChanging(value);
                ReportPropertyChanging("Cmd");
                _Cmd = StructuralObject.SetValidValue(value, false, "Cmd");
                ReportPropertyChanged("Cmd");
                OnCmdChanged();
            }
        }
        private global::System.String _Cmd;
        partial void OnCmdChanging(global::System.String value);
        partial void OnCmdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime regdate
        {
            get
            {
                return _regdate;
            }
            set
            {
                OnregdateChanging(value);
                ReportPropertyChanging("regdate");
                _regdate = StructuralObject.SetValidValue(value, "regdate");
                ReportPropertyChanged("regdate");
                OnregdateChanged();
            }
        }
        private global::System.DateTime _regdate;
        partial void OnregdateChanging(global::System.DateTime value);
        partial void OnregdateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value, "Status");
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.Int32 _Status;
        partial void OnStatusChanging(global::System.Int32 value);
        partial void OnStatusChanged();

        #endregion

    }

    #endregion

}
