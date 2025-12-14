using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class InstrumentsDB : BaseDB
    {
        public override BaseEntity NewEntity()
        {
            return new Instruments();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Instruments instrument = entity as Instruments;
            if (instrument == null)
            {
                throw new ArgumentException("Entity must be of type Instruments", nameof(entity));
            }

            instrument.InstrumentName = reader["InstrumentName"]?.ToString();

            base.CreateModel(entity);
            return instrument;
        }

        public InstrumentsList SelectAll()
        {
            
            command.CommandText = "SELECT * FROM Instruments";
            InstrumentsList iList = new InstrumentsList(base.Select());
            return iList;
        }

        static private InstrumentsList list = new InstrumentsList();

        public static Instruments SelectById(int id)
        {
            foreach (Instruments i in list)
            {
                if (i.Id == id)
                    return i;
            }
            InstrumentsDB db = new InstrumentsDB();
            list = db.SelectAll();
            Instruments g = list.Find(x => x.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            throw new Exception("Not a needed requriement for the app");
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Instruments instrument = entity as Instruments;
            if (instrument == null)
                throw new ArgumentException("Entity must be of type Instruments", nameof(entity));
            cmd.CommandText = "INSERT INTO Instruments (InstrumentName) VALUES (@InstrumentName)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InstrumentName", instrument.InstrumentName);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Instruments instrument = entity as Instruments;
            if (instrument == null)
                throw new ArgumentException("Entity must be of type Instruments", nameof(entity));
            cmd.CommandText = "UPDATE Instruments SET InstrumentName=@InstrumentName WHERE Id=@Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InstrumentName", instrument.InstrumentName);
            cmd.Parameters.AddWithValue("@Id", instrument.Id);
        }

    }
    
}
