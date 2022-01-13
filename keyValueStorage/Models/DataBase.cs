using System.Collections.Generic;

namespace keyValueStorage.Models
{
    public class DataBase
    {
        public string Name { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    }

    class DataBases : Dictionary<string, DataBase> { }
}
