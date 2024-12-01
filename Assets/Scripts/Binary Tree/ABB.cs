using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ABB : MonoBehaviour
{
    public NodoABB raiz;

    [SerializeField] private TreeBoss treeBoss;

    public void InicializarArbol()
    {
        raiz = null;
    }

    public void AgregarElem(ref NodoABB raiz, GameObject prefab)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.prefab = prefab;
        }
        else if (string.Compare(raiz.prefab.name, prefab.name) > 0) 
        {
            AgregarElem(ref raiz.hijoIzq, prefab);
        }
        else if (string.Compare(raiz.prefab.name, prefab.name) < 0)
        {
            AgregarElem(ref raiz.hijoDer, prefab);
        }
    }

    public void EliminarElem(ref NodoABB raiz, GameObject prefab)
    {
        if (raiz != null)
        {
            if (raiz.prefab == prefab && raiz.hijoIzq == null && raiz.hijoDer == null)
            {
                raiz = null;
            }
            else if (raiz.prefab == prefab && raiz.hijoIzq != null)
            {
                raiz.prefab = mayor(raiz.hijoIzq).prefab;
                EliminarElem(ref raiz.hijoIzq, raiz.prefab);
            }
            else if (raiz.prefab == prefab && raiz.hijoIzq == null)
            {
                raiz.prefab = menor(raiz.hijoDer).prefab;
                EliminarElem(ref raiz.hijoDer, raiz.prefab);
            }
            else if (string.Compare(raiz.prefab.name, prefab.name) < 0)
            {
                EliminarElem(ref raiz.hijoDer, prefab);
            }
            else
            {
                EliminarElem(ref raiz.hijoIzq, prefab);
            }
        }
    }

    public NodoABB mayor(NodoABB nodo)
    {
        if (nodo.hijoDer == null)
        {
            return nodo;
        }
        else
        {
            return mayor(nodo.hijoDer);
        }
    }

    public NodoABB menor(NodoABB nodo)
    {
        if (nodo.hijoIzq == null)
        {
            return nodo;
        }
        else
        {
            return menor(nodo.hijoIzq);
        }
    }

    private IEnumerator InstanciarPreOrderCoroutine(NodoABB nodo, Transform[] spawnPoints, float delay, int index)
    {
        if (nodo != null && index < spawnPoints.Length)
        {
            Instantiate(nodo.prefab, spawnPoints[index].position, Quaternion.Euler(0, 180, 0));
            index++;  
            yield return new WaitForSeconds(delay);

            if (nodo.hijoIzq != null)
            {
                yield return StartCoroutine(InstanciarPreOrderCoroutine(nodo.hijoIzq, spawnPoints, delay, index));
            }
            if (nodo.hijoDer != null)
            {
                yield return StartCoroutine(InstanciarPreOrderCoroutine(nodo.hijoDer, spawnPoints, delay, index));
            }
        }
    }

    public void InstanciarPreOrder(NodoABB nodo, Transform[] spawnPoints, float delay)
    {
        StartCoroutine(InstanciarPreOrderCoroutine(nodo, spawnPoints, delay, 0));
    }

    private IEnumerator InstanciarInOrderCoroutine(NodoABB nodo, Transform[] spawnPoints, float delay, int index)
    {
        if (nodo != null && index < spawnPoints.Length)
        {
            if (nodo.hijoIzq != null)
            {
                yield return StartCoroutine(InstanciarInOrderCoroutine(nodo.hijoIzq, spawnPoints, delay, index));
            }

            Instantiate(nodo.prefab, spawnPoints[index].position, Quaternion.Euler(0, 180, 0));
            index++;
            yield return new WaitForSeconds(delay);

            if (nodo.hijoDer != null)
            {
                yield return StartCoroutine(InstanciarInOrderCoroutine(nodo.hijoDer, spawnPoints, delay, index));
            }
        }
    }

    public void InstanciarInOrder(NodoABB nodo, Transform[] spawnPoints, float delay)
    {
        StartCoroutine(InstanciarInOrderCoroutine(nodo, spawnPoints, delay, 0));
    }


    private IEnumerator InstanciarPostOrderCoroutine(NodoABB nodo, Transform[] spawnPoints, float delay, int index)
    {
        if (nodo != null && index < spawnPoints.Length)
        {
            if (nodo.hijoIzq != null)
            {
                yield return StartCoroutine(InstanciarPostOrderCoroutine(nodo.hijoIzq, spawnPoints, delay, index));
            }

            if (nodo.hijoDer != null)
            {
                yield return StartCoroutine(InstanciarPostOrderCoroutine(nodo.hijoDer, spawnPoints, delay, index));
            }
            Instantiate(nodo.prefab, spawnPoints[index].position, Quaternion.Euler(0, 180, 0));
            index++;
            yield return new WaitForSeconds(delay);
        }
    }

    public void InstanciarPostOrder(NodoABB nodo, Transform[] spawnPoints, float delay)
    {
        StartCoroutine(InstanciarPostOrderCoroutine(nodo, spawnPoints, delay, 0));
    }

    private IEnumerator InstanciarLevelOrderCoroutine(NodoABB nodo, Transform[] spawnPoints, float delay, int index)
    {
        if (nodo != null && index < spawnPoints.Length)
        {
            Queue<NodoABB> queue = new Queue<NodoABB>();
            queue.Enqueue(nodo);

            while (queue.Count > 0 && index < spawnPoints.Length)
            {
                NodoABB current = queue.Dequeue();
                
                if (spawnPoints[index] != null)
                {
                    Instantiate(current.prefab, spawnPoints[index].position, Quaternion.Euler(0, 180, 0));
                    treeBoss.PlaySpawnSound();
                }

                index++;
                yield return new WaitForSeconds(delay);

                if (current.hijoIzq != null)
                {
                    queue.Enqueue(current.hijoIzq);
                }
                if (current.hijoDer != null)
                {
                    queue.Enqueue(current.hijoDer);
                }
            }
        }
    }

    public void InstanciarLevelOrder(NodoABB nodo, Transform[] spawnPoints, float delay)
    {
        StartCoroutine(InstanciarLevelOrderCoroutine(nodo, spawnPoints, delay, 0));
    }
}