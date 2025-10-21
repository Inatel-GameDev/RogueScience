
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TeslaEstadoAtivo : JogadorAtivo
{
    [SerializeField] private Tesla tesla;
    [Header("Bobina")]
    [SerializeField] private GameObject Bobina;
    [Header("Passiva")]
    public Image barraPreenchimento; // referência para a Image "Fill"
    public float tempoMaximo = 10f;   // tempo total para encher
    private float tempoAtual = 0f;

    [Header("UI")]

    [SerializeField] private Image dashCooldownImage;
    [SerializeField] private Image ataquePertoCooldownImage;
    [SerializeField] private Image habilidadeCooldownImage;

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
            if (!PodeAtaqueBasico)
                return;
            AudioManager.Instance.PlaySound(AudioLibrary.Instance.teslaSomTiro);
            PodeAtaqueBasico = false;
            StartCoroutine(CooldownAtaqueBasico());
            Vector2 direcao = (jogador._mouse.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

            GameObject proj = Instantiate(AtaqueBasico, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direcao * AtaqueBasicoVelocidade;
            consumirBarra(5f);
        }
    }

    public override void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!PodeAtaquePerto)
                return;
            AudioManager.Instance.PlaySound(AudioLibrary.Instance.teslaSomAtaque);
            PodeAtaquePerto = false;
            consumirBarra(5f);
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
    public override void OnDash(InputAction.CallbackContext context)
    {
        if (PodeDash)
        {
            AudioManager.Instance.PlaySound(AudioLibrary.Instance.teslaSomDash);
            jogador.MudarEstado(tesla.estadoDash);
            PodeDash = false;
            consumirBarra(5f);
            StartCoroutine(CooldownDash());
        }
    }

    // Bobina de Tesla
    public override void OnAbility(InputAction.CallbackContext context)
    {
        if (PodeHabilidade)
        {
            Instantiate(Bobina, transform.position, Quaternion.identity);
            PodeHabilidade = false;
            consumirBarra(5f);
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
        StartCoroutine(UIAbilityCD(ataquePertoCooldownImage, AtaquePertoCD));
        yield return new WaitForSeconds(AtaquePertoCD);
        PodeAtaquePerto = true;
        tesla.Velocidade = 6;
    }

    public IEnumerator CooldownDash()
    {
        StartCoroutine(UIAbilityCD(dashCooldownImage, DashCD));
        yield return new WaitForSeconds(DashCD);
        PodeDash = true;
    }
    public IEnumerator CooldownBobina()
    {
        StartCoroutine(UIAbilityCD(habilidadeCooldownImage, HabilidadeCD));
        yield return new WaitForSeconds(HabilidadeCD);
        PodeHabilidade = true;
    }
    

}
