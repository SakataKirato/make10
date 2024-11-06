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


    void Start()
    {
        resultText.text = "あなたの得点は" + " " + formulaimput.countText.text + "点です";
    }
}