using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerContext;
    [SerializeField] public Button TerminarTurnoButton;

    
    void Start()
    {
        TriggerButton(false);
    }

    public void RenderPlayerContext(PlayerController player) // ejecutar al inicio de un turno
    {
        TriggerButton(true);
        playerContext.text = "Ahorros: " + player.getAhorros() + "\nInvertido = " + player.getInvertido();
    }

    public void LoadProperty()
    {
        // Implement
    }

    public void RenderNewTurnContext()
    {
        TriggerButton(false);
    }

    private void TriggerButton(bool state)
    {
        TerminarTurnoButton.gameObject.SetActive(state);
        TerminarTurnoButton.enabled = state;    
    }
}