using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketDataToJson
{
    public static String Jsonify(String name, Stock[] movimientos, int desde, int hasta) 
    {
        String json = "{\"movements\":[";
        desde = desde % movimientos.Length;
        for (int i = desde; i < hasta ; i++)
        {
            Stock data = movimientos[i];
            DateTime dateTime = data.timestamp;
            json += "{\"stockName\":\"" + name + "\","
            + "\"stockPrice\":" + RoundTwoDigits(data.close) + ","
            + "\"dateTime\":\"" + dateTime.ToString("dd'/'MM'/'yyyy") 
            + "\"},";
        }
        json = json.Remove(json.Length-1);
        json += "]}";
        return json;
    }

    private static Double RoundTwoDigits(double num){
        int aux = (int) (num*100);
        return ((double)aux)/100;
    }

}
