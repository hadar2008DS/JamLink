using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProducerAppsList : List<ProducerApps>
    {
        public ProducerAppsList() { }
        public ProducerAppsList(IEnumerable<ProducerApps> producerApps) : base(producerApps) { }
        public ProducerAppsList(IEnumerable<BaseEntity> producerApps) : base(producerApps.Cast<ProducerApps>().ToList()) { }
    }
    
}
