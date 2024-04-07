using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        ui.jugadorActualDisplay.text = "Jugador " + ((turno%jugadores.Length) + 1);
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
        for (int i = ui.carteraContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ui.carteraContent.transform.GetChild(i).gameObject);
        }
        foreach (ItemData data in jugadores[turno % jugadores.Length].cartera.Keys)
        {
            GameObject obj = Instantiate(ui.carteraItemPref, ui.carteraContent.transform);
            obj.transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = data.nombre;
            obj.transform.Find("Descripción").GetComponent<TextMeshProUGUI>().text = data.detalle;
            obj.transform.Find("Precio").GetComponent<TextMeshProUGUI>().text = "$" + data.precio.precioBase * (1+data.precio.variacion);
            obj.transform.Find("Image").GetComponent<Image>().sprite = data.sprite;
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
