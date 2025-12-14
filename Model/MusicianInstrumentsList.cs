using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MusicianInstrumentsList : List<MusicianInstruments>
    {
        public MusicianInstrumentsList() { }
        public MusicianInstrumentsList(IEnumerable<MusicianInstruments> collection) : base(collection) { }
        public MusicianInstrumentsList(IEnumerable<BaseEntity> collection) : base(collection.Cast<MusicianInstruments>().ToList()) { }
    }
}
