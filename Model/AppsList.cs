using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AppsList : List<Apps>
    {
        public AppsList() { }
        public AppsList(IEnumerable<Apps> apps) : base(apps) { }
        public AppsList(IEnumerable<BaseEntity> apps) : base(apps.Cast<Apps>().ToList()) { }
    }
}
