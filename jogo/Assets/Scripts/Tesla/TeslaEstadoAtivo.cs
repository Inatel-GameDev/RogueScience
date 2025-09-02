
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeslaEstadoAtivo : JogadorAtivo
{
    [SerializeField] private Tesla tesla;
    [Header("Longe")]
    public GameObject AtaqueBasico;
    public float AtaqueBasicoVelocidade = 10f;
    public float AtaqueBasicoCD = 0.7f;
    public bool PodeAtaqueBasico = true;
    [Header("Perto")]
    public GameObject AtaquePerto;
    public float AtaquePertoVelocidade = 1f;
    public float AtaquePertoCD = 1f;
    public bool PodeAtaquePerto = true;
    // barra passiva 
    
    
    public override void OnAttackRanged(InputAction.CallbackContext context)
    {
        if (context.performed) // só dispara quando o botão é pressionado
        {
            if(!PodeAtaqueBasico)
                return;
            PodeAtaqueBasico = false;
            StartCoroutine(CooldownAtaqueBasico());
            Vector2 direcao = (jogador._mouse.transform.position - transform.position).normalized;
            
            float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            
            GameObject proj = Instantiate(AtaqueBasico, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
            
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direcao * AtaqueBasicoVelocidade;
        }
    }
    
    public override void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(!PodeAtaquePerto)
                return;
            PodeAtaquePerto = false;
            StartCoroutine(CooldownAtaquePerto());
            
            if (AtaquePerto.activeSelf)
                return;
            StartCoroutine(AtivarAtaquePerto());
        }
    }

    private IEnumerator AtivarAtaquePerto()
    {
        AtaquePerto.SetActive(true);
        yield return new WaitForSeconds(AtaquePertoVelocidade); // espera o tempo definido
        AtaquePerto.SetActive(false);
    }

    
    // Dash 
    public override void OnAbilityOne(InputAction.CallbackContext context)
    { 
        // cd do dash
         jogador.MudarEstado(tesla.estadoDash);
    }

    // Bobina de Tesla
    public override void OnAbilityTwo(InputAction.CallbackContext context)
    {
        // instanciar o objeto 
        // o objeto precisa dar dano em área e quando tiver 3 fazer um triangulo (controlador?) 
    }
    
    
    
    // Cooldowns com IEnumerator ----------------------------------------------------- 

    public IEnumerator CooldownAtaqueBasico()
    {
        yield return new WaitForSeconds(AtaqueBasicoCD);
        PodeAtaqueBasico = true;
    }
    public IEnumerator CooldownAtaquePerto()
    {
        yield return new WaitForSeconds(AtaquePertoCD);
        PodeAtaquePerto = true;
    }
    
}
