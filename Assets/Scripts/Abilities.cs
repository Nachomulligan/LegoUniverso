using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HabilidadBase
{
    public int Modifier;              // Modificador de la habilidad
    public string nombre;             // Nombre de la habilidad
    public bool Unlock;
    public bool Blocked;
    public List<HabilidadBase> requieredAbilities;  // Habilidades requeridas para desbloquear

    public HabilidadBase(string nombre, int modifier = 5)
    {
        this.nombre = nombre;
        this.Modifier = modifier;
        this.Unlock = false;
        this.Blocked = false;  // Inicialmente, ninguna habilidad está bloqueada
        this.requieredAbilities = new List<HabilidadBase>();
    }

    public bool Desbloquear()
    {
        // Si la habilidad está bloqueada, no se puede desbloquear
        if (Blocked)
        {
            Debug.Log("No puedes desbloquear " + nombre + ". La rama está bloqueada.");
            return false;
        }

        // Verificar que las habilidades requeridas estén desbloqueadas
        foreach (HabilidadBase habilidad in requieredAbilities)
        {
            if (!habilidad.Unlock)
            {
                Debug.Log("No puedes desbloquear " + nombre + ". Faltan habilidades requeridas.");
                return false;
            }
        }

        Unlock = true;
        Debug.Log("Habilidad " + nombre + " desbloqueada!");
        return true;
    }

    // Método para bloquear la habilidad
    public void Bloquear()
    {
        Blocked = true;
        Debug.Log("Habilidad " + nombre + " está bloqueada por elegir otra rama.");
    }
}

public class LifeUp : HabilidadBase
{
    public LifeUp(string nombre, int modifier = 5) : base(nombre, modifier)
    {
        // LifeUp puede tener características adicionales si es necesario.
    }
}


public class SpeedUp : HabilidadBase
{
    public SpeedUp(string nombre, int modifier = 5) : base(nombre, modifier)
    {
        // SpeedUp puede tener características adicionales si es necesario.
    }
}


public class Defense : HabilidadBase
{
    public Defense(string nombre, int modifier = 5) : base(nombre, modifier)
    {
        // SpeedUp puede tener características adicionales si es necesario.
    }
}


public class HyperJump : HabilidadBase
{
    public HyperJump(string nombre, int modifier = 5) : base(nombre, modifier)
    {
        // SpeedUp puede tener características adicionales si es necesario.
    }
}

