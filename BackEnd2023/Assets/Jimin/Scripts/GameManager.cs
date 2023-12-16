using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

//[Serializable]
//public class SpawnTime
//{
//    public float FirstMax;
//    public float FirstMin;
//    public float SecondMax;
//    public float SecondMin;
//    public float ThirdMax;
//    public float ThirdMin;
//    public float FourthMax;
//    public float FourthMin;
//    public float FifthMax;
//    public float FifthMin;
//}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    GameObject BulletParent;

    [SerializeField]
    ObjectManager objectManager;

    [SerializeField]
    ScriptableManager scriptableManager;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    float SpawnTime;

    [SerializeField]
    public List<Transform> Objectlist;

    //[SerializeField]
    //public SpawnTime[] MonsterList;

    float CurrentTime = 0f;
    float SpawnCoolTime;
    bool bisGameOver = false;

    int stage = 1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        scriptableManager.Initalize();
        objectManager.Initalize();
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        SpawnCoolTime += Time.deltaTime;
        uiManager.text.text = Mathf.Round(CurrentTime).ToString();
    }

    public void AddtoTransformlist(Transform transform)
    {
        Objectlist.Add(transform);
    }

    public void DeleteTransformlist(Transform transform)
    {
        if(Objectlist.Contains(transform))
        {
            for(int i = 0; i < Objectlist.Count; i++)
            {
                if(Objectlist[i] == transform)
                {
                    Objectlist.Remove(Objectlist[i]);
                }
            }
        }
    }

    public Transform ReturnClosesetObject(Transform _transform)
    {
        if (Objectlist.Count > 0)
        {
            int index = 0;
            float distance = Vector2.Distance(Objectlist[0].transform.position, _transform.position);

            for (int i = 1; i < Objectlist.Count; i++)
            {
                if (Vector2.Distance(Objectlist[i].transform.position, _transform.position) < distance)
                {
                    index = i;
                }
            }

            return Objectlist[index];
        }
        return null;
    }

    //public float SetSpawnTime(GameObject monster)
    //{
    //    switch(stage)
    //    {
    //        case 1:
    //            return Random.Range(monster.SpawnTime.FirstMin, monster.SpawnTime.FirstMax);
    //            break;

    //        case 2:
    //            return Random.Range(monster.SpawnTime.SecondMin, monster.SpawnTime.SecondMax);
    //            break;

    //        case 3:
    //            return Random.Range(monster.SpawnTime.ThridMin, monster.SpawnTime.ThirdMax);
    //            break;

    //        case 4:
    //            return Random.Range(monster.SpawnTime.FourthMin, monster.SpawnTime.FourthMax);
    //            break;

    //        case 5:
    //            return Random.Range(monster.SpawnTime.fifthMin, monster.SpawnTime.fifthMax);
    //            break;
    //    }
    //}
   
}

