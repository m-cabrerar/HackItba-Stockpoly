using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController[] jugadores;
    [SerializeField] DiceController dice;
    [HideInInspector] public int diceSideThrown;
    [HideInInspector] public bool gameOver = false;
    private int turno = 0;
    public void Play(int steps)
    {
        jugadores[turno % jugadores.Length].steps = steps;
        StartCoroutine(jugadores[turno % jugadores.Length].Move());
        UIController.RenderPlayerContext(jugadores[turno % jugadores.Length]);
    }
}
