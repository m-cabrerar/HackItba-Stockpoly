using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Stock
{
    public DateTime timestamp {get; set;}
    public double open {get; set;}    
    public double high {get; set;}    
    public double low {get; set;}
    public double close {get; set;}
    public long volume {get; set;}

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