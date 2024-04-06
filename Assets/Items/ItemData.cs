using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public String nombre;
    public Sprite sprite;
    public String detalle;
    [System.Serializable] public struct Precio { public long precioBase; public float variacion; }
    public Precio precio;
}
