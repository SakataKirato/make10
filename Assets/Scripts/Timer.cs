using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int timeLimit;
    [SerializeField] TextMeshProUGUI timerText;
    float time;
    int remaining;
    public static int finalscore;

    public TextMeshProUGUI FinishText;

    void Start()
    {
        formulaimput.count = 0;
        timerText.text = "残り:" + " " + timeLimit.ToString();
    }

    void Update()
    {
        if (formulaimput.isCounting == false)
        {
            if (remaining >= 0)
            {
                //フレーム毎の経過時間をtime変数に追加
                time += Time.deltaTime;
                //time変数をint型にし制限時間から引いた数をint型のlimit変数に代入
                remaining = timeLimit - (int)time;
                //timerTextを更新していく
                if (remaining >= 100)
                {
                    timerText.text = "残り:" + " " + $"{remaining.ToString("D3")}";
                }
                else if (remaining >= 10)
                {
                    timerText.text = "残り:" + " " + $"{remaining.ToString("D2")}";
                }
                else if (remaining >= 0)
                {
                    timerText.text = "残り:" + " " + $"{remaining.ToString("D1")}";
                }
            }
            else
            {
                //制限時間を超えたら
                timerText.text = "残り:" + " " + "0";
                finalscore = formulaimput.count;
                PlayFabManager.SendPlayScore(finalscore);
                StartCoroutine(ChangeScene());
            }
        }
    }

    IEnumerator ChangeScene()
    {
        FinishText.text = "終了！";
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("result");
        formulaimput.isCounting = true;
    }
}
