using Discord.WebSocket;
using System;
using System.IO;

namespace discord_bot
{
    class Economy
    {

        //Makes Folder For User
        public static void MakeDirectory(SocketUser user)
        {
            if (!Directory.Exists(Program.dataPath + "\\" + user))
            {
                Directory.CreateDirectory(Program.dataPath + "\\" + user);
            }
        }

        //Makes Money Text For User
        public static void MakeMoneyFile(SocketUser user)
        {
            MakeDirectory(user);
            if (!File.Exists(Program.dataPath + "\\" + user + "\\" + "points.txt"))
            {
                File.WriteAllText(Program.dataPath + "\\" + user + "\\" + "points.txt", "100");
            }
        }

        //Gets User Money
        public static int GetMoney(SocketUser user)
        {
            if (File.Exists(Program.dataPath + "\\" + user + "\\" + "points.txt"))
            {
                return int.Parse(File.ReadAllText(Program.dataPath + "\\" + user + "\\" + "points.txt"));
            }
            else
            {
                MakeMoneyFile(user);
                return 100;
            }
        }


        //Gives Money To User
        public static void AddMoney(SocketUser user, int valuetoadd)
        {
            if (File.Exists(Program.dataPath + "\\" + user + "\\" + "points.txt"))
            {
                int value = int.Parse(File.ReadAllText(Program.dataPath + "\\" + user + "\\" + "points.txt"));
                value += valuetoadd;

                File.WriteAllText(Program.dataPath + "\\" + user + "\\" + "points.txt", value.ToString());
            }
            else
            {
                int value = 100 + valuetoadd;
                File.WriteAllText(Program.dataPath + "\\" + user + "\\" + "points.txt", value.ToString());
            }
        }

        public static void DailyMoney(SocketUser user)
        {
            if (File.Exists(Program.dataPath + "\\" + user + "\\" + "lastdaily.txt"))
            {
                string lastdate = File.ReadAllText(Program.dataPath + "\\" + user + "\\" + "lastdaily.txt");
                if (lastdate == DateTime.Today.ToString())
                {
                    return;
                }

                AddMoney(user, 50);
                File.WriteAllText(Program.dataPath + "\\" + user + "\\" + "lastdaily.txt", DateTime.Today.ToString());
            }
            else
            {
                File.WriteAllText(Program.dataPath + "\\" + user + "\\" + "lastdaily.txt", DateTime.Today.ToString());

                AddMoney(user, 50);
            }
        }
    }
}
