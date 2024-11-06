using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RandomNumberToObject : MonoBehaviour
{
    // public TextMeshProUGUI[] numberTexts; // UIに表示する数字（4つのTextMeshPro）
    public Button regenerateButton;       // 再生成ボタン
    public static int[] numbers = new int[4];   // ランダムに生成された4つの数字
    public GameObject formula; // フォーミュラオブジェクト
    [SerializeField] private TextMeshProUGUI[] numbertexts;
    public static TextMeshProUGUI[] numberTexts;

    public Button num1;
    public Button num2;
    public Button num3;
    public Button num4;

    private bool isStarted = false;



    void Start()
    {
        numberTexts = numbertexts;
        GenerateRandomNumbers();
        DisplayNumbers();
        // 再生成ボタンが押されたときにランダムな数字を再生成するイベントを追加
        regenerateButton.onClick.AddListener(GenerateAndDisplayNumbersOnClick);
        regenerateButton.GetComponent<Button>().interactable = false;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(4.0f);
        regenerateButton.GetComponent<Button>().interactable = true;
        isStarted = true;
    }



    void Update()
    {
        if (formulaimput.isSuccess == true)
        {
            GenerateAndDisplayNumbers();
            formulaimput.isSuccess = false;
        }
        if (isStarted)
        {
            regenerateButton.GetComponent<Button>().interactable = true;
        }

    }

    // ランダムな1～10の数字を4つ生成
    void GenerateRandomNumbers()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = Random.Range(0, 10); // 1から10までのランダムな数字を生成
        }
    }

    // 生成された数字をUIに表示
    void DisplayNumbers()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numberTexts[i].text = numbers[i].ToString();
        }
    }

    // 数字の生成と表示を同時に行うメソッド
    void GenerateAndDisplayNumbers()
    {
        GenerateRandomNumbers(); // 新しいランダムな数字を生成
        DisplayNumbers();        // 新しい数字をUIに表示
        num1.gameObject.SetActive(true);
        num2.gameObject.SetActive(true);
        num3.gameObject.SetActive(true);
        num4.gameObject.SetActive(true);
        formulaimput.currentFormula = "";
    }

    void GenerateAndDisplayNumbersOnClick()
    {
        GenerateRandomNumbers(); // 新しいランダムな数字を生成
        DisplayNumbers();        // 新しい数字をUIに表示
        num1.gameObject.SetActive(true);
        num2.gameObject.SetActive(true);
        num3.gameObject.SetActive(true);
        num4.gameObject.SetActive(true);
        formulaimput.currentFormula = "";
        formulaimput.formulaText.text = "";
    }

    // 生成された数字を取得するためのメソッド
    public int[] GetNumbers()
    {
        return numbers;
    }
}
