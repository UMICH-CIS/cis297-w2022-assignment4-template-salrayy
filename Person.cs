using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summer Alrayyashi
/// CIS 297 Winter 2022
/// </summary>
/// This class is inherited from the patient record system and is directy focused on the person's ID number, name, and balanced. 
namespace PatientRecordSystem
{
    public class Person
    {
        public int IDNumber { get; set; }
        public string Name { get; set; }
        public double currentbalance { get; set; }
    }
}
