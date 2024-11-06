using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int timeLimit;
    [SerializeField] TextMeshProUGUI timerText;
    float time;
    int remaining;

    void Start()
    {
        timerText.text = timeLimit.ToString();
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
                    timerText.text = $"{remaining.ToString("D3")}";
                }
                else if (remaining >= 10)
                {
                    timerText.text = $"{remaining.ToString("D2")}";
                }
                else
                {
                    timerText.text = $"{remaining.ToString("D1")}";
                }
            }
            else
            {
                //制限時間を超えたら
                timerText.text = "0";
                SceneManager.LoadScene("result");
            }
        }
    }
}
