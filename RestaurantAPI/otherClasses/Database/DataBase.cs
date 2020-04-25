using System;
using System.Collections.Generic;
using System.IO;
using Npgsql;
using RestaurantAPI.Models;

namespace RestaurantAPI.otherClasses
{
    public partial class DataBase
    {
        public static DataBase DB = new DataBase();
        private NpgsqlConnection conn;
        NpgsqlCommand cmd = new NpgsqlCommand();

        private const string connectionStr = "Host=localhost;Username=postgres;Password=12345678;Database=postgres";

        public void Connect(string connStr = connectionStr)
        {
            conn = new NpgsqlConnection(connStr);
            conn.Open();
            cmd.Connection = conn;
        }
        // language=postgres

        public void InitDB()
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS restaurants(id SERIAL PRIMARY KEY ,name VARCHAR(40))";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS workers(id SERIAL PRIMARY KEY,first_name VARCHAR(40),timetable VARCHAR(40),coock_type VARCHAR(30))";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS timetable(id SERIAL PRIMARY KEY,restaurant VARCHAR(40),data JSON)";
            cmd.ExecuteNonQuery();

            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS rus(id SERIAL PRIMARY KEY,restaurant VARCHAR(40),date_week DATE,workers JSON)";
            cmd.ExecuteNonQuery();

            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS italy(id SERIAL PRIMARY KEY,restaurant VARCHAR(40),date_week DATE,workers JSON)";
            cmd.ExecuteNonQuery();

            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS french(id SERIAL PRIMARY KEY,restaurant VARCHAR(40),date_week DATE,workers JSON)";
            cmd.ExecuteNonQuery();

            
        }

        public string[] GetAllRestaurants()
        {
            var restaurants = new List<string>();
            lock (cmd)
            {
                
                cmd.CommandText = "SELECT name FROM restaurants";
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    restaurants.Add(reader.GetString(0));
                }

                reader.Close();
            }

            return restaurants.ToArray();
        }

        

        public void AddRestaurant(string restaurant)
        {
            cmd.CommandText="INSERT INTO restaurants(name) VALUES(@namerest)";
            cmd.Parameters.AddWithValue("namerest", restaurant);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void DeleteRestaurant(string restaurant)
        {
            cmd.CommandText = "DELETE FROM restaurants WHERE name=@restaurant";
            cmd.Parameters.AddWithValue("restaurant", restaurant);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        
        
        
        

        public void AddDateTable(string restaurant, string coockType, string date)
        {
            cmd.CommandText = "INSERT INTO " + coockType + "(restaurant,date_week,workers)" +
                              "VALUES(@restaurant,@date,'{}'";
            cmd.Parameters.AddWithValue("restaurant", restaurant);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        // public void AddWorkerInTable(string worker, string restaurant, string coockType, string date)
        // {
        //     cmd.CommandText =
        //         "UPDATE @coockType SET workers = workers || '{@worker}' WHERE restaurant = @restaurant AND date_week = @date";
        //     cmd.Parameters.AddWithValue("coockType", coockType);
        //     cmd.Parameters.AddWithValue("worker", worker);
        //     cmd.Parameters.AddWithValue("date", date);
        //     cmd.Parameters.AddWithValue("restaurant", restaurant);
        //     cmd.Prepare();
        //     cmd.ExecuteNonQuery();
        //
        // }
    }
}