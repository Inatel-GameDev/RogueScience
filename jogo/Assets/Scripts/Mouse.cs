using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Jogador jogador;       // Referência ao jogador
    public float maxDistance;
    public float minDistance;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        maxDistance = jogador._mouseDistMax;
        minDistance = jogador._mouseDistMin;
    }

    private void Update()
    {
        // Pega posição do mouse no mundo
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Direção do jogador até o mouse
        Vector3 direction = mousePos - jogador.transform.position;
        float distance = direction.magnitude;

        // Se a distância for maior que o máximo, limita
        if (distance < minDistance)
        {
            direction = direction.normalized * minDistance; // sem o return a mira circunda o personagem
            // return; // com return a mira teleporta pro outro lado do personagem
        }
        else if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // Posição final da mira
        transform.position = jogador.transform.position + direction;
    }
}