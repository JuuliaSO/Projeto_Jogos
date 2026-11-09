using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovimentoCoelho : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 7f;
    public float gravidade = -20f;

    CharacterController controle;
    Animator animator;
    float velocidadeVertical;
    bool estavaNoChao;

    Transform cam;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        var teclado = Keyboard.current;
        if (teclado == null) return;

        float h = ((teclado.dKey.isPressed || teclado.rightArrowKey.isPressed) ? 1 : 0)
                - ((teclado.aKey.isPressed || teclado.leftArrowKey.isPressed) ? 1 : 0);
        float v = ((teclado.wKey.isPressed || teclado.upArrowKey.isPressed) ? 1 : 0)
                - ((teclado.sKey.isPressed || teclado.downArrowKey.isPressed) ? 1 : 0);

        Vector3 frente = cam.forward;  frente.y = 0;  frente.Normalize();
        Vector3 direita = cam.right;   direita.y = 0; direita.Normalize();
        Vector3 direcao = frente * v + direita * h;

        if (direcao.magnitude > 0.1f)
            transform.forward = Vector3.Lerp(transform.forward, direcao.normalized, 10f * Time.deltaTime);

        bool noChao = controle.isGrounded;
        bool pulou = false;

        if (noChao)
        {
            velocidadeVertical = -1f;
            if (teclado.spaceKey.wasPressedThisFrame)
            {
                velocidadeVertical = forcaPulo;
                pulou = true;
            }
        }
        else
        {
            velocidadeVertical += gravidade * Time.deltaTime;
        }

        Vector3 movimento = direcao.normalized * velocidade;
        movimento.y = velocidadeVertical;
        controle.Move(movimento * Time.deltaTime);

        // ---- ANIMAÇÃO ----
        float intensidade = direcao.normalized.magnitude;   // 0 ou 1
        animator.SetFloat("moveValueX", intensidade);
        animator.SetFloat("linearVelocityX", intensidade * velocidade);
        animator.SetFloat("linearVelocityY", velocidadeVertical);
        animator.SetBool("isGrounded", noChao);

        if (pulou) animator.SetTrigger("hasJumped");
        if (noChao && !estavaNoChao && !pulou) animator.SetTrigger("hasLanded");

        estavaNoChao = noChao;
    }
}