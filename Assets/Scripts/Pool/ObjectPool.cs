using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private Stack<T> pool = new Stack<T>();

    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
    }

    public T GetFromPool(Vector3 position, Quaternion rotation)
    {
        T item;
        if (pool.Count > 0)
        {
            item = pool.Pop();
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.gameObject.SetActive(true);
        }
        else
        {
            item = Object.Instantiate(prefab, position, rotation);
        }

        return item;
    }

    public void ReturnToPool(T item)
    {
        item.gameObject.SetActive(false);
        pool.Push(item);
    }
}