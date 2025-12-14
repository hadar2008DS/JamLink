using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupMembersList : List<GroupMembers>
    {
        public GroupMembersList() { }
        public GroupMembersList(IEnumerable<GroupMembers> groupMembers) : base(groupMembers) { }
        public GroupMembersList(IEnumerable<BaseEntity> groupMembers) : base(groupMembers.Cast<GroupMembers>().ToList()) { }
    }
    
}
