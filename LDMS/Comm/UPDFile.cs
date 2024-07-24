using LDMS.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMS.Comm
{
    public class UPDFile<T>
    {
        public UPDFile() 
        {
        
        }

        public static List<T> UPDFiles(string path)
        {
            var data = new List<T>();
                    
            if (File.Exists(path)) 
            {
                string jsons =  File.ReadAllText(path);
                if (jsons != "") 
                {
                    data = JsonConvert.DeserializeObject<List<T>>(jsons);
                }                          
            }
            return data;
        }

        public static void UPDFile_A(string path,List<T> list)
        {
            string data = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, data);
        }

        
       
    }
}
