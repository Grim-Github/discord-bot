using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace discord_bot
{
    public class Program
    {
        public static DiscordSocketClient _client = new DiscordSocketClient();
        private CommandHandler commandHandler = new CommandHandler(_client , new Discord.Commands.CommandService());
        private UserStatus botStatus = UserStatus.DoNotDisturb;
        private string botPlayingStatus = "Cei ce vom izbandi, din incercarea aceasta....vom iesi mai puternici.....";
        public static ulong ownerID = 209173187345383425;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, "Nzc0MTYwODY1MTY3NDA5MTUy.X6TvfQ.haMFSzuvPtIQBcr7oe56Eod3vpw");

            await _client.StartAsync();
            await commandHandler.InstallCommandsAsync();

            await _client.SetStatusAsync(botStatus);
            await _client.SetGameAsync(botPlayingStatus);

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
