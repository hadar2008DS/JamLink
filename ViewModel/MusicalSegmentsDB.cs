using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MusicalSegmentsDB : BaseDB
    {
        public MusicalSegmentsList SelectAll()
        {
            command.CommandText = "SELECT * FROM MusicalSegments";
            MusicalSegmentsList list = new MusicalSegmentsList(base.Select());
            return list;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            MusicalSegments segment = entity as MusicalSegments;
            if (segment == null)
                throw new ArgumentException("Entity must be of type MusicalSegment", nameof(entity));

            segment.Lengthinseconds = Convert.ToInt32(reader["Lengthinseconds"]);
            segment.Musician = MusicianDB.SelectById(Convert.ToInt32(reader["Id_musician"]));
            segment.Instruments =InstrumentsDB.SelectById((Convert.ToInt32(reader["Id_instrument"])));
            segment.Link = reader["Link"]?.ToString();
            segment.SegmentName = reader["SegmentName"].ToString()!;
            segment.Genre = reader["Genre"]?.ToString();
            segment.Mood = reader["Mood"]?.ToString();
            segment.Key = reader["Key"]?.ToString();
            segment.Bpm = reader["BPM"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["Bpm"]);

            base.CreateModel(entity);
            return segment;
        }

        public override BaseEntity NewEntity()
        {
            return new MusicalSegments();
        }

        static private MusicalSegmentsList list = new MusicalSegmentsList();

        public static MusicalSegments SelectById(int id)
        {
            foreach (MusicalSegments segment in list)
            {
                if (segment.Id == id)
                    return segment;
            }

            MusicalSegmentsDB db = new MusicalSegmentsDB();
            list = db.SelectAll();
            MusicalSegments result = list.Find(x => x.Id == id);
            return result;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicalSegments segment = entity as MusicalSegments;
            if (segment == null)
                throw new ArgumentException("Entity must be of type MusicalSegments", nameof(entity));

            cmd.CommandText = $"DELETE FROM MusicalSegments WHERE Id=@Id";
            //cmd.Parameters.AddWithValue("@SegmentName", segment.SegmentName);
            //cmd.Parameters.AddWithValue("@Lengthinseconds", segment.Lengthinseconds);
            //cmd.Parameters.AddWithValue("@Id_musician", segment.Musician.Id);
            //cmd.Parameters.AddWithValue("@Id_instrument", segment.Instruments.Id);
            //cmd.Parameters.AddWithValue("@Link", segment.Link ?? (object)DBNull.Value);
            //cmd.Parameters.AddWithValue("@Genre", segment.Genre ?? (object)DBNull.Value);
            //cmd.Parameters.AddWithValue("@Mood", segment.Mood ?? (object)DBNull.Value);
            //cmd.Parameters.AddWithValue("@Key", segment.Key ?? (object)DBNull.Value);
            //cmd.Parameters.AddWithValue("@Bpm", segment.Bpm.HasValue ? (object)segment.Bpm.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", segment.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicalSegments segment = entity as MusicalSegments;
            if (segment == null)
                throw new ArgumentException("Entity must be of type MusicalSegment", nameof(entity));
            cmd.CommandText = $"INSERT INTO MusicalSegments (SegmentName, LengthInSeconds, Id_musician, Id_instrument, Link, Genre, Mood, [Key], Bpm) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
            cmd.Parameters.AddWithValue("@SegmentName", segment.SegmentName);
            cmd.Parameters.AddWithValue("@Lengthinseconds", segment.Lengthinseconds);
            cmd.Parameters.AddWithValue("@Id_musician", segment.Musician.Id);
            cmd.Parameters.AddWithValue("@Id_instrument", segment.Instruments.Id);
            cmd.Parameters.AddWithValue("@Link", segment.Link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Genre", segment.Genre ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Mood", segment.Mood ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Key", segment.Key ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Bpm", segment.Bpm.HasValue ? (object)segment.Bpm.Value : DBNull.Value);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicalSegments segment = entity as MusicalSegments;
            if (segment == null)
                throw new ArgumentException("Entity must be of type MusicalSegment", nameof(entity));
            cmd.CommandText = $"UPDATE MusicalSegments SET SegmentName = ?, LengthInSeconds = ?, Id_musician = ?, Id_instrument = ?, Link = ?, Genre = ?, Mood = ?, [Key] = ?, Bpm = ? WHERE Id = ?";
            cmd.Parameters.AddWithValue("@SegmentName", segment.SegmentName);
            cmd.Parameters.AddWithValue("@Lengthinseconds", segment.Lengthinseconds);
            cmd.Parameters.AddWithValue("@Id_musician", segment.Musician.Id);
            cmd.Parameters.AddWithValue("@Id_instrument", segment.Instruments.Id);
            cmd.Parameters.AddWithValue("@Link", segment.Link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Genre", segment.Genre ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Mood", segment.Mood ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Key", segment.Key ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Bpm", segment.Bpm.HasValue ? (object)segment.Bpm.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", segment.Id);
        }
    }

}
