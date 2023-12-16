using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int SpawnCount;

    float CurrentTime = 0f;
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

        if(SpawnCoolTime > SpawnTime)
        {
            for(int i = 0; i < SpawnCount; i++)
            {
                objectManager.MakeZombie();
            }
            Debug.Log("Spawn!!");
            SpawnCoolTime = 0f;
        }
    }

}
