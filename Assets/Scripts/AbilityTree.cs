using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTree : MonoBehaviour
{
    private LifeUp vidaExtra;
    private SpeedUp velocidadExtra;
    private Defense defensaExtra;
    private HyperJump megaSalto;

    public List<HabilidadBase> habilidades;

    private bool isFirstBranchSelected = false;  // Variable para determinar si una rama ha sido seleccionada

    void Start()
    {
        // Crear habilidades
        vidaExtra = new LifeUp("Vida Extra", 10);
        velocidadExtra = new SpeedUp("Velocidad Extra", 5);
        defensaExtra = new Defense("Defensa Aplicada", 80);
        megaSalto = new HyperJump("Salto Extra", 50);

        // Definir dependencias
        defensaExtra.requieredAbilities.Add(vidaExtra);   // Defensa depende de Vida Extra
        megaSalto.requieredAbilities.Add(velocidadExtra); // Mega Salto depende de Velocidad Extra

        // Inicializar la lista de habilidades y agregar todas las habilidades
        habilidades = new List<HabilidadBase> { vidaExtra, velocidadExtra, defensaExtra, megaSalto };

        // Desbloquear habilidades iniciales
        vidaExtra.Desbloquear();   // Vida Extra se desbloquea directamente
    }

    void Update()
    {
        // Comprobar si el jugador presiona el botón "Debug Next"
        if (Input.GetButtonDown("Debug Next"))
        {
            // Intentar desbloquear la rama de velocidad
            if (!isFirstBranchSelected)
            {
                // Desbloquear la primera habilidad de la rama
                if (!velocidadExtra.Unlock)
                {
                    velocidadExtra.Desbloquear();
                    BloquearOtraRama(vidaExtra);  // Bloquear la rama de vida al seleccionar velocidad
                }

                // Intentar desbloquear Mega Salto
                if (velocidadExtra.Unlock && !megaSalto.Unlock)
                {
                    megaSalto.Desbloquear();
                }
            }
        }

        // Comprobar si el jugador presiona el botón "Debug Previous"
        if (Input.GetButtonDown("Debug Previous"))
        {
            // Intentar desbloquear la rama de vida
            if (!isFirstBranchSelected)
            {
                // Desbloquear la primera habilidad de la rama
                if (!vidaExtra.Unlock)
                {
                    vidaExtra.Desbloquear();
                    BloquearOtraRama(velocidadExtra);  // Bloquear la rama de velocidad al seleccionar vida
                }

                // Intentar desbloquear Defensa
                if (vidaExtra.Unlock && !defensaExtra.Unlock)
                {
                    defensaExtra.Desbloquear();
                }
            }
        }
    }

    // Método para bloquear la otra rama de habilidades
    private void BloquearOtraRama(HabilidadBase habilidadInicialRamaOpuesta)
    {
        // Marcar que una rama ha sido seleccionada
        isFirstBranchSelected = true;

        // Bloquear la otra rama
        foreach (HabilidadBase habilidad in habilidades)
        {
            if (!habilidad.requieredAbilities.Contains(habilidadInicialRamaOpuesta) && !habilidad.Unlock)
            {
                habilidad.Bloquear();
            }
        }
    }

    // Método para intentar desbloquear una habilidad por nombre
    public void IntentarDesbloquear(string nombreHabilidad)
    {
        HabilidadBase habilidad = habilidades.Find(h => h.nombre == nombreHabilidad);
        if (habilidad != null)
        {
            habilidad.Desbloquear();
        }
    }
}