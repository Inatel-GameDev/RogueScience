using UnityEngine;

public class petdefault : MonoBehaviour
{
    public GameObject Target;
    public float speed = 5f;
    public float followRadius = 5f;
    public SpriteRenderer sR;

    void Update()
    {
        if (Target == null) return; // segurança caso o Target não esteja definido

        Vector2 petPos = transform.position;
        Vector2 targetPos = Target.transform.position;

        float distance = Vector2.Distance(petPos, targetPos);
        
        if (targetPos.x > petPos.x)
        {
            sR.flipX = true; // olha pra direita
        }
        else if (targetPos.x < petPos.x)
        {
            sR.flipX = false; // olha pra esquerda
        }

        // Só anda se estiver fora do raio
        if (distance > followRadius)
        {
            transform.position = Vector2.MoveTowards(petPos, targetPos, speed * Time.deltaTime);
        }
    }
}