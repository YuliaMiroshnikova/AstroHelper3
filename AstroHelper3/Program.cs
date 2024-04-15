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
           
            
            //Мой
            string url =
                "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Юля&fd=12&fm=3&fy=1986&fh=15&fmn=44&c1=Брянск%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=53.2520&ln=34.3716&hs=P&sb=1";

            //Карина
            // string url =
                // "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Карина&fd=23&fm=4&fy=1997&fh=16&fmn=45&c1=Киев%2C+Украина&ttz=20&tz=Europe%2FKiev&tm=2&lt=50.4546&ln=30.5238&hs=P&sb=1";
            
            //Дима от Карины
                // string url =
                //     "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=ДимаКарина&fd=8&fm=12&fy=1991&fh=7&fmn=25&c1=Барановичи%2C+Беларусь&ttz=20&tz=Europe%2FMinsk&tm=3&lt=53.1327&ln=26.0139&hs=P&sb=1";
            
            //Женя
            // string url =
            // "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Женя&fd=27&fm=3&fy=1997&fh=12&fmn=1&c1=Владимир%2C+Россия&ttz=3&tz=Europe%2FMoscow&tm=3&lt=56.1365&ln=40.3965&hs=P&sb=1";
            
            //Даша
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Даша&fd=12&fm=7&fy=1987&fh=16&fmn=10&c1=Москва%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=55.7522&ln=37.6155&hs=P&sb=1";
            
            //Таня
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Таня&fd=7&fm=7&fy=1997&fh=22&fmn=25&c1=Липецк%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=52.6031&ln=39.5707&hs=P&sb=1";
            
            // Маша фото
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Маша+фото&fd=14&fm=9&fy=1995&fh=10&fmn=20&c1=Запорожье%2C+Украина&ttz=20&tz=Europe%2FKiev&tm=2&lt=47.8516&ln=35.1171&hs=P&sb=1";
           
            // Виктория от Марии (модель)
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Виктория&fd=23&fm=7&fy=2000&fh=20&fmn=0&c1=Пафос%2C+Кипр&ttz=20&tz=Asia%2FNicosia&tm=2&lt=34.7767&ln=32.4245&hs=P&sb=1";
           
            // Лена
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Лена&fd=12&fm=3&fy=1998&fh=16&fmn=15&c1=Москва%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=55.7522&ln=37.6155&hs=P&sb=1";
            
            // Паша
            // string url = "https://geocult.ru/natalnaya-karta-onlayn-raschet?fn=Паша&fd=2&fm=4&fy=1992&fh=16&fmn=0&c1=Брянск%2C+Россия&ttz=20&tz=Europe%2FMoscow&tm=3&lt=53.2520&ln=34.3716&hs=P&sb=1";
            
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

        Instruction instruction = new Instruction(outputPath);
        instruction.WriteInstruction();
        
        string jsonFilePath = "PlanetHomeOpisanie.json";
        var processor = new PlanetInHomeDescription(context, outputPath, jsonFilePath);
        processor.GenerateDescriptionsPlanetHome();


        string jsonFilePathUpr = "Upravitel.json";
        var processorupr = new UpravitelInHomeDescription(context, outputPath, jsonFilePathUpr);
        processorupr.GenerateDescriptionsUpraviteltHome();
        
       
        
        string jsonFilePathHomes = "KarmaHome.json"; 
        string jsonFilePathPositions = "KarmaPosition.json";
        var processorKarma = new KarmicNodesProcessor(context, outputPath, jsonFilePathHomes, jsonFilePathPositions);
        processorKarma.ProcessKarmicNodes();
    }

}





