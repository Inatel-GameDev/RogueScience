using System;
using UnityEngine;

// Classe principal, controla qual estado está rodando e possui as informações gerais do jogador
public class Jogador : MonoBehaviour, MaquinaDeEstado
{
    [Header("Atributos")]
    private int _vida;
    public float Velocidade { get; } = 10;
    private float _energia;
    
    
    [Header("Estados")]
    private Estado _estadoAtual;
    [SerializeField] private Estado estadoAtivo;
    [SerializeField] private Estado estadoDesativado;

    [Header("Componentes")]
    public Rigidbody2D Rb { get; private set; }

    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); 
    }
    
    void Start()
    {
        _estadoAtual = estadoAtivo;
        _estadoAtual.Enter();
    }

    public void MudarEstado(Estado novoEstado)
    {
        try
        {
            _estadoAtual.Exit();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        _estadoAtual = novoEstado;
        _estadoAtual.Enter();
    }

    public void FixedUpdate()
    {
        _estadoAtual.FixedDo();
    }
}