using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;

namespace discord_bot
{
    class EmoteClass
    {
        string[] emotelist = {"<:gabor:773165564049752064>"  };

        public IEmote SendEmote(int value)
        {
            if (value <= emotelist.Length)
            {
                return Emote.Parse(emotelist[value]);
            }
            else
            {
                return null;
            }
        }
    }
}
