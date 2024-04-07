using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public String nombre;
    public Sprite sprite;
    public String detalle;
    [System.Serializable] public struct Precio { public long precioBase; public double variacion; }
    public Precio precio;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ItemData other = (ItemData)obj;

        // Check if all fields are equal
        return nombre == other.nombre &&
               sprite == other.sprite &&
               detalle == other.detalle &&
               precio.precioBase == other.precio.precioBase &&
               precio.variacion == other.precio.variacion;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17; // Prime number

            // Combine hash codes for all fields
            hash = hash * 23 + (nombre != null ? nombre.GetHashCode() : 0);
            hash = hash * 23 + (sprite != null ? sprite.GetHashCode() : 0);
            hash = hash * 23 + (detalle != null ? detalle.GetHashCode() : 0);
            hash = hash * 23 + precio.precioBase.GetHashCode();
            hash = hash * 23 + precio.variacion.GetHashCode();

            return hash;
        }
    }
}
