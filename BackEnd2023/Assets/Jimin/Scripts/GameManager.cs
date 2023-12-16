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

    float CurrentTime = 0f;
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
        uiManager.text.text = Mathf.Round(CurrentTime).ToString();
    }

}
