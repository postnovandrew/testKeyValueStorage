using System;
using keyValueStorage.Models;

namespace keyValueStorage.Controllers
{
    public class KeyValueDBController
    {
        private static DataBases databases = new DataBases();
        private static object locker = new object();

        private DataBase GetDB(string DBName)
        {
            if (!databases.TryGetValue(DBName, out DataBase dataBase))
            {
                dataBase = new DataBase();
                dataBase.Name = DBName;
                databases[DBName] = dataBase;
            }
            return dataBase;
        }

        public string GetDBKey(string DBName, string key)
        {
            string ReturnVal = null;
            try
            {
                lock (locker)
                {
                    if (!databases.TryGetValue(DBName, out DataBase dataBase))
                        throw new Exception("database not found");

                    var data = dataBase.Data;

                    if (!data.TryGetValue(key, out object val))
                        throw new Exception("key not found");

                    ReturnVal = val.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetDBKey: " + DBName + ", " + ex.Message);                
            }
            return ReturnVal;
        }

        public void SetDBKey(string DBName, string key, string value)
        {
            try
            {
                lock (locker)
                {
                    if (!databases.TryGetValue(DBName, out DataBase dataBase))
                        throw new Exception("database not found");

                    var data = dataBase.Data;
                    data[key] = value;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error SetDBKey: " + DBName + ", " + ex.ToString());
            }
        }

        public void SetDeleteDBKey(string DBName, string key)
        {
            try
            {
                lock (locker)
                {
                    if (!databases.TryGetValue(DBName, out DataBase dataBase))
                        throw new Exception("SetDeleteDBKey error, database not found");

                    var data = dataBase.Data;
                    data.Remove(key);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error SetDeleteDBKey: " + DBName + ", " + ex.ToString());
            }
        }

        public void SetDB(DataBase data)
        {
            lock (locker)
            {
                var dataBase = GetDB(data.Name);
                dataBase.Data = data.Data;
            }
        }

        public void SetDeleteDB(string DBName)
        {
            lock (locker)
            {
                databases.Remove(DBName);
            }
        }
    }
}
