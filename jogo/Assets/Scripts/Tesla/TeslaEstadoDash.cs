
using System.Collections;
using UnityEngine;

public class TeslaEstadoDash : Estado
{
    [SerializeField] public Jogador jogador;
    [SerializeField] private float velocidadeDash;
    [SerializeField] private float tempoDash;
    private Vector2 direcao;
    public override void Enter()
    {
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
        
    }

    public IEnumerator TempoDash()
    {
        yield return new WaitForSeconds(tempoDash);
        jogador.MudarEstado(jogador.getEstadoAtivo());
    }
}
