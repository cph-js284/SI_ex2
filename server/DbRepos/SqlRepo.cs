
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace server
{
    public class SqlRepo
    {
        string DbName;
        string connectionString = "Server=mysql01;Uid=root;Pwd=test1234;"; // <- yes, password right there in the code yieks
        public SqlRepo(string DbName)
        {
            this.DbName = DbName;
        }
        
        public void setupdb(){
            CreateDb();
            CreateTable();
            PopulateTable();
        }

        private void CreateDb(){
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sqlstr = @"DROP DATABASE if exists " + DbName + "; CREATE DATABASE " + DbName + ";";
                var command = new MySqlCommand(sqlstr,conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("DEBUG: Db created.");
            }
        }

        private void CreateTable(){
            using (var conn = new MySqlConnection(connectionString +"Database="+ DbName+";"))
            {
                conn.Open();
                var sqlstr = @"CREATE TABLE Students(
                    Id INT NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(50),
                    Email VARCHAR(50),
                    Address VARCHAR(100),
                    StoreType VARCHAR(30),
                    PRIMARY KEY (Id)
                );";

                var command = new MySqlCommand(sqlstr, conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("DEBUG: Table created");
            }
        }

        private void PopulateTable(){
            using (var conn = new MySqlConnection(connectionString+"Database="+DbName+";"))
            {
                conn.Open();
                var sqlstr=@"
                INSERT INTO Students (Name, Email, Address, StoreType) values ('Adam', 'Adam@funkymail.com', 'AbbyRoad 13', 'Database');
                INSERT INTO Students (Name, Email, Address, StoreType) values ('James', 'James@klunkymail.com', 'Road to hell 666', 'Database');
                INSERT INTO Students (Name, Email, Address, StoreType) values ('Liz', 'Liz@cphmail.com', 'AbbyRoad 13', 'Database');
                INSERT INTO Students (Name, Email, Address, StoreType) values ('Ellen', 'Ellen@Somemail.com', 'Downtown Bld. 13', 'Database');
                ";

                var command = new MySqlCommand(sqlstr,conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("DEBUG: Table populated");
            }
        }

        public List<string> ReadTable(){
            List<string> res = new List<string>();
            using (var conn = new MySqlConnection(connectionString+"Database="+DbName+";"))
            {
                conn.Open();
                var sqlstr = @"SELECT * FROM Students;";
                var command = new MySqlCommand(sqlstr,conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    res.Add(reader.GetString(1)+"¤"+reader.GetString(2)+"¤"+reader.GetString(3)+"¤"+reader.GetString(4)+"¤");
                }
            }
            return res;
        }


    }
}