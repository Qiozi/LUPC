﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteModels.View
{
    [Serializable]
    public class SysList
    {
        public SysList() { }

        public string ImgUrl { get; set; }

        public string Name { get; set; }

        public int Sku { get; set; }

        public int CateId { get; set; }

        public decimal price { get; set; }

        public decimal discount { get; set; }

        public decimal sell { get; set; }


        public List<SysListDetail> DetailList { get; set; }

    }

    public class SysListDetail
    {
        public SysListDetail() { }

        public int CommentId { get; set; }

        public string CommentName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int PartSku { get; set; }

        public string PartName { set; get; }
    }
}
