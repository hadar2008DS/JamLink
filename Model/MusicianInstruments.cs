using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // Many:Many with Musician and Instruments
    public class MusicianInstruments : BaseEntity
    {
        private Musician musician;
        private Instruments instruments;

        public Musician Musician { get => musician; set => musician = value; }
        public Instruments Instruments { get => instruments; set => instruments = value; }

        public override string ToString()
        {
            return $"Id: {Id}, Musician: {Musician}, Instruments: {Instruments}";
        }

    }
}
