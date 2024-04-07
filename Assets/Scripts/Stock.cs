using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Stock
{
    DateTime timestamp {get; set;}
    double open {get; set;}    
    double high {get; set;}    
    double low {get; set;}
    double close {get; set;}
    long volume {get; set;}

    public Stock(DateTime timestamp, double open, double high, double low, double close, long volume)
    {
        this.timestamp = timestamp;
        this.open = open;
        this.high = high;
        this.low = low;
        this.close = close;
        this.volume = volume;
    }
}