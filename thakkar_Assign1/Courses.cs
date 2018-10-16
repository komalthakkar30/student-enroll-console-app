using System;
using System.Collections.Generic;
using System.IO;

namespace thakkar_Assign1
{
    class Course : IComparable
    {
        /******** Class Attributes and Getter Setter Methods ********/
        #region
        private string deptCode;
        private Nullable<uint> courseNumber;
        private string sectionNumber;
        private Nullable<ushort> creditHours;
        private List<int> currentlyEnrolled = new List<int>();
        private Nullable<ushort> numOfCurrentlyEnrolled;
        private Nullable<ushort> maxCapacity;
        public static List<Course> CoursePool = new List<Course>();

        public string DeptCode
        {
            get { return deptCode; }
            set
            {
                if(value.Length >= 1 && value.Length <= 4) deptCode = value.ToUpper();
            }
        }
        public Nullable<uint> CourseNumber
        {
            get { return courseNumber; }
            set { if(value <= 100 && value >= 499) courseNumber = value; }
        }
        public string SectionNumber
        {
            get { return sectionNumber; }
            set
            {
                if(value.Length == 4) sectionNumber = value;
            }
        }
        public Nullable<ushort> CreditHours
        {
            get { return creditHours; }
            set { if(value >= 0 && value <= 6) creditHours = value; }
        }
        public Nullable<ushort> MaxCapacity
        {
            get { return maxCapacity; }
            set { maxCapacity = value; }
        }
        public Nullable<ushort> NumOfCurrentlyEnrolled
        {
            get { return numOfCurrentlyEnrolled; }
            set { numOfCurrentlyEnrolled = value; }
        }
        public List<int> CurrentlyEnrolled
        {
            get { return currentlyEnrolled; }
            set { currentlyEnrolled.Add(value[0]); }
        }
        #endregion

        /******** Constructor ********/
        #region
        public Course()
        {
            deptCode = "";
            courseNumber = null;
            sectionNumber = "";
            creditHours = null;
            maxCapacity = 0;
            numOfCurrentlyEnrolled = 0;
        }
        #endregion

        /******** Features ********/
        #region
        public void ReadCoursesFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a1_input02.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');

                    Course c = new Course();
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        c.deptCode = tokens[0];
                        c.courseNumber = Convert.ToUInt32(tokens[1]);
                        c.sectionNumber = tokens[2];
                        c.creditHours = Convert.ToUInt16(tokens[3]);
                        c.maxCapacity = Convert.ToUInt16((tokens[4]));

                    }
                    CoursePool.Add(c);
                    line = sr.ReadLine();
                }
                CoursePool.Sort();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void PrintRoster()
        {
            try
            {
                string userInput = Console.ReadLine();
                string[] words = userInput.Split(' ');
                string dept = words[0];
                string[] Words1 = words[1].Split('-');
                string courseNum = Words1[0];
                string sectionNum = Words1[1];

                if (Course.CoursePool.Exists(x => x.DeptCode == dept && x.CourseNumber.ToString() == courseNum && x.SectionNumber == sectionNum))
                {
                    Course obj = Course.CoursePool.Find(x => x.DeptCode == dept && x.CourseNumber.ToString() == courseNum && x.SectionNumber == sectionNum);

                    if (obj != null && obj.DeptCode == dept && obj.CourseNumber.ToString() == courseNum && obj.SectionNumber == sectionNum)
                    {
                        Console.WriteLine("Course: " + obj.DeptCode + " " + obj.CourseNumber + "-" + obj.SectionNumber + " (" + obj.NumOfCurrentlyEnrolled + "/" + obj.MaxCapacity + ")");
                        Console.WriteLine("------------------------------------");
                        if (Convert.ToInt16(obj.NumOfCurrentlyEnrolled) == 0)
                        {
                            Console.WriteLine("There isn't anyone enrolled into " + obj.DeptCode + " " + obj.CourseNumber + "-" + obj.SectionNumber + ".");
                        }
                        else
                        {
                            foreach (int rec in currentlyEnrolled)
                            {
                                Student s = Student.StudentPool.Find(x => x.ZID == rec);
                                Console.WriteLine(s.ZID + " " + s.FirstName + " " + s.LastName + " " + s.Major);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Record not found with given details. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Record with given information not found.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Please enter valid information.\n" + e.Message);
                Console.ReadKey();
            }
        }
        #endregion

        /******** Built in Methods ********/
        #region
        public override string ToString()
        {
            return DeptCode + " " + CourseNumber + "-" + SectionNumber + "  (" + NumOfCurrentlyEnrolled + "/" + MaxCapacity + ")";
        }

        public int CompareTo(Object alpha)
        {
            if (alpha == null) return 1;
            Course c = alpha as Course;
            int sortLevel1 = this.deptCode.ToString().CompareTo(c.deptCode.ToString());
            if (sortLevel1 != 0) return sortLevel1;
            else return this.courseNumber.ToString().CompareTo(c.courseNumber.ToString());
        }
        #endregion
    }
}