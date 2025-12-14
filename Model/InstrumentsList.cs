using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InstrumentsList : List<Instruments>
    {
        public InstrumentsList() { }
        public InstrumentsList(IEnumerable<Instruments> instruments) : base(instruments) { }
        public InstrumentsList(IEnumerable<BaseEntity> instruments) : base(instruments.Cast<Instruments>().ToList()) { }
    }
    
}
