using System;
using UnityEngine;

public interface MaquinaDeEstado
{
    
    public abstract void MudarEstado(Estado novoEstado);
    
    public void FixedUpdate();
    
}