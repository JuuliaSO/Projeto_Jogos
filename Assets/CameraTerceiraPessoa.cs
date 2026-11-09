using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTerceiraPessoa : MonoBehaviour
{
    public Transform alvo;            // o Cenourito
    public float distancia = 6f;      // quão longe atrás
    public float altura = 1.5f;       // ponto que a câmera olha, acima dos pés
    public float sensibilidade = 0.15f;
    public float minInclinacao = -10f;
    public float maxInclinacao = 60f;

    float giro;
    float inclinacao = 20f;

    void Start()
    {
        giro = alvo.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;   // prende o mouse no jogo
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 mov = mouse.delta.ReadValue();
            giro += mov.x * sensibilidade;
            inclinacao -= mov.y * sensibilidade;
            inclinacao = Mathf.Clamp(inclinacao, minInclinacao, maxInclinacao);
        }

        Quaternion rotacao = Quaternion.Euler(inclinacao, giro, 0);
        Vector3 foco = alvo.position + Vector3.up * altura;

        transform.position = foco - rotacao * Vector3.forward * distancia;
        transform.LookAt(foco);
    }
}