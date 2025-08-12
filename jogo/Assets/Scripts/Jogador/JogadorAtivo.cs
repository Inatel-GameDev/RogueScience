using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorAtivo: Estado
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
    
    // Captura o input do WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public override void Exit()
    {
        
    }
}
