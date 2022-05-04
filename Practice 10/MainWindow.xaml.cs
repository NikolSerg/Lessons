using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Practice_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramBot bot;
        public MainWindow()
        {
            InitializeComponent();
            bot = new TelegramBot(this, "5338294319:AAGcKJNnsHjUmBUzjrRFV3Vf3M8Jz-XAhSI");
            chatsList.ItemsSource = bot.Chats;
            bot.Update();
            
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = ((TextBlock)((StackPanel)sender).Children[0]).Text;
            foreach (Chat chat in chatsList.Items)
            {
                if (chat.Name == name)
                {
                    messages.ItemsSource = chat.messages;
                }
            }
        }

        async private void sendMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Message chat = messages.ItemsSource.Cast<Message>().ElementAt(0);
                long id = chat.ID;
                string text = sendMessageBox.Text;
                await bot.Bot.SendTextMessageAsync(id, text);
                sendMessageBox.Clear();
            }
        }
    }


}
