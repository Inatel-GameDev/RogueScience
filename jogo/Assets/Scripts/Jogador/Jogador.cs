using System;
using UnityEditor;
using UnityEngine;

// Classe principal, controla qual estado está rodando e possui as informações gerais do jogador
public abstract class Jogador : MonoBehaviour, MaquinaDeEstado
{
    [Header("Atributos")]
    private int _vida;
    public float Velocidade { get; } = 2;
    private float _energia;
    [SerializeField] public float _mouseDistMax;
    [SerializeField] public float _mouseDistMin;
    [SerializeField] public GameObject _mouse;

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
        Cursor.visible = false;
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