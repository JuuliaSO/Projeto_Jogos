using System.Collections;
using TMPro;
using UnityEngine;

public class ContadorCenouras : MonoBehaviour
{
    public TextMeshProUGUI texto;      // "Cenouras: 0"
    public TextMeshProUGUI mensagem;   // mensagem no meio da tela
    public float tempoMensagem = 1.5f;

    int totalNoMapa;
    Coroutine rotina;

    void Start()
    {
        Cenoura.total = 0;
        totalNoMapa = FindObjectsByType<Cenoura>(FindObjectsSortMode.None).Length;
        texto.text = "Cenouras: 0 / " + totalNoMapa;
        mensagem.text = "";
    }

    void OnEnable()  { Cenoura.AoColetar += AoColetarCenoura; }
    void OnDisable() { Cenoura.AoColetar -= AoColetarCenoura; }

    void AoColetarCenoura(int total)
    {
        texto.text = "Cenouras: " + total + " / " + totalNoMapa;

        bool pegouTodas = total >= totalNoMapa;
        if (rotina != null) StopCoroutine(rotina);
        rotina = StartCoroutine(MostrarMensagem(
            pegouTodas ? "Parabéns! Você pegou todas!" : "+1 cenoura!",
            pegouTodas));
    }

    IEnumerator MostrarMensagem(string msg, bool fixa)
    {
        mensagem.text = msg;
        if (fixa) yield break;          // a de vitória fica na tela
        yield return new WaitForSeconds(tempoMensagem);
        mensagem.text = "";
    }
}