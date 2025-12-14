using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AppsDB : BaseDB
    {
        public override BaseEntity NewEntity()
        {
            return new Apps();
        }

        public AppsList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Apps";
            //SELECT Id, AppName FROM Apps
            AppsList appList = new AppsList(base.Select());
            return appList;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Apps app = entity as Apps;
            app.AppName = reader["AppName"].ToString();
            base.CreateModel(entity);
            return entity;
        }



        static private AppsList list = new AppsList();
        public static Apps SelectById(int id)
        {
            foreach (Apps app in list)
            {
                if (app.Id == id)
                    return app;
            }
            AppsDB db = new AppsDB();
            list = db.SelectAll();
            Apps result = list.Find(x => x.Id == id);
            return result;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            //there is no delete for Apps
            throw new NotImplementedException();
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Apps app = entity as Apps;
            if (app == null)
                throw new ArgumentException("Entity must be of type Apps", nameof(entity));
            cmd.CommandText = "INSERT INTO Apps (AppName) VALUES (@AppName)";
            cmd.Parameters.AddWithValue("@AppName", app.AppName);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Apps app = entity as Apps;
            if (app == null)
                throw new ArgumentException("Entity must be of type Apps", nameof(entity));
            cmd.CommandText = "UPDATE Apps SET AppName=@AppName WHERE Id=@Id";
            cmd.Parameters.AddWithValue("@AppName", app.AppName);
            cmd.Parameters.AddWithValue("@Id", app.Id);
        }
    }
}
