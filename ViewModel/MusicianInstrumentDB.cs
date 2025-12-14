using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MusicianInstrumentsDB : BaseDB
    {
        public MusicianInstrumentsList SelectAll()
        {
            command.CommandText = "SELECT * FROM MusicianInstruments";
            MusicianInstrumentsList list = new MusicianInstrumentsList(base.Select());
            return list;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            MusicianInstruments mi = entity as MusicianInstruments;
            if (mi == null)
                throw new ArgumentException("Entity must be of type MusicianInstruments", nameof(entity));

            mi.Musician = MusicianDB.SelectById(Convert.ToInt32(reader["Id_musician"]));
            mi.Instruments = InstrumentsDB.SelectById(Convert.ToInt32(reader["Id_instruments"]));


            base.CreateModel(entity);
            return mi;
        }

        public override BaseEntity NewEntity()
        {
            return new MusicianInstruments();
        }

        static private MusicianInstrumentsList list = new MusicianInstrumentsList();

        public static MusicianInstruments SelectById(int id)
        {
            foreach (MusicianInstruments mi in list)
            {
                if (mi.Id == id)
                    return mi;
            }

            MusicianInstrumentsDB db = new MusicianInstrumentsDB();
            list = db.SelectAll();
            MusicianInstruments g = list.Find(x => x.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicianInstruments mi = entity as MusicianInstruments;
            if (mi == null)
                throw new ArgumentException("Entity must be of type MusicianInstruments", nameof(entity));
            cmd.CommandText = "DELETE FROM MusicianInstruments WHERE Id=@Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", mi.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicianInstruments mi = entity as MusicianInstruments;
            if (mi == null)
                throw new ArgumentException("Entity must be of type MusicianInstruments", nameof(entity));
            cmd.CommandText = "INSERT INTO MusicianInstruments (Id_musician, Id_instruments) VALUES (?, ?)";
            cmd.Parameters.AddWithValue("@Id_musician", mi.Musician.Id);
            cmd.Parameters.AddWithValue("@Id_instruments", mi.Instruments.Id);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MusicianInstruments mi = entity as MusicianInstruments;
            if (mi == null)
                throw new ArgumentException("Entity must be of type MusicianInstruments", nameof(entity));
            cmd.CommandText = "UPDATE MusicianInstruments SET Id_musician = ?, Id_instruments = ? WHERE Id = ?";
            cmd.Parameters.AddWithValue("@Id_musician", mi.Musician.Id);
            cmd.Parameters.AddWithValue("@Id_instruments", mi.Instruments.Id);
            cmd.Parameters.AddWithValue("@Id", mi.Id);

        }
    }



}
