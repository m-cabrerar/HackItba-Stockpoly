using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerContext;
    [SerializeField] public Button TerminarTurnoButton;
    [SerializeField] public CasillaController[] casillas;
    [SerializeField] AlphaVantageAPI apiStocks;
    public GameObject carteraContent;
    public TextMeshProUGUI jugadorActualDisplay;

    private static string gptURI = "https://stockpoly-api-production.up.railway.app/profile";

    [System.Serializable] public struct TarjetaUI 
    { 
        public TextMeshProUGUI nombre;
        public Image image;
        public TextMeshProUGUI detalle;
        public TextMeshProUGUI precio;
        public Button comprarButton;
        public Button noButton;
    }
    
    [SerializeField] public TarjetaUI tarjeta;
    [SerializeField] public GameObject carteraItemPref;

    void Start()
    {
        TriggerButton(false);
        tarjetaEnable(false);
    }

    public void RenderPlayerContext(PlayerController player, int turno) // ejecutar al inicio de un turno
    {
        TriggerButton(true);
        refreshSaldo(player);
        if (casillas[player.posicionTablero].getData() != null)
        {
            RenderTarjeta(casillas[player.posicionTablero].getData(), turno);
        
        }
        
        Dictionary<string, Stack<Stock>> data = apiStocks.stocksData;
    }

    public void refreshSaldo(PlayerController player)
    {
        playerContext.text = "Disponible: " + player.getDisponible() + "\nInvertido = " + player.getInvertido();
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

    public void RenderTarjeta(ItemData data, int turno)
    {
        tarjetaEnable(true);
        tarjeta.nombre.text = data.nombre;
        tarjeta.image.GetComponent<Image>().sprite = data.sprite;
        tarjeta.detalle.text = data.detalle;
        tarjeta.precio.text = "$" + RoundTwoDigits(data.precio.precioBase * (1 + data.precio.variacion));
    }

    private void TriggerButton(bool state)
    {
        TerminarTurnoButton.gameObject.SetActive(state);
        TerminarTurnoButton.enabled = state;   
    }

    public void tarjetaEnable(bool state)
    {
        tarjeta.nombre.enabled = state;
        tarjeta.detalle.enabled = state;
        tarjeta.image.enabled = state;
        tarjeta.precio.enabled = state;
        tarjeta.comprarButton.enabled = state;
        tarjeta.noButton.enabled = state;
    }

    public IEnumerator RefreshStocks(int turno)
    {                
        foreach(var casilla in casillas)
        {

            ItemData data = casilla.getData();
            if(data is TituloData) 
            {   
                TituloData auxData = (TituloData) data;
                if(apiStocks.stocksData[auxData.nombre].Count < 1) 
                {
                    apiStocks.resetStack(auxData.nombre);
                }
                if(turno < 1)
                {
                    while(auxData.precioAnterior < 0.0001) auxData.precioAnterior = apiStocks.stocksData[auxData.nombre].Pop().close;
                }
                double aux = apiStocks.stocksData[auxData.nombre].Pop().close;
                auxData.precio.variacion = (aux - auxData.precioAnterior)/auxData.precioAnterior;
                auxData.precioAnterior = aux;
                
                int from = turno % apiStocks.stocksHistorical[data.nombre].Length;
                string json = MarketDataToJson.Jsonify(data.nombre, apiStocks.stocksHistorical[data.nombre], from - 10 > 0 ? from - 10 : 0, from + 20);
                using(UnityWebRequest webRequest = UnityWebRequest.Post(gptURI, json, null))
                {
                    yield return webRequest.SendWebRequest();
                    switch(webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:                
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(String.Format("Some processing went wrong : {0}", webRequest.error));
                            break;
                        case UnityWebRequest.Result.Success:
                            data.detalle = webRequest.downloadHandler.text;
                            break;
                    }
                }  
            }
        }
    }

    private Double RoundTwoDigits(double num){
        int aux = (int) (num*100);
        return ((double)aux)/100;
    }
}