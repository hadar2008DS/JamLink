using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Instruments : BaseEntity
    {
        private string instrumentName;
        public string InstrumentName { get => instrumentName; set => instrumentName = value; }

        public override string ToString()
        {
            return $"Id: {Id}, InstrumentName: {InstrumentName}";
        }
    }
}
