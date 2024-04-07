using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed = 2f;
    [SerializeField] float wait = 0.2f;
    [SerializeField] DiceController dice;
    [SerializeField] UIController ui;
    public HashSet<int> properties = new HashSet<int>();
    [HideInInspector] public int posicionTablero = 0;
    [HideInInspector] public int steps;
    private long disponible;
    private Dictionary<ItemData, int> cartera = new Dictionary<ItemData, int>();
    bool isMoving;
    
    public IEnumerator Move(DiceController dice)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        for (;steps > 0; steps--)
        {
            Vector3 nextPos = path.nodeList[(posicionTablero + 1) % path.nodeList.Count].position;
            while (MoveToNode(nextPos)) {yield return null;}
            yield return new WaitForSeconds(wait);
            posicionTablero++;
        }
        posicionTablero = (posicionTablero + 1) % path.nodeList.Count;
        isMoving = false;
        dice.DisableDice();
        ui.RenderPlayerContext(this);
    }

    bool MoveToNode(Vector3 node)
    {
        return node != (transform.position = Vector3.MoveTowards(transform.position, node, speed * Time.deltaTime));
    }

    public long getDisponible()
    {
        return disponible;
    }

    public long getInvertido()
    {
        long invertido = 0;
        foreach (ItemData data in cartera.Keys)
        {
            invertido = (long)Math.Floor(data.precio.precioBase * (1 + data.precio.variacion) * cartera[data]);
        }
        return invertido; 
    }

}
