using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Flags")]
    public bool isPaused = false;
    
    [Header("Canvas")]
    public Canvas menuPause;

    [Header("Checkpoint")]
    public Transform ultimoCheckpoint;

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        menuPause.gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menuPause.gameObject.SetActive(false);
    }

    public void DefinirCheckpoint(Transform checkpoint)
    {
        ultimoCheckpoint = checkpoint;
        Debug.Log("Novo checkpoint definido: " + checkpoint.position);
    }

    public void Respawn(Jogador jogador)
    {
        if (ultimoCheckpoint != null)
        {
            jogador.transform.position = ultimoCheckpoint.position;
            jogador.Reviver();
            Debug.Log("Player renasceu no checkpoint!");
        }
        else
        {
            Debug.LogWarning("Nenhum checkpoint definido! Respawn impossível.");
        }
    }
}