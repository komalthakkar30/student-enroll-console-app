/***************************************
 * Assignment: 1
 * Due Date: Thursday, 13th Sep
 * 
 * Name: Komal Thakkar (Z1834925)
 * Partner Name: Sai Keerthi Tsundupalli (Z1836733)
 *
 * ************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace thakkar_Assign1
{
    enum AcademicYear { Freshman, Sophomore, Junior, Senior, PostBacc };
    class Student : IComparable
    {
        /******** Class Attributes and Getter Setter Methods ********/
        #region
        private readonly uint zID;
        private string firstName;
        private string lastName;
        private string major;
        private Nullable<float> cumulativeGPA;
        private Nullable<ushort> numOfCreditHoursEnrolled;
        private AcademicYear academicYear;
        public static List<Student> StudentPool = new List<Student>();

        public uint ZID {
            get { return zID; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { if (value != null) firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { if (value != null) lastName = value; }
        }
        public string Major
        {
            get { return major; }
            set { if (value != null) major = value; }
        }
        public Nullable<float> CumulativeGPA
        {
            get { return cumulativeGPA; }
            set
            {
                if (value - 4.000 <= 0)
                {
                    cumulativeGPA = value;
                }
                else
                {
                    //throw new Exception("GPA must be between 0 to 4.");
                }
            }
        }
        public Nullable<ushort> NumOfCreditHoursEnrolled
        {
            get { return numOfCreditHoursEnrolled; }
            set {
                if (value >= 0 && value <= 18) numOfCreditHoursEnrolled = value;
            }
        }
        public AcademicYear AcademicYears
        {
            get { return academicYear; }
            set { academicYear = value; }
        }
        #endregion

        /******** Constructors ********/
        #region
        public Student()
        {
            FirstName = null;
            LastName = null;
            Major = null;
            CumulativeGPA = null;
            NumOfCreditHoursEnrolled = 0;
            AcademicYears = 0;
        }

        public Student(uint zid, string lName, string fName, string majorStr, float cumGPA, AcademicYear ay)
        {
            if (zid > 1000000)
            {
                zID = zid;
                FirstName = fName;
                LastName = lName;
                Major = majorStr;
                CumulativeGPA = cumGPA;
                AcademicYears = ay;
                NumOfCreditHoursEnrolled = 0;
            }
            else
            {
                throw new Exception("Invalid Student Z-ID.");
            }
        }
        #endregion

        /******** Features ********/
        #region
        public void ReadStudentsFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a1_input01.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');
                    Student s = new Student(Convert.ToUInt32(tokens[0]), tokens[1], tokens[2], tokens[3], float.Parse(tokens[5]), (AcademicYear)Enum.Parse(typeof(AcademicYear), tokens[4]));
                    StudentPool.Add(s);
                    line = sr.ReadLine();
                }
                StudentPool.Sort();
                PrintMenu();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadKey();
            }
        }

        private void PrintMenu()
        {
            Student student = new Student();
            Course course = new Course();
            Console.WriteLine("We have 11 students and 9 courses");
            Console.WriteLine(" 1. Print Student List (All)\n 2. Print Student List(Major)\n 3. Print Student List(Academic Year)\n 4. Print Course List\n 5. Print Course Roster\n 6. Enroll Student\n 7. Drop Student\n 8. Quit");
            Console.WriteLine("Please enter your choice: ");

            string inp = Console.ReadLine().Trim().ToLower();
            string userInput;

            while (inp != null)
            {
                switch (inp)
                {
                    case ("1"):
                        Console.WriteLine("Student List (All Students): ");
                        foreach (Student obj in StudentPool)
                        {
                            System.Console.WriteLine(obj);
                        }
                        break;
                    case ("2"):
                        Console.WriteLine("Which major would you like to printed?");
                        userInput = Console.ReadLine().Trim();
                        if (StudentPool.Exists(x => x.major == userInput))
                        {
                            Console.WriteLine("Student List (" + userInput + " Majors): ");
                            foreach (Student obj in StudentPool)
                            {
                                if(obj.major == userInput)
                                    System.Console.WriteLine(obj);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Student List <" + userInput + " Major" + ">");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("There doesn't appear to be any students majoring in" + "'" + userInput + "'");
                        }
                        break;
                    case ("3"):
                        Console.WriteLine("Which academic year would you like printed?");
                        Console.WriteLine("( Freshman, Sophomore, Junior, Senior, PostBacc )");
                        userInput = Console.ReadLine().Trim();
                        if (StudentPool.Exists(x => ((AcademicYear)(int)x.academicYear).ToString() == userInput))
                        {
                            Console.WriteLine("Student List (" + userInput + " Majors): ");
                            foreach (Student obj in StudentPool)
                            {
                                if(((AcademicYear)(int)obj.academicYear).ToString() == userInput)
                                    System.Console.WriteLine(obj);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Student List <" + userInput + " Majors" + ">");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("There doesn't appear to be any students majoring in" + "'" + userInput + "'");
                        }
                        break;
                    case ("4"):
                        Console.WriteLine("Course List (All Courses): ");
                        Console.WriteLine("------------------------------------");
                        foreach (Course obj in Course.CoursePool)
                        {
                            Console.WriteLine(obj);
                        }
                        break;
                    case ("5"):
                        Console.WriteLine("Which course roaster would you like printed?");
                        Console.Write("(DEPT COURSE_NUM-SECTION_NUM) ");
                        course.PrintRoster();
                        break;
                    case ("6"):
                        try
                        {
                            Console.WriteLine("Please enter the Z-ID (omitting the Z character) of the student you like to enroll into a course.");
                            uint userZID = Convert.ToUInt32(Console.ReadLine().Trim());
                            Console.WriteLine("Which course will this student be enrolled into?");
                            userInput = Console.ReadLine().Trim();

                            string[] words = userInput.Split(' ');
                            string dept = words[0];
                            string[] Words1 = words[1].Split('-');
                            string courseNum = Words1[0];
                            string sectionNum = Words1[1];

                            course = Course.CoursePool.Find(x => x.DeptCode == dept && x.CourseNumber.ToString() == courseNum && x.SectionNumber == sectionNum);
                            student = StudentPool.Find(x => x.ZID == userZID);
                            if (course != null && student != null)
                            {
                                student.Enroll(course);
                            }
                            else
                            {
                                Console.WriteLine("Entered Student or Course details does not exist.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " Please try again! \n\n");
                            PrintMenu();
                        }
                        break;
                    case ("7"):
                        try
                        {
                            Console.WriteLine("Please enter the Z-ID (omitting the Z character) of the student you like to drop into a course.");
                            uint userZID = Convert.ToUInt32(Console.ReadLine().Trim());
                            Console.WriteLine("Which course will this student be dropped from?");
                            userInput = Console.ReadLine().Trim();

                            string[] words = userInput.Split(' ');
                            string dept = words[0];
                            string[] Words1 = words[1].Split('-');
                            string courseNum = Words1[0];
                            string sectionNum = Words1[1];

                            course = Course.CoursePool.Find(x => x.DeptCode == dept && x.CourseNumber.ToString() == courseNum && x.SectionNumber == sectionNum);
                            student = StudentPool.Find(x => x.ZID == userZID);
                            if (course != null && student != null)
                            {
                                student.Drop(course);
                            }
                            else
                            {
                                Console.WriteLine("Entered Student or Course details are not valid.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message + " Please try again!");
                            PrintMenu();
                        }
                        break;
                    case ("8"):
                    case ("h"):
                    case ("q"):
                    case ("quit"):
                    case ("exit"):
                        Console.WriteLine("Good Bye!");
                        Environment.Exit(0);
                        break;

                }
                Console.WriteLine("\n\n We have 11 students and 9 courses");
                Console.WriteLine(" 1. Print Student List (All)\n 2. Print Student List(Major)\n 3. Print Student List(Academic Year)\n 4. Print Course List\n 5. Print Course Roster\n 6. Enroll Student\n 7. Drop Student\n 8. Quit");
                Console.WriteLine("Please enter your choice: ");
                inp = Console.ReadLine().Trim();
            }
        }

        public int Enroll(Course newCourse)
        {
            if (newCourse.NumOfCurrentlyEnrolled + 1 > newCourse.MaxCapacity)
            {
                Console.WriteLine("Error: " + newCourse.DeptCode + " " + newCourse.CourseNumber + "-" + newCourse.SectionNumber + " is full. No seat available.");
                return 5;
            }
            else if (newCourse.CurrentlyEnrolled.Contains((int)this.zID))
            {
                Console.WriteLine("Error: Z" + this.ZID + " is already enrolled into this course.");
                return 10;
            }
            else if (newCourse.CreditHours + this.NumOfCreditHoursEnrolled > 18)
            {
                Console.WriteLine("Error: Z" + this.ZID + " has already enrolled for 18 credits.");
                return 15;
            }
            this.NumOfCreditHoursEnrolled = (ushort)(this.NumOfCreditHoursEnrolled + newCourse.CreditHours);
            newCourse.NumOfCurrentlyEnrolled = (ushort)((int)(newCourse.NumOfCurrentlyEnrolled) + 1);
            newCourse.CurrentlyEnrolled.Add((int)this.ZID);

            Console.WriteLine("Z" + this.ZID + " has successfully been enrolled into " + newCourse.DeptCode + " " + newCourse.CourseNumber + "-" + newCourse.SectionNumber);
            return 0;
        }

        public int Drop(Course newCourse)
        {
            if (!newCourse.CurrentlyEnrolled.Contains((int)this.zID))
            {
                return 20;
            }
            this.NumOfCreditHoursEnrolled = (ushort)(this.NumOfCreditHoursEnrolled - newCourse.CreditHours);
            newCourse.NumOfCurrentlyEnrolled = (ushort)((int)(newCourse.NumOfCurrentlyEnrolled) - 1);
            newCourse.CurrentlyEnrolled.Remove((int)this.ZID);

            Console.WriteLine("Z" + this.ZID + " has successfully been dropped into " + newCourse.DeptCode + " " + newCourse.CourseNumber + "-" + newCourse.SectionNumber);
            return 0;
        }
        #endregion

        /******** Built in Methods ********/
        #region
        public int CompareTo(Object alpha)
        {
            if (alpha == null) return 1;
            Student s = alpha as Student;
            if (s != null)
            {
                return this.zID.CompareTo(s.zID);
            }
            else
            {
                throw new ArgumentException("[Student]:CompareTo argument is not a Student");
            }
        }

        public override string ToString()
        {
            return String.Format("{0,-8} -- {1,13}, {2,-10} [{3,9}] ({4,15}) | {5,5} |", "Z"+ ZID, LastName, FirstName, AcademicYears, Major, CumulativeGPA);
        }
        #endregion

        static void Main(string[] args)
        {
            Student student = new Student();
            Course course = new Course();
            course.ReadCoursesFile();
            student.ReadStudentsFile();
        }
    }
}
