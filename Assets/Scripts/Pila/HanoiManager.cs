using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HanoiManager : MonoBehaviour
{
    public GameObject[] torres;
    public TorrePosiciones[] posicionesTorres;
    private PilaTDA[] pilas;
    [SerializeField] private GameObject mountain;

    private int torreOrigenSeleccionada = -1;

    public event Action OnVictory;

    private void Start()
    {
        pilas = new PilaTDA[3];
        for (int i = 0; i < 3; i++)
        {
            pilas[i] = new PilaTF();
            pilas[i].InicializarPila();
        }
        
        for (int i = 5; i > 0; i--)
        {
            pilas[0].Apilar(i);
        }
        
        MostrarEstadoTorres();
        SeleccionarTorreOrigen();
    }

    private void MostrarEstadoTorres()
    {
        Debug.Log("Estado de las torres:");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log($"Torre {i + 1}: {ObtenerEstadoTorre(pilas[i])}");
        }
    }

    private string ObtenerEstadoTorre(PilaTDA pila)
    {
        return pila.PilaVacia() ? "Vacía" : pila.Tope().ToString();
    }

    private void SeleccionarTorreOrigen()
    {
        Debug.Log("Seleccione la torre de la cual desea mover un disco (1, 2, 3):");
    }

    private void SeleccionarTorreDestino(int origen)
    {
        Debug.Log($"Torre {origen + 1} seleccionada. ¿A qué torre desea mover el disco? (1, 2, 3):");
    }

    private void MoverDisco(int origen, int destino)
    {
        PilaTDA torreOrigen = pilas[origen];
        PilaTDA torreDestino = pilas[destino];

        if (torreDestino.PilaVacia() || torreOrigen.Tope() < torreDestino.Tope())
        {
            int disco = torreOrigen.Tope();
            torreOrigen.Desapilar();
            torreDestino.Apilar(disco);
            
            Transform discoTransform = torres[origen].transform.GetChild(torres[origen].transform.childCount - 1);
            
            discoTransform.SetParent(torres[destino].transform, true);
            
            ActualizarPosicionesDiscos(destino);

            UnityEngine.Debug.Log($"Movido disco de Torre {origen + 1} a Torre {destino + 1}");
            
            VerificarVictoria();
        }
        else
        {
            UnityEngine.Debug.Log("No se puede mover un disco mayor sobre uno menor.");
        }

        MostrarEstadoTorres();
        SeleccionarTorreOrigen();
    }

    private void ActualizarPosicionesDiscos(int torre)
    {
        PilaTDA pila = pilas[torre];
        
        for (int i = 0; i < torres[torre].transform.childCount; i++)
        {
            Transform discoTransform = torres[torre].transform.GetChild(i);
            
            Transform posicionDestino = posicionesTorres[torre].posiciones[i];
            Vector3 posicionDestinoGlobal = posicionDestino.position;
            
            Vector3 posicionDestinoLocal = torres[torre].transform.InverseTransformPoint(posicionDestinoGlobal);
            
            discoTransform.localPosition = posicionDestinoLocal;
        }
    }

    private void VerificarVictoria()
    {
        PilaTDA torre3 = pilas[2];
        
        if (torre3.PilaVacia() || torre3.Tope() != 1)
        {
            return;
        }

        PilaTF torreTemporal = new PilaTF();
        torreTemporal.InicializarPila();

        bool esVictoria = true;
        for (int i = 1; i <= 3; i++)
        {
            if (torre3.PilaVacia() || torre3.Tope() != i)
            {
                esVictoria = false;
                break;
            }
            torreTemporal.Apilar(torre3.Tope());
            torre3.Desapilar();
        }
        
        while (!torreTemporal.PilaVacia())
        {
            torre3.Apilar(torreTemporal.Tope());
            torreTemporal.Desapilar();
        }

        if (esVictoria)
        {
            Destroy(mountain);
            OnVictory?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ManejarInput(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ManejarInput(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ManejarInput(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResolverPuzzle(3, 0, 2, 1);
        }
    }

    private void ManejarInput(int seleccion)
    {
        seleccion -= 1;

        if (torreOrigenSeleccionada == -1)
        {
            if (pilas[seleccion].PilaVacia())
            {
                Debug.Log("La torre seleccionada está vacía. Seleccione otra torre.");
                SeleccionarTorreOrigen();
            }
            else
            {
                torreOrigenSeleccionada = seleccion;
                SeleccionarTorreDestino(seleccion);
            }
        }
        else
        {
            MoverDisco(torreOrigenSeleccionada, seleccion);
            torreOrigenSeleccionada = -1;
        }
    }

    private void ResolverPuzzle(int n, int origen, int destino, int auxiliar)
    {
        if (n <= 0)
            return;
        
        ResolverPuzzle(n - 1, origen, auxiliar, destino);
        
        MoverDisco(origen, destino);
        
        ResolverPuzzle(n - 1, auxiliar, destino, origen);
    }
}