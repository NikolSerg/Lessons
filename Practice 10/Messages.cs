using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Telegram.Bot.Types;

namespace Practice_10
{
    internal class Message
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string Text { get; set; }
        public Message(long id, string name, string dateTime, string text)
        {
            ID = id;
            Name = name;
            DateTime = dateTime;
            Text = text;
        }
    }
}
