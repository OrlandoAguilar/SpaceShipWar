using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsGame1
{
    class EO
    {
        public static int load(string fileName)
        {
            int val = 0;
            try
            {
                // string fileName = "temp.txt";
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                val = reader.ReadInt32();
                reader.Close();
                stream.Close();
            }
            catch 
            {
                return -1;
            }
            return (val);

        }

        public static void save(string fileName, int val)
        {

            try
            {
              //  string fileName = "temp.txt";
                FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(val);
                writer.Flush();
                writer.Close();
                stream.Close();
            }
            catch
            {
            }
        }
    }
}
