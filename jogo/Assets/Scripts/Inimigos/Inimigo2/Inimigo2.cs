using UnityEngine;

public class Inimigo2 : MonoBehaviour
{
    public GameObject Target;
    public float speed = 5f;
    public float visionRadius = 8f; // raio de visão (para "acordar")
    public SpriteRenderer sR;

    private bool alerta = false; // estado inicial: desavisado

    void Start()
    {

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
          
            transform.position = Vector2.MoveTowards(iniPos, targetPos, speed * Time.deltaTime);
            
        }
    }
}