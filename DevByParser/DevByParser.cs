using System.Configuration;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;


namespace DevBy
{
    public class DevByParser
    {
        string host { get; } = "https://events.dev.by/";
        HttpClient clientParser = new HttpClient();
        HtmlDocument doc = new HtmlDocument();
        public int Pages { get; set; } = int.Parse(ConfigurationManager.AppSettings.Get("Pages") ?? "1");
        public DevByParser()
        {
            clientParser.DefaultRequestHeaders.Add("Accept", "text/html, */*; q=0.01");
            clientParser.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

        }
        private string GetResponse(int pages)   //getting respons in HTML format from host
        {
                string data = "";
                for (int i = 1; i <= pages; i++)
                {
                   data +=   clientParser.GetStringAsync($"{host}?page={i}").Result;

                }
            return data;
        }
        public  List<EventObject> GetEvents()
        {
            List<EventObject> meetings = new List<EventObject>();
            string resp = GetResponse(Pages);
            doc.LoadHtml(resp);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='list-item-events list-more']");

            foreach (HtmlNode node in nodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].NodeType == HtmlAgilityPack.HtmlNodeType.Text)
                    {
                        node.ChildNodes[i].Remove();
                    }
                }

                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    string date = $"{ @Regex.Replace(node.ChildNodes[i].ChildNodes[1].InnerText, @"\s", " ")}";
                    string name = $"{ @Regex.Replace(node.ChildNodes[i].ChildNodes[3].ChildNodes[1].InnerText, @"\s", " ")}";
                    meetings.Add(new EventObject(name, date));

                }

            }

            Console.WriteLine("**********END OF GETTING EVENTS*********");
            return meetings;
        }
        public List<EventObject> (GetNewEventsList<EventObject> prevEvents)
        {
            return (this.GetEvents().Except(prevEvents ?? new List<EventObject>())).ToList();
        }
    }
}
