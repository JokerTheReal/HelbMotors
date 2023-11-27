using System.Data.OleDb;

namespace MyReference.Services
{
    public partial class UserManagementService
    {
        //internal OleDbDataAdapter UsersAdapter = new OleDbDataAdapter(); //internal = public
        internal OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();
        internal OleDbConnection Connexion = new OleDbConnection();


        public partial void ConfigTools()
        {
            Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
                                       + "Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataFromDesktop/UserAccounts.accdb") //On a enlevé le @
                                       + ";Persist Security Info=False";


            //////////////////////////// FOR CAR ////////////////////////////
            string Insert_Voiture_CommandText = "INSERT INTO DB_Voitures(Marque, Modele, Annee) VALUES (@Marque, @Modele, @Annee)";
            string Delete_Voiture_CommandText = "DELETE FROM DB_Voitures WHERE VoituresID = @VoituresID";
            string Select_Voiture_CommandText = "SELECT * FROM DB_Voitures ORDER BY VoituresID;";
            string Update_Voiture_CommandText = "UPDATE DB_Voitures SET Marque = @Marque, Modele = @Modele, Annee = @Annee WHERE VoituresID=@VoituresID;";

            //////////////////////////// FOR USERS /////////////////////////
/*            string Insert_User_CommandText = "INSERT INTO DB_Users(UserName, UserPassword, UserAccesType) VALUES (@UserName, @UserPassword, @UserAccesType)";
            string Delete_User_CommandText = "DELETE FROM DB_Users WHERE UserName = @UserName";
            string Select_User_CommandText = "SELECT * FROM DB_Users ORDER BY User_ID";
            string Update_User_CommandText = "UPDATE DB_Users SET UserPassword = @UserPassword, UserAccessType = @UserAccessType WHERE UserName=@UserName";*/


            //////////////////////////// FOR CAR //////////////////////////// 
            OleDbCommand Insert_Voiture_Command = new OleDbCommand(Insert_Voiture_CommandText, Connexion);
            OleDbCommand Delete_Voiture_Command = new OleDbCommand(Delete_Voiture_CommandText, Connexion);
            OleDbCommand Select_Voiture_Command = new OleDbCommand(Select_Voiture_CommandText, Connexion);
            OleDbCommand Update_Voiture_Command = new OleDbCommand(Update_Voiture_CommandText, Connexion);

            //////////////////////////// FOR USERS /////////////////////////
/*            OleDbCommand Insert_User_Command = new OleDbCommand(Insert_User_CommandText, Connexion);
            OleDbCommand Delete_User_Command = new OleDbCommand(Delete_User_CommandText, Connexion);
            OleDbCommand Select_User_Command = new OleDbCommand(Select_User_CommandText, Connexion);
            OleDbCommand Update_User_Command = new OleDbCommand(Update_User_CommandText, Connexion);*/


            //Adapteur pret à travailler avec la base de donnée qui se trouve sur le desktop

            //////////////////////////// FOR CAR //////////////////////////// 
            OleDbAdapter.InsertCommand = Insert_Voiture_Command;
            OleDbAdapter.DeleteCommand = Delete_Voiture_Command;
            OleDbAdapter.SelectCommand = Select_Voiture_Command;
            OleDbAdapter.UpdateCommand = Update_Voiture_Command;

            //////////////////////////// FOR USERS /////////////////////////
/*            OleDbAdapter.InsertCommand = Insert_User_Command;
            OleDbAdapter.DeleteCommand = Delete_User_Command;
            OleDbAdapter.SelectCommand = Select_User_Command;
            OleDbAdapter.UpdateCommand = Update_User_Command;*/


            //On spécifie la table qui se trouve dans notre base de données  (DB_Users= nom de la table dans acces)  (Users=DataTable)

            //////////////////////////// FOR CAR //////////////////////////// 
            OleDbAdapter.TableMappings.Add("DB_Voitures", "Voitures");

            //////////////////////////// FOR USERS /////////////////////////
/*            OleDbAdapter.TableMappings.Add("DB_Users", "Users");
            OleDbAdapter.TableMappings.Add("DB_Acces", "Access");*/


            // Changer ces champs pour notre projet (pour le projet il faut avoir 2 profil, Par exemple Admin et User)

            //////////////////////////// FOR CAR //////////////////////////// 
            OleDbAdapter.InsertCommand.Parameters.Add("@Marque", OleDbType.VarChar, 40, "Marque");
            OleDbAdapter.InsertCommand.Parameters.Add("@Modele", OleDbType.VarChar, 40, "Modele");
            OleDbAdapter.InsertCommand.Parameters.Add("@Annee", OleDbType.Integer, 100, "Annee");

            OleDbAdapter.DeleteCommand.Parameters.Add("@VoituresID", OleDbType.Integer, 40, "VoituresID");

            OleDbAdapter.UpdateCommand.Parameters.Add("@Marque", OleDbType.VarChar, 40, "Marque");
            OleDbAdapter.UpdateCommand.Parameters.Add("@Modele", OleDbType.VarChar, 40, "Modele");
            OleDbAdapter.UpdateCommand.Parameters.Add("@Annee", OleDbType.Integer, 40, "Annee");
            OleDbAdapter.UpdateCommand.Parameters.Add("@VoituresID", OleDbType.Integer, 40, "VoituresID");

            //////////////////////////// FOR USERS /////////////////////////// 
/*            OleDbAdapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
            OleDbAdapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
            OleDbAdapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");

            OleDbAdapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");

            OleDbAdapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
            OleDbAdapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
            OleDbAdapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 40, "UserAccessType");
*/

            if (!Globals.UserSet.Tables.Contains("Voitures"))
                Globals.UserSet.Tables.Add("Voitures");

/*            if (!Globals.UserSet.Tables.Contains("Access"))
                Globals.UserSet.Tables.Add("Access");

            if (!Globals.UserSet.Tables.Contains("Users"))
                Globals.UserSet.Tables.Add("Users");*/

        }

 /*       //////////////////////////// PART FOR USERS ///////////////////////////

        public async Task FillFromUsersDBTable()
        {
            Globals.UserSet.Tables["Users"].Clear();
            try
            {
                Connexion.Open();
                OleDbAdapter.Fill(Globals.UserSet.Tables["Users"]);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }

        public async Task InsertUser(string name, string password, Int32 access)
        {
            try
            {
                Connexion.Open();
                OleDbAdapter.InsertCommand.Parameters[0].Value = name;
                OleDbAdapter.InsertCommand.Parameters[1].Value = password;
                OleDbAdapter.InsertCommand.Parameters[2].Value = access;

                if (OleDbAdapter.InsertCommand.ExecuteNonQuery() != 0)
                {
                    await Shell.Current.DisplayAlert("Database", "User sucessfully inserted", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Database", "User not inserted", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
            {
                Connexion.Close();
            }
        }

        public async Task DeleteUser(string name)
        {
            try
            {
                Connexion.Open();
                OleDbAdapter.DeleteCommand.Parameters[0].Value = name;


                if (OleDbAdapter.DeleteCommand.ExecuteNonQuery() != 0)
                {
                    await Shell.Current.DisplayAlert("Database", "User sucessfully deleted", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Database", "User not deleted", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
            {
                Connexion.Close();
            }
        }*/


        //////////////////////////// PART FOR CAR //////////////////////////// 

        public async Task FillFromCarsDBTable()
        {
            Globals.UserSet.Tables["Voitures"].Clear();
            try
            {
                Connexion.Open();
                OleDbAdapter.Fill(Globals.UserSet.Tables["Voitures"]);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }

        public async Task InsertCar(string marque, string modele, int? annee)
        {
            try
            {
                if (Connexion.State == ConnectionState.Closed)
                    Connexion.Open();

                OleDbAdapter.InsertCommand.Parameters[0].Value = marque;
                OleDbAdapter.InsertCommand.Parameters[1].Value = modele;
                OleDbAdapter.InsertCommand.Parameters[2].Value = annee;

                if (OleDbAdapter.InsertCommand.ExecuteNonQuery() != 0)
                {
                    await Shell.Current.DisplayAlert("Database", "Voiture successfully inserted", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Database", "Voiture not inserted", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }

        public async Task DeleteCar(int voitureID)
        {
            try
            {
                Connexion.Open();
                OleDbAdapter.DeleteCommand.Parameters[0].Value = voitureID;


                if (OleDbAdapter.DeleteCommand.ExecuteNonQuery() != 0)
                {
                    await Shell.Current.DisplayAlert("Database", "Voiture successfully deleted", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Database", "Voiture not deleted", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }

        public async Task ReadCarsTable()
        {
            OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Voitures ORDER BY VoituresID;", Connexion);
            try
            {
                Connexion.Open();

                OleDbDataReader Reader = SelectCommand.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Globals.UserSet.Tables["Voitures"].Rows.Add(new Object[] { Reader[0], Reader[1], Reader[2] });
                    }
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }


        public async Task UpdateCar()
        {
            try
            {
                Connexion.Open();

                OleDbAdapter.Update(Globals.UserSet.Tables["Voitures"]);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
            }
            finally
            {
                Connexion.Close();
            }
        }
    


        /*

     public async Task ReadAccessTable()
     {
         OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Access ORDER BY Access_ID;", Connexion); //
         try
         {
             Connexion.Open();

             OleDbDataReader Reader = SelectCommand.ExecuteReader(); //Récupère tous les données de la AccessTable

             if(Reader.HasRows)
             {
                 while(Reader.Read()) //Tant que le reader n'est pas null
                 {
                     Globals.UserSet.Tables["Access"].Rows.Add(new Object[] { Reader[0], Reader[1], Reader[2], Reader[3], Reader[4], Reader[5] }); //On a UserSet qui a deux tables, on rempli la colonne Access avec les valeurs
                 }
             }

             Reader.Close();
         }
         catch(Exception ex)
         {
             await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
         }
         finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
         {
             Connexion.Close();
         }
     }

     public async Task FillFromDBTable()
     {
         Globals.UserSet.Tables["Voitures"].Clear();
         try
         {
             Connexion.Open();
             OleDbAdapter.Fill(Globals.UserSet.Tables["Voitures"]);
         }
         catch (Exception ex)
         {
             await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
         }
         finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
         {
             Connexion.Close();
         }
     }



     public async Task InsertUser(string name, string password, Int32 access)
     {
         try
         {
             Connexion.Open();
             UsersAdapter.InsertCommand.Parameters[0].Value = name;
             UsersAdapter.InsertCommand.Parameters[1].Value = password;
             UsersAdapter.InsertCommand.Parameters[2].Value = access;

             if(UsersAdapter.InsertCommand.ExecuteNonQuery() != 0)
             {
                 await Shell.Current.DisplayAlert("Database", "User sucessfully inserted", "Ok");
             }
             else
             {
                 await Shell.Current.DisplayAlert("Database", "User not inserted", "Ok");
             }

         }
         catch (Exception ex)
         {
             await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
         }
         finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
         {
             Connexion.Close();
         }
     }

     public async Task DeleteUser(string name)
     {
         try
         {
             Connexion.Open();
             UsersAdapter.DeleteCommand.Parameters[0].Value = name;


             if (UsersAdapter.DeleteCommand.ExecuteNonQuery() != 0)
             {
                 await Shell.Current.DisplayAlert("Database", "User sucessfully deleted", "Ok");
             }
             else
             {
                 await Shell.Current.DisplayAlert("Database", "User not deleted", "Ok");
             }

         }
         catch (Exception ex)
         {
             await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
         }
         finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
         {
             Connexion.Close();
         }
     }

     public async Task UpdateUser()
     {
         try
         {
             Connexion.Open();

             UsersAdapter.Update(Globals.UserSet.Tables["Users"]); //Faire une mise à jour de toute la table

         }
         catch (Exception ex)
         {
             await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
         }
         finally //en fin de parcours peu importe erreur ou pas il va qd meme faire ce qu'il y'a dedans
         {
             Connexion.Close();
         }
     }*/


    }
}
