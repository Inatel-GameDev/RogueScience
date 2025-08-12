using UnityEngine;

public abstract class Estado : MonoBehaviour
{
    // No Estado, deve usar essas 3 funções como base para o script de maquina de estado conseguir controlar
    public abstract void Enter();
    
    public abstract void FixedDo();
    
    public abstract void Exit();
}