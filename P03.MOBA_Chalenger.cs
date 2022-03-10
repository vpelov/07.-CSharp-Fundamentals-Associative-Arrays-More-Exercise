using System;
using System.Collections.Generic;
using System.Linq;

namespace P03.MOBA_Chalenger
{
    class Players
    {
        public Players(string playerName, string position, int skill)
        {
            this.PlayerName = playerName;
            this.Position = position;
            this.Skill = skill;
        }

        public string PlayerName { get; set; }

        public string Position { get; set; }

        public int Skill { get; set; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            List<Players> playersDataList = new List<Players>();

            string inputCommand = Console.ReadLine();

            while (inputCommand != "Season end")
            {
                string[] commandArray = inputCommand
                    .Split(new char[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries).ToArray();


                if (!commandArray.Contains("vs"))           //Read input Data "{player} -> {position} -> {skill}"
                {
                    string playerName = commandArray[0];
                    string position = commandArray[1];
                    int skill = int.Parse(commandArray[2]);

                    if (!playersDataList.Any(x => x.PlayerName == playerName))
                    {
                        Players player = new Players(playerName, position, skill);
                        playersDataList.Add(player);
                    }
                    else
                    {
                        bool isExist = true;
                        for (int i = 0; i < playersDataList.Count; i++)
                        {
                            if (playersDataList[i].PlayerName == playerName && playersDataList[i].Position == position && playersDataList[i].Skill < skill)
                            {
                                playersDataList[i].Skill = skill;
                                isExist = false;
                                break;
                            }
                        }

                        if (isExist)
                        {
                            Players player = new Players(playerName, position, skill);
                            playersDataList.Add(player);
                        }
                    }
                }
                else                                        // Read input Data "{player} vs {player}"
                {
                    string playerOne = commandArray[0];
                    string playerTwo = commandArray[2];

                    if (playersDataList.Any(x => x.PlayerName == playerOne) && playersDataList.Any(t => t.PlayerName == playerTwo))
                    {
                        bool isRemovePlayer = false;
                        for (int i = 0; i < playersDataList.Count; i++)
                        {
                            if (isRemovePlayer)
                            {
                                break;
                            }

                            for (int j = i + 1; j < playersDataList.Count; j++)
                            {
                                if (playersDataList[i].Position == playersDataList[j].Position)
                                {
                                    if (playersDataList[i].Skill < playersDataList[j].Skill)
                                    {
                                        playersDataList.RemoveAt(i);
                                        isRemovePlayer = true;
                                        break;
                                    }
                                    else if (playersDataList[i].Skill > playersDataList[j].Skill)
                                    {
                                        playersDataList.RemoveAt(j);
                                        isRemovePlayer = true;
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }

                inputCommand = Console.ReadLine();
            }


            Dictionary<string, int> totalSkill = GetTotalSkill(playersDataList);      // Order and Print

            playersDataList = playersDataList.OrderByDescending(x => x.Skill).ThenBy(x => x.Position).ToList();


            foreach (var item in totalSkill)
            {
                Console.WriteLine($"{item.Key}: {item.Value} skill");

                foreach (Players player in playersDataList)
                {
                    if (item.Key == player.PlayerName)
                    {
                        Console.WriteLine($"- {player.Position} <::> {player.Skill}");
                    }
                }

            }

        }



        static int GetAllSkils(List<Players> playerData, string name)
        {
            int sum = 0;

            foreach (var item in playerData)
            {

                if (item.PlayerName == name)
                {
                    sum += item.Skill;
                }
            }

            return sum;
        }

        static Dictionary<string, int> GetTotalSkill(List<Players> playersDataList)
        {
            Dictionary<string, int> totalSkill = new Dictionary<string, int>();

            foreach (var item in playersDataList)
            {
                if (totalSkill.ContainsKey(item.PlayerName))
                {
                    totalSkill[item.PlayerName] += item.Skill;
                }
                else
                {
                    totalSkill.Add(item.PlayerName, item.Skill);
                }
            }

            totalSkill = totalSkill.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return totalSkill;
        }


    }
}
