using System;
using keyValueStorage.Controllers;
using keyValueStorage.Models;
using Newtonsoft.Json;

namespace keyValueStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello keyvalue World!");
            KeyValueDBController storage = new KeyValueDBController();

            string json = @"{
                              ""name"": ""new1"",
                              ""data"": {
                                            ""1"": ""val1"",
                                            ""2"": ""val2"",
                                            ""3"": ""val3""
                                        }
                            }";

            var newDB = JsonConvert.DeserializeObject<DataBase>(json);
            storage.SetDB(newDB);

            Console.WriteLine("value of key==1 is: " + storage.GetDBKey("new1", "1"));

            storage.SetDBKey("new1", "1", "newval1");

            Console.WriteLine("value of key==1 is: " + storage.GetDBKey("new1", "1"));

            storage.SetDeleteDBKey("new1", "1");

            Console.WriteLine("value of key==1 is: " + storage.GetDBKey("new1", "1"));

            storage.SetDBKey("new1", "1", "newval11");

            Console.WriteLine("value of key==1 is: " + storage.GetDBKey("new1", "1"));

            storage.SetDBKey("new1", "4", "val4");

            Console.WriteLine("value of key==4 is: " + storage.GetDBKey("new1", "4"));

            storage.SetDeleteDB("new1");

            Console.WriteLine("value of key==4 is: " + storage.GetDBKey("new1", "4"));

            Console.ReadLine();
        }


    }
}
