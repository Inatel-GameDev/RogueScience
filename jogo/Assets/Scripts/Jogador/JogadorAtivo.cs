using UnityEngine;
using UnityEngine.InputSystem;

public abstract class JogadorAtivo: Estado
{
    [SerializeField] private Jogador jogador;
    private Vector2 _moveInput;
    
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

    public abstract void OnAttackRanged(InputAction.CallbackContext context);
    public abstract void OnAttackMelee(InputAction.CallbackContext context);
    public abstract void OnAbilityOne(InputAction.CallbackContext context);
    public abstract void OnAbilityTwo(InputAction.CallbackContext context);
}
