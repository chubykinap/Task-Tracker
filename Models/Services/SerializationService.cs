using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using TaskTracker.Interfaces;

namespace TaskTracker.Services
{
    public class SerializationService : ISerialization
    {
        private readonly string Path;

        public SerializationService(string path)
        {
            Path = path;
        }

        public void Serialize<T>(List<T> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(Path + typeof(T).Name.ToString() + ".json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, list);
            }
        }

        public List<T> Deserialize<T>()
        {
            List<T> result = new List<T>();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(Path + typeof(T).Name.ToString() + ".json", FileMode.OpenOrCreate))
            {
                result = (List<T>)jsonFormatter.ReadObject(fs);
            }
            return result;
        }
    }
}