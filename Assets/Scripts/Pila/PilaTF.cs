using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaTF : PilaTDA
{
    int[] a;
    int indice;

    public void InicializarPila()
    {
        a = new int[5];
        indice = 0;
    }

    public void Apilar(int x)
    {
        a[indice] = x;
        indice++;
    }

    public void Desapilar()
    {
        indice--;
    }

    public bool PilaVacia()
    {
        return (indice == 0);
    }

    public int Tope()
    {
        return a[indice - 1];
    }
}
