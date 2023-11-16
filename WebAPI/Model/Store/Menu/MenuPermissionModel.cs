using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Store.Menu
{
    public class MenuPermissionViewModel 
    {
        public int?  MenuId { get; set; }
        public string? MenuName { get; set; }
        public int? ParentId { get; set; }
        public int? MenuPermissionId { get; set; } 
        public bool? IsPermission { get; set; }	
        public bool? IsInsert { get; set; }	
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
         public string? IconName { get; set; }
        public string? ClassName { get; set; }
        public int? SLNo { get; set; }
        public string? MenuURL { get; set; }
        public List<MenuPermissionViewModel>? MenuList { get; set; } 
    }
    public class TypeMenuPermissionModel 
    {
        public int? MenuPermissionId { get; set; } 
        public bool? IsPermission { get; set; }
        public bool? IsInsert { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
