using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Test";
            Console.WriteLine(str);
            Console.WriteLine(Reverse(str));
        }

        static string Reverse(string str)
        {
            string result = string.Empty;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                result += str[i];
            }

            return result;
        }

        class HeaderRecord
        {
            public string name;
            public string description;

            public DetailRecord[] details;
        }

        class DetailRecord
        {
            public string detailName;
            public string deatilDescription;
        }

        static void ProcessHeaderBad(HeaderRecord header)
        {
            Console.WriteLine($"Name: {header.name}");
            Console.WriteLine($"Description: {header.description}");

            foreach (DetailRecord detailRecord in header.details)
            {
                Console.WriteLine($"Name: {detailRecord.detailName}");
                Console.WriteLine($"Description: {detailRecord.deatilDescription}");
            }
        }

        static void ProcessHeaderGood(HeaderRecord header)
        {
            Console.WriteLine($"Name: {header.name}");
            Console.WriteLine($"Description: {header.description}");

            foreach (DetailRecord detailRecord in header.details)
            {
                ProcessDetail(detailRecord);
            }
        }

        private static void ProcessDetail(DetailRecord detailRecord)
        {
            Console.WriteLine($"Name: {detailRecord.detailName}");
            Console.WriteLine($"Description: {detailRecord.deatilDescription}");
        }
    }    
}
