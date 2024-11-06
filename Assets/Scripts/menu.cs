using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button start;
    void Start()
    {
        start.onClick.AddListener(() => StartGame());
    }

    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
