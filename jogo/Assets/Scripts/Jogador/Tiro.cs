using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime); // destrói depois de um tempo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("Acertou inimigo!");
            // todo dano no inimigo
            Destroy(gameObject);
        }
    }
}
