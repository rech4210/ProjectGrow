using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    private static Transform _poolParent;
    public static Transform poolParent
    {
        get
        {
            if (_poolParent == null)
            {
                if (instance == null)
                {
                    _poolParent = new GameObject().transform;
                }
                else
                {
                    _poolParent = instance.transform;
                }
            }
            return _poolParent;
        }
    }
    public static GameManager instance = null;

    [SerializeField]
    GameObject BulletParent;

    [SerializeField]
    ObjectManager objectManager;

    [SerializeField]
    ScriptableManager scriptableManager;

    [SerializeField]
    UIManager uiManager;

    public List<I_Faction> Objectlist = new List<I_Faction>();

    public float CurrentTime = 0f;
    public int stage = 0;
    float SpawnCoolTime;
    bool bisGameOver = false;
    public Action startCall;

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

        if (ItemCtrl.poolDic == null)
        {
            ItemCtrl.poolDic = new Dictionary<ItemKind, ObjectPooling<ItemCtrl>>();
        }
        ItemCtrl.poolDic.Clear();
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        SpawnCoolTime += Time.deltaTime;
        uiManager.text.text = Mathf.Round(CurrentTime).ToString();
    }

    public void AddtoTransformlist(I_Faction factionTarget)
    {
        Objectlist.Add(factionTarget);
    }

    public void DeleteTransformlist(I_Faction factionTarget)
    {
        if (Objectlist.Contains(factionTarget))
        {
            for (int i = 0; i < Objectlist.Count; i++)
            {
                if (Objectlist[i] == factionTarget)
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
            float distance = Vector2.Distance(Objectlist[0].myTransform.position, _transform.position);

            for (int i = 1; i < Objectlist.Count; i++)
            {
                if (Vector2.Distance(Objectlist[i].myTransform.position, _transform.position) < distance)
                {
                    index = i;
                }
            }

            return Objectlist[index].myTransform;
        }
        return null;
    }

}

