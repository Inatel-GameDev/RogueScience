using UnityEngine;

public class inimigo1 : MonoBehaviour
{
    public GameObject Target;
    public float speed = 5f;
    public float followRadius = 5f; // raio de perseguição
    public float visionRadius = 8f; // raio de visão (para "acordar")
    public SpriteRenderer sR;

    public float tempoInicial = 10f; // segundos
    private float tempoRestante;

    private bool alerta = false; // estado inicial: desavisado

    void Start()
    {
        tempoRestante = tempoInicial;
    }

    void Update()
    {
        if (Target == null) return;

        Vector2 iniPos = transform.position;
        Vector2 targetPos = Target.transform.position;

        float distance = Vector2.Distance(iniPos, targetPos);

        // Se o jogador entrar no campo de visão, o inimigo "acorda"
        if (!alerta && distance <= visionRadius)
        {
            alerta = true;
            Debug.Log("Inimigo avistou o jogador!");
        }

        if (alerta) // Só roda a lógica quando o inimigo está alerta
        {
            // Flip de sprite
            if (targetPos.x > iniPos.x)
                sR.flipX = true;  // olha pra direita
            else if (targetPos.x < iniPos.x)
                sR.flipX = false; // olha pra esquerda

            // Movimento se estiver fora do raio mínimo
            if (distance > followRadius)
            {
                transform.position = Vector2.MoveTowards(iniPos, targetPos, speed * Time.deltaTime);
            }

            // Contagem regressiva
            if (tempoRestante > 0)
            {
                tempoRestante -= Time.deltaTime;
                Debug.Log("Tempo Restante: " + Mathf.Ceil(tempoRestante));
            }
            else
            {
                if (distance < followRadius)
                { 
                    Debug.Log("POW");
                }
                tempoRestante = tempoInicial;
            }
        }
    }
}