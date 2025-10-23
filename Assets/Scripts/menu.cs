using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button start;
    public Button ranking;
    public Button backtomenu;
    public Button HowToPlay;

    public GameObject[] RankingText;
    public GameObject RankingColumn;
    public TextMeshProUGUI title;
    public Button end;
    void Start()
    {
        start.onClick.AddListener(() => StartGame());
        ranking.onClick.AddListener(() => Ranking());
        backtomenu.onClick.AddListener(() => BackToMenu());
        HowToPlay.onClick.AddListener(() => HowToPlayGame());
        end.onClick.AddListener(() => EndGame());
    }

    void StartGame()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("game");
        PlayFabManager.nameImput.gameObject.SetActive(true);
        PlayFabManager.submitButton.gameObject.SetActive(true);
        start.gameObject.SetActive(false);
        ranking.gameObject.SetActive(false);
        backtomenu.gameObject.SetActive(false);
        RankingColumn.SetActive(false);
        title.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        HowToPlay.gameObject.SetActive(false);
        backtomenu.gameObject.SetActive(true);
    }

    void Ranking()
    {
        start.gameObject.SetActive(false);
        ranking.gameObject.SetActive(false);
        backtomenu.gameObject.SetActive(true);
        RankingColumn.SetActive(true);
        title.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        HowToPlay.gameObject.SetActive(false);
        foreach (GameObject text in RankingText)
        {
            text.SetActive(true);
        }
    }

    void BackToMenu()
    {
        start.gameObject.SetActive(true);
        ranking.gameObject.SetActive(true);
        backtomenu.gameObject.SetActive(false);
        RankingColumn.SetActive(false);
        PlayFabManager.nameImput.gameObject.SetActive(false);
        PlayFabManager.submitButton.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        end.gameObject.SetActive(true);
        HowToPlay.gameObject.SetActive(true);
        foreach (GameObject text in RankingText)
        {
            text.SetActive(false);
        }
    }

    //ゲーム終了:ボタンから呼び出す
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    void HowToPlayGame()
    {
        start.gameObject.SetActive(false);
        ranking.gameObject.SetActive(false);
        backtomenu.gameObject.SetActive(true);
        RankingColumn.SetActive(false);
        title.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        HowToPlay.gameObject.SetActive(false);
    }

    // Update is called once per frame



}
