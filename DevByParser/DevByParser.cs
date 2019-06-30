using System.Configuration;
using HtmlAgilityPack;
using System.Collections.Generic;
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
        public List<Event> GetEvents(int pages)
        {
            List<Event> meetings = new List<Event>();
            for (int i = 1; i <= pages; i++)
            {
                string data = clientParser.GetStringAsync($"{host}?page={i}").Result;
                doc.LoadHtml(data);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='list-item-events list-more']/div[@class='item']");

                for (int j = 0; j < nodes.Count; j++)
                {
                    string date = Regex.Replace(nodes[j].SelectSingleNode(".//p/time").InnerText, "\n", " ");
                    HtmlNode UrlNode = nodes[j].SelectSingleNode(".//a[@class='title']");
                    string url = UrlNode.GetAttributeValue("href", null);
                    string name = UrlNode.InnerText;
                    meetings.Add(new Event { EventName = name, EventURL = "https://events.dev.by" + url, EventDate = date });
                }
            }
            return meetings;

        }
    }
}

