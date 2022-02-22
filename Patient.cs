using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Summer Alrayyashi
/// CIS 297 Winter 2022
/// </summary>
/// This class is inherited from the patient record system and is directly focused on the patient and the balance in the system.
namespace PatientRecordSystem
{
    public class Patient
    {
        public int IDNumber { get; set; }
        public string Name { get; set; }
        public double currentbalance { get; set; }
    }
}
