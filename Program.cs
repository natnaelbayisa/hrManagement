using System;
using System.Threading;
using System.Linq;


namespace hrCSharp
{


    public class HRManager{
        public struct EmployeeData{
            public string id, name, age, position ;
        }
             int employeeCount = 0;

        static public EmployeeData [] employeeList = new EmployeeData[20];

           
        public void notFound(){
            Console.WriteLine("\n\t[!] the requested data doesn't exist .. \n");
        }


         public EmployeeData[] getAll(){
            return employeeList;
        }

        //  simple functions 
        public int getEmployeesByName(){
            bool isFound = false;
            string name ;
            Console.Write("\n\t>> enter the name: ");
            name   = Console.ReadLine();
            EmployeeData[] allData = getAll();

            foreach(EmployeeData employee in allData ){
                if(name == employee.name){
                    isFound = true;
                    Console.WriteLine("{0} -- {1} -- {2} -- {3}",employee.id,employee.name, employee.age, employee.position);
                }
            }

            if(!isFound) notFound();

            return 1;
        }

        public bool getEmployeeById(){
            string id;
            bool isFound = false;
            Console.Write(" \n\t>> enter the id: ");
            id = Console.ReadLine();


            EmployeeData[] allData = getAll();

            foreach(EmployeeData employee in allData ){
                if(id == employee.id){
                    displayData();
                    isFound = true;
                    break;
                }
            }

            if(isFound == false){
               notFound();
            }

            return isFound;
        }


        public void addData(){
            Console.WriteLine("\n\n\t\t [+] ADD EMPLOYEE\n\n");
            Console.Write("\n\t\t id: ");
            employeeList[employeeCount].id =Console.ReadLine();
            Console.Write("\n\t\t Name: ");
            employeeList[employeeCount].name =Console.ReadLine();
            Console.Write("\n\t\t Age: ");
            employeeList[employeeCount].age =Console.ReadLine();
            Console.Write("\n\t\t Position: ");
            employeeList[employeeCount].position =Console.ReadLine();
            
            employeeCount++;
        }

        public void displayData(){
            if(noRecord()){
                Console.WriteLine("\n\n\t-----------------------------");
                Console.WriteLine("\t THERE IS NO EMPLOYEE RECORD");
                Console.WriteLine("\n\t-----------------------------");
            } else{
                EmployeeData[] allData = getAll();
                for(int i = 0;i< employeeCount;i++){
                    Console.WriteLine("\t_______________________");
                    Console.WriteLine("\n\n\tID: {0} \n\tName: {1} \n\tAge: {2} \n\tPosition: {3}", allData[i].id, allData[i].name, allData[i].age, allData[i].position);
                    Console.WriteLine("\t_______________________");
                }
            }
        }


        public  int  displaySingle( string searchBy = "id"){ 

            if(noRecord()){
                Console.WriteLine("\n\n\t-----------------------------");
                Console.WriteLine("\t THERE IS NO EMPLOYEE RECORD");
                Console.WriteLine("\n\t-----------------------------");
            } else{
                getEmployeeById();
                return 1;
            }
            return 0;
        }


        public int search(){

            Console.Write("\n\n\t>> search by id or name (id / name) ");
            string key;
            key = Console.ReadLine();

            if (key == "name"){
                getEmployeesByName();
            } else{
               getEmployeeById();
            }
            return 1;
        }

        public void updateEmployeeData(){
            string id;
            Console.Write("\t\t>> enter id: ");
            id = Console.ReadLine();
            int index = 0;
            int foundIndex = -1;
            foreach(EmployeeData employee in employeeList){
                if(id == employee.id){
                    Console.WriteLine(id);
                    foundIndex = index;
                    // addData
                };
                index++;
            }

            if(foundIndex != -1){
                Console.WriteLine("\n\n\t\t [+] EDIT EMPLOYEE\n\n");
                Console.Write("\n\t\t Name: ");
                employeeList[foundIndex].name =Console.ReadLine();
                Console.Write("\n\t\t Age: ");
                employeeList[foundIndex].age =Console.ReadLine();
                Console.Write("\n\t\t Position: ");
                employeeList[foundIndex].position =Console.ReadLine();
            }
            displayData();

        }

        public void deleteEmployee(){
            Console.Write("\t\t>> enter id: ");
            string id = Console.ReadLine();

            employeeList =  getAll().Where(employee => employee.id != id).ToArray();
            employeeCount--;

            displayData();
        }

        private bool noRecord(){
            if(employeeCount == 0) return true;
            return false;
        }
    }

    class Program
    {

        static void Main(string[] args)
        { 
            HRManager hr = new HRManager();



            int choice;
            intro();
            Thread.Sleep(2000);


            do{
                m:
                Console.Clear();

                menu();
                choice = int.Parse(Console.ReadLine());

                if(choice == 1){
                    Console.Clear();
                    hr.addData();
                } 
                 if(choice == 2){
                     Console.Clear();
                     hr.displayData();
                     Console.Write("\n\t >> press any key to continue ...");
                     Console.ReadKey();
                 }

                 if(choice == 3){
                     Console.Clear();
                     g:
                     hr.displaySingle();
                     
                     readKey();
                     if(Console.ReadKey(true).Key == ConsoleKey.R){
                         goto g;
                     } else goto m;
                 }
                 if(choice == 4){
                     Console.Clear();
                     s:
                     hr.search();
                     
                     readKey();
                     if(Console.ReadKey(true).Key == ConsoleKey.R){
                         goto s;
                     } else goto m;
                 }

                 if(choice == 5){
                     Console.Clear();
                     hr.updateEmployeeData();
                     
                     Console.ReadKey();
                 }

                 if(choice == 6){
                     Console.Clear();
                     hr.deleteEmployee();
                     
                     Console.ReadKey();
                 }



            }while(choice != 0);

            showGroupMembers();
            

            Thread.Sleep(2000);

            Console.Clear();
            Console.WriteLine("\n\n\n\n\t\t\t\t\t\tTHANK YOU ....");
            Thread.Sleep(1000);
            Console.Clear();
        }
        
        
        static void intro(){
            Console.Clear();
            Console.WriteLine("\t+--------------------------------------------------------------------");
            Console.WriteLine("\t| \n\t|");
            Console.WriteLine("\t|  \tWELCOME TO HR MANAGEMENT SYSTEM\n\t|\n\t|");
            Console.WriteLine("\t|");
            Console.WriteLine("\t+_____________________________________________________________________");
        }

        static void menu(){
            Console.WriteLine("\t 1. Add Employees");
            Console.WriteLine("\t 2. Display Employees");
            Console.WriteLine("\t 3. Display Single Employees");
            Console.WriteLine("\t 4. Search Employees");
            Console.WriteLine("\t 5. Update Employee");
            Console.WriteLine("\t 6. Delete Employee");
            Console.WriteLine("\t 7. Sort");
            Console.WriteLine("\t 0. Exit");

            Console.Write("\n\t >> enter your choice: ");
        }

        static void readKey(){
            Console.Write("\t[!!] press r to get another employee's data or other key to menu ... ");
        }

        // additional code >> just freeking out

        static void showGroupMembers()
        {
            Console.Clear();
            string[] members = {"Faruq Ismael --- 14819/20 ","Natnael Bayisa --- 14629/20","Amanuel Frew --- 14704/20","Ephrata Tesfaye --- 15255/20"};

            Console.WriteLine("\n\n\n\n\n\t\t\t\t\t\tGROUP MEMBERS");

            for (int i = 0; i < members.Length; i++) {
                
                Console.WriteLine($"\t\t\t\t{i+1}. {members[i]} --- CS2  ");

                Thread.Sleep(1000);
            }
        }
    }
}
