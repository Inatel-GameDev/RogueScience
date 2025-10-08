using UnityEngine;
using UnityEngine.InputSystem;

public abstract class JogadorAtivo: Estado
{
    [SerializeField] public Jogador jogador;
    private Vector2 _moveInput;
    
    [Header("Dash")]
    public float DashCD = 1f;
    public bool PodeDash = true;
    [Header("Habilidade")]
    public float HabilidadeCD = 4f;
    public bool PodeHabilidade = true;
    [Header("Longe")]
    // todo trocar GameObject para Especifico
    public GameObject AtaqueBasico;
    public float AtaqueBasicoVelocidade = 10f;
    public float AtaqueBasicoCD = 0.7f;
    public bool PodeAtaqueBasico = true;
    [Header("Perto")]
    // todo trocar GameObject para Especifico
    public GameObject AtaquePerto;
    public float AtaquePertoVelocidade = 1f;
    public float AtaquePertoCD = 1f;
    public bool PodeAtaquePerto = true;
    
    
    public override void Enter()
    {
    }

    public override void FixedDo()
    {
        jogador.Rb.MovePosition(jogador.Rb.position + _moveInput * (jogador.Velocidade * Time.fixedDeltaTime));
    }
    
    public override void Exit()
    {
    }
    
    // Captura o input do WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        // context possui a direção com base na tecla apertada (-1,0 ou 1) de cada eixo
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnPause()
    {
        if (!jogador.gameManager.isPaused)
        {
            jogador.gameManager.Pause();
        }
        else
        {
            jogador.gameManager.Resume();
        }
    }

    public abstract void OnAttackRanged(InputAction.CallbackContext context);
    public abstract void OnAttackMelee(InputAction.CallbackContext context);
    public abstract void OnDash(InputAction.CallbackContext context);
    public abstract void OnAbility(InputAction.CallbackContext context);

    public Vector2 getMoveInput()
    {
        return  _moveInput;
    }
}
