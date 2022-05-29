using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_10
{
    internal class GraphicDrawer
    {
        string path = "SugarNo11Formatted.csv";
        List<string[]> quotations = new List<string[]>();
        Bitmap bitmap;
        int lastX = 0;
        int lastY = 1000;
        string lastValue;
        public void AddQuotation(string[] quotation)
        {

        }

        void DrawGraphic()
        {
            int width = quotations.Count * 10 + 10;
            int height = 1000;
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < quotations.Count; i++)
            {
                int x = i * 10 + 1;
                int y = 1000-(int)(Convert.ToDouble(quotations[i][1]) * 20);
                double tan = ((double)y - (double)lastY) / ((double)x - (double)lastX);
                if (i > 1)
                {
                    for (int j = lastX; j < x; j++)
                    {
                        int midY = lastY + (int)((j - lastX) * tan);
                        bitmap.SetPixel(j, midY, Color.Black);
                        bitmap.SetPixel(j, midY-1, Color.Black);
                        bitmap.SetPixel(j, midY-2, Color.Black);
                        bitmap.SetPixel(j, midY+1, Color.Black);
                        bitmap.SetPixel(j, midY+2, Color.Black);

                        for (int k = 0; k < 50; k++)
                        {
                            bitmap.SetPixel(j, k * 20, Color.Black);
                        }

                        if (j == lastX)
                        {
                            for (int k = 1; k < 1000; k++)
                                bitmap.SetPixel(j, k, Color.Black);
                        }
                    }
                }
                bitmap.SetPixel(x, y, Color.Black);
                lastX = x;
                lastY = y;
            }
            bitmap.Save("graphic.jpg");
        }

        void GetData()
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] separator = new string[1] { "\",\"" };
                string[] values = line.Split(separator, StringSplitOptions.None);
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Replace("\"", null);
                }
                quotations.Add(values);
            }
            if (Convert.ToDateTime(quotations.Last()[0]).DayOfYear != DateTime.Now.DayOfYear)
            {
                quotations.Add(new string[] { DateTime.Now.ToShortDateString(), lastValue });
                string formattedText = null;
                foreach (string[] quotation in quotations)
                {
                    if (quotation != quotations.Last()) formattedText += $"\"{quotation[0]}\",\"{quotation[1]}\"\n";
                    else formattedText += $"\"{quotation[0]}\",\"{quotation[1]}\"";
                }
                File.WriteAllText(path, formattedText);
            }
        }

        public GraphicDrawer(string value)
        {
            lastValue = value;
            GetData();
            DrawGraphic();
        }

        public void Update(string value)
        {
            lastValue = value;
            quotations.Add(new string[] { DateTime.Now.ToShortDateString(), lastValue });
            string formattedText = null;
            foreach (string[] quotation in quotations)
            {
                if (quotation != quotations.Last()) formattedText += $"\"{quotation[0]}\",\"{quotation[1]}\"\n";
                else formattedText += $"\"{quotation[0]}\",\"{quotation[1]}\"";
            }
            File.WriteAllText(path, formattedText);
            DrawGraphic();
        }

        public string GetLastValue()
        {
            return quotations[quotations.Count - 2][1];
        }
    }
}
