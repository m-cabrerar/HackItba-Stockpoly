using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] DiceController dice;
    
    public void FinishTurn() {
        turno++;
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