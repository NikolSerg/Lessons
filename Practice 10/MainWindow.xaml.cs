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
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

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
            string path = "../../../../token.txt";
            string token = System.IO.File.ReadAllText(path);
            sendMessageBox.Text = "To send a message press \"Enter\".";
            sendMessageBox.Foreground = System.Windows.Media.Brushes.Gray;
            bot = new TelegramBot(this, token);
            if (!System.IO.File.Exists("chat.json")) System.IO.File.Create("chat.json").Close();
            else
            {
                string chats = System.IO.File.ReadAllText("chat.json");
                bot.Chats = JsonConvert.DeserializeObject<ObservableCollection<Chat>>(chats);
            }
            chatsList.ItemsSource = bot.Chats;
            bot.Update();
            this.MinWidth = 215;
            this.Width = 215;
            this.Height = 400;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeMode = ResizeMode.CanResize;
            this.Width = 880;
            this.MinWidth = 430;
            clearButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            minimizeButton.Visibility = Visibility.Visible;

            string id = ((TextBlock)((StackPanel)sender).Children[2]).Text;
            foreach (Chat chat in chatsList.Items)
            {
                if (chat.Id.ToString() == id)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(bot.Chats);
            System.IO.File.WriteAllText("chat.json", json);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {

            Message chat = messages.ItemsSource.Cast<Message>().ElementAt(0);
            long id = chat.ID;
            foreach (Chat ch in bot.Chats)
            {
                if (ch.Id == id)
                    ch.messages.Clear();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "*.JSON | *.json";
            if (dialog.ShowDialog() == true)
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                string json = JsonConvert.SerializeObject(bot.Chats);
                System.IO.File.WriteAllText(dialog.FileName, json);
            }
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            this.MinWidth = 215;
            this.Width = 215;
            this.Height = 400;
            this.ResizeMode = ResizeMode.NoResize;
            minimizeButton.Visibility = Visibility.Hidden;
            clearButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
        }

        private void sendMessageBox_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (sendMessageBox.Text == "To send a message press \"Enter\".")
            {
                sendMessageBox.Clear();
                sendMessageBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
    }


}
