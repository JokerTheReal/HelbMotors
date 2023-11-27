using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReference.Services
{
    public partial class UserManagementService
    {
        public partial void ConfigTools();
    }

    public class CreateUserDataTables
    {
        public CreateUserDataTables()
        {
            DataTable UserTable = new DataTable(); //Tous les utilisateurs
            DataTable AccessTable = new DataTable(); //Tous les types de point d'accès

            DataColumn User_ID = new DataColumn("User_ID", typeof(Int16)); //Ce sont les noms dans les "" qui doivent correspondre aux noms de la base de données Access
            DataColumn UserName = new DataColumn("UserName", typeof(string));
            DataColumn UserPassword = new DataColumn("UserPassword", typeof(string));
            DataColumn AccessType = new DataColumn("UserAccessType", typeof(Int16));

            DataColumn Access_ID = new DataColumn("Access_ID", typeof(Int16));
            DataColumn AccessName = new DataColumn("AccessName", typeof(string));
            DataColumn CreateObject = new DataColumn("CreateObject", typeof(bool));
            DataColumn DestroyObject = new DataColumn("DestroyObject", typeof(bool));
            DataColumn ModifyObject = new DataColumn("ModifyObject", typeof(bool));
            DataColumn ChangeUserRights = new DataColumn("ChangeUserRights", typeof(bool));

            //UserTable
            UserTable.TableName = "Users";
            User_ID.AutoIncrement = true;
            User_ID.Unique = true;
            UserTable.Columns.Add(User_ID);

            UserName.Unique = true;
            UserTable.Columns.Add(UserName);

            UserTable.Columns.Add(UserPassword);
            UserTable.Columns.Add(AccessType);

            //AccessTable
            AccessTable.TableName = "Access";

            Access_ID.AutoIncrement = true;
            Access_ID.Unique = true;
            AccessTable.Columns.Add(Access_ID);

            AccessName.Unique = true;
            AccessTable.Columns.Add(AccessName);

            AccessTable.Columns.Add(CreateObject);
            AccessTable.Columns.Add(DestroyObject);
            AccessTable.Columns.Add(ModifyObject);
            AccessTable.Columns.Add(ChangeUserRights);

            //DataSet
            Globals.UserSet.Tables.Add(UserTable);
            Globals.UserSet.Tables.Add(AccessTable);

            DataColumn parentColumn = Globals.UserSet.Tables["Access"].Columns["Access_ID"]; //Access_ID -> 1, 2, 4 Dans la colonne AccessType, on pourra mettre seulement 1, 2, 4
            DataColumn childColumn = Globals.UserSet.Tables["Users"].Columns["UserAccessType"];

            DataRelation relation = new DataRelation("AcCess2Users", parentColumn, childColumn);
            Globals.UserSet.Tables["Users"].ParentRelations.Add(relation);
        }
    }
}
