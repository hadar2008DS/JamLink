using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // 1:1 with Person and 1:N with Group
    public class GroupMembers : Person  
    {
        
        private Group group;
        public Group Group { get => group; set => group = value; }

        public override string ToString()
        {
            return $"GroupMembers: {Id}, Group: {Group?.GroupName}";
        }

    }
}
