using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text playerContext;
    
    public void RenderPlayerContext(PlayerController player) // ejecutar al inicio de un turno
    {
        playerContext.text = "Ahorros: " + player.getAhorros() + "\nInvertido = " + player.getInvertido();
    }

    public void LoadProperty()
    {
        // Implement
    }

    public void RenderNewTurnContext()
    {
        // TODO
    }
}