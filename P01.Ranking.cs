using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Ranking
{
    class Raser
    {
        public Raser(string contest, string password, string userName, int point)
        {
            this.Contest = contest;
            this.Password = password;
            this.UserName = userName;
            this.Point = point;
        }

        public string Contest { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public int Point { get; set; }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> checkDick = new Dictionary<string, string>();

            string command = Console.ReadLine();

            while (command != "end of contests")          // Read first input
            {
                string[] commandArray = command
                    .Split(':', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string contest = commandArray[0];
                string password = commandArray[1];

                checkDick.Add(contest, password);

                command = Console.ReadLine();
            }

            List<Raser> raserList = new List<Raser>();
            string commandArg = Console.ReadLine();

            while (commandArg != "end of submissions")   // Read second input
            {
                string[] secondCommand = commandArg
                    .Split("=>", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string contest = secondCommand[0];
                string password = secondCommand[1];
                string userName = secondCommand[2];
                int points = int.Parse(secondCommand[3]);

                if (checkDick.ContainsKey(contest) && checkDick[contest] == password)
                {
                    bool isCheck = true;

                    for (int i = 0; i < raserList.Count; i++)
                    {
                        if (raserList[i].Contest == contest && raserList[i].UserName == userName && raserList[i].Point < points)
                        {
                            raserList[i].Point = points;
                            isCheck = false;
                            break;

                        }
                        else if (raserList[i].Contest == contest && raserList[i].UserName == userName && raserList[i].Point >= points)
                        {
                            isCheck = false;
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        Raser player = new Raser(contest, password, userName, points);
                        raserList.Add(player);
                    }
                }
                commandArg = Console.ReadLine();
            }


            PrintUserMaxPoint(raserList);

            List<Raser> printRaser = raserList.OrderBy(x => x.UserName).ThenByDescending(t => t.Point).ToList();

            PrintRegularUser(printRaser);


            static void PrintUserMaxPoint(List<Raser> raserList)        
            {
                Dictionary<string, int> maxPointDict = new Dictionary<string, int>();    


                foreach (var item in raserList)
                {
                    if (!maxPointDict.ContainsKey(item.UserName))
                    {
                        maxPointDict.Add(item.UserName, item.Point);
                    }
                    else
                    {
                        maxPointDict[item.UserName] += item.Point;
                    }
                }

                string userMaxPoint = string.Empty;
                int maxPoint = int.MinValue;

                foreach (var item in maxPointDict)
                {
                    if (item.Value > maxPoint)
                    {
                        userMaxPoint = item.Key;
                        maxPoint = item.Value;
                    }
                }

                Console.WriteLine($"Best candidate is {userMaxPoint} with total {maxPoint} points.");
                Console.WriteLine("Ranking: ");
            }


            static void PrintRegularUser(List<Raser> printRaser)
            {
                for (int i = 0; i < printRaser.Count; i++)
                {
                    string name = printRaser[i].UserName;

                    Console.WriteLine(name);

                    for (int j = 0; j < printRaser.Count; j++)
                    {
                        if (printRaser[j].UserName == name)
                        {
                            Console.WriteLine($"#  {printRaser[j].Contest} -> {printRaser[j].Point}");
                        }
                    }

                    for (int k = 0; k < printRaser.Count; k++)
                    {
                        if (printRaser[k].UserName == name)
                        {
                            printRaser.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }
    }
}
