using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class UserDocumentDto
    {
        public int UserDocumentId { get; set; }
        public int? StaffId { get; set; }
        public int? MemberId { get; set; }
        public int? MemberPhotoStorageId { get; set; }
        public byte[] MemberPhoto { get; set; }
        public string MemberPhotoString { get; set; }
        public string MemberPhotoLocation { get; set; }
        public string CitizenshipFront { get; set; }
        public string CitizenshipBack { get; set; }
        public string PanCard { get; set; }
        public string EducationalDocument { get; set; }
    }
}
