
using System;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
 
    public GameObject Target;
    public float vida; 
    public float speed = 5f;
    public float visionRadius = 8f; // raio de visão (para "acordar")
    public SpriteRenderer sR;
    
    public void PerdeVida(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Jogador>().PerdeVida(50);
            // knockback 
        }
    }
}
