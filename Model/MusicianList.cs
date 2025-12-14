using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MusicianList : List<Musician>
    {
        public MusicianList() { }
        public MusicianList(IEnumerable<Musician> musicians) : base(musicians) { }
        public MusicianList(IEnumerable<BaseEntity> musicians) : base(musicians.Cast<Musician>().ToList()) { }
    }
}
