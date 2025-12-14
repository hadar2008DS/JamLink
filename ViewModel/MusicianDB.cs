using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    
    public class MusicianDB : PersonDB
    {
        public override BaseEntity NewEntity()
        {
            return new Musician();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Musician musician = entity as Musician;
            if (musician == null)
            { throw new ArgumentException("Entity must be of type Musician", nameof(entity)); }
            musician.IsActive = Convert.ToBoolean(reader["isact"]);
            base.CreateModel(entity);
            return musician;
        }

        public MusicianList SelectAll()
        {
            // Select all musicians from the database
            //יש כפילות בשדה IsActive ולכן ניתן לו שם אחר כדי שהקורא לא יתבלבל בשדות
            command.CommandText = "SELECT Musician.IsActive, Person.Id, Person.UserName, Person.PassW , Person.isActive as isact " +
                      "FROM ( Person INNER JOIN Musician ON Person.Id = Musician.Id)";
            MusicianList mList = new MusicianList(base.Select());
            return mList;
        }

        static private MusicianList list = new MusicianList();

        public static Musician SelectById(int id)
        {
            // Select a musician by their ID
            foreach (Musician m in list)
            {
                if (m.Id == id)
                    return m;
            }
            MusicianDB db = new MusicianDB();
            list = db.SelectAll();
            Musician g = list.Find(x => x.Id == id);
            return g;
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));
            cmd.CommandText = "UPDATE Musician SET IsActive=@IsActive WHERE Id=@Id";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IsActive", musician.IsActive);
            cmd.Parameters.AddWithValue("@Id", musician.Id);
        }
        public override void Update(BaseEntity entity)
        {
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));

            updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
            updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));
            cmd.CommandText = "INSERT INTO Musician (Id, IsActive) VALUES (@Id, @IsActive)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", musician.Id);
            cmd.Parameters.AddWithValue("@IsActive", musician.IsActive);
        }

        public override void Insert(BaseEntity entity)
        {
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));
            inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
            inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));

        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            // Soft Delete implementation that sets isActive to false
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));
            cmd.CommandText = "DELETE FROM Musician WHERE Id=@Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", musician.Id);
        }
        public override void Delete(BaseEntity entity) 
        {
            Musician musician = entity as Musician;
            if (musician == null)
                throw new ArgumentException("Entity must be of type Musician", nameof(entity));
            deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
        }
    }
}
