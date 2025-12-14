using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModel
{
    public class GroupDB : BaseDB
    {
        public override BaseEntity NewEntity()
        {
            return new Group();
        }

        public GroupList SelectAll()
        {
            command.CommandText = "SELECT * FROM [Group]";
            GroupList list = new GroupList(base.Select());
            return list;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Group g = entity as Group;
            g.GroupName = (string)reader["GroupName"];
            g.CreationDate = (DateTime?)reader["CreationDate"];
            g.IsActive = Convert.ToBoolean(reader["IsActive"]);
            base.CreateModel(entity);
            return entity;
        }

        static private GroupList list = new GroupList();

        public static Group SelectById(int id)
        {
            foreach (Group g in list)
            {
                if (g.Id == id)
                    return g;
            }
            GroupDB db = new GroupDB();
            list = db.SelectAll();
            Group result = list.Find(x => x.Id == id);
            return result;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            // Soft Delete implementation that sets isActive to false
            Group group = entity as Group;
            if (group == null)
                throw new ArgumentException("Entity must be of type Group", nameof(entity));
            else
            {
                cmd.CommandText = " DELETE FROM [Group] WHERE Id=@Id";
                command.Parameters.AddWithValue("@Id", group.Id);
            }

            //GroupName=@GroupName, CreationDate=@CreationDate, IsActive=@IsActive 
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Group group = entity as Group;
            if (group == null)
                throw new ArgumentException("Entity must be of type Group", nameof(entity));
            cmd.CommandText = "UPDATE [Group] SET GroupName=@GroupName, CreationDate=@CreationDate, IsActive=@IsActive WHERE Id=@Id";
            cmd.Parameters.Clear();
            // GroupName
            cmd.Parameters.Add(new OleDbParameter("@GroupName", OleDbType.VarChar)
            {
                Value = group.GroupName
            });

            // CreationDate
            OleDbParameter dateParam = new OleDbParameter("@CreationDate", OleDbType.DBDate);
            dateParam.Value = group.CreationDate.HasValue ? group.CreationDate.Value : DateTime.Now;
            cmd.Parameters.Add(dateParam);

            // isActive
            cmd.Parameters.Add(new OleDbParameter("@IsActive", OleDbType.Boolean)
            {
                Value = group.IsActive
            });

            // Id
            cmd.Parameters.Add(new OleDbParameter("@Id", OleDbType.Integer)
            {
                Value = group.Id
            });
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Group group = entity as Group;
            if (group == null)
                throw new ArgumentException("Entity must be of type Group", nameof(entity));

            cmd.CommandText = "INSERT INTO [Group] (GroupName, CreationDate, IsActive) VALUES (@GroupName, @CreationDate, null)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@GroupName", "New Group");
            OleDbParameter dateParam = new OleDbParameter("@CreationDate", OleDbType.DBDate);
            dateParam.Value = DateOnly.FromDateTime(DateTime.Now);
            command.Parameters.Add(new OleDbParameter("@IsActive", OleDbType.Boolean)
            {
                Value = true // Assign the boolean directly
            });


        }
    }
}

