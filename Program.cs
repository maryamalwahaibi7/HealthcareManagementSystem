using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Linq;

namespace HealthcareManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //System Storage
            string[] patientNames = new string[100];
            string[] patientIDs = new string[100];
            string[] diagnoses = new string[100];
            bool[] admitted = new bool[100];       // true = currently admitted
            string[] assignedDoctors = new string[100];
            string[] departments = new string[100];     // e.g. "Cardiology", "Orthopedics"
            int[] visitCount = new int[100];        // how many times admitted
            double[] billingAmount = new double[100];     // total fees owed
            int lastPatientIndex = 0;

            //Seed Data
            patientNames[lastPatientIndex] = "Ali Hassan";
            patientIDs[lastPatientIndex] = "P000";
            diagnoses[lastPatientIndex] = "Flu";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            departments[lastPatientIndex] = "General";
            visitCount[lastPatientIndex] = 2;
            billingAmount[lastPatientIndex] = 0;

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Sara Ahmed";
            patientIDs[lastPatientIndex] = "P001";
            diagnoses[lastPatientIndex] = "Fracture";
            admitted[lastPatientIndex] = true;
            assignedDoctors[lastPatientIndex] = "Dr. Noor";
            departments[lastPatientIndex] = "Orthopedics";
            visitCount[lastPatientIndex] = 4;
            billingAmount[lastPatientIndex] = 0;

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Omar Khalid";
            patientIDs[lastPatientIndex] = "P002";
            diagnoses[lastPatientIndex] = "Diabetes";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            departments[lastPatientIndex] = "Cardiology";
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;

            lastPatientIndex++;

            bool exit = false;
            while (exit == false)
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
                        patientNames[lastPatientIndex] = Console.ReadLine();

                        //Console.WriteLine("Patient ID: "); 
                        //patientIDs[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Diagnosis: ");
                        diagnoses[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Department: ");
                        departments[lastPatientIndex] = Console.ReadLine();

                        patientIDs[lastPatientIndex] = "P" + lastPatientIndex.ToString("D3") ; 
                        admitted[lastPatientIndex] = false;
                        assignedDoctors[lastPatientIndex] = "";
                        visitCount[lastPatientIndex] = 0;
                        billingAmount[lastPatientIndex] = 0;


                        Console.WriteLine("Patient registered successfully with ID: " + patientIDs[lastPatientIndex]);
                        lastPatientIndex++;

                        break;

                    case 2: //Admit Patient
                        Console.WriteLine("Enter patient ID or name");
                        string AdmitInput = Console.ReadLine();

                        bool patientFound = false;
                        for(int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientNames[i] == AdmitInput || patientIDs[i]== AdmitInput)
                            {
                                patientFound = true;

                                if (admitted[i] == false) 
                                {
                                    Console.WriteLine("enter doctor name : ");
                                    assignedDoctors[i] = Console.ReadLine();
                                    admitted[i] = true;
                                    visitCount[i]++;

                                    Console.WriteLine("Patient admitted successfully and assigned to doctor: " + assignedDoctors[i]);

                                    if (visitCount[i] > 1)
                                    {
                                        Console.WriteLine("This patient has been admitted " + visitCount[i] + " times");

                                    }
                                    else
                                    {
                                        Console.WriteLine("The patient is being admitted for the first time.");
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("Patient is already admitted under doctor: " + assignedDoctors[i]);
                                }
                                break;

                            }
                                

                        }

                        if (patientFound == false)
                        {

                            Console.WriteLine("Patient not found");
                        }
                        break;


                    case 3: //Discharge Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string dischargeInput = Console.ReadLine();

                        bool dischargeFound = false;

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientNames[i] == dischargeInput || patientIDs[i] == dischargeInput)
                            {
                                dischargeFound = true;

                                if (admitted[i] == true)
                                {
                                    double visitCharges = 0;

                                    Console.Write("Was there a consultation fee? (yes/no): ");
                                    string hasFee = Console.ReadLine().ToLower();

                                    if (hasFee == "yes")
                                    {
                                        Console.Write("Enter consultation fee amount: ");
                                        double fee = 0;
                                        try
                                        {
                                            fee = double.Parse(Console.ReadLine());
                                            if (fee > 0)
                                            {
                                                billingAmount[i] += fee;
                                                visitCharges += fee;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The amount is invalid");
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                                        }
                                    }

                                    Console.Write("Any medication charges? (yes/no): ");
                                    string hasMeds = Console.ReadLine().ToLower();

                                    if (hasMeds == "yes")
                                    {
                                        Console.Write("Enter medication charges amount: ");
                                        double meds = 0;
                                        try
                                        {
                                            meds = double.Parse(Console.ReadLine());
                                            if (meds > 0)
                                            {
                                                billingAmount[i] += meds;
                                                visitCharges += meds;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The amount is invalid");
                                            }
                                            
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                                        }

                                    }

                                    if (visitCharges > 0)
                                    {
                                        Console.WriteLine("Total charges added this visit: " + visitCharges + " OMR");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No charges recorded for this visit");
                                    }

                                    admitted[i] = false;
                                    assignedDoctors[i] = "";

                                    Console.WriteLine("Patient discharged successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("This patient is not currently admitted");
                                }

                                break;
                            }
                        }

                        if (dischargeFound == false)
                        {
                            Console.WriteLine("Patient not found");
                        }

                        break;


                    case 4: //Search Patient
                        Console.WriteLine("Enter patient ID or name");
                        string SearchInput = Console.ReadLine();

                        bool SearchFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientNames[i] == SearchInput || patientIDs[i] == SearchInput)
                            {
                                SearchFound = true;

                                Console.WriteLine("Name: "+ patientNames[i]);
                                Console.WriteLine("ID: " + patientIDs[i]);
                                Console.WriteLine("Diagnosis: "+ diagnoses[i]);
                                Console.WriteLine("Admission status: " + admitted[i]);
                                Console.WriteLine("Visit count: "+ visitCount[i]);
                                Console.WriteLine("Total billing amount: "+ billingAmount[i]);

                                if(admitted[i]== true)
                                {
                                    Console.WriteLine("Assigned doctor: " + assignedDoctors[i]);
                                }
                               
                                break;

                            }


                        }

                        if (SearchFound == false)
                        {

                            Console.WriteLine("Patient not found");
                        }

                        break;


                    case 5: //List All Admitted Patients
                        Console.WriteLine("Admitted Patients: ");

                        int admittedCount = 0; 
                        bool AdmittedFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i]== true)
                            {
                                AdmittedFound = true;
                                Console.WriteLine("Name: " + patientNames[i]);
                                Console.WriteLine("ID: " + patientIDs[i]);
                                Console.WriteLine("Diagnosis: " + diagnoses[i]);
                                Console.WriteLine("Department: "+ departments[i]);
                                Console.WriteLine("Assigned doctor: " + assignedDoctors[i]);
                                admittedCount++;
                            }

                        }

                        if (admittedCount > 0)
                        {
                            Console.WriteLine("Total admitted patients: " + admittedCount);
                        }

                        if (AdmittedFound == false)
                        {
                            Console.WriteLine("No patients currently admitted");
                        }

                            break;


                    case 6: //Transfer Patient to Another Doctor
                        Console.WriteLine("Enter current doctor name");
                        string CurrentDoctor = Console.ReadLine();

                        Console.WriteLine("Enter new doctor name");
                        string NewDoctor = Console.ReadLine();

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

                        Console.WriteLine("Patients in department '" + DepartmentName + "':");

                        bool DepartmentFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (departments[i] != null && departments[i].ToLower().Contains(DepartmentName.ToLower()))
                            {
                                DepartmentFound = true;
                                string AdmissionStatus = admitted[i] ? "Admitted" : "Not Admitted";
                                Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i] + " | Diagnosis: " + diagnoses[i] + " | Admission Status: " + AdmissionStatus);
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

                        double totalBilling = 0;

                        if (option == 1)
                        {
                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                totalBilling += billingAmount[i];
                            }

                            Console.WriteLine("System-wide total billing: " + totalBilling + " OMR");
                        }

                        else if (option == 2)
                        {
                            Console.WriteLine("Enter patient ID or name");
                            string Input = Console.ReadLine();

                            bool BillingFound = false;
                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                if (patientNames[i] == Input || patientIDs[i] == Input)
                                {
                                    BillingFound = true;

                                    if(billingAmount[i] > 0)
                                    {
                                        Console.WriteLine("Total billing: " + billingAmount[i] + " OMR");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No billing records");
                                    }

                                    break;

                                }
                            }

                            if (BillingFound == false)
                            {

                                Console.WriteLine("No billing records found for this patient");
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

                    default:
                        Console.WriteLine("Invalid option. Please try again."); 
                        break;

                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
          
            }


        }
    }
}
