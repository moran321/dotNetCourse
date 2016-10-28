using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


/*
this class parses xml files 
and uses "AddDataToDb" class to insert the elements to db
*/
namespace PriceCompare.Model.App
{
    class DbBuilder
    {
        string _dataPath = @"C:\Users\Moran\Desktop\prices"; //working directory where the data found
        ExtractArchiveFiles _archiveExtract;
        AddDataToDb _dbEditor;
        Dictionary<string, Chain> _chains;
        HashSet<Item> _items;
        /*---------------------------------*/

        public DbBuilder()
        {
            _dbEditor = new AddDataToDb();
            _archiveExtract = new ExtractArchiveFiles();
            _chains = new Dictionary<string, Chain>();
            _items = new HashSet<Item>();
        }
        /*---------------------------------*/

        public void InitializeDB()
        {
            DirectoryInfo[] dirInfo = new DirectoryInfo(_dataPath).GetDirectories();
            Parallel.ForEach(dirInfo, (dir) =>
            {
                InitializeStoresData(dir.Name);
                InitializePricesData(dir.Name);
            });

            //foreach (DirectoryInfo dir in dirInfo)
            //{
            //    InitializeStoresData(dir.Name);
            //    InitializePricesData(dir.Name);
            //}
        }
        /*---------------------------------*/

        //return list of xml files name
        private void InitializeStoresData(string dirName)
        {

            Console.WriteLine($"Initialize Stores of: {dirName}");

            string zipPath = Path.Combine(_dataPath, dirName);
            string[] zipFiles = Directory.GetFiles(zipPath, "Stores*.*z*");

             _archiveExtract.ExtractCompressedFiles(zipPath, zipFiles); //@@@@@@@@@@@@@@@@ uncomment

            //read xml files
            string[] xmlStoresFiles = Directory.GetFiles(zipPath, "Stores*.xml");

            var stores = new List<Store>();
            foreach (string xmlFile in xmlStoresFiles)
            {
                stores.AddRange(ParseStoresXml(xmlFile));
                Console.WriteLine($"xmlStoresFiles parse");
            }
            _dbEditor.InsertOrUpdate(stores);
        }
        /*---------------------------------*/

        private void InitializePricesData(string dirName)
        {
            Console.WriteLine($"Initialize Prices of: {dirName}");

            string zipPath = Path.Combine(_dataPath, dirName);
            string[] zipFiles = Directory.GetFiles(zipPath, "PriceFull*.*z*");

             _archiveExtract.ExtractCompressedFiles(zipPath, zipFiles); //@@@@@@@@@@@@@@@@ uncomment

            string[] xmlFiles = Directory.GetFiles(zipPath, "PriceFull*.xml");


            Parallel.ForEach(xmlFiles, (xmlFile) =>
            {
                var entries = ParsePricesXml(xmlFile);
                _dbEditor.InsertOrUpdate(entries);
            });


            //foreach (string xmlFile in xmlFiles)
            //{
            //    AddPricesXmlToDb(xmlFile);
            //}

        }
        /*---------------------------------*/


        private List<Store> ParseStoresXml(string xmlFile)
        {
            var storesList = new List<Store>();
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);

            var chainName = xml.SelectSingleNode("//ChainName | //CHAINNAME").InnerText;
            var chainId = xml.SelectSingleNode("//ChainId | //CHAINID").InnerText;

            XmlNodeList xnList = xml.SelectNodes("//Store | //STORE");

            var chain = new Chain()
            {
                Name = chainName,
                ChainNumber = Convert.ToInt64(chainId)
            };

            //if (!_chains.ContainsValue(chain))
            //{
            //        _chains.Add(chain.Name, chain); 
            //}
           // _dbEditor.InsertOrUpdate(new List<Chain>() { chain });

            foreach (XmlNode xn in xnList)
            {
                var storeId = xn.SelectSingleNode("StoreId | StoreID | Storeid | STOREID").InnerText; 
                var name = xn.SelectSingleNode("StoreName | STORENAME").InnerText;
                var address = xn.SelectSingleNode("Address | ADDRESS").InnerText;
                var city = xn.SelectSingleNode("City | CITY").InnerText;

                var store = new Store()
                {
                    StoreId = Convert.ToInt64(storeId),
                    Name = name,
                    Adress = address,
                    City = city,
                    Chain = chain,
                   // ChainId = chain.ChainNumber
                };
                storesList.Add(store);
            }
            return storesList;
        }
        /*---------------------------------*/


        private List<Price> ParsePricesXml(string xmlFile)
        {
            XElement root = XElement.Load(xmlFile);
            var chain = root.ElementsCaseInsensitive("ChainId").First().Value;
            var store = root.ElementsCaseInsensitive("StoreId").First().Value;

            var items = root.ElementsCaseInsensitive("item").ToList();
            if (!items.Any())
            {
                var itemsNode = root.ElementsCaseInsensitive("items").First();
                items = itemsNode.ElementsCaseInsensitive("item").ToList();
            }


            var pricesList = new List<Price>();
            var itemsList = new List<Item>();
            object locker = new object();

            Parallel.ForEach(items, 
               () => { return new List<Price>(); },
              (el, loopState, local_prices) =>
              {
                  var itemCode = el.ElementsCaseInsensitive("ItemCode").First().Value;
                  if (Convert.ToInt64(itemCode) < 10000000)
                  {
                      return local_prices;
                  }

                  var itemName = el.ElementsCaseInsensitive("ItemName").First().Value;
                  var itemPrice = Convert.ToDouble(el.ElementsCaseInsensitive("ItemPrice").First().Value);
                  var itemType = el.ElementsCaseInsensitive("ItemType").First().Value;
                  var unitQty = el.ElementsCaseInsensitive("UnitQty").First().Value;
                  var quantity = el.ElementsCaseInsensitive("Quantity").First().Value;
                  var updateDate = el.ElementsCaseInsensitive("PriceUpdateDate").First().Value;
                  var manufacturer = el.ElementsCaseInsensitive("ManufacturerName").FirstOrDefault();
                  if (manufacturer == null)
                  {
                      manufacturer = el.ElementsCaseInsensitive("ManufactureName").FirstOrDefault();
                  }
                  var manufacturerName = manufacturer.Value;
                  var item = new Item()
                  {
                      ItemCode = Convert.ToInt64(itemCode),
                      Name = itemName,
                      Type = itemType,
                      UnitQuantity = quantity,
                      UpdateTime = updateDate,
                      ManufacturerName = manufacturerName
                  };
                  var price = new Price()
                  {
                      Item = item,
                      ItemPrice = itemPrice,
                      UnitQty = unitQty,
                      UpdateDate = updateDate,
                      Quantity = quantity,
                      StoreId = Convert.ToInt64(store),
                      ChainId = Convert.ToInt64(chain)
                  };

                  local_prices.Add(price);
                  return local_prices;

              },

               (task_items =>
               {
                   lock (locker)
                   {
                       pricesList.AddRange(task_items);
                   }
               }
               ));

            return pricesList;
        }
        /*---------------------------------*/
    }
    /*---------------------------------------------------------------------------------------*/


    public static class XDocumentExtensions
    {
        //extention method
        public static IEnumerable<XElement> ElementsCaseInsensitive(this XContainer source,
            XName name)
        {
            return source.Elements()
                .Where(e => e.Name.Namespace == name.Namespace
                    && e.Name.LocalName.Equals(name.LocalName, StringComparison.OrdinalIgnoreCase));
        }
        /*---------------------------------*/
    }
    /*---------------------------------------------------------------------------------------*/

}
