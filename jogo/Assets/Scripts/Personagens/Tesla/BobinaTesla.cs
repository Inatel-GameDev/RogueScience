using System;
using System.Collections;
using UnityEngine;

public class BobinaTesla : MonoBehaviour
{
    [SerializeField] GameObject bobina;

    private void Start()
    {
        StartCoroutine(AtivarDano());
        StartCoroutine(Destroi());
    }

    private IEnumerator AtivarDano()
    {
        bobina.SetActive(true);
        yield return new WaitForSeconds(0.5f); // espera o tempo definido
        StartCoroutine(DesativarDano());
    }
    
    private IEnumerator DesativarDano()
    {
        bobina.SetActive(false);
        yield return new WaitForSeconds(1f); // espera o tempo definido
        StartCoroutine(AtivarDano());
    }
    
    private IEnumerator Destroi()
    {
        yield return new WaitForSeconds(13f); // espera o tempo definido
        Destroy(this.gameObject);
    }
}
