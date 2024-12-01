using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaEnlazada : ColaTDA
{
    private Nodo raiz;

    public ColaEnlazada()
    {
        InicializarCola();
    }

    public void InicializarCola()
    {
        raiz = null;
    }

    public void Acolar(GameObject x)
    {
        if (raiz == null)
        {
            raiz = new Nodo();
            raiz.datos = x;
            raiz.siguiente = null;
        }
        else
        {
            Nodo actual = raiz;
            while (actual.siguiente != null)
            {
                actual = actual.siguiente;
            }
            Nodo nuevo = new Nodo();
            nuevo.datos = x;
            nuevo.siguiente = null;

            actual.siguiente = nuevo;
        }
    }

    public void Desacolar()
    {
        if (raiz != null)
        {
            raiz = raiz.siguiente;
        }
    }

    public bool ColaVacia()
    {
        return raiz == null;
    }

    public GameObject Primero()
    {
        if (raiz != null)
        {
            return raiz.datos;
        }
        return null;
    }
}