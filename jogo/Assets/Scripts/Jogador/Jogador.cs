using System;
using UnityEngine;

public class Jogador : MonoBehaviour, MaquinaDeEstado
{
    private int _vida;
    public float Velocidade { get; } = 10;
    private float _energia;
    
    [Header("Estados")]
    private Estado _estadoAtual;
    [SerializeField] private Estado _estadoAtivo;
    [SerializeField] private Estado _estadoDesativado;

    public Rigidbody2D Rb { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); 
    }
    
    void Start()
    {
        _estadoAtual = _estadoAtivo;
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