
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TeslaEstadoAtivo : JogadorAtivo
{
    [SerializeField] private Tesla tesla;
    [Header("Dash")]
    public float DashCD = 1f;
    public bool PodeDash = true;
    [Header("Bobina")]
    public GameObject Bobina;
    public float BobinaCD = 4f;
    public bool PodeBobina = true;
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
    [Header("Perto")]
    public Image barraPreenchimento; // referência para a Image "Fill"
    public float tempoMaximo = 10f;   // tempo total para encher
    private float tempoAtual = 0f;


    public override void FixedDo()
    {
      base.FixedDo();
      
      // Incrementa o tempo
      tempoAtual += Time.deltaTime;
      // Calcula porcentagem de preenchimento (0 → 1)
      float porcentagem = Mathf.Clamp01(tempoAtual / tempoMaximo);
      // Atualiza a barra
      barraPreenchimento.fillAmount = porcentagem;
    }

    public float consumirBarra(float quantidade)
    {
        return tempoAtual -= quantidade;
    }
    

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
            consumirBarra(1.5f);
        }
    }
    
    public override void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(!PodeAtaquePerto)
                return;
            PodeAtaquePerto = false;
            tesla.Velocidade = 3;
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
        if (PodeDash)
        {
            jogador.MudarEstado(tesla.estadoDash);
            PodeDash = false;
            StartCoroutine(CooldownDash());
        }
    }

    // Bobina de Tesla
    public override void OnAbilityTwo(InputAction.CallbackContext context)
    {
        if (PodeBobina)
        {
            Instantiate(Bobina, transform.position, Quaternion.identity);
            PodeBobina = false;
            StartCoroutine(CooldownBobina());
        }
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
        tesla.Velocidade = 6;
    }
    
    public IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(DashCD);
        PodeDash = true;
    }
    public IEnumerator CooldownBobina()
    {
        yield return new WaitForSeconds(BobinaCD);
        PodeBobina = true;
    }
    
}
