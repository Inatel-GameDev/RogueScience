using System;
using UnityEngine;

// Interface maquinas de estado, deve ser implementado pelo jogador e todos os inimigos
public interface MaquinaDeEstado
{
    public abstract void MudarEstado(Estado novoEstado);
    
    public void FixedUpdate();
    
}