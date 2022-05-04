using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Practice_10
{
    internal class TelegramBot
    {
        public ObservableCollection<Chat> Chats { get; set; }
        public TelegramBotClient Bot { get; set; }
        MainWindow window;

        public TelegramBot(MainWindow window, string token)
        {
            this.window = window;
            Bot = new TelegramBotClient(token);
            Chats = new ObservableCollection<Chat>();
        }

        public async void Update()
        {
            int offset = 0;
            while (true)
            {

                var updates = await Bot.GetUpdatesAsync(offset);
                foreach (Update update in updates)
                {
                    
                    bool contains = false;
                    System.Diagnostics.Debug.WriteLine(update.Message.Text);
                    foreach (Chat chat in Chats)
                    {
                        if (chat.Name == update.Message.Chat.FirstName)
                        {
                            contains = true;
                            
                            window.Dispatcher.Invoke(() =>
                            {
                                chat.Count++;
                                chat.messages.Add(new Message(update.Message.Chat.Id, chat.Name,
                                    update.Message.Date.ToString(), update.Message.Text));
                            });

                            break;
                        }
                    }
                    if (!contains)
                    {
                        window.Dispatcher.Invoke(() =>
                        {
                            Chats.Add(new Chat(update, window));
                        });
                    }

                    offset = update.Id + 1;
                }

            }
        }
    }
}
