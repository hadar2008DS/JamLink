using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Test
{
    public class ProducerDB : PersonDB
    {
        public PreducerList SelectAll()
        {
            // Select all producers from the database
            command.CommandText = "SELECT Producer.IsActive, Person.Id, Person.UserName, Person.PassW, Person.isActive AS Expr1 " +
                      "FROM(Person INNER JOIN Producer ON Person.Id = Producer.Id)";
            PreducerList pList = new PreducerList(base.Select());
            return pList;
        }

        public override BaseEntity NewEntity()
        {
            return new Producer();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Producer producer = entity as Producer;
            if (producer == null)
            { throw new ArgumentException("Entity must be of type Producer", nameof(entity)); }
            producer.IsActive = Convert.ToBoolean(reader["IsActive"]);
            base.CreateModel(entity);
            return producer;
        }

        static private PreducerList pList = new PreducerList();
        public static Producer SelectById(int id)
        {
            // Select a producer by their ID
            foreach (Producer p in pList)
            {
                if (p.Id == id)
                    return p;
            }
            ProducerDB db = new ProducerDB();
            pList = db.SelectAll();
            Producer g = pList.Find(x => x.Id == id);
            return g;
        }
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));
            cmd.CommandText = "DELETE FROM Producer WHERE Id=@Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", producer.Id);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));

            
            cmd.CommandText = "UPDATE Producer SET IsActive=@IsActive WHERE Id=@Id";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IsActive", producer.IsActive);
            cmd.Parameters.AddWithValue("@Id", producer.Id);
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));
            cmd.CommandText = $"INSERT INTO Producer (Id, IsActive) VALUES (@Id, @IsActive)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", producer.Id);
            cmd.Parameters.AddWithValue("@IsActive", producer.IsActive);
        }

        public override void Update(BaseEntity entity)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));
            updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
            updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));

        }
        public override void Insert(BaseEntity entity)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));
            inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
            inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
        }

        public override void Delete(BaseEntity entity)
        {
            Producer producer = entity as Producer;
            if (producer == null)
                throw new ArgumentException("Entity must be of type Producer", nameof(entity));
            deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
        }



    }
}
