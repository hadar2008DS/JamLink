using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    // Data access class for Person entities
    // Inherits from BaseDB to utilize common database functionalities
    public class PersonDB : BaseDB
    {
        public PersonList SelectAll()
        {
            command.CommandText = "SELECT * FROM Person";
            PersonList pList = new PersonList(base.Select());  
            return pList;
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person == null)
            { throw new ArgumentException("Entity must be of type Person", nameof(entity)); }

            person.Username = reader["Username"]?.ToString();
            person.PassW = reader["PassW"]?.ToString();
            person.IsActive = Convert.ToBoolean(reader["IsActive"]);

            base.CreateModel(entity);
            return person;
        }
        public override BaseEntity NewEntity()
        {
            return new Person();
        }


        static private PersonList list = new PersonList();
        public static Person SelectById(int id)
        {
            foreach (Person p in list)
            {
                if (p.Id == id)
                    return p;
            }
            PersonDB db = new PersonDB();
            list = db.SelectAll();
            Person g = list.Find(x => x.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            // Soft Delete implementation that sets isActive to false
            Person person = entity as Person;
            if (person == null)
                throw new ArgumentException("Entity must be of type Person", nameof(entity));
            cmd.CommandText = "DELETE FROM Person WHERE Id=@Id";
            cmd.Parameters.Clear();
            
            cmd.Parameters.AddWithValue("@Id", person.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person person = entity as Person;
            if (person == null)
                throw new ArgumentException("Entity must be of type Person", nameof(entity));
            cmd.CommandText = "INSERT INTO Person (UserName, PassW, isActive) VALUES (@UserName, @PassW, @isActive)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserName", person.Username);
            cmd.Parameters.AddWithValue("@PassW", person.PassW);
            cmd.Parameters.AddWithValue("@isActive", person.IsActive);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person person = entity as Person;
            if (person == null)
                throw new ArgumentException("Entity must be of type Person", nameof(entity));

            cmd.CommandText = "UPDATE Person SET UserName=@UserName, PassW=@PassW, isActive=@isActive WHERE Id=@Id";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserName", person.Username);
            cmd.Parameters.AddWithValue("@PassW", person.PassW);
            cmd.Parameters.AddWithValue("@isActive", person.IsActive);
            cmd.Parameters.AddWithValue("@Id", person.Id);
        }
    }
}
