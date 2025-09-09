using UnityEngine;

public class inimigo1 : Inimigo
{
    public float followRadius = 5f; // raio de perseguição
    public float tempoInicial = 10f; // segundos
    private float tempoRestante;
    public GameObject Tiro;

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
                // Debug.Log("Tempo Restante: " + Mathf.Ceil(tempoRestante));
            }
            else
            {
                if (distance < followRadius)
                { 
                    Vector2 direcao = (Target.transform.position - transform.position).normalized;
            
                    float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            
                    GameObject proj = Instantiate(Tiro, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
                    
                    proj.GetComponent<Tiro>().tiroInimigo = true;
            
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = direcao * 15f;
                    
                    Debug.Log("POW");
                }
                tempoRestante = tempoInicial;
            }
        }
    }

}