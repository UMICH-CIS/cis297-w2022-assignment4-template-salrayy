using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summer Alrayyashi
/// CIS 297 Winter 2022
/// </summary> This is the main function for the patient record system.
namespace PatientRecordSystem
{
    internal class MainFunctions
    {
        //File operations
        public void FileOperations()
        {
            string fileName = "";
            try
            {
                Write("Enter a filename >> ");
                fileName = ReadLine();
                if (File.Exists(fileName))
                {
                    WriteLine("File exists");
                    WriteLine("File was created " +
                       File.GetCreationTime(fileName));
                    WriteLine("File was last written to " +
                       File.GetLastWriteTime(fileName));
                }
                else
                {
                    WriteLine("File does not exist");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Must enter the file Name.");
                }
            }
        }
        /// <summary>
        /// Summer Alrayyashi
        /// CIS 297 Winter 2022
        /// </summary>
        //Directory Operations
        public void DirectoryOperations()
        {
            //Directory operations
            string directoryName = "";
            try
            {

                string[] listOfFiles;
                Write("Enter a folder >> ");
                directoryName = ReadLine();
                if (Directory.Exists(directoryName))
                {
                    WriteLine("Directory exists, and it contains the following:");
                    listOfFiles = Directory.GetFiles(directoryName);
                    for (int x = 0; x < listOfFiles.Length; ++x)
                        WriteLine("   {0}", listOfFiles[x]);
                }
                else
                {
                    WriteLine("Directory does not exist");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (string.IsNullOrEmpty(directoryName))
                {
                    Console.WriteLine("must enter the directory name.");
                }
            }
        }
        /// <summary>
        /// Summer Alrayyashi
        /// CIS 297 Winter 2022
        /// </summary>
        //Using FileStream to create and write some text into it
        public void FileStreamOperations()
        {
            FileStream outFile = new FileStream("SomeText.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            try
            {
                Write("Enter some text >> ");
                string text = ReadLine();
                writer.WriteLine(text);
                // Error occurs if the next two statements are reversed
                writer.Close();
                outFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat(ex.Message, ex.StackTrace));
            }
            finally
            {
                if (outFile != null)
                {
                    writer.Close();
                }
                if (writer != null)
                {
                    outFile.Close();
                }

            }
        }
        /// <summary>
        /// Summer Alrayyashi
        /// CIS 297 Winter 2022
        /// </summary>
        //Writing data to a Sequential Access text file
        public void SequentialAccessWriteOperation()
        {
            const string FILENAME = "PatientData.txt";
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            try
            {
                const int END = 999;
                const string DELIM = ",";
                Patient emp = new Patient();
                Write("Enter patient number or " + END + " to quit >> ");
                string userinput = Console.ReadLine();
                int idnumberout;
                bool isvalidinput = int.TryParse(userinput, out idnumberout);
                if (isvalidinput)
                {
                    emp.IDNumber = idnumberout;
                    while (emp.IDNumber != END)
                    {
                        Write("Enter last name >> ");
                        emp.Name = ReadLine();
                        Write("Enter Current Balance >> ");
                        emp.currentbalance = Convert.ToDouble(ReadLine());
                        writer.WriteLine(emp.IDNumber + DELIM + emp.Name +
                           DELIM + emp.currentbalance);
                        Write("Enter next Patient number or " +
                           END + " to quit >> ");
                        string userinput2 = Console.ReadLine();
                        int idnumberout2;
                        bool isvalidinput2 = int.TryParse(userinput2, out idnumberout2);
                        if (isvalidinput2)
                        {
                            emp.IDNumber = idnumberout2;
                        }
                        else
                        {
                            throw new Exception("Invalid input. Patient number must be a number!");
                        }

                    }
                    writer.Close();
                    outFile.Close();
                }
                else
                {
                    throw new Exception("Your input is not valid. Patient number must be a number");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat(ex.Message, ex.StackTrace));
            }
            finally
            {
                if (outFile != null)
                {
                    writer.Close();
                }
                if (writer != null)
                {
                    outFile.Close();
                }

            }
        }

        //Read data from a Sequential Access File
        public void ReadSequentialAccessOperation()
        {
            const string FILENAME = "PatientData.txt";
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            try
            {
                const char DELIM = ',';
                Patient emp = new Patient();
                string recordIn;
                string[] fields;
                WriteLine("\n{0,-5}{1,-12}{2,8}\n", "Num", "Name", "Current Balance");
                recordIn = reader.ReadLine();
                while (recordIn != null)
                {
                    fields = recordIn.Split(DELIM);
                    emp.IDNumber = Convert.ToInt32(fields[0]);
                    emp.Name = fields[1];
                    emp.currentbalance = Convert.ToDouble(fields[2]);
                    WriteLine("{0,-5}{1,-12}{2,8}", emp.IDNumber, emp.Name, emp.currentbalance.ToString("C"));
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                inFile.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (inFile != null)
                {
                    inFile.Close();
                }

            }
        }

        //repeatedly searches a file to produce 
        //lists of patients who meet a minimum salary requirement
        //and this method has user defined exception
        public void FindPatients()
        {
            double minSalary;
            try
            {
                const char DELIM = ',';
                const int END = 999;
                const string FILENAME = "patientData.txt";
                Patient emp = new Patient();
                FileStream inFile = new FileStream(FILENAME,
                   FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                string recordIn;
                string[] fields;

                Write("Enter minimum Current Balance to find or " +
                   END + " to quit >> ");
                //minSalary = Convert.ToDouble(Console.ReadLine());
                string userinputsalary = Console.ReadLine();
                bool isdoublevalue = double.TryParse(userinputsalary, out minSalary);
                if (isdoublevalue)
                {
                    while (minSalary != END)
                    {
                        WriteLine("\n{0,-5}{1,-12}{2,8}\n",
                           "Num", "Name", "Current Balance");
                        inFile.Seek(0, SeekOrigin.Begin);
                        recordIn = reader.ReadLine();
                        while (recordIn != null)
                        {
                            fields = recordIn.Split(DELIM);
                            emp.IDNumber = Convert.ToInt32(fields[0]);
                            emp.Name = fields[1];
                            emp.currentbalance = Convert.ToDouble(fields[2]);
                            if (emp.currentbalance >= minSalary)
                                WriteLine("{0,-5}{1,-12}{2,8}", emp.IDNumber,
                                   emp.Name, emp.currentbalance.ToString("C"));
                            recordIn = reader.ReadLine();
                        }
                        Write("\nEnter minimum Current Balance to find or " +
                           END + " to quit >> ");
                        minSalary = Convert.ToDouble(Console.ReadLine());
                    }
                    reader.Close();  // Error occurs if
                    inFile.Close(); //these two statements are reversed
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Current Balance must be a number");
            }
        }

        //Serializable Demonstration
        /// <summary>
        /// writes Person class objects to a file and later reads them 
        /// from the file into the program
        /// </summary>
        public void SerializableDemonstration()
        {
            try
            {
                const int END = 999;
                const string FILENAME = "Data.ser";
                Person emp = new Person();
                FileStream outFile = new FileStream(FILENAME,
                   FileMode.Create, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                Write("Enter Patient number or " + END +
                   " to quit >> ");
                string userinputIDnumber = Console.ReadLine();
                int idnumberout;
                bool isvalidinput = int.TryParse(userinputIDnumber, out idnumberout);
                if (isvalidinput)
                {
                    emp.IDNumber = idnumberout;
                    while (emp.IDNumber != END)
                    {
                        Write("Enter last name >> ");
                        emp.Name = ReadLine();
                        Write("Enter Current Balance >> ");
                        emp.currentbalance = Convert.ToDouble(ReadLine());
                        bFormatter.Serialize(outFile, emp);
                        Write("Enter Patient number or " + END +
                           " to quit >> ");
                        emp.IDNumber = Convert.ToInt32(ReadLine());
                    }
                    outFile.Close();
                    FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
                    WriteLine("\n{0,-5}{1,-12}{2,8}\n",
                       "Num", "Name", "Current Balance");
                    while (inFile.Position < inFile.Length)
                    {
                        emp = (Person)bFormatter.Deserialize(inFile);
                        WriteLine("{0,-5}{1,-12}{2,8}",
                           emp.IDNumber, emp.Name, emp.currentbalance.ToString("C"));
                    }
                    inFile.Close();
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Patient Number must be a number.");
            }
        }
    }
}
