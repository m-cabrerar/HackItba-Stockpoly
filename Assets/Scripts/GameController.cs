using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController[] jugadores;
    [SerializeField] DiceController dice;
    [SerializeField] UIController ui;
    [SerializeField] AlphaVantageAPI apiStocks;
    [SerializeField] ItemData[] items;
    [HideInInspector] public int diceSideThrown;
    [HideInInspector] public bool gameOver = false;
    private int turno = 0;

    void Start()
    {
        ui.TerminarTurnoButton.onClick.AddListener(FinishTurn);
        ui.tarjeta.comprarButton.onClick.AddListener(Comprar);
        //ui.tarjeta.noButton
        foreach (PlayerController jugador in jugadores)
        {
            jugador.cobrar(1000);
        }
    }

    public void Play(int steps)
    {
        jugadores[turno % jugadores.Length].steps = steps;
        StartCoroutine(jugadores[turno % jugadores.Length].Move(dice, turno));
        ui.RefreshStocks(turno);
    }

    public void BuyProperty()
    {
        // ...
    }

    public void BuyHouse()
    {
        // ...
    }

    public void SellHouse()
    {
        // ...
    }

    public void Comprar()
    {
        ItemData data = ui.casillas[jugadores[turno].posicionTablero].getData();
        jugadores[turno].cobrar(-1 * (long)Math.Floor((data.precio.precioBase * (1 + data.precio.variacion))));
        if (!jugadores[turno].cartera.ContainsKey(data)) { jugadores[turno].cartera[data] = 0; }
        jugadores[turno].cartera[data]++;
        ui.refreshSaldo(jugadores[turno]);
    }

    public void FinishTurn()
    {
        turno++;
        ui.RenderNewTurnContext();
        dice.EnableDice();
    }
}
