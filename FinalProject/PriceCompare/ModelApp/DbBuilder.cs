﻿using System;
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
using System.Collections;

/*
this class parses xml files and builds the database
*/
namespace PriceCompare.Model.App
{
    class DbBuilder
    {
        //working directory where the data found
        string _dataPath = @"D:\prices\myChains";
        ExtractArchiveFiles _archiveExtract;
        AddDataToDb _dbEditor;
        Dictionary<string, Chain> _chains;


        /*---------------------------------*/

        public DbBuilder()
        {
            _dbEditor = new AddDataToDb();
            _archiveExtract = new ExtractArchiveFiles();
            _chains = new Dictionary<string, Chain>();
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
                //  var list = ParseStoresXml(xmlFile);
                stores.AddRange(ParseStoresXml(xmlFile));
                Console.WriteLine($"xmlStoresFiles parse");
                // _dbEditor.AddStoresToDb(list); 
            }

            // _dbEditor.AddChainsToDb(_chains.Values);
            //  _dbEditor.AddStoresToDb(stores); //@@@@@@@@@@@@@@@@@@@@@@@@@
            //    _dbEditor.InsertOrUpdate(_chains.Values);
           
            _dbEditor.InsertOrUpdate(stores);
          //  _chains.Clear();

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
                AddPricesXmlToDb(xmlFile);
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
                // ChainNumber = chainId
            };

            if (!_chains.ContainsValue(chain))
                _chains.Add(chain.Name, chain);

            _dbEditor.InsertOrUpdate(new List<Chain>() { chain });

            foreach (XmlNode xn in xnList)
            {
                var storeId = xn.SelectSingleNode("StoreId | StoreID | Storeid | STOREID").InnerText; //@@@@@@@@@@@@@
                var name = xn.SelectSingleNode("StoreName | STORENAME").InnerText;
                var address = xn.SelectSingleNode("Address | ADDRESS").InnerText;
                var city = xn.SelectSingleNode("City | CITY").InnerText;

                var store = new Store()
                {
                    StoreId = Convert.ToInt64(storeId),
                    Name = name,
                    Adress = address,
                    City = city,
                    //Chain = chain,
                    ChainId = chain.ChainNumber
                };
                storesList.Add(store);
            }
            return storesList;
        }
        /*---------------------------------*/



        private void AddPricesXmlToDb(string xmlFile)
        {

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);

            var chain = xml.SelectSingleNode("//ChainId | //CHAINID |//Chainid").InnerText;
            var store = xml.SelectSingleNode("//StoreId | //Storeid | //StoreID  | //STOREID").InnerText;
            xml.RemoveAll();


            XElement root = XElement.Load(xmlFile);

            var items = root.ElementsCaseInsensitive("item").ToList();
            if (!items.Any())
            {
                var itemsNode = root.ElementsCaseInsensitive("items").First();
                items = itemsNode.ElementsCaseInsensitive("item").ToList();
            }



            var pricesList = new List<Price>();
            var itemsList = new List<Item>();


            object locker = new object();

            Parallel.ForEach(items.Take(600), //@@@@@@@@@@@@@@@@@@@@@@@@ remove take
               () => { return new List<Price>(); },
              (el, loopState, local_prices) =>
              {
                  var itemCode = el.ElementsCaseInsensitive("ItemCode").First().Value;
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

            //_dbEditor.AddPricesToDb(pricesList);
            _dbEditor.InsertOrUpdate(pricesList);
        }
        /*---------------------------------*/


        /*---------------------------------*/
    }

    public static class XDocumentExtensions
    {
        public static IEnumerable<XElement> ElementsCaseInsensitive(this XContainer source,
            XName name)
        {
            return source.Elements()
                .Where(e => e.Name.Namespace == name.Namespace
                    && e.Name.LocalName.Equals(name.LocalName, StringComparison.OrdinalIgnoreCase));
        }
    }

}
