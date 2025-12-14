using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MusicalSegmentsList : List<MusicalSegments>
    {
        public MusicalSegmentsList() { }
        public MusicalSegmentsList(IEnumerable<MusicalSegments> musicalSegments) : base(musicalSegments) { }
        public MusicalSegmentsList(IEnumerable<BaseEntity> musicalSegments) : base(musicalSegments.Cast<MusicalSegments>().ToList()) { }
    }
    
}
