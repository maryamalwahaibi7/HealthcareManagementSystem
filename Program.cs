using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Linq;

namespace HealthcareManagementSystem
{
    internal class Program
    {
        //System Storage
        static string[] patientNames = new string[100];
        static string[] patientIDs = new string[100];
        static string[] diagnoses = new string[100];
        static bool[] admitted = new bool[100];       // true = currently admitted
        static string[] assignedDoctors = new string[100];
        static string[] departments = new string[100];     // e.g. "Cardiology", "Orthopedics"
        static int[] visitCount = new int[100];        // how many times admitted
        static double[] billingAmount = new double[100];     // total fees owed
        static DateTime[] lastVisitDate = new DateTime[100];
        static DateTime[] lastDischargeDate = new DateTime[100];
        static int[] daysInHospital = new int[100];
        static string[] bloodType = new string[100];
        static int lastPatientIndex = 0;
        //New Array Declarations For Doctor Management Module 
        static string[] doctorNames = new string[50]; 
        static int[] doctorAvailableSlots = new int[50];
        static int[] doctorVisitCount = new int[50];
        static int lastDoctorIndex = 0; 


        static public void DisplayMenu()
        {
            Console.WriteLine("Healthcare Management System");
            Console.WriteLine("1. Register New Patient");
            Console.WriteLine("2. Admit Patient");
            Console.WriteLine("3. Discharge Patient");
            Console.WriteLine("4. Search Patient");
            Console.WriteLine("5. List All Admitted Patients");
            Console.WriteLine("6. Transfer Patient to Another Doctor");
            Console.WriteLine("7. View Most Visited Patients");
            Console.WriteLine("8. Search Patients by Department");
            Console.WriteLine("9. Billing Report");
            Console.WriteLine("10. Exit");
            Console.WriteLine("11. Add Doctor");
            Console.WriteLine("12. Doctor Salary Report");
        }
        static public void SeedData()
        {
            //Seed Data
            patientNames[lastPatientIndex] = "Ali Hassan";
            patientIDs[lastPatientIndex] = "P000";
            diagnoses[lastPatientIndex] = "Flu";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            departments[lastPatientIndex] = "General";
            visitCount[lastPatientIndex] = 2;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = DateTime.Parse("2025-01-10");
            lastDischargeDate[lastPatientIndex] = DateTime.Parse("2025-01-15");
            daysInHospital[lastPatientIndex] = 12;
            bloodType[lastPatientIndex] = "A+";

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Sara Ahmed";
            patientIDs[lastPatientIndex] = "P001";
            diagnoses[lastPatientIndex] = "Fracture";
            admitted[lastPatientIndex] = true;
            assignedDoctors[lastPatientIndex] = "Dr. Noor";
            departments[lastPatientIndex] = "Orthopedics";
            visitCount[lastPatientIndex] = 4;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = DateTime.Parse("2025-03-02");
            lastDischargeDate[lastPatientIndex] = DateTime.MinValue;
            daysInHospital[lastPatientIndex] = 8;
            bloodType[lastPatientIndex] = "O-";

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Omar Khalid";
            patientIDs[lastPatientIndex] = "P002";
            diagnoses[lastPatientIndex] = "Diabetes";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            departments[lastPatientIndex] = "Cardiology";
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = DateTime.Parse("2024-12-20");
            lastDischargeDate[lastPatientIndex] = DateTime.Parse("2024-12-28");
            daysInHospital[lastPatientIndex] = 5;
            bloodType[lastPatientIndex] = "B+";

            lastPatientIndex++;

            //Doctor 1
            doctorNames[lastDoctorIndex] = "Dr. Noor";
            doctorAvailableSlots[lastDoctorIndex] = 5;
            doctorVisitCount[lastDoctorIndex] = 0;

            lastDoctorIndex++;

            //Doctor 2
            doctorNames[lastDoctorIndex] = "Dr. Salem";
            doctorAvailableSlots[lastDoctorIndex] = 3;
            doctorVisitCount[lastDoctorIndex] = 0;

            lastDoctorIndex++; 

            //Doctor 3
            doctorNames[lastDoctorIndex] = "Dr. Hana";
            doctorAvailableSlots[lastDoctorIndex] = 8;
            doctorVisitCount[lastDoctorIndex] = 0;

            lastDoctorIndex++;


        }
        static public string RegisterPatient(string Name, string Diagnosis, string Department, string BloodType)
        {
            Console.WriteLine("Registering New Patient..."); 

            patientIDs[lastPatientIndex] = "P" + lastPatientIndex.ToString("D3");
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            visitCount[lastPatientIndex] = 0;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = DateTime.MinValue;
            lastDischargeDate[lastPatientIndex] = DateTime.MinValue;
            daysInHospital[lastPatientIndex] = 0;

            Console.WriteLine("Patient registered successfully!");
            return patientIDs[lastPatientIndex]; 

        }
        static public int SearchPatient(string SearchInput)
        {
            int Found = -1;
            for (int i = 0; i <= lastPatientIndex; i++)
            {
                if (patientNames[i] == SearchInput || patientIDs[i] == SearchInput)
                {
                    Found = i;
                    break;
                }
            }
            return Found;
        }
        static public void PrintPatientDetails(int index)
        {
            Console.WriteLine("Name: " + patientNames[index]);
            Console.WriteLine("ID: " + patientIDs[index].ToUpper());
            Console.WriteLine("Blood type: " + bloodType[index]); //New
            Console.WriteLine("Diagnosis: " + diagnoses[index] + " (" + diagnoses[index].Length + " characters)");
            Console.WriteLine("Admission status: " + admitted[index]);
            Console.WriteLine("Visit count: " + visitCount[index]);
            Console.WriteLine("Total billing amount: " + Convert.ToString(Math.Round(billingAmount[index], 2)) + " OMR");
            Console.WriteLine("Total days in hospital: " + daysInHospital[index]); //New
        }

        static void Main(string[] args)
        {

            SeedData(); 

            bool exit = false;
            while (exit == false)
            {
                DisplayMenu();

                Console.WriteLine("Choose option: ");
                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 10.");
                }

                switch (choice)
                {
                    case 1: //Register New Patient

                        Console.WriteLine("Name: ");
                        patientNames[lastPatientIndex] = Console.ReadLine().Trim();

                        //Console.WriteLine("Patient ID: "); 
                        //patientIDs[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Diagnosis: ");
                        diagnoses[lastPatientIndex] = Console.ReadLine().Trim();

                        Console.WriteLine("Department: ");
                        departments[lastPatientIndex] = Console.ReadLine().Trim();

                        Console.WriteLine("Enter patient's blood type: ");
                        bloodType[lastPatientIndex] = Console.ReadLine().ToUpper();

                        string PID = RegisterPatient(patientNames[lastPatientIndex], diagnoses[lastPatientIndex], departments[lastPatientIndex], bloodType[lastPatientIndex]);

                        Console.WriteLine("Patient ID: " + PID );
                        lastPatientIndex++;

                        break;

                    case 2: //Admit Patient
                        Console.WriteLine("Enter patient ID or name");
                        string AdmitInput = Console.ReadLine();

                        int patientFound = SearchPatient(AdmitInput);

                        if (patientFound == -1)
                        {
                            Console.WriteLine("Patient not found");
                        }
                        else
                        {
                            if (admitted[patientFound] == false)
                            {
                                Console.WriteLine("Doctor Name: ");
                                string doctorInput = Console.ReadLine().Trim();

                                int doctorIndex = -1;

                                for (int i = 0; i <= lastDoctorIndex; i++)
                                {
                                    if (doctorNames[i].ToLower() == doctorInput.ToLower())
                                    {
                                        doctorIndex = i;
                                        break;
                                    }
                                }

                                if (doctorIndex == -1)
                                {
                                    Console.WriteLine("Doctor not found in the system. Please register the doctor first.");
                                    break;
                                }

                                if (doctorAvailableSlots[doctorIndex] == 0)
                                {
                                    Console.WriteLine("Dr. " + doctorNames[doctorIndex] + " has no available slots at this time.");
                                    break;
                                }

                                admitted[patientFound] = true;
                                assignedDoctors[patientFound] = doctorNames[doctorIndex];
                                doctorAvailableSlots[doctorIndex]--;
                                doctorVisitCount[doctorIndex]++;

                                visitCount[patientFound]++;

                                lastVisitDate[patientFound] = DateTime.Now;
                                lastDischargeDate[patientFound] = DateTime.MinValue;

                                Console.WriteLine("Patient admitted successfully and assigned to doctor: " + assignedDoctors[patientFound]);
                                Console.WriteLine("Dr. " + doctorNames[doctorIndex] + " now has " + doctorAvailableSlots[doctorIndex] + " slot(s) remaining.");
                                Console.WriteLine("Admitted on: " + lastVisitDate[patientFound].ToString("yyyy-MM-dd HH:mm"));

                                if (visitCount[patientFound] > 1)
                                {
                                    Console.WriteLine("This patient has been admitted " + visitCount[patientFound] + " times");
                                }
                                else
                                {
                                    Console.WriteLine("The patient is being admitted for the first time.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Patient is already admitted under doctor: " + assignedDoctors[patientFound]);
                            }
                        }

                        break;


                    case 3: //Discharge Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string dischargeInput = Console.ReadLine();

                        int dischargeFound = SearchPatient(dischargeInput);

                        if (dischargeFound == -1)
                        {
                            Console.WriteLine("Patient not found");
                        }

                        else
                        {
                            if (admitted[dischargeFound] == true)
                            {
                                double visitCharges = 0;

                                Console.Write("Was there a consultation fee? (yes/no): ");
                                string hasFee = Console.ReadLine().ToLower();

                                if (hasFee == "yes")
                                {
                                    Console.Write("Enter consultation fee amount: ");
                                    double fee;
                                    if (double.TryParse(Console.ReadLine(), out fee) && fee > 0)
                                    {
                                        billingAmount[dischargeFound] += fee;
                                        visitCharges += fee;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount entered. No charge added.");
                                    }
                                }

                                Console.Write("Any medication charges? (yes/no): ");
                                string hasMeds = Console.ReadLine().ToLower();

                                if (hasMeds == "yes")
                                {
                                    Console.Write("Enter medication charges amount: ");
                                    double meds;
                                    if (double.TryParse(Console.ReadLine(), out meds) && meds > 0)
                                    {
                                        billingAmount[dischargeFound] += meds;
                                        visitCharges += meds;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount entered. No charge added.");
                                    }

                                }

                                if (visitCharges > 0)
                                {
                                    Console.WriteLine("Total charges added this visit: " + Math.Round(visitCharges, 2) + " OMR");
                                }
                                else
                                {
                                    Console.WriteLine("No charges recorded for this visit");
                                }

                                string assignedDoctorName = assignedDoctors[dischargeFound];
                                int doctorIndex = -1;

                                for (int i = 0; i <= lastDoctorIndex; i++)
                                {
                                    if (doctorNames[i].ToLower() == assignedDoctorName.ToLower())
                                    {
                                        doctorIndex = i;
                                        break;
                                    }
                                }

                                admitted[dischargeFound] = false;
                                assignedDoctors[dischargeFound] = "";

                                Console.WriteLine("Patient discharged successfully!");

                                if (doctorIndex != -1)
                                {
                                    doctorAvailableSlots[doctorIndex]++;
                                    Console.WriteLine("Dr. " + doctorNames[doctorIndex] + " now has " + doctorAvailableSlots[doctorIndex] + " slot(s) available.");
                                }
                                else
                                {
                                    Console.WriteLine("Warning: assigned doctor not found in registry. Slots not updated.");
                                }

                                Console.WriteLine("Total billing: " + Math.Round(billingAmount[dischargeFound], 2) + " OMR");

                                Console.WriteLine("Enter discharge date (YYYY-MM-DD):");
                                lastDischargeDate[dischargeFound] = DateTime.Parse(Console.ReadLine());

                                Console.WriteLine("Enter number of days the patient spent in hospital:");
                                int days = int.Parse(Console.ReadLine());

                                daysInHospital[dischargeFound] += days;

                                Console.WriteLine("Total days in hospital: " + daysInHospital[dischargeFound]);
                            }
                            else
                            {
                                Console.WriteLine("This patient is not currently admitted");
                            }

                            break;
                        }

                        break;

                    case 4: //Search Patient
                        Console.WriteLine("Enter patient ID or name");
                        string SearchInput = Console.ReadLine();

                        int SearchFound = SearchPatient(SearchInput);

                        if (SearchFound == -1)
                        {

                            Console.WriteLine("Patient not found");
                        }

                        else
                        {
                            PrintPatientDetails(SearchFound); 

                            if (admitted[SearchFound] == true)
                            {
                                Console.WriteLine("Assigned doctor: " + assignedDoctors[SearchFound]);
                            }
                            if (lastVisitDate[SearchFound] == DateTime.MinValue)
                            {
                                Console.WriteLine("Last Visit Date: No admission recorded");
                            }
                            else
                            {
                                Console.WriteLine("Last Visit Date: " + lastVisitDate[SearchFound]);
                            }
                            if (lastDischargeDate[SearchFound] == DateTime.MinValue)
                            {
                                Console.WriteLine("Last Discharge Date: Still admitted");
                            }
                            else
                            {
                                Console.WriteLine("Last Discharge Date: " + lastDischargeDate[SearchFound]);
                            }

                            break;
                        }

                        break;


                    case 5: //List All Admitted Patients
                        Console.WriteLine("Admitted Patients: ");

                        Console.Write("Filter by name keyword (press Enter to skip): ");
                        string keyword = Console.ReadLine();

                        int admittedCount = 0;
                        bool AdmittedFound = false;
                        double maxBilling = 0;

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i] == true)
                            {
                                if (keyword == "" || patientNames[i].ToLower().Contains(keyword.ToLower()))
                                {
                                    AdmittedFound = true;

                                    Console.WriteLine("Name: " + patientNames[i]);
                                    Console.WriteLine("ID: " + patientIDs[i]);
                                    Console.WriteLine("Diagnosis: " + diagnoses[i]);
                                    Console.WriteLine("Department: " + departments[i]);
                                    Console.WriteLine("Assigned doctor: " + assignedDoctors[i]);
                                    Console.WriteLine("Admitted Since: " + lastVisitDate[i]);

                                    maxBilling = Math.Max(maxBilling, billingAmount[i]); 

                                    admittedCount++;
                                }
                            }
                        }

                        if (admittedCount > 0)
                        {
                            Console.WriteLine("Total admitted patients: " + admittedCount);
                            Console.WriteLine("Highest billing among admitted patients: " + Math.Round(maxBilling, 2) + " OMR"); 
                        }

                        if (AdmittedFound == false)
                        {
                            Console.WriteLine("No patients currently admitted");
                        }

                        break;

                    case 6: //Transfer Patient to Another Doctor
                        Console.WriteLine("Enter current doctor name");
                        string CurrentDoctor = Console.ReadLine().Trim();
                        CurrentDoctor = CurrentDoctor.Replace("Dr ", "Dr. ");

                        Console.WriteLine("Enter new doctor name");
                        string NewDoctor = Console.ReadLine().Trim();
                        NewDoctor = NewDoctor.Replace("Dr ", "Dr. "); 

                        if (CurrentDoctor == NewDoctor)
                        {
                            Console.WriteLine("Transfer cancelled: the current doctor and the new doctor must be different.");
                            break;
                        }

                        bool CurrentDoctorFound = false;
                       
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if(CurrentDoctor == assignedDoctors[i] && admitted[i] == true)
                            {
                                CurrentDoctorFound = true; 
                                assignedDoctors[i] = NewDoctor;
                                Console.WriteLine("Patient: " + patientNames[i] + " has been transferred to: " + NewDoctor);
                                Console.WriteLine("Patient last admitted on: " + lastVisitDate[i]);
                                break;
                            }
                        }

                        if (CurrentDoctorFound == false) 
                        {

                            Console.WriteLine("No admitted patient found under this doctor");
                        }

                        break;

                    case 7: //View Most Visited Patients
                        Console.WriteLine("Most Visited Patients: ");
                        for (int count = 100; count >= 0; count--)
                        {
                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                if (visitCount[i] == count)
                                {
                                    Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i] + " | Department: " + departments[i] + " | Diagnosis: " + diagnoses[i] + " | Visit Count: " + visitCount[i]);
                                }
                            }

                        }
                        break;


                    case 8: //Search Patients by Department 
                        Console.WriteLine("Enter department name: ");
                        string DepartmentName = Console.ReadLine();

                        Console.WriteLine("Patients in department '" + DepartmentName.ToUpper() + "':");

                        bool DepartmentFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (departments[i] != null && departments[i].ToLower().Contains(DepartmentName.ToLower()))
                            {
                                DepartmentFound = true;
                                string AdmissionStatus = admitted[i] ? "Admitted" : "Not Admitted";

                                string displayDiagnosis;

                                if (diagnoses[i].Length > 15)
                                {
                                    displayDiagnosis = diagnoses[i].Substring(0, 15) + "...";
                                }
                                else
                                {
                                    displayDiagnosis = diagnoses[i];
                                }

                                Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i] + " | Blood Type: " + bloodType[i] + " | Diagnosis: " + displayDiagnosis + " | Admission Status: " + AdmissionStatus);
                            }
                        }

                        if (DepartmentFound == false)
                        {
                            Console.WriteLine("No patients found in this department");
                        }

                        break;

                    case 9:
                        Console.WriteLine("Billing Report");
                        Console.WriteLine("1. System-wide total");
                        Console.WriteLine("2. Individual patient");
                        Console.WriteLine("Choose option: ");

                        int option = 0;
                        try
                        {
                            option = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        }


                        if (option == 1)
                        {
                            double totalBilling = 0;
                            double maxBillingReport = 0;
                            double minBillingReport = billingAmount[0];
                            bool firstFound = false;

                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                totalBilling += billingAmount[i];

                                if (billingAmount[i] > 0)
                                {
                                    if (firstFound == false)
                                    {
                                        minBillingReport = billingAmount[i];
                                        firstFound = true;
                                    }

                                    maxBillingReport = Math.Max(maxBillingReport, billingAmount[i]);
                                    minBillingReport = Math.Min(minBillingReport, billingAmount[i]);
                                }
                            }

                            Console.WriteLine("System-wide total billing: " + Math.Round(totalBilling, 2) + " OMR");

                            if (firstFound)
                            {
                                Console.WriteLine("Highest individual billing: " + Math.Round(maxBillingReport, 2) + " OMR");
                                Console.WriteLine("Lowest individual billing: " + Math.Round(minBillingReport, 2) + " OMR");
                            }
                        }

                        else if (option == 2)
                        {
                            Console.WriteLine("Enter patient ID or name");
                            string Input = Console.ReadLine();

                            int BillingFound = SearchPatient(Input);

                            if (BillingFound == -1)
                            {
                                Console.WriteLine("No billing records found for this patient");
                            }

                            else
                            {

                                if (billingAmount[BillingFound] > 0)
                                {
                                    Console.WriteLine("Total billing: " + Math.Round(billingAmount[BillingFound], 2) + " OMR");

                                    Random rnd = new Random();
                                    int discount = rnd.Next(5, 21);

                                    double discountAmount = billingAmount[BillingFound] * discount / 100;
                                    double finalAmount = billingAmount[BillingFound] - discountAmount;

                                    Console.WriteLine("Discount applied: " + discount + "% — Amount after discount: " + Math.Round(finalAmount, 2) + " OMR");
                                }
                                else
                                {
                                    Console.WriteLine("No billing records");
                                }

                                Console.WriteLine("Last Visit Date: " + lastVisitDate[BillingFound]);
                                Console.WriteLine("Total Days: " + daysInHospital[BillingFound]);

                                break;
                            }
                                
                        }

                        break;

                    case 10: //Exit
                        Console.WriteLine("Are you sure you want to exit? (yes/no)");
                        string input = Console.ReadLine();

                        if(input == "yes")
                        {
                            Console.WriteLine("Exiting system...");
                            Console.WriteLine("Thank you for using the Healthcare Management System!");
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Exit cancelled. Returning to main menu."); 
                        }

                            break;

                    case 11:
                        Console.WriteLine("Enter doctor name: ");
                        string inputDoctorName = Console.ReadLine().Trim();

                        Console.WriteLine("Enter number of available slots: ");
                        string slotInput = Console.ReadLine();

                        int slots;
                        if (!int.TryParse(slotInput, out slots) || slots < 1)
                        {
                            Console.WriteLine("Invalid slot count. Doctor not registered.");
                            break;
                        }

                        bool doctorFound = false; 
                        for(int i = 0; i <= lastDoctorIndex; i++)
                        {
                            doctorFound = true;
                            if (doctorNames[i].ToLower() == inputDoctorName.ToLower()) 
                            {
                                lastDoctorIndex++;
                                doctorNames[lastDoctorIndex] = inputDoctorName;
                                doctorAvailableSlots[lastDoctorIndex] = slots;
                                doctorVisitCount[lastDoctorIndex] = 0;

                                Console.WriteLine("Doctor " + doctorNames[lastDoctorIndex] + " registered successfully with " + slots + " available slots.");
                                break;
                            }

                        }
                        if (doctorFound == false)
                        {
                            Console.WriteLine("Doctor already exists in the system.");
                        }

                        break;

                    case 12:

                        if (lastDoctorIndex == -1)
                        {
                            Console.WriteLine("No doctors registered in the system.");
                            break;
                        }

                        double highestSalary = 0;
                        int highestDoctorIndex = 0;

                        for (int i = 0; i <= lastDoctorIndex; i++)
                        {
                            double salary = 300 + (doctorVisitCount[i] * 15);
                            salary = Math.Round(salary, 2);

                            string salaryText = Convert.ToString(salary);

                            Console.WriteLine(doctorNames[i] + " | Visits: " + doctorVisitCount[i] +
                                              " | Available Slots: " + doctorAvailableSlots[i] +
                                              " | Salary: " + salaryText + " OMR");

                            if (i == 0)
                            {
                                highestSalary = salary;
                                highestDoctorIndex = i;
                            }

                            double maxSalary = Math.Max(highestSalary, salary);

                            if (maxSalary != highestSalary)
                            {
                                highestSalary = maxSalary;
                                highestDoctorIndex = i;
                            }
                        }

                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Highest earning doctor: " + doctorNames[highestDoctorIndex] + " — " + Convert.ToString(highestSalary) + " OMR");
                        
                        break;


                    default:
                        Console.WriteLine("Invalid option. Please try again."); 
                        break;

                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
          
            }


        }
    }
}
