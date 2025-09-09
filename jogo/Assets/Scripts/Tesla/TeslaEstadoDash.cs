
using System.Collections;
using UnityEngine;

public class TeslaEstadoDash : Estado
{
    [SerializeField] public Jogador jogador;
    [SerializeField] private float velocidadeDash;
    [SerializeField] private float tempoDash;
    private Vector2 direcao;
    [SerializeField]  private Collider2D collider2D;
    public override void Enter()
    {
        collider2D.enabled = true;
        direcao = jogador.getEstadoAtivo().getMoveInput();
        StartCoroutine(TempoDash());
    }

    public override void FixedDo()
    {
        jogador.Rb.MovePosition(jogador.Rb.position + direcao * (velocidadeDash * Time.fixedDeltaTime));
        // movimenta até o final do tempo   
        // final do tempo troca o estado  
    }

    public override void Exit()
    {
        collider2D.enabled = false;
    }

    public IEnumerator TempoDash()
    {
        yield return new WaitForSeconds(tempoDash);
        jogador.MudarEstado(jogador.getEstadoAtivo());
    }
}
