﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BidAskCore.Telegram
{
    public class Telegram
    {
        TelegramBotClient bot;
        public Telegram()
        {
            this.bot = new TelegramBotClient("");
        }

        public async void sendMessage(string message)
        {
            try
            {
                await this.bot.SendTextMessageAsync("244146002", message);
            }
            catch
            {

            }
        }
    }
}
