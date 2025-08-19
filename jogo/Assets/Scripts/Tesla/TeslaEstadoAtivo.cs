
using UnityEngine;
using UnityEngine.InputSystem;

public class TeslaEstadoAtivo : JogadorAtivo
{
    public GameObject AtaqueBasico;
    public float AtaqueBasicoVelocidade = 10f;
    public override void OnAttackRanged(InputAction.CallbackContext context)
    {
        if (context.performed) // só dispara quando o botão é pressionado
        {
            Vector2 direcao = (jogador._mouse.transform.position - transform.position).normalized;
            
            
            float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            
            GameObject proj = Instantiate(AtaqueBasico, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
            
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direcao * AtaqueBasicoVelocidade;
        }
    }

    public override void OnAttackMelee(InputAction.CallbackContext context)
    {
        
    }

    public override void OnAbilityOne(InputAction.CallbackContext context)
    {
        
    }

    public override void OnAbilityTwo(InputAction.CallbackContext context)
    {
        
    }
}
