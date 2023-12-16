using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


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
    public List<Transform> Objectlist;

    public float CurrentTime = 0f;
    public int stage = 0;
    float SpawnCoolTime;
    bool bisGameOver = false;

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
   
}

