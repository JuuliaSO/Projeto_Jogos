using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;
    public Vector3 deslocamento = new Vector3(0, 3, -6);
    public float suavidade = 5f;

    void LateUpdate()
    {
        if (alvo == null) return;

        // o deslocamento gira junto com o coelho
        Vector3 destino = alvo.position + alvo.rotation * deslocamento;
        transform.position = Vector3.Lerp(transform.position, destino, suavidade * Time.deltaTime);
        transform.LookAt(alvo.position + Vector3.up * 1.5f);
    }
}