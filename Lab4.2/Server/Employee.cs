using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Employee
    {
        private int employeeID;
        private int lastNameSize;
        private string lastName;
        private int firstNameSize;
        private string firstName;
        private int yearService;
        private double salary;
        private int size;
        private byte[] dataInByte = null;

        public byte[] DataInByte
        {
            get 
            {
                if (dataInByte == null)
                    dataInByte = getByte();
                return dataInByte; 
            }
            private set { dataInByte = value; }
        }



        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public int LastNameSize
        {
            get { return lastNameSize; }
            set { lastNameSize = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set 
            { 
                lastName = value;
                lastNameSize = lastName.Length;
            }
        }
        public int FirstNameSize
        {
            get { return firstNameSize; }
            set { firstNameSize = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set 
            {
                firstName = value;
                firstNameSize = firstName.Length;
            }
        }
        public int YearService
        {
            get { return yearService; }
            set { yearService = value; }
        }
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public Employee()
        { }
        public Employee(int employeeID, string lastName, string firstName,int yearSV, double salary)
        {
            EmployeeID = employeeID;
            LastName = lastName;
            FirstName = firstName;
            YearService = yearSV;
            Salary = salary;
            DataInByte = getByte();
        }

        public Employee(byte[] data)
        {
            DataInByte = data;
            int place = 0;
            EmployeeID = BitConverter.ToInt32(data, place);
            place += 4;
            LastNameSize = BitConverter.ToInt32(data, place);
            place += 4;
            LastName = Encoding.ASCII.GetString(data, place, LastNameSize);
            place += LastNameSize;
            FirstNameSize = BitConverter.ToInt32(data, place);
            place += 4;
            FirstName = Encoding.ASCII.GetString(data, place, FirstNameSize);
            place += FirstNameSize;
            YearService = BitConverter.ToInt32(data, place);
            place += 4;
            Salary = BitConverter.ToDouble(data, place);
            place += 8;
            Size = place;
        }

        public byte[] getByte()
        {
            byte[] data = new byte[1024];
            int place = 0;

            Buffer.BlockCopy(BitConverter.GetBytes(EmployeeID), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(BitConverter.GetBytes(LastNameSize), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(LastName), 0, data, place, LastNameSize);
            place += LastNameSize;
            Buffer.BlockCopy(BitConverter.GetBytes(FirstNameSize), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(FirstName), 0, data, place, FirstNameSize);
            place += FirstNameSize;
            Buffer.BlockCopy(BitConverter.GetBytes(YearService), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(BitConverter.GetBytes(Salary), 0, data, place, 8);
            place += 8;
            Size = place;
            return data;
        }

        public void WriteData()
        {
            Console.Write("ID: ");
            EmployeeID = Int32.Parse(Console.ReadLine());
            Console.Write("Last name: ");
            LastName = Console.ReadLine();
            Console.Write("First name: ");
            FirstName = Console.ReadLine();
            Console.Write("Year service: ");
            YearService = Int32.Parse(Console.ReadLine());
            Console.Write("Salary: ");
            Salary = Double.Parse(Console.ReadLine());
            DataInByte = getByte();
        }

        public override string ToString()
        {
            string s = "";
            s += String.Format("ID: {0}", EmployeeID);
            s += "\r\n";
            s += String.Format("Last name: {0}", LastName);
            s += "\r\n";
            s += String.Format("First name: {0}", FirstName);
            s += "\r\n";
            s += String.Format("Year service: {0}", YearService);
            s += "\r\n";
            s += String.Format("Salary: {0}", Salary);
            s += "\r\n";
            return s;
        }
    }
}
