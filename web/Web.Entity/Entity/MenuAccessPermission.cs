using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class MenuAccessPermission
    {
        public int MenuAccessPermissionId { get; set; }

        [Required]
        public int MenuId { get; set; }
        
        [Required]
        public int StaffId { get; set; }

        public bool ReadAccess { get; set; }

        public bool WriteAccess { get; set; }

        public bool ModifyAccess { get; set; }

        public bool DeleteAccess { get; set; }

        public bool AdminAccess { get; set; }
    }
}
