﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace discord_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public static List<SocketGuildUser> RandomBanUser = new List<SocketGuildUser>();
        public static List<SocketGuildUser> RandomKickUser = new List<SocketGuildUser>();
        private Random rng = new Random();
        private EmoteClass ec = new EmoteClass();

        #region MISC

        [Command("spune")]
        public async Task Say([Remainder] string text)
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(text);
        }


        [Command("bully", RunMode = RunMode.Async)]
        public async Task Bully(SocketGuildUser user, int amount, [Remainder] string text)
        {
            await Context.Message.DeleteAsync();
            for (int i = 0; i < amount; i++)
            {
                await Context.Channel.SendMessageAsync(user.Mention + " " + text);
                await user.SendMessageAsync(user.Mention + " " + text);
                await Task.Delay(1000);
            }
        }

        [Command("ajutor")]
        public async Task Help()
        {
            await Context.Message.AddReactionAsync(ec.SendEmote(0));
            await Context.Channel.SendMessageAsync(">>> **Comenzi** : \n Bully (nume) (numar) (mesaj) " +
                "\n Spune (mesaj) " +
                "\n Random Kick " +
                "\n Random Ban" +
                "\n Gabor");
        }

        [Command("server")]
        public async Task GetGuildCount()
        {
            await Context.Channel.SendMessageAsync(">>> Sunt in " + Program._client.Guilds.Count + " servere");
        }
        #endregion

        #region Random BAN/KICK
        [Command("random ban")]
        public async Task RandomBanEnter()
        {
            if (RandomBanUser.Contains(Context.User as SocketGuildUser))
            {
                await Context.Channel.SendMessageAsync(">>> Deja esti inscris in random ban");
                return;
            }
            RandomBanUser.Add(Context.User as SocketGuildUser);
            await Context.Channel.SendMessageAsync(">>> " + Context.User + " Participa (" + RandomBanUser.Count + ")");
        }

        [Command("random ban start")]
        public async Task RandomBanStart()
        {
            if (Context.User.Id == Program.ownerID)
            {
                SocketGuildUser bannedUser = RandomBanUser[rng.Next(0, RandomBanUser.Count)];
                RandomBanUser.Clear();
                await bannedUser.SendMessageAsync("f");
                await Context.Channel.SendMessageAsync(">>> Dori me Interimo adapare Dori me Ameno ameno " + bannedUser);
                await bannedUser.BanAsync();

            }
        }

        [Command("random kick")]
        public async Task RandomKickEnter()
        {
            if (RandomKickUser.Contains(Context.User as SocketGuildUser))
            {
                await Context.Channel.SendMessageAsync(">>> Deja esti inscris");
                return;
            }
            RandomKickUser.Add(Context.User as SocketGuildUser);
            await Context.Channel.SendMessageAsync(">>> " + Context.User + " Participa (" + RandomKickUser.Count + ")");
        }

        [Command("random kick start")]
        public async Task RandomKickStart()
        {
            if (Context.User.Id == Program.ownerID)
            {
                SocketGuildUser kickedUser = RandomKickUser[rng.Next(0, RandomKickUser.Count)];
                RandomKickUser.Clear();
                await kickedUser.SendMessageAsync("ez");
                await Context.Channel.SendMessageAsync(">>> f " + kickedUser);
                await kickedUser.KickAsync();
            }

        }
        #endregion

        #region Images
        [Command("gabor")]
        public async Task SendImageGabor()
        {
            string path = Program.imagePath + "\\gabor";
            string[] images = Directory.GetFiles(path);
            await Context.Channel.SendMessageAsync(">>> Poza cu Gabor " + ec.SendEmote(0));
            await Task.Delay(2000);
            await Context.Channel.SendFileAsync(images[rng.Next(0, images.Length)]);
        }

        #endregion

        #region Economy
        [Command("zilnic")]
        public async Task DailyPoints()
        {
            Economy.DailyMoney(Context.Message.Author);
        }

        [Command("puncte")]
        public async Task GetPoints()
        {
            await Context.Channel.SendMessageAsync(Economy.GetMoney(Context.Message.Author).ToString());
        }
        #endregion
    }
}
