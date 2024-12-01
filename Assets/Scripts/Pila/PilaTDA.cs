using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PilaTDA
{
    void InicializarPila();
    void Apilar(int x);
    void Desapilar();
    bool PilaVacia();
    int Tope();
}