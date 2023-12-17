using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadMainScene : MonoBehaviour
{

    private float time;
    private string sceneName = "SolidScene";
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(sceneName);
        }
        );
    }

}
