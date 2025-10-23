using UnityEngine;
using Unity.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Threading;


public class result : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI resultText; // 結果を表示するTextMeshPro
    public Button restartButton;       // 再開ボタン
    public Button backtohomeButton;    // ホームに戻るボタン


    void Start()
    {
        resultText.text = "あなたの得点は" + " " + formulaimput.count.ToString() + "点です";
        // 再開ボタンが押されたときにゲームを再開するイベントを追加
        restartButton.onClick.AddListener(() => RestartGame());
        // ホームに戻るボタンが押されたときにホームに戻るイベントを追加
        backtohomeButton.onClick.AddListener(() => BackToHome());

    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

    void BackToHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }
}