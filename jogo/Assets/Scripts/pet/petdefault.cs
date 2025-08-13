using UnityEngine;

public class petdefault : MonoBehaviour
{
    public GameObject Target;
    //tentando nao collidir com o player
    public LayerMask playerLayer;
    public LayerMask petLayer; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //tentando nao collidir com o player
        Physics.IgnoreLayerCollision(playerLayer.value, petLayer.value, true);
        // 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 PetPos = new Vector2(Target.transform.position.x +3 , Target.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, PetPos, 5*Time.deltaTime);
    }
}
