using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Main program class which reads in user input,
/// sorts and prints out the names in the specified
/// sort by order.
/// </summary>
/// 
/// <author>
/// Jacqlyne Mba-Jonas
/// </author>
/// <date>04/25/2019</date>
namespace NameSorting
{
    public class NameSorting
    {
        private static List<Name> NamesInList = new List<Name>();
        private static bool SortByFirstName = false; //By default, sort by last names

        /// <summary>
        /// Reads in and responds to user input. Calls the underlying
        /// methods to produce the desired results.
        /// </summary>
        public static void Main()
        {
            while (true)
            {
                string resultFilePath = "";

                if (ProcessRequest(ref resultFilePath))
                {
                    Console.WriteLine("Sorted names have been written to the file: " + resultFilePath);

                    Console.WriteLine("You can [Q]uit the program or sort a [N]ew file");
                    Console.WriteLine("Choose an option by entering Q or N:");

                    //Continue asking until we get a valid option
                    while (true)
                    {
                        string optionValue = Console.ReadLine();

                        if (optionValue.Equals("N", System.StringComparison.OrdinalIgnoreCase))
                        {
                            NamesInList.Clear();
                            break; //Restart the program
                        }
                        else if (optionValue.Equals("Q", System.StringComparison.OrdinalIgnoreCase))
                        {
                            return; //Exit the program
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please enter Q or N:");
                        }
                    }
                }
                else
                {
                    return; //Exit the program
                }
            }
        }

        /// <summary>
        /// Reads in and responds to user input. Calls the underlying
        /// methods to produce the desired results.
        /// </summary>
        private static bool ProcessRequest(ref string resultFilePath)
        {
            Console.WriteLine("Please enter the path to a .txt file containing names:");
            string sourceFilePath = Console.ReadLine();

            //Continue asking until we get a valid file path
            while (!File.Exists(sourceFilePath) || Path.GetExtension(sourceFilePath) != ".txt")
            {
                Console.WriteLine("File not found or invalid path. Please enter a valid path to a .txt file:");
                sourceFilePath = Console.ReadLine();
            }

            Console.WriteLine("You can sort by [L]ast name or [F]irst name, or you can [Q]uit the program");
            Console.WriteLine("Choose an option by entering L, F, or Q:");

            //Continue asking until we get a valid option
            while (true)
            {
                string optionValue = Console.ReadLine();

                if (optionValue.Equals("L", System.StringComparison.OrdinalIgnoreCase))
                {
                    break; //The default is to sort by last name
                }
                else if (optionValue.Equals("F", System.StringComparison.OrdinalIgnoreCase))
                {
                    SortByFirstName = true;
                    break;
                }
                else if (optionValue.Equals("Q", System.StringComparison.OrdinalIgnoreCase))
                {
                    return false; //Exit the program
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter L, F, or Q:");
                }
            }

            ReadFile(sourceFilePath);
            SortNames();

            //Create a file path where the results should be written
            resultFilePath = Path.GetFileNameWithoutExtension(sourceFilePath) + "-Sorted.txt";
            resultFilePath = Path.Combine(Path.GetDirectoryName(sourceFilePath), resultFilePath);

            WriteSortedNamesToFile(resultFilePath);

            return true;
        }

        /// <summary>
        /// Populates the list of name objects using the names from the
        /// given file. Each properly formed name line should be in the
        /// following format "Last Name, First Name". Lines without 
        /// commas will be disregarded. Lines in which either the first
        /// or last name is missing will be processed, but not in the
        /// case were both are missing.
        /// </summary>
        /// <param name="sourceFilePath">File to read names from</param>
        private static void ReadFile(string sourceFilePath)
        {
            IEnumerable<string> lines = File.ReadLines(sourceFilePath);

            foreach (string line in lines)
            {
                if (!line.Contains(","))
                {
                    continue;
                }

                string[] content = line.Split(',');

                if(content.Length != 2 
                    || (string.IsNullOrEmpty(content[0]) && string.IsNullOrEmpty(content[1])))
                {
                    continue; //More than one comma and/or no names before and after a comma
                }

                NamesInList.Add(new Name(content[1].Trim(), content[0].Trim()));
            }
        }

        /// <summary>
        /// Names list will be sorted according to
        /// what the user specifies
        /// </summary>
        private static void SortNames()
        {
            if (!SortByFirstName)
            {
                NamesInList.Sort((n1, n2) => n1.LastName.CompareTo(n2.LastName));
            }
            else
            {
                NamesInList.Sort((n1, n2) => n1.FirstName.CompareTo(n2.FirstName));
            }
        }

        /// <summary>
        /// Writes out the names starting with the last names
        /// </summary>
        /// <param name="filePath">File to write the sorted names to</param>
        private static void WriteSortedNamesToFile(string resultFilePath)
        {
            //Creates or overwrites the file, and writes the names in the specified order
            using (StreamWriter fileStream = new StreamWriter(File.Create(resultFilePath)))
            {
                fileStream.WriteLine("Last Name  First Name"); //header

                foreach (Name name in NamesInList)
                {
                    fileStream.WriteLine(name.LastName + ",  " + name.FirstName);
                }
            }
        }
    }
}
