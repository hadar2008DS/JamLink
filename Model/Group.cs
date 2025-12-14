using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Group : BaseEntity
    {
        private string groupName;
        private DateTime? creationDate;
        private bool isActive;

        public string GroupName { get => groupName; set => groupName = value; }
        public DateTime? CreationDate { get => creationDate; set => creationDate = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public override string ToString()
        {
            return $"Id: {Id}, GroupName: {GroupName}, CreationDate: {CreationDate}, IsActive: {IsActive}";
        }
        
    }
}
