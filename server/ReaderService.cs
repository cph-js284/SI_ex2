using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace server
{
    public class ReaderService
    {
        public List<string> ReadTxtFile(){
            var tmp = File.ReadAllLines("data.txt");
            string res="";
            for (int i = 0; i < tmp.Length; i++)
            {
                res += tmp[i];
            }
            return tmp.ToList();
        }
    }
}