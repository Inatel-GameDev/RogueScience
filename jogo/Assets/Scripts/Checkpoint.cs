using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Jogador jogador = other.GetComponent<Jogador>();
        if (jogador != null)
        {
            jogador.gameManager.DefinirCheckpoint(transform);
        }
    }
}