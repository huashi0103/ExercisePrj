using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    class Student
    {
        public string RollNo { get; set; }
        public string Name { get; set; }

    }
    class StudentView
    {
        public void PrintDetail(string studentname, string rollno)
        {
            Console.WriteLine("Student");
            Console.WriteLine("name:{0}", studentname);
            Console.WriteLine("rollno:", rollno);
        }
    }
    class StudentController
    {
        private Student model;
        private StudentView view;
        public StudentController(Student student, StudentView view)
        {
            model = student;
            this.view = view;

        }
        public void SetName(string name)
        {
            model.Name = name;
        }
        public string getName()
        {
            return model.Name;
        }
        public void setRollNo(string rollno)
        {
            model.RollNo = rollno;

        }
        public string getRollNo()
        {
            return model.RollNo;
        }
    }
}
