using System.Collections;
using UnityEngine;

public class Cenoura : MonoBehaviour
{
    public float velocidadeGiro = 90f;
    public float duracaoColeta = 0.5f;

    public static int total;                              // cenouras coletadas
    public static event System.Action<int> AoColetar;     // o "sino"

    bool coletada;

    void Update()
    {
        if (coletada) return;
        transform.Rotate(0, velocidadeGiro * Time.deltaTime, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (coletada) return;
        if (other.GetComponentInParent<MovimentoCoelho>() == null) return;

        coletada = true;
        total++;
        AoColetar?.Invoke(total);        // toca o sino
        Debug.Log("Cenouras: " + total);
        StartCoroutine(AnimacaoColeta());   // volta a animação
    }

    IEnumerator AnimacaoColeta()
    {
        Vector3 posicao = transform.position;
        Vector3 escala = transform.localScale;
        float t = 0f;

        while (t < duracaoColeta)
        {
            t += Time.deltaTime;
            float p = t / duracaoColeta;

            transform.position = posicao + Vector3.up * p * 1.5f;                      // sobe
            transform.Rotate(0, velocidadeGiro * 6f * Time.deltaTime, 0, Space.World);  // gira rápido
            transform.localScale = escala * (1f - p);                                   // encolhe até sumir
            yield return null;
        }
        Destroy(gameObject);
    }
}