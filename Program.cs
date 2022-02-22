using System;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summer Alrayyashi
/// CIS 297 Winter 2022
/// </summary>
/// This class simply calls all the functions from all the classes into one. 
namespace PatientRecordSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MainFunctions mf = new MainFunctions();
            mf.FileOperations();
            mf.DirectoryOperations();
            mf.FileStreamOperations();
            mf.SequentialAccessWriteOperation();
            mf.ReadSequentialAccessOperation();
            mf.FindPatients();
            mf.SerializableDemonstration();

        }

       
    }

}
