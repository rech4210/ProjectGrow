using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum MonsterKind
{
    Gorani,
    Wildboar,
    Player,
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
    public RootCtrl zombies;

    [SerializeField]
    public RootCtrl wildboar;

    [SerializeField]
    public SpawnRoundInfo[] MonsterList;

    [SerializeField]
    List<Transform> RandomVector;


    ObjectPooling<RootCtrl> zombiePool = new ObjectPooling<RootCtrl>();
    ObjectPooling<RootCtrl> wildboarPool = new ObjectPooling<RootCtrl>();

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

        Invoke("StartSpawn", 30f);
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
        RootCtrl newzombie = zombiePool.GetObject(zombies);
        newzombie.transform.position = RandomVector[Random.Range(0, RandomVector.Count)].position;
        newzombie.lifeAction?.Invoke();
    }

    public void MakeWildboar()
    {
        RootCtrl newwildboar = wildboarPool.GetObject(wildboar);
        newwildboar.transform.position = RandomVector[Random.Range(0, RandomVector.Count)].position;
        newwildboar.lifeAction?.Invoke();
    }

    public void Update()
    {
        if (bisPlaying)
        {
            if (roundInfo.roundtimer < GameManager.instance.CurrentTime)
            {
                if (MonsterList.Length > GameManager.instance.stage +1)
                {
                    GameManager.instance.stage++;
                }
            }

            for (int i = 0; i < roundInfo.monsterinfo.Length; i++)
            {
                if (spawndelay.ContainsKey(roundInfo.monsterinfo[i].monster))
                {
                    spawndelay[roundInfo.monsterinfo[i].monster] -= Time.deltaTime;
                    if (spawndelay[roundInfo.monsterinfo[i].monster] <= 0)
                    {
                        int count = (int)Random.Range(roundInfo.monsterinfo[i].count.x, roundInfo.monsterinfo[i].count.x);
                        for (int j = 0; j < count; j++)
                        {
                            MakeMob(roundInfo.monsterinfo[i].monster);
                            spawndelay[roundInfo.monsterinfo[i].monster] = Random.Range(roundInfo.monsterinfo[i].time.x, roundInfo.monsterinfo[i].time.y);
                        }
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
