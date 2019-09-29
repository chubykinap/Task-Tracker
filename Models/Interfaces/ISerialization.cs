using System.Collections.Generic;

namespace TaskTracker.Interfaces
{
    public interface ISerialization
    {
        void Serialize<T>(List<T> list);

        List<T> Deserialize<T>();
    }
}