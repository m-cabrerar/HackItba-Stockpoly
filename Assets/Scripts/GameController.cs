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
        ui.tarjeta.noButton.onClick.AddListener(SaltoACartera);
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
        ItemData data = ui.casillas[jugadores[turno % jugadores.Length].posicionTablero].getData();
        jugadores[turno % jugadores.Length].cobrar(-1 * (long)Math.Floor((data.precio.precioBase * (1 + data.precio.variacion))));
        if (!jugadores[turno % jugadores.Length].cartera.ContainsKey(data)) { jugadores[turno % jugadores.Length].cartera[data] = 0; }
        jugadores[turno % jugadores.Length].cartera[data]++;
        ui.refreshSaldo(jugadores[turno % jugadores.Length]);
    }

    public void SaltoACartera()
    {
        ui.tarjetaEnable(false);
        while (ui.carteraContent.transform.childCount != 0)
        {
            Destroy(ui.carteraContent.transform.GetChild(0));
        }
        foreach (ItemData data in jugadores[turno % jugadores.Length].cartera.Keys)
        {
            GameObject obj = Instantiate(ui.carteraItemPref, ui.carteraContent.transform);
        }
        ui.carteraContent.SetActive(true);
    }

    public void FinishTurn()
    {
        turno++;
        ui.RenderNewTurnContext();
        dice.EnableDice();
        ui.carteraContent.SetActive(false);
    }
}
