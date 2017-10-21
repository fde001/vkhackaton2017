using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VkHackathon.Models;

namespace VkHackaton.Import
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MetadataDBDataContext())
            {
                //ctx.Sources.Add(new Source() { Name = "Afisha" });
                //ctx.Sources.Add(new Source() { Name = "MinKultura" });

                //ctx.EntityTypes.Add(new EntityType() { Name = "Company" });
                //ctx.SaveChanges();

                //LoadCompaniesAfisha(ctx);

                FillInGeoIndex(ctx);

                
            
            }
        }

        private static void FillInGeoIndex(MetadataDBDataContext ctx)
        {
            var iCnt = 0;
            foreach(var line in ctx.Companies)
            {
                line.GeoIndex = System.Data.Entity.Spatial.DbGeography.FromText("POINT(" + line.Longitude + " " + line.Latitude + ")");
                if (iCnt++ > 5000)
                {
                    //ctx.SaveChanges();
                    Console.Write('.');
                    iCnt = 0;
                }
            }
            ctx.SaveChanges();

//            UPDATE[dbo].[Companies]
//SET TextIndex = 'Концертный зал Концерт ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'ConcertHall%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Спорт Стадион ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'SportBuilding%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Кино ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Cinema%'


// UPDATE[dbo].[Companies]
//SET TextIndex = 'Зал Фитнес-центр ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'FitnessCenter%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Галлерея ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Gallery%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Музей Музеи ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Museum%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Парк Сквер ' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Park%'

// UPDATE[dbo].[Companies]
//SET TextIndex = 'Ресторан Рестораны' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Resta%'

//UPDATE[dbo].[Companies]
//SET TextIndex = 'Магазин Магазины' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Shop%'


//UPDATE[dbo].[Companies]
//SET[TextIndex] = 'Театр Театры' + Name + ' ' + FullName + ' ' + ISNULL(Description, '')
//WHERE ExternalId LIKE 'Theat%'
        }

        private static void LoadCompaniesAfisha(MetadataDBDataContext ctx)
        {
            ctx.Companies.RemoveRange(ctx.Companies);
            ctx.SaveChanges();

            XDocument xdoc = XDocument.Load("D:\\sources\\vkhackaton2017\\source Afisha\\companies.xml");

            var iCnt = 0;
            foreach (var cXml in xdoc.Descendants("company"))
            {
                var id = cXml.Element("company-id").Value;


                var item = new Company();

                item.ExternalId = id;
                item.Name = cXml.Element("name").Value;
                item.Url = cXml.Element("add-url").Value;
                item.Site = cXml.Element("url").Value;
                item.Address = cXml.Element("address").Value;
                //item.Description

                try
                {
                    item.Latitude = double.Parse(cXml.Element("coordinates").Element("lat").Value);
                    item.Longitude = double.Parse(cXml.Element("coordinates").Element("lon").Value);
                }
                catch { }
                item.Source = ctx.Sources.First(c => c.Name == "Afisha");
                item.EntityType = ctx.EntityTypes.First(c => c.Name == "Company");
                try
                {
                    item.FullName = cXml.Element("name-other").Value;
                }
                catch { }

                try
                {
                    item.Rating = double.Parse(cXml.Element("rating").Value);
                }
                catch { }

                ctx.Companies.Add(item);
                if (iCnt++ > 500)
                {
                    ctx.SaveChanges();
                    Console.Write('.');
                    iCnt = 0;
                }
            }
            ctx.SaveChanges();
        }
    }
}
