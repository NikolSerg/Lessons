using System;
using System.Net;
using System.Text;
using Telegram.Bot;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Practice_9
{
    internal class Program
    {
        static TelegramBotClient bot;
        static dynamic files;
        static int nPhotos;
        static int nVideos;
        static int nAudios;
        static int nDocuments;
        static void Main(string[] args)
        {
            Console.WriteLine("Не забудьте поменять путь к файлу с токеном\nИспользую Telegram.Bot версии 16.0");
            Console.ReadLine();


            string token = File.ReadAllText(@"C:\Users\Administrator\source\repos\token.txt");
            Console.WriteLine("Токен: " + token);

            nPhotos = 0;
            nVideos = 0;
            nAudios = 0;
            nDocuments = 0;

            if (!Directory.Exists("./downloads/")) Directory.CreateDirectory("./downloads/");

            if (!File.Exists("files.xml"))
            {
                XElement nFiles = new XElement("Files");
                XElement photos = new XElement("Photos");
                XElement videos = new XElement("Videos");
                XElement documents = new XElement("Documents");
                XElement audios = new XElement("Audios");

                nFiles.Add(photos);
                nFiles.Add(videos);
                nFiles.Add(documents);
                nFiles.Add(audios);


               
                using (Stream stream = new FileStream("files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                    serializer.Serialize(stream, nFiles);
                }

                using (Stream stream = new FileStream("files.xml", FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                    files = serializer.Deserialize(stream);
                }
            }
            else
            {
                using (Stream stream = new FileStream("files.xml", FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                    files = serializer.Deserialize(stream);
                }
            }
            
            foreach (XElement element in files.Element("Photos").Elements())
            {
                nPhotos++;
            }
            foreach (XElement element in files.Element("Videos").Elements())
            {
                nVideos++;
            }
            foreach (XElement element in files.Element("Audios").Elements())
            {
                nAudios++;
            }
            foreach (XElement element in files.Element("Documents").Elements())
            {
                nDocuments++;
            }

            Console.WriteLine(files);


            bot = new TelegramBotClient(token);
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Console.ReadKey();
            
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.Write($"Name: {e.Message.Chat.FirstName}\nLast Name: {e.Message.Chat.LastName}\n" +
                $"Chat ID: {e.Message.Chat.Id}\nUser Name: {e.Message.From.Username}\n");
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Console.Write($"Message Text: {e.Message.Text}");
                if (e.Message.Text.ToLower() == "/download")
                {
                    string text = "Выберите файл, который хотите скачать\n\n";
                    foreach (XElement element in files.Elements())
                    {
                        text += $"{element.Name}: ";
                        foreach (XElement elm in element.Elements())
                        {
                            text += $"\n      /{elm.Name} ({elm.Value})";
                        }
                        text += "\n";
                    }
                    bot.SendTextMessageAsync(e.Message.Chat.Id, text);
                }
                else if (e.Message.Text.Contains("/Photo"))
                {
                    string name = e.Message.Text.Replace("/", null);
                    UploadPhoto(files.Element("Photos").Element(name).Value, e.Message.Chat.Id);
                }
                else if (e.Message.Text.Contains("/Video"))
                {
                    string name = e.Message.Text.Replace("/", null);
                    UploadVideo(files.Element("Videos").Element(name).Value, e.Message.Chat.Id);
                }
                else if (e.Message.Text.Contains("/Audio"))
                {
                    string name = e.Message.Text.Replace("/", null);
                    UploadAudio(files.Element("Audios").Element(name).Value, e.Message.Chat.Id);
                }
                else if (e.Message.Text.Contains("/Document"))
                {
                    string name = e.Message.Text.Replace("/", null);
                    UploadFile(files.Element("Documents").Element(name).Value, e.Message.Chat.Id);
                }
                else if (e.Message.Text.ToLower() == "/start")
                {
                    string text = $"Приветсвую, {e.Message.Chat.FirstName}" +
                        $"\nЧтобы выгрузить в облако файлы, просто отправьте их в сообщении." +
                        $"\n\nЧтобы загрузить файлы отправьте /download и выберите их из списка.";
                    bot.SendTextMessageAsync(e.Message.Chat.Id, text);
                }
                else bot.SendTextMessageAsync(e.Message.Chat.Id, "Отправь что-то другое, ");
            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                Download(e.Message.Photo[2]);
            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Video)
            {
                Download(e.Message.Video);
            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                Download(e.Message.Audio);
            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
            {
                Download(e.Message.Document);
            }
            else bot.SendTextMessageAsync(e.Message.Chat.Id, "Не написан обработчик такого типа))");
        }


        static async void Download(Telegram.Bot.Types.PhotoSize e)
        {
            var photo = await bot.GetFileAsync(e.FileId);
            FileStream stream = new FileStream("./downloads/" + e.FileUniqueId + ".png", FileMode.Create);
            await bot.DownloadFileAsync(photo.FilePath, stream);
            stream.Close();
            stream.Dispose();
            XElement xmlPhoto = new XElement($"Photo_{nPhotos + 1}", $"{e.FileUniqueId}.png");
            nPhotos++;
            files.Element("Photos").Add(xmlPhoto);
            using (Stream xStream = new FileStream("files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                serializer.Serialize(xStream, files);
            }
        }
        static async void Download(Telegram.Bot.Types.Video e)
        {
            var file = await bot.GetFileAsync(e.FileId);
            FileStream stream = new FileStream("./downloads/" + e.FileName, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, stream);
            stream.Close();
            stream.Dispose();
            XElement xmlVideo = new XElement($"Video_{nVideos + 1}", e.FileName);
            files.Element("Videos").Add(xmlVideo);
            nVideos++;
            using (Stream xStream = new FileStream("files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                serializer.Serialize(xStream, files);
            }
        }
        static async void Download(Telegram.Bot.Types.Audio e)
        {
            var file = await bot.GetFileAsync(e.FileId);
            FileStream stream = new FileStream("./downloads/" + e.FileName, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, stream);
            stream.Close();
            stream.Dispose();
            XElement xmlAudio = new XElement($"Audio_{nAudios+1}", e.FileName);
            files.Element("Audios").Add(xmlAudio);
            nAudios++;
            using (Stream xStream = new FileStream("files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                serializer.Serialize(xStream, files);
            }
        }
        static async void Download(Telegram.Bot.Types.Document e)
        {
            var file = await bot.GetFileAsync(e.FileId);
            FileStream stream = new FileStream("./downloads/" + e.FileName, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, stream);
            stream.Close();
            stream.Dispose();
            XElement xmlDocument = new XElement($"Document_{nDocuments + 1}", e.FileName);
            files.Element("Documents").Add(xmlDocument);
            nDocuments++;
            using (Stream xStream = new FileStream("files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XElement));
                serializer.Serialize(xStream, files);
            }
        }



        static async void UploadFile(string path, Telegram.Bot.Types.ChatId id)
        {
            if (path.ToLower().Contains(".jpg") || path.ToLower().Contains(".png") || path.ToLower().Contains(".jpeg"))
            {
                UploadPhoto(path, id);
            }
            else if (path.ToLower().Contains(".mp4") || path.ToLower().Contains(".avi"))
            {
                UploadVideo(path, id);
            }
            else
            {
                Stream stream = new FileStream("./downloads/" + path, FileMode.Open, FileAccess.Read);
                Telegram.Bot.Types.InputFiles.InputOnlineFile file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
                await bot.SendDocumentAsync(id, file);
                stream.Close();
                stream.Dispose();
            }
        }
        static async void UploadVideo(string path, Telegram.Bot.Types.ChatId id)
        {
            Stream stream = new FileStream("./downloads/" + path, FileMode.Open, FileAccess.Read);
            Telegram.Bot.Types.InputFiles.InputOnlineFile file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
            await bot.SendVideoAsync(id, file);
            stream.Close();
            stream.Dispose();
        }
        static async void UploadAudio(string path, Telegram.Bot.Types.ChatId id)
        {
            Stream stream = new FileStream("./downloads/" + path, FileMode.Open, FileAccess.Read);
            Telegram.Bot.Types.InputFiles.InputOnlineFile file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
            await bot.SendAudioAsync(id, file);
            stream.Close();
            stream.Dispose();
        }
        static async void UploadPhoto(string path, Telegram.Bot.Types.ChatId id)
        {
            Stream stream = new FileStream("./downloads/" + path, FileMode.Open, FileAccess.Read);
            Telegram.Bot.Types.InputFiles.InputOnlineFile file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
            await bot.SendPhotoAsync(id, file);
            stream.Close();
            stream.Dispose();
        }
    }
}
