using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupList : List<Group>
    {
        public GroupList() { }
        public GroupList(IEnumerable<Group> groups) : base(groups) { }
        public GroupList(IEnumerable<BaseEntity> groups) : base(groups.Cast<Group>().ToList()) { }
    }
}
