using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GroupMembersDB : PersonDB
    {
        public GroupMembersList SelectAll()
        {
            command.CommandText = "SELECT        GroupMembers.Id, GroupMembers.Id_Group, Person.UserName, Person.PassW, Person.isActive "+
                                  $" FROM (GroupMembers INNER JOIN    Person ON GroupMembers.Id = Person.Id)";
            GroupMembersList gmList = new GroupMembersList(base.Select());
            return gmList;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            GroupMembers gm = entity as GroupMembers;
            if (gm == null)
                throw new ArgumentException("Entity must be of type GroupMember", nameof(entity));

            //gm.Person = PersonDB.SelectById(Convert.ToInt32(reader["Id_person"]));
            gm.Group = GroupDB.SelectById(Convert.ToInt32(reader["Id_group"]));

            base.CreateModel(entity);
            return gm;
        }

        public override BaseEntity NewEntity()
        {
            return new GroupMembers();
        }

        static private GroupMembersList list = new GroupMembersList();

        public static GroupMembers SelectById(int id)
        {
            foreach (GroupMembers gm in list)
            {
                if (gm.Id == id)
                    return gm;
            }

            GroupMembersDB db = new GroupMembersDB();
            list = db.SelectAll();
            GroupMembers result = list.Find(x => x.Id == id);
            return result;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            GroupMembers gm = entity as GroupMembers;
            if (gm == null)
                throw new ArgumentException("Entity must be of type GroupMembers", nameof(entity));
            cmd.CommandText = "DELETE FROM GroupMembers WHERE Id=@Id";
            cmd.Parameters.AddWithValue("@Id", gm.Id);
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            GroupMembers gm = entity as GroupMembers;
            if (gm == null)
                throw new ArgumentException("Entity must be of type GroupMembers", nameof(entity));

            cmd.CommandText = "INSERT INTO GroupMembers (id,Id_group) VALUES ( @Id_person,@Id_group)";

            // Make sure both IDs are set
            cmd.Parameters.AddWithValue("@Id_person", gm.Id);  // Person PK

            cmd.Parameters.AddWithValue("@Id_group", gm.Group.Id);
        }


        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            GroupMembers gm = entity as GroupMembers;
            if (gm == null)
                throw new ArgumentException("Entity must be of type GroupMember", nameof(entity));
            cmd.CommandText = "UPDATE GroupMembers SET Id_group=@Id_group WHERE Id=@Id"; //Id_person=@Id_person;
            //cmd.Parameters.AddWithValue("@Id_person", gm.Person.Id);
            cmd.Parameters.AddWithValue("@Id_group", gm.Group.Id);
            cmd.Parameters.AddWithValue("@Id", gm.Id);
        }


        public override void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }
        public override void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            }
        }
        public override void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
            }
        }
    }
}
