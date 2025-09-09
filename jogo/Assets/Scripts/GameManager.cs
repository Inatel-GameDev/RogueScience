using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public Jogador Jogador;
    [Header("Flags")]
    public bool isPaused = false;
    public bool isFinalizado = false;
    
    [Header("Canvas")]
    public Canvas menuPause;
    public Canvas menuFim;

    [Header("Checkpoint")]
    public Transform ultimoCheckpoint;

    public static GameManager Instance { get; private set; }
    

    private void Awake()
    {
        // Se já existe uma instância e não é essa → destrói
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Define a instância
        Instance = this;
    }

    public void Pause()
    {
        if(isFinalizado)
            return;
        
        isPaused = true;
        Time.timeScale = 0;
        menuPause.gameObject.SetActive(true);
        Jogador.MudarEstado(Jogador.getEstadoDesativado());
    }
    
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menuPause.gameObject.SetActive(false);
        Jogador.MudarEstado(Jogador.getEstadoAtivo());
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

    public void FaseFinalizada()
    {   
        isFinalizado = true;
        menuFim.gameObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Jogador.MudarEstado(Jogador.getEstadoDesativado());
    }
}