using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float lifetime = 2f;
    public float dano = 1;
    public bool tiroInimigo = false;

    void Start()
    {
        Destroy(gameObject, lifetime); // destrói depois de um tempo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tiroInimigo)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Acertou jogador!");
                other.GetComponent<Jogador>().PerdeVida(dano);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Inimigo"))
            {
                Debug.Log("Acertou inimigo!");
                other.GetComponent<Inimigo>().PerdeVida(dano);
                Destroy(gameObject);
            }
        }
    }
}
