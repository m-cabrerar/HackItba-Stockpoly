using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] DiceController dice; //mover a game controller
    //[SerializeField] Panel playerContextPanel;
    
    public void FinishTurn() { // mover a game controller
        //turno++;
        dice.EnableDice();
    }

    public static void RenderPlayerContext(PlayerController player)
    {
        // TODO
    }

    public void LoadProperty()
    {
        // Implement
    }
}