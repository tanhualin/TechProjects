using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDataCenter.Models.SystemManage
{
    public class SmartPagesModel
    {
        /// <summary>[ModuleName]</summary>
        public virtual string PageAction { get; set; }

        /// <summary>[Idx]</summary>
        public virtual int Idx { get; set; }

        /// <summary>[ParentIdx]</summary>
        public virtual string Link { get; set; }

        /// <summary>[Description]</summary>
        public virtual string Description { get; set; }

        /// <summary>[State]</summary>
        public virtual int State { get; set; }

        /// <summary>[CreateOn]</summary>
        public virtual System.DateTime CreateOn { get; set; }

        /// <summary>[CreateBy]</summary>
        public virtual string CreateBy { get; set; }

        /// <summary>[LastModifyOn]</summary>
        public virtual System.DateTime LastModifyOn { get; set; }

        /// <summary>[LastModifyBy]</summary>
        public virtual string LastModifyBy { get; set; }
    }
}
