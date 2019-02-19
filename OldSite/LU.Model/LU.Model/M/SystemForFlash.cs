using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.M
{
    public class SystemForFlash : SystemBase
    {
        public int DetailId { get; set; }

        public int PartGroupId { get; set; }

        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public bool IsLabelOfFlash { get; set; }

        public bool IsMotherboard { get; set; }

        public bool IsCpu { get; set; }

        public bool IsVideo { get; set; }

        public bool IsAudio { get; set; }

        public bool IsNetwork { get; set; }

        public bool IsCpuFan { get; set; }
        
        public List<SystemSubPart> SubParts { get; set; }
    }
}
