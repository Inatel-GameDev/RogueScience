using System;
using UnityEngine;

public class AtaquePerto: MonoBehaviour
{
    public float dano = 3;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("Acertou inimigo!");
            other.GetComponent<Inimigo>().PerdeVida(dano);
                
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            Debug.Log(inimigos.Length);
            if (inimigos.Length <= 1)
            {
                GameManager.Instance.FaseFinalizada();
                Debug.Log("Todos os inimigos morreram!");
            }
        }
    }
}
