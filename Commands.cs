using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace discord_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("spune")]
        public async Task Say([Remainder] string text)
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(text);
        }


        [Command("bully" , RunMode = RunMode.Async)]
        public async Task Bully(SocketGuildUser user, int amount , [Remainder]string text)
        {
            await Context.Message.DeleteAsync();
            for (int i = 0; i < amount; i++)
            {
                await Context.Channel.SendMessageAsync(user.Mention + " " + text);
                await user.SendMessageAsync(user.Mention + " " + text);
                await Task.Delay(1000);
            }
        }
    }
}
