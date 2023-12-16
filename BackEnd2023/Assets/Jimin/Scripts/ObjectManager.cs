using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    public Zombies zombies;

    //[SerializeField]
    //public items items;

    [SerializeField]
    List<Vector2> RandomVector;

    ObjectPooling<Zombies> zombiePool = new ObjectPooling<Zombies>();
    //ObjectPooling<items> itemPool = new ObjectPooling<items>;

    public void Initalize()
    {
        zombiePool.Initialize(zombies, this.gameObject.transform, 10);
        //itemPool.Initalize(items, this.gameObject.transform, 10);
    }

    public void MakeZombie()
    {
        Zombies newzombie = zombiePool.GetObject(zombies);
        newzombie.transform.position = RandomVector[Random.Range(0, RandomVector.Count)];
    }

    protected void Makeitem()
    {
        
    }
}
