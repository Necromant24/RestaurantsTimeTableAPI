using System.Collections.Generic;
using RestaurantAPI.Models;

namespace RestaurantAPI.otherClasses
{
    public partial class DataBase
    {
        
        public Worker[] GetAllWorkers()
        {
            var workers = new List<Worker>();
            lock (cmd)
            {
                cmd.CommandText = "SELECT first_name,timetable,coock_type FROM workers";
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    workers.Add(new Worker()
                    {
                        FirstName = reader.GetString(0),
                        Timetable = reader.GetString(1),
                        CoockType = reader.GetString(2)
                    });
                }

                reader.Close();
            }

            return workers.ToArray();
        }

        public void AddWorker(Worker worker)
        {
            cmd.CommandText="INSERT INTO workers(first_name,coock_type,timetable) VALUES(@fname,@type,@timetable)";
            cmd.Parameters.AddWithValue("fname", worker.FirstName);
            cmd.Parameters.AddWithValue("timetable", worker.Timetable);
            cmd.Parameters.AddWithValue("type", worker.CoockType);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void DeleteWorker(string name)
        {
            cmd.CommandText="DELETE FROM workers WHERE first_name=@fname";
            cmd.Parameters.AddWithValue("fname", name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        
        
        
    }
}