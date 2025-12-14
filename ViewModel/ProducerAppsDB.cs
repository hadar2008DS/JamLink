using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace ViewModel
{
    public class ProducerAppsDB : BaseDB
    {
        public ProducerAppsList SelectAll()
        {
            command.CommandText = "SELECT * FROM ProducerApps";
            ProducerAppsList paList = new ProducerAppsList(base.Select());
            return paList;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            ProducerApps pa = entity as ProducerApps;
            if (pa == null)
            {
                throw new ArgumentException("Entity must be of type ProducerApps", nameof(entity));
            }

            pa.Producer = ProducerDB.SelectById((int)reader["Id_producer"]);
            pa.Apps = AppsDB.SelectById((int)reader["Id_app"]);

            base.CreateModel(entity);
            return pa;
        }

        public override BaseEntity NewEntity()
        {
            return new ProducerApps();
        }

        static private ProducerAppsList list = new ProducerAppsList();

        public static ProducerApps SelectById(int id)
        {
            foreach (ProducerApps pa in list)
            {
                if (pa.Id == id)
                    return pa;
            }

            ProducerAppsDB db = new ProducerAppsDB();
            list = db.SelectAll();
            ProducerApps g = list.Find(x => x.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            ProducerApps pa = entity as ProducerApps;
            if (pa == null)
                throw new ArgumentException("Entity must be of type ProducerApps", nameof(entity));
            cmd.CommandText = "DELETE FROM ProducerApps WHERE Id=@Id";
            cmd.Parameters.AddWithValue("@Id", pa.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            ProducerApps pa = entity as ProducerApps;
            if (pa == null)
                throw new ArgumentException("Entity must be of type ProducerApps", nameof(entity));
            cmd.CommandText = "INSERT INTO ProducerApps (Id_producer, Id_app) VALUES (?, ?)";
            cmd.Parameters.AddWithValue("@Id_producer", pa.Producer.Id);
            cmd.Parameters.AddWithValue("@Id_app", pa.Apps.Id);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            ProducerApps pa = entity as ProducerApps;
            if (pa == null)
                throw new ArgumentException("Entity must be of type ProducerApps", nameof(entity));
            cmd.CommandText = "UPDATE ProducerApps SET Id_producer = ?, Id_app = ? WHERE Id = ?";
            cmd.Parameters.AddWithValue("@Id_producer", pa.Producer.Id);
            cmd.Parameters.AddWithValue("@Id_app", pa.Apps.Id);
            cmd.Parameters.AddWithValue("@Id", pa.Id);
        }
    }
}
