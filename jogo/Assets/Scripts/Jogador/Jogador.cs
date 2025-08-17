using System;
using UnityEngine;

// Classe principal, controla qual estado está rodando e possui as informações gerais do jogador
public abstract class Jogador : MonoBehaviour, MaquinaDeEstado
{
    [Header("Atributos")]
    private int _vida;
    public float Velocidade { get; } = 10;
    private float _energia;

    [Header("Estados")]
    public Estado EstadoAtual;
    [SerializeField] private Estado EstadoAtivo;
    [SerializeField] private Estado estadoDesativado;
    
    [Header("Componentes")]
    public Rigidbody2D Rb { get; set; }
    public GameManager gameManager;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); 
    }
    
    void Start()
    {
        Debug.Log("start");
        EstadoAtual = EstadoAtivo;
        EstadoAtual.Enter();
    }
    
    public void MudarEstado(Estado novoEstado)
    {
        try
        {
            EstadoAtual.Exit();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        EstadoAtual = novoEstado;
        EstadoAtual.Enter();
    }

    public void FixedUpdate()
    {
        EstadoAtual.FixedDo();
    }
}