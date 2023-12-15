using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface I_Pool
{
    void SetPoolEvent(Action<I_Pool> poolevent);
}

public class ObjectPooling<T> where T : Component, I_Pool
{
    Transform Parent;
    Queue<T> itemPool = new Queue<T>();

    public void Initialize(T item, Transform Parent, int Count)
    {
        this.Parent = Parent;
        for (int i = 0; i < Count; i++)
        {
            itemPool.Enqueue(CreateNewObject(item));
        }
    }

    private T CreateNewObject(T obj)
    {
        var newObj = GameObject.Instantiate(obj);
        newObj.GetComponent<I_Pool>().SetPoolEvent((item) => {
            itemPool.Enqueue(newObj);
            newObj.gameObject.SetActive(false);
        });
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(Parent);
        return newObj;
    }

    public T GetObject(T prefab)
    {
        T newObj = null;
        if (itemPool.Count > 0)
        {
            newObj = itemPool.Dequeue();
        }
        else
        {
            newObj = CreateNewObject(prefab);
        }

        newObj.gameObject.SetActive(true);
        return newObj;
    }
}
