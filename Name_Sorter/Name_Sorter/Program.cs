using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Name_Sorter
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var unsorted_names = ReadNames();
            var sorted_names = SortNames(unsorted_names);

            foreach (var name in sorted_names)
            {
                Console.WriteLine(name.ToString());
            }

            WriteNames(sorted_names);
        }

        //Read names from .txt file
        public static List<Name> ReadNames()
        {
            var unsorted_name_list = new List<Name>();

            try
            {
                //Fill the file path where unsorted_names.txt exists
                var fileStream = new FileStream("/Users/maythukyaw/Projects/Name_Sorter/unsorted_names.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string fullname;
                    while ((fullname = streamReader.ReadLine()) != null)
                    {
                        string[] nameArr = fullname.Split(' ');
                        string lastName = nameArr[nameArr.Length - 1];
                        Name name = new Name(fullname.Replace(lastName, ""), lastName);
                        unsorted_name_list.Add(name);
                    }
                }

                return unsorted_name_list;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return unsorted_name_list;
        }

        //Sort the names using linq
        public static IEnumerable<Name> SortNames(List<Name> unsorted_names)
        {
            IEnumerable<Name> sorted_names = unsorted_names.OrderBy(x => x.GetLastName()).ThenBy(x => x.GetGivenName());
            return sorted_names;
        }

        //Write the sorted names to sorted_names_list.txt file
        public static void WriteNames(IEnumerable<Name> sorted_names) {

            try
            {
                //Fill the file path where sorted_names.txt will be exported
                StreamWriter sw = new StreamWriter("/Users/maythukyaw/Projects/Name_Sorter/sorted_names_list.txt");

                foreach(var name in sorted_names)
                {
                    sw.WriteLine(name);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

    }

    class Name
    {
        string givenName;
        string lastName;

        public Name(string givenName, string lastName)
        {
            this.givenName = givenName;
            this.lastName = lastName;
        }

        public string GetGivenName()
        {
            return this.givenName;
        }

        public string GetLastName()
        {
            return this.lastName;
        }

        public override string ToString()
        {
            return givenName + " " + lastName;
        }

    }
}
