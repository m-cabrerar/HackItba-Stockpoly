using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static int Main(string[] args)
    {
        //acciones hardcodeadas a utilizar en el juego
        string[] symbols = { "IBM", "KO", "GOOGL", "MCD" };

        //diccionario donde metemos la data x cada stock    
        Dictionary<string, string[]> stockData = MainAsync(symbols).Result;

        foreach (var entry in stockData)
        {
            Console.WriteLine($"Symbol: {entry.Key}");
            Console.WriteLine("Data:");
            foreach (string line in entry.Value)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        return 0;
    }

    static async Task<Dictionary<string, string[]>> MainAsync(string[] symbols)
    {
        Dictionary<string, string[]> stockData = new Dictionary<string, string[]>();

        foreach (string symbol in symbols)
        {
            string[] data = await MakeRequestAsync(symbol);
            stockData[symbol] = data;
        }

        return stockData;
    }

    static async Task<string[]> MakeRequestAsync(string symbol)
    {
        string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey=YVFHTBXFDSO30PAX";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Console.WriteLine($"La solicitud falló con el código de estado: {response.StatusCode} para {symbol}");
                    return new string[0];
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error al hacer la solicitud para {symbol}: {e.Message}");
                return new string[0];
            }
        }
    }
}