using System.Net;
using HtmlAgilityPack;
using System.Xml.Linq;
using AstroHelper3;


class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        try
        {
            //todo разобраться, какие параметры подставлять
            // string fn = "Юлия";
            // int fd = 12;
            // int fm = 3;
            // int fy = 1986;
            // int fh = 15;
            // int fmn = 44;
            // string c1 = Uri.EscapeDataString("Москва, Россия"); // Кодирование для использования в URL
            //
            // string url = $"https://geocult.ru/natalnaya-karta-onlayn-raschet?fn={fn}&fd={fd}&fm={fm}&fy={fy}&fh={fh}&fmn={fmn}&c1={c1}&ttz={{ttz}}&tz={{tz}}&tm={{tm}}&lt={{lt}}&ln={{ln}}&hs={{hs}}&sb={{sb}}";
            string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Юлия&fd=12&fm=3&fy=1986&fh=15&fmn=44&c1=Москва%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=55.7522&ln=37.6155&hs=P&sb=1";
            
            WebClient client = new WebClient();
            string html = client.DownloadString(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            string textOnly = doc.DocumentNode.InnerText;

            XDocument xdoc = new XDocument(
                new XElement("root",
                    new XElement("text", textOnly)
                )
            );

            xdoc.Save("DATA1.xml");
            CleaningPlanets cleaningPlanets = new CleaningPlanets();
            cleaningPlanets.SimplifyTablePlanets();
            CleaningHome cleaningHome = new CleaningHome();
            cleaningHome.SimplifyTableHome();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
        string filePathHome = "Home.xml"; 
        FormattingHome formattingHome = new FormattingHome(filePathHome);
        formattingHome.ProcessFileHome();
        
        string filePathPlanets = "Planets.xml"; 
        FormattingPlanets formattingPlanets = new FormattingPlanets(filePathPlanets);
        formattingPlanets.ProcessFilePlanets();
        
        
        
        
        var context = new AppDBContext();
        string outputPath = "Horoscope.txt";
        string jsonFilePath = "PlanetHomeOpisanie.json"; 
        var processor = new PlanetInHomeDescription(context, outputPath, jsonFilePath);
        processor.GenerateDescriptionsPlanetHome();

        
    }

    

   


}
