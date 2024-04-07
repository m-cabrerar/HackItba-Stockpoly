using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;



public class AlphaVantageAPI : MonoBehaviour
{
    public Dictionary<string, Stack<Stock>> stocksData;
    public Dictionary<string, Stock[]> stocksHistorical;
    
    void Start()
    {
        string[] symbols = { "AAPL", "SPY", "MELI", "COKE", "NVDA", "BTC" };
        GetRequest(symbols);
    }

    void GetRequest(string[] symbols)
    {
        stocksData = new Dictionary<string, Stack<Stock>>();
        stocksHistorical = new Dictionary<string, Stock[]>();

        foreach(var symbol in symbols){
    
            /*
            **API Deprecated because it was a free trial, hardcoding the CSVs for the MVP

            using(UnityWebRequest webRequest = UnityWebRequest.Get($"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey=HMI2Y6EU1EEPAIYP&datatype=csv"))
            {
                yield return webRequest.SendWebRequest();
                switch(webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:                
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(String.Format("Some processing went wrong : {0}", webRequest.error));
                        break;
                    case UnityWebRequest.Result.Success:
                        string data = webRequest.downloadHandler.text;
                        byte[] byteArray = Encoding.UTF8.GetBytes(data);
                        Stack<Stock> stack = new Stack<Stock>();
                        stocksData[symbol] = stack;
                        MemoryStream stream = new MemoryStream(byteArray);
                        StreamReader reader = new StreamReader(stream);
                        bool EOF = false;
                        reader.ReadLine();
                        while(!EOF)
                        {
                            string dataString = reader.ReadLine();
                            if(dataString == null)
                            {
                                EOF = true;
                                break;
                            }
                            var dataValues = dataString.Split(',');
                            stack.Push(new Stock(DateTime.Parse(dataValues[0]), Convert.ToDouble(dataValues[1]), Convert.ToDouble(dataValues[2]), Convert.ToDouble(dataValues[3]), Convert.ToDouble(dataValues[4]), Convert.ToInt32(dataValues[5])));
                        }
                        Debug.Log(stack.Count);
                        break;
                }
            }
            */   
            Stack<Stock> stack = new Stack<Stock>();
            stocksData[symbol] = stack;
            StreamReader stream = new StreamReader(String.Format("./Assets/Items/{0}.csv", symbol));
            bool EOF = false;
            stream.ReadLine();
            while(!EOF)
            {
                string dataString = stream.ReadLine();
                if(dataString == null)
                {
                    EOF = true;
                    break;
                }
                var dataValues = dataString.Split(',');
                stack.Push(new Stock(DateTime.Parse(dataValues[0]), Convert.ToDouble(dataValues[1]), Convert.ToDouble(dataValues[2]), Convert.ToDouble(dataValues[3]), Convert.ToDouble(dataValues[4]), Convert.ToInt64(dataValues[5])));
            }
            stocksHistorical[symbol] = stack.ToArray();
        }
    }

    public void resetStack(string symbol){
        for(int i = stocksHistorical[symbol].Length ; i >= 0 ;i--) {
            stocksData[symbol].Push(stocksHistorical[symbol][i]);
        }
    }
}