using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PreducerList : List<Producer>
    {
        public PreducerList() { }
        public PreducerList(IEnumerable<Producer> producers) : base(producers) { }
        public PreducerList(IEnumerable<BaseEntity> producers) : base(producers.Cast<Producer>().ToList()) { }
    }
    
}
