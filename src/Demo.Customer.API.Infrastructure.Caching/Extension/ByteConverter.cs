using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Demo.Customer.API.Infrastructure.Caching.Extension
{
    internal static class ByteConverter
    {
        internal static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return default;
            string result = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(result);
        }
        internal static byte[] ToMemoryStreamByteArray(this object obj)
        {
            if (obj == null)
                return default;
            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            var bin = ms.ToArray();
            return bin;
        }
        internal static T ToType<T>(this byte[] array)
        {
            if (array == null)
                return default;
            string result = Encoding.UTF8.GetString(array);
            return JsonConvert.DeserializeObject<T>(result);
        }
        internal static T ToMemoryStreamType<T>(this byte[] arrBytes)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using MemoryStream memStream = new MemoryStream();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                T obj = (T)bf.Deserialize(memStream);

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
