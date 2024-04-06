using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController[] jugadores;
    [SerializeField] DiceController dice;
    [SerializeField] UIController ui;
    [HideInInspector] public int diceSideThrown;
    [HideInInspector] public bool gameOver = false;
    private int turno = 0;

    void Start()
    {
        ui.TerminarTurnoButton.onClick.AddListener(FinishTurn);
    }

    public void Play(int steps)
    {
        jugadores[turno % jugadores.Length].steps = steps;
        StartCoroutine(jugadores[turno % jugadores.Length].Move(dice));
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

    public void BuyStock()
    {
        // ...
    }

    public void SellStock()
    {
        // ...
    }

    public void FinishTurn()
    {
        turno++;
        ui.RenderNewTurnContext();
        dice.EnableDice();
    }
}
