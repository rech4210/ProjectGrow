using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum MonsterKind
{
    Gorani,
    Wildboar
}

[Serializable]
public class SpawnRoundInfo
{
    public float roundtimer;
    public SpawnMonsterInfo[] monsterinfo;
}

[Serializable]
public class SpawnMonsterInfo
{
    public MonsterKind monster;

    public Vector2 time;
    public Vector2 count;
}

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    public Zombies zombies;

    [SerializeField]
    public Wildboar wildboar;

    [SerializeField]
    public SpawnRoundInfo[] MonsterList;

    [SerializeField]
    List<Vector2> RandomVector;


    ObjectPooling<Zombies> zombiePool = new ObjectPooling<Zombies>();
    ObjectPooling<Wildboar> wildboarPool = new ObjectPooling<Wildboar>();

    bool bisPlaying;

    SpawnRoundInfo roundInfo => MonsterList[GameManager.instance.stage];


    Dictionary<MonsterKind, float> spawndelay = new Dictionary<MonsterKind, float>();

    public void Initalize()
    {
        zombiePool.Initialize(zombies, this.gameObject.transform, 10);
        wildboarPool.Initialize(wildboar, this.gameObject.transform, 10);
        List<ScriptableMonsterInfo.PrefabInfo> mobList = ScriptableManager.instance.getTable(ScriptableManager.MobTableTag).Prefablist<ScriptableMonsterInfo.PrefabInfo>();

        for (int i = 0; i < mobList.Count; i++)
        {
            spawndelay[mobList[i].Name] = 0f;
        }

        StartSpawn();
    }

    public void MakeMob(MonsterKind mob)
    {
        switch (mob)
        {
            case MonsterKind.Gorani:
                MakeZombie();
                break;

            case MonsterKind.Wildboar:
                MakeWildboar();
                break;
        }
    }

    public void MakeZombie()
    {
        Zombies newzombie = zombiePool.GetObject(zombies);
        newzombie.transform.position = RandomVector[Random.Range(0, RandomVector.Count)];
    }

    public void MakeWildboar()
    {
        Wildboar newwildboar = wildboarPool.GetObject(wildboar);
        newwildboar.transform.position = RandomVector[Random.Range(0, RandomVector.Count)];
    }

    public void Update()
    {
        if (bisPlaying)
        {
            if (roundInfo.roundtimer > GameManager.instance.CurrentTime)
            {
                GameManager.instance.stage++;
                if (MonsterList.Length <= GameManager.instance.stage)
                {
                    //클리어
                    bisPlaying = false;
                    return;
                }
            }

            for (int i = 0; i < roundInfo.monsterinfo.Length; i++)
            {
                if (spawndelay.ContainsKey(roundInfo.monsterinfo[i].monster))
                {
                    spawndelay[roundInfo.monsterinfo[i].monster] -= Time.deltaTime;
                    if (spawndelay[roundInfo.monsterinfo[i].monster] <= 0)
                    {
                        spawndelay[roundInfo.monsterinfo[i].monster] = Random.Range(roundInfo.monsterinfo[i].time.x, roundInfo.monsterinfo[i].time.y);
                        MakeMob(roundInfo.monsterinfo[i].monster);
                    }
                }
            }
        }
    }

    public void StartSpawn()
    {
        bisPlaying = true;
    }

}
