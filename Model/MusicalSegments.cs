using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // Many: Many with Musician and Instruments
    public class MusicalSegments : BaseEntity
    {
        private int lengthinseconds;
        private Musician musician;
        private Instruments? instruments;
        private string? link;
        private string segmentName;
        private string? genre;
        private string? mood;
        private string? key;
        private int? bpm;
        public int Lengthinseconds { get => lengthinseconds; set => lengthinseconds = value; }
        public Musician Musician { get => musician; set => musician = value; }
        public Instruments? Instruments { get => instruments; set => instruments = value; }
        public string? Link { get => link; set => link = value; }
        public string SegmentName { get => segmentName; set => segmentName = value; }
        public string? Genre { get => genre; set => genre = value; }
        public string? Mood { get => mood; set => mood = value; }
        public string? Key { get => key; set => key = value; }
        public int? Bpm { get => bpm; set => bpm = value; }

        public override string ToString()
        {
            return $"Id: {Id}, SegmentName: {SegmentName}, Lengthinseconds: {Lengthinseconds}, Genre: {Genre}, Mood: {Mood}, Key: {Key}, Bpm: {Bpm}, Link: {Link}, Instruments: {Instruments}, Musician: {Musician?.Username}";
        }
    }
}
