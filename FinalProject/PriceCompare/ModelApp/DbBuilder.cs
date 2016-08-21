using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PriceCompare.Model;
using System.Xml.Linq;
using System.Data.SqlClient;

/*
this class parses xml files and builds the database
*/
namespace ModelApp
{
    class DbBuilder
    {
        //working directory where the data found
        string _dataPath = @"C:\GiT\dotNetCourse\FinalProject\bin\data\";
        ExtractArchiveFiles _archiveExtract;
        AddDataToDb _DbEditor;

        /*---------------------------------*/

        public DbBuilder()
        {
            _DbEditor = new AddDataToDb();
            _archiveExtract = new ExtractArchiveFiles();
        }
        /*---------------------------------*/

        public void InitializeDB()
        {
            DirectoryInfo[] dirInfo = new DirectoryInfo(_dataPath).GetDirectories();
            //_DbEditor.AddChainToDb(); //@@@@@@@@@@@@@@@@@@@@@@@@         
            foreach (DirectoryInfo dir in dirInfo)
            {
                InitializeStoresData(dir.Name);
                InitializePricesData(dir.Name);
            }
        }
        /*---------------------------------*/

        //return list of xml files name
        private void InitializeStoresData(string dirName)
        {
            //unzip all "store" files

            Console.WriteLine($"InitializeStoresTable: {dirName}");

            string zipPath = (_dataPath + dirName);
            string[] zipFiles = Directory.GetFiles(zipPath, "Stores*.*z*");

            // _archiveExtract.ExtractCompressedFiles(zipPath, zipFiles); //@@@@@@@@@@@@@@@@ uncomment

            //read xml files
            string[] xmlStoresFiles = Directory.GetFiles(zipPath, "Stores*.xml");

            /*
            if (dirName.Equals("shufersal")) //because shufersal wrote with capital letters
            {
                foreach (string xmlFile in xmlStoresFiles)
                {
                    var list = ParseStoresXml_Shufersal(xmlFile);
                    _DbEditor.AddStoresToDb(list);
                }
            }
            else*/
            {
                foreach (string xmlFile in xmlStoresFiles)
                {
                    var list = ParseStoresXml(xmlFile);
                    _DbEditor.AddStoresToDb(list);
                }
            }

        }
        /*---------------------------------*/

        private void InitializePricesData(string dirName)
        {
            Console.WriteLine($"InitializePricesTable of: {dirName}");

            string zipPath = (_dataPath + dirName);
            string[] zipFiles = Directory.GetFiles(zipPath, "PriceFull*.*z*");

            //  _archiveExtract.ExtractCompressedFiles(zipPath, zipFiles); //@@@@@@@@@@@@@@@@ uncomment

            string[] xmlFiles = Directory.GetFiles(zipPath, "PriceFull*.xml");

            XmlDocument doc = new XmlDocument();
            foreach (string xmlFile in xmlFiles)
            {
                AddPricesXmlToDb(xmlFile);
            }

        }
        /*---------------------------------*/

        /*
    private List<Store> ParseStoresXml_Shufersal(string xmlFile)
    {
        var storesList = new List<Store>();
        XmlDocument xml = new XmlDocument();
        xml.Load(xmlFile);
        var chainName = xml.SelectSingleNode("//CHAINNAME").InnerText;
        var chainId = xml.SelectSingleNode("//CHAINID").InnerText;

        XmlNodeList xnList = xml.SelectNodes("//STORE");

        foreach (XmlNode xn in xnList)
        {
            var storeId = xn["STOREID"].InnerText;
            var name = xn["STORENAME"].InnerText;
            var address = xn["ADDRESS"].InnerText;
            var city = xn["CITY"].InnerText;

            var store = new Store()
            {
                StoreId = Convert.ToInt64(storeId),
               ChainName = chainName,
                Name = name,
                Adress = address,
                City = city,
                Chain = new Chain() { Name = chainName, ChainId = Convert.ToInt64(chainId) }
            };
            storesList.Add(store);
        }
        return storesList;
    }
    /*---------------------------------*/

        private List<Store> ParseStoresXml(string xmlFile)
        {
            var storesList = new List<Store>();
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);
            var chainName = xml.SelectSingleNode("//ChainName").InnerText;
            var chainId = xml.SelectSingleNode("//ChainId").InnerText;


            XmlNodeList xnList = xml.SelectNodes("//Store");

            var chain = new Chain()
            {
                Name = chainName,
                ChainNumber = chainId
            };

            foreach (XmlNode xn in xnList)
            {
                var storeId = xn["StoreId"].InnerText;
                var name = xn["StoreName"].InnerText;
                var address = xn["Address"].InnerText;
                var city = xn["City"].InnerText;

                var store = new Store()
                {
                    StoreId = storeId,
                    // ChainName = chainName,
                    // ChainId = Convert.ToInt64(chainId),
                    Name = name,
                    Adress = address,
                    City = city,
                    Chain = chain
                };
                storesList.Add(store);

            }
            return storesList;
        }
        /*---------------------------------*/

        private void AddPricesXmlToDb(string xmlFile)
        {
            XElement root = XElement.Load(xmlFile);

            IEnumerable<XElement> items = from el in root.Elements("Items")
                                          where (from add in el.Elements("Item")
                                                 select add)
                                                 .Any()
                                          select el;

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);
            var chain = xml.SelectSingleNode("//ChainId | //Chainid").InnerText;
            var store = xml.SelectSingleNode("//StoreId | //Storeid").InnerText;
            xml.RemoveAll();

            //   XElement chain = root.Element("ChainId");
            //  XElement store = root.Element("StoreId");

            /*
           string chainname = null;

           using (var db = new PricesContext())
           {
               // chainname = db.Stores.Where(x => x.Chain.Name.Equals(chain)).Select(y => y.Name).FirstOrDefault();
               chainname = (from x in db.Stores.Select(x => x.Chain)
                            where (x.Id == chain)
                            select x.Name)
                            .FirstOrDefault();
           }*/

            var pricesList = new List<Price>();
            var itemsList = new List<Item>();
            foreach (XElement el in items.Descendants("Item").Take(5)) //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ remove take
            {

                var itemCode = el.Element("ItemCode").Value;
                //  var itemId = el.Element("ItemId").Value;
                var itemName = el.Element("ItemName").Value;
                var itemPrice = el.Element("ItemPrice").Value;
                var itemType = el.Element("ItemType").Value;
                var unitQty = el.Element("UnitQty").Value;
                var quantity = el.Element("Quantity").Value;
                var updateDate = el.Element("PriceUpdateDate").Value;

                var item = new Item()
                {
                    ItemCode = itemCode,
                    ChainNumber = chain,
                    StoreId = store,
                    Name = itemName,
                    Type = itemType,
                    UnitQuantity = quantity,
                    UpdateTime = updateDate
                };

                var price = new Price()
                {
                    Item = item,
                    ItemPrice = itemPrice,
                    UnitQty = unitQty,
                    UpdateDate = updateDate,
                    Quantity = quantity,
                    StoreId = store
                };
                itemsList.Add(item);
                pricesList.Add(price);
            }
         //   _DbEditor.AddItemsToDb(itemsList);
            _DbEditor.AddPricesToDb(pricesList);
        }
        /*---------------------------------*/


    }
}
