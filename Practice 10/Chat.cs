using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Practice_10
{
    internal class Chat
    {
        public ObservableCollection<Message> messages { get; set; }
        MainWindow window;
        public string Name { get; set; }
        public long Id { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
        public uint Count { get; set; }
        public List<string> Files { get; set; }
        public bool Initialized { get; set; }


        public Chat()
        {

        }

        public Chat(Update update, MainWindow window)
        {
            Count = 1;
            messages = new ObservableCollection<Message>();
            Name = update.Message.Chat.FirstName;
            Id = update.Message.Chat.Id;
            LastName = update.Message.Chat.LastName;
            Text = update.Message.Text;
            Files = new List<string>();
            this.window = window;
            if (update.Message.Text == "Unknown initializing command")
                Initialized = false;
            else Initialized = true;
            window.Dispatcher.Invoke(() =>
            {
                messages.Add(new Message(Id, Name, update.Message.Date.ToString(), update.Message.Text));
            }
            );
        }
        
    }
}
