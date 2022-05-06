﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Threading;
using Telegram.Bot.Types.InputFiles;

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
                    MessageType type = update.Message.Type;
                    bool contains = false;
                    //System.Diagnostics.Debug.WriteLine(update.Message.Text);
                    Chat thisChat;

                    foreach (Chat chat in Chats)
                    {
                        if (chat.Name == update.Message.Chat.FirstName)
                        {
                            thisChat = chat;
                            if (!Directory.Exists($"./{chat.Id}"))
                            {
                                chat.Files = new List<string>();
                                Directory.CreateDirectory($"./{chat.Id}");
                            }

                            contains = true;

                            if (!chat.Initialized)
                            {
                                if (update.Message.Type != MessageType.Text || update.Message.Text != "/start")
                                {
                                    update.Message.Text = "Unknown initializing command";
                                    await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Для инициализации бота отправьте команду \"/start\"");
                                    window.Dispatcher.Invoke(() =>
                                    {
                                        chat.Count++;
                                        chat.messages.Add(new Message(update.Message.Chat.Id, chat.Name,
                                        update.Message.Date.ToString(), update.Message.Text));
                                    });
                                    break;
                                }
                                else chat.Initialized = true;
                            }

                            string messageText = null;

                            if (type == MessageType.Text)
                            {
                                IncomingTextMessageProcessor(update, chat);
                                messageText = update.Message.Text;
                            }
                            else if (type == MessageType.Photo)
                            {
                                messageText = $"{chat.Name} send photo.";
                                var photo = await Bot.GetFileAsync(update.Message.Photo[2].FileId);
                                Stream fileStream = new FileStream($"./{chat.Id}/{photo.FilePath.Replace('/', '_')}", FileMode.Create);
                                await Bot.DownloadFileAsync(photo.FilePath, fileStream);
                                fileStream.Close();
                                fileStream.Dispose();
                                if (!chat.Files.Contains($"{photo.FilePath.Replace('/', '_')}"))
                                    chat.Files.Add($"{photo.FilePath.Replace('/', '_')}");
                            }
                            else if (type == MessageType.Video)
                            {
                                messageText = $"{chat.Name} send video.";
                                Telegram.Bot.Types.File video;
                                try
                                {
                                    video = await Bot.GetFileAsync(update.Message.Video.FileId);

                                    Stream fileStream = new FileStream($"./{chat.Id}/{video.FilePath.Replace('/', '_')}", FileMode.Create);
                                    await Bot.DownloadFileAsync(video.FilePath, fileStream);
                                    fileStream.Close();
                                    fileStream.Dispose();
                                    if (!chat.Files.Contains($"{video.FilePath.Replace('/', '_')}"))
                                        chat.Files.Add($"{video.FilePath.Replace('/', '_')}");
                                }
                                catch
                                {
                                    await Bot.SendTextMessageAsync(chat.Id, "File is too big");
                                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File is too big\n".ToUpper()));
                                }
                            }
                            else if (type == MessageType.Audio)
                            {
                                try
                                {
                                    messageText = $"{chat.Name} send audio.";
                                    var audio = await Bot.GetFileAsync(update.Message.Audio.FileId);
                                    Stream fileStream = new FileStream($"./{chat.Id}/{audio.FilePath.Replace('/', '_')}", FileMode.Create);
                                    await Bot.DownloadFileAsync(audio.FilePath, fileStream);
                                    fileStream.Close();
                                    fileStream.Dispose();
                                    if (!chat.Files.Contains($"{audio.FilePath.Replace('/', '_')}"))
                                        chat.Files.Add($"{audio.FilePath.Replace('/', '_')}");
                                }
                                catch
                                {
                                    await Bot.SendTextMessageAsync(chat.Id, "File is too big");
                                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File is too big\n".ToUpper()));
                                }
                            }
                            else if (type == MessageType.Document)
                            {
                                try
                                {
                                    messageText = $"{chat.Name} send document.";
                                    var document = await Bot.GetFileAsync(update.Message.Document.FileId);
                                    Stream fileStream = new FileStream($"./{chat.Id}/{document.FilePath.Replace('/', '_')}", FileMode.Create);
                                    await Bot.DownloadFileAsync(document.FilePath, fileStream);
                                    fileStream.Close();
                                    fileStream.Dispose();
                                    if (!chat.Files.Contains($"{document.FilePath.Replace('/', '_')}"))
                                        chat.Files.Add($"{document.FilePath.Replace('/', '_')}");
                                }
                                catch
                                {
                                    await Bot.SendTextMessageAsync(chat.Id, "File is too big");
                                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File is too big\n".ToUpper()));
                                }
                            }

                            window.Dispatcher.Invoke(() =>
                            {
                                chat.Count++;
                                chat.messages.Add(new Message(update.Message.Chat.Id, chat.Name,
                                update.Message.Date.ToString(), messageText));
                            });
                            break;
                        }
                    }
                    if (!contains)
                    {
                        window.Dispatcher.Invoke(() =>
                        {
                            if (update.Message.Text != "/start")
                            {
                                update.Message.Text = "Unknown initializing command";
                                Bot.SendTextMessageAsync(update.Message.Chat.Id, "Send \"/start\" to initialize this bot " +
                                    "and to find out how this bot works");
                            }
                            Chats.Add(new Chat(update, window));
                            IncomingTextMessageProcessor(update, GetChat(update.Message.Chat.Id));
                        });
                    }
                    offset = update.Id + 1;
                    Thread.Sleep(100);
                }

            }
        }

        async void IncomingTextMessageProcessor(Update update, Chat chat)
        {
            if (update.Message.Text == "/start")
            {

            }
            else if (update.Message.Text == "/download")
            {
                ShowDownloadFilesList(chat);
            }
            else if (update.Message.Text.Contains("/photos_file"))
            {
                bool found = false;
                string path = update.Message.Text.Replace("/", null);
                foreach (string file in chat.Files)
                {
                    if (file.Contains(path))
                    {
                        path = $"./{chat.Id}/{file}";
                        UploadPhoto(chat, path);
                        found = true;
                        chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File successfully send.\n".ToUpper()));
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }
                if (!found)
                {
                    await Bot.SendTextMessageAsync(chat.Id, "Unable to find this file.");
                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File not found.\n".ToUpper()));
                }
            }
            else if (update.Message.Text.Contains("/videos_file"))
            {
                bool found = false;
                string path = update.Message.Text.Replace("/", null);
                foreach (string file in chat.Files)
                {
                    if (file.Contains(path))
                    {
                        path = $"./{chat.Id}/{file}";
                        UploadVideo(chat, path);
                        found = true;
                        chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File successfully send.\n".ToUpper()));
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }
                if (!found)
                {
                    await Bot.SendTextMessageAsync(chat.Id, "Unable to find this file.");
                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File not found.\n".ToUpper()));
                }
            }
            else if (update.Message.Text.Contains("/documents_file"))
            {
                bool found = false;
                string path = update.Message.Text.Replace("/", null);
                foreach (string file in chat.Files)
                {
                    if (file.Contains(path))
                    {
                        path = $"./{chat.Id}/{file}";
                        UploadDocument(chat, path);
                        found = true;
                        chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File successfully send.\n".ToUpper()));
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }
                if (!found)
                {
                    await Bot.SendTextMessageAsync(chat.Id, "Unable to find this file.");
                    chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: File not found.\n".ToUpper()));
                }
            }
            else if (update.Message.Text.Contains("/"))
            {
                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Unknown command, send \"/start\" to find out " +
                "which command you can use with this bot.");
                chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: Unknown command\n".ToUpper()));
            }
        }

        Chat GetChat(long id)
        {
            Chat chat = null;
            foreach (Chat ch in Chats)
            {
                if (ch.Id.Equals(id))
                {
                    chat = ch;
                    break;
                }
            }
            return chat;
        }

        async void ShowDownloadFilesList(Chat chat)
        {
            string files = null;
            foreach (string file in chat.Files)
            {
                files += $" /{file}\n";
            }
            await Bot.SendTextMessageAsync(chat.Id, $"List of files available for download:\n{files}");
            chat.messages.Add(new Message(chat.Id, chat.Name, DateTime.Now.ToString(), "\n            You: Send list of files\n".ToUpper()));
        }

        async void UploadPhoto(Chat chat, string path)
        {
            Stream stream = new FileStream(path, FileMode.Open);
            InputOnlineFile file = new InputOnlineFile(stream);
            await Bot.SendPhotoAsync(chat.Id, file);
            stream.Close();
            stream.Dispose();
        }

        async void UploadVideo(Chat chat, string path)
        {
            Stream stream = new FileStream(path, FileMode.Open);
            InputOnlineFile file = new InputOnlineFile(stream);
            await Bot.SendVideoAsync(chat.Id, file);
            stream.Close();
            stream.Dispose();
        }

        async void UploadDocument(Chat chat, string path)
        {
            if (path.ToLower().Contains(".png") || path.ToLower().Contains(".jpg") || path.ToLower().Contains(".jpeg"))
                UploadPhoto(chat, path);
            else if (path.ToLower().Contains(".mp4") || path.ToLower().Contains(".avi"))
                UploadVideo(chat, path);
            else
            {
                Stream stream = new FileStream(path, FileMode.Open);
                InputOnlineFile file = new InputOnlineFile(stream);
                await Bot.SendDocumentAsync(chat.Id, file);
                stream.Close();
                stream.Dispose();
            }
        }
    }
}
