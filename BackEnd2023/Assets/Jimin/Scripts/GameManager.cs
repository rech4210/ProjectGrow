using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField]
    ObjectManager objectManager;

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

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        uiManager.text.text = Mathf.Round(CurrentTime).ToString();
    }

}
