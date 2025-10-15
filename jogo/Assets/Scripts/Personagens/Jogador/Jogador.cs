using System;
using UnityEngine;
using UnityEngine.UI;

// Classe principal, controla qual estado está rodando e possui as informações gerais do jogador
public abstract class Jogador : MonoBehaviour, MaquinaDeEstado
{
    [Header("Atributos")]
    [SerializeField] private float vidaMax = 100;
    [SerializeField] private float _vida;
    [SerializeField] private Image lifeImage;
    public float Velocidade { get; set; } = 6;
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
        _vida = vidaMax;
    }

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            PerdeVida(100);
        }
    }

    public void PerdeVida(float dano)
    {
        // todo pegar esse audio do jogador ativo, e setar ele no jogadorAtivoEspecifico
        AudioManager.Instance.PlaySound(AudioLibrary.Instance.teslaSomMachucado);
        _vida -= dano;
        LifeUI();
        if (_vida <= 0)
        {
            Debug.Log("Morri");
            gameManager.Respawn(this);
        }
    }

    public void Reviver()
    {
        _vida = vidaMax;
        LifeUI();
    }

    public JogadorAtivo getEstadoAtivo()
    {
        return (JogadorAtivo)EstadoAtivo;
    }
    public JogadorDesativado getEstadoDesativado()
    {
        return (JogadorDesativado)estadoDesativado;
    }
    
    public void LifeUI()
    {
        lifeImage.fillAmount = _vida / vidaMax;
    }
}