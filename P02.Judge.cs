using System;
using System.Collections.Generic;
using System.Linq;

namespace P02.Judge
{

    class Student
    {
        public Student(string userName, string contest, int point)
        {
            this.UserName = userName;
            this.Contest = contest;
            this.Point = point;
        }

        public string UserName { get; set; }

        public string Contest { get; set; }

        public int Point { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentData = new List<Student>();

            string inputData = Console.ReadLine();

            while (inputData != "no more time")           // Read input data
            {
                string[] inputArray = inputData.Split(" -> ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string userName = inputArray[0];
                string contest = inputArray[1];
                int point = int.Parse(inputArray[2]);

                Student currentStudent = new Student(userName, contest, point);

                bool isCheckUserContest = true;

                for (int i = 0; i < studentData.Count; i++)
                {
                    if (studentData[i].Contest == contest && studentData[i].UserName == userName && studentData[i].Point < point)
                    {
                        studentData[i].Point = point;
                        isCheckUserContest = false;
                        break;
                    }
                }
                if (isCheckUserContest)
                {
                    studentData.Add(currentStudent);
                }
                inputData = Console.ReadLine();
            }


            PrintStudent(studentData);   //Print general data

            Console.WriteLine("Individual standings:");


            PrintIndividual(studentData);


        }

        static void PrintStudent(List<Student> studentData)
        {
            List<Student> printList = studentData
                           .OrderByDescending(x => x.Point)
                           .ThenBy(t => t.UserName)
                           .ToList();

            List<string> recycle = new List<string>();

            for (int i = 0; i < studentData.Count; i++)
            {
                string contest = studentData[i].Contest;

                if (recycle.Contains(contest))
                {
                    continue;
                }
                else
                {
                    Console.WriteLine($"{contest}: {GetNumberCount(studentData, contest)} participants");
                    recycle.Add(contest);

                    int count = 1;

                    for (int j = 0; j < printList.Count; j++)
                    {
                        if (printList[j].Contest == contest)
                        {
                            Console.WriteLine($"{count}. {printList[j].UserName} <::> {printList[j].Point}");
                            count++;
                        }
                    }
                }

            }
        }


        static int GetNumberCount(List<Student> studentData, string contest)
        {
            int count = 0;
            foreach (var item in studentData)
            {
                if (item.Contest == contest)
                {
                    count++;
                }
            }

            return count;
        }


        static void PrintIndividual(List<Student> studentData)
        {
            Dictionary<string, int> printStudentIndividual = new Dictionary<string, int>();
            List<Student> workList = studentData.ToList();

            for (int i = 0; i < workList.Count; i++)
            {
                int sum = workList[i].Point;
                string name = workList[i].UserName;

                for (int j = i + 1; j < workList.Count; j++)
                {
                    if (workList[j].UserName == name)
                    {
                        sum += workList[j].Point;
                        workList.RemoveAt(j);
                        j--;
                    }
                }

                printStudentIndividual.Add(name, sum);
            }


            int count = 1;

            foreach (var item in printStudentIndividual.OrderByDescending(key => key.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{count}. {item.Key} -> {item.Value}");
                count++;
            }
        }
    }
}
