using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class CateMenu
    {
        public string ParentTitle { get; set; }

        public int ParentCateId { get; set; }

        public string Title { get; set; }

        public List<LU.Model.Cate> SubCates { get; set; }

        public CateMenu(Data.nicklu2Entities context, int cateId)
        {
            Do(context, cateId);
        }

        void Do(Data.nicklu2Entities context, int cateId)
        {
            var allCate = LU.BLL.CacheProvider.GetAllCates(context);
            foreach (var cate in allCate)
            {
                foreach (var subCate in cate.SubCates)
                {
                    if (subCate.Id.Equals(cateId))
                    {
                        ParentTitle = cate.Title + "";// +cateModel.menu_child_name;
                        Title = subCate.Title;
                        SubCates = cate.SubCates;
                        ParentCateId = cate.Id;
                    }
                }
            }
        }
    }
}
