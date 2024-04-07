using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerContext;
    [SerializeField] public Button TerminarTurnoButton;
    [SerializeField] CasillaController[] casillas;
    [SerializeField] AlphaVantageAPI apiStocks;

    [System.Serializable] public struct TarjetaUI 
    { 
        public TextMeshProUGUI nombre;
        public Image image;
        public TextMeshProUGUI detalle;
        public TextMeshProUGUI precio;
    }
    
    [SerializeField] TarjetaUI tarjeta;

    void Start()
    {
        TriggerButton(false);
        tarjetaEnable(false);
    }

    public void RenderPlayerContext(PlayerController player) // ejecutar al inicio de un turno
    {
        TriggerButton(true);
        playerContext.text = "Disponible: " + player.getDisponible() + "\nInvertido = " + player.getInvertido();
        if (casillas[player.posicionTablero].getData() != null)
        {
            RenderTarjeta(casillas[player.posicionTablero].getData());
        
        }
        
        Dictionary<string, Stack<Stock>> data = apiStocks.stocksData;
        Debug.Log(data);
    }

    public void LoadProperty()
    {
        // Implement
    }

    public void RenderNewTurnContext()
    {
        TriggerButton(false);
        tarjetaEnable(false);
    }

    public void RenderTarjeta(ItemData data)
    {
        tarjetaEnable(true);
        tarjeta.nombre.text = data.nombre;
        tarjeta.image.GetComponent<Image>().sprite = data.sprite;
        tarjeta.detalle.text = data.detalle;
        tarjeta.precio.text = "$" + data.precio.precioBase * (1 + data.precio.variacion);
    }

    private void TriggerButton(bool state)
    {
        TerminarTurnoButton.gameObject.SetActive(state);
        TerminarTurnoButton.enabled = state;   
    }

    private void tarjetaEnable(bool state)
    {
        tarjeta.nombre.enabled = state;
        tarjeta.detalle.enabled = state;
        tarjeta.image.enabled = state;
        tarjeta.precio.enabled = state;
    }
}