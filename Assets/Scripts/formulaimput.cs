using UnityEngine;
using TMPro; // TextMeshProの使用
using UnityEngine.UI;
using System; // ボタンの使用
using System.Collections.Generic;
using System.Collections;

public class formulaimput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI FormulaText; // 数式を表示するTextMeshPro

    public static TextMeshProUGUI formulaText; // 数式を表示するTextMeshPro

    [SerializeField] private TextMeshProUGUI ResultText; // 結果を表示するTextMeshPro
    public static TextMeshProUGUI resultText; // 結果を表示するTextMeshPro
    // public TextMeshProUGUI countText; // カウントを表示するTextMeshPro
    private int count;
    static public string currentFormula = ""; // 現在の数式
    public Button plus;
    public Button minus;
    public Button multiply;
    public Button divide;
    public Button leftkakko;
    public Button rightkakko;
    public Button num1;
    public Button num2;
    public Button num3;
    public Button num4;
    public Button clear;
    public Button delete;
    public Button calculate; // 計算ボタン
    [SerializeField] private TextMeshProUGUI CountText;
    public static TextMeshProUGUI countText;
    public AudioClip success;
    public AudioClip failure;
    public AudioClip countdown;
    public TextMeshProUGUI countdownDisplay; // カウントダウンテキスト
    public int countdownTime; // カウントダウンの秒数
    static public bool isCounting = true;
    static public bool isSuccess = false;
    private string tmptext;




    private Dictionary<Button, int> numberButtonMap = new Dictionary<Button, int>();
    private Stack<int> usedNumbers = new Stack<int>(); // 使用した数字をスタックで保存


    void Start()
    {
        plus.onClick.AddListener(() => AddOperator("+"));
        minus.onClick.AddListener(() => AddOperator("-"));
        multiply.onClick.AddListener(() => AddOperator("*"));
        divide.onClick.AddListener(() => AddOperator("/"));
        leftkakko.onClick.AddListener(() => AddOperator("("));
        rightkakko.onClick.AddListener(() => AddOperator(")"));
        num1.onClick.AddListener(() => AddNumber(num1, RandomNumberToObject.numbers[0]));
        num2.onClick.AddListener(() => AddNumber(num2, RandomNumberToObject.numbers[1]));
        num3.onClick.AddListener(() => AddNumber(num3, RandomNumberToObject.numbers[2]));
        num4.onClick.AddListener(() => AddNumber(num4, RandomNumberToObject.numbers[3]));
        clear.onClick.AddListener(() => ClearFormula(num1, num2, num3, num4));
        delete.onClick.AddListener(() => DeleteLastCharacter());
        calculate.onClick.AddListener(() => CalculateResult());
        StartCoroutine(CountdownToStart());
        this.GetComponent<AudioSource>().PlayOneShot(countdown);
        countText = CountText;
        formulaText = FormulaText;
        resultText = ResultText;
        num1.GetComponent<Button>().interactable = false;
        num2.GetComponent<Button>().interactable = false;
        num3.GetComponent<Button>().interactable = false;
        num4.GetComponent<Button>().interactable = false;
        clear.GetComponent<Button>().interactable = false;
        delete.GetComponent<Button>().interactable = false;
        plus.GetComponent<Button>().interactable = false;
        minus.GetComponent<Button>().interactable = false;
        multiply.GetComponent<Button>().interactable = false;
        divide.GetComponent<Button>().interactable = false;
        leftkakko.GetComponent<Button>().interactable = false;
        rightkakko.GetComponent<Button>().interactable = false;
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {

            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "Go!";
        yield return new WaitForSeconds(1f);
        isCounting = false;
        num1.GetComponent<Button>().interactable = true;
        num2.GetComponent<Button>().interactable = true;
        num3.GetComponent<Button>().interactable = true;
        num4.GetComponent<Button>().interactable = true;
        plus.GetComponent<Button>().interactable = true;
        minus.GetComponent<Button>().interactable = true;
        multiply.GetComponent<Button>().interactable = true;
        divide.GetComponent<Button>().interactable = true;
        leftkakko.GetComponent<Button>().interactable = true;
        rightkakko.GetComponent<Button>().interactable = true;

        countdownDisplay.gameObject.SetActive(false); // カウントダウンテキストを非表示にする
    }

    void Update()
    {
        numberButtonMap[num1] = RandomNumberToObject.numbers[0];
        numberButtonMap[num2] = RandomNumberToObject.numbers[1];
        numberButtonMap[num3] = RandomNumberToObject.numbers[2];
        numberButtonMap[num4] = RandomNumberToObject.numbers[3];
        if (num1.gameObject.activeSelf | num2.gameObject.activeSelf | num3.gameObject.activeSelf | num4.gameObject.activeSelf | !IsFormulaValid(currentFormula))
        {
            calculate.GetComponent<Button>().interactable = false;
        }
        else
        {
            calculate.GetComponent<Button>().interactable = true;
        }

        if (currentFormula == "")
        {
            delete.GetComponent<Button>().interactable = false;
            clear.GetComponent<Button>().interactable = false;
        }
        else
        {
            delete.GetComponent<Button>().interactable = true;
            clear.GetComponent<Button>().interactable = true;
        }
    }

    // 演算子を数式に追加する関数
    private void AddOperator(string operatorSymbol)
    {
        currentFormula += " " + operatorSymbol + " "; // 演算子を数式に追加

        formulaText.text = currentFormula.Replace("*", "×").Replace("/", "÷"); // 数式を更新
    }

    // 数字を数式に追加する関数
    private void AddNumber(Button clickedButton, int number)
    {
        currentFormula += " " + number.ToString() + " "; // 数字を数式に追加
        formulaText.text = formulaText.text = currentFormula.Replace("*", "×").Replace("/", "÷"); // 数式を更新
        // 使用した数字をスタックに保存
        usedNumbers.Push(number);

        // ボタンを非表示にする
        clickedButton.gameObject.SetActive(false);
    }

    private void ClearFormula(Button num1, Button num2, Button num3, Button num4)
    {
        currentFormula = "";
        formulaText.text = currentFormula;
        // 数字ボタンをすべて再表示する
        num1.gameObject.SetActive(true);
        num2.gameObject.SetActive(true);
        num3.gameObject.SetActive(true);
        num4.gameObject.SetActive(true);
        usedNumbers.Clear(); // 使用した数字の履歴をクリア
    }

    private void DeleteLastCharacter()
    {
        if (formulaText.text.Length > 0)
        {
            // 最後に追加された数字や演算子を削除
            formulaText.text = formulaText.text.Replace("×", "*").Replace("÷", "/");
            string[] formulaParts = formulaText.text.Trim().Split(" ");
            if (formulaParts.Length > 0)
            {
                string lastPart = formulaParts[formulaParts.Length - 1];

                // 数字であればボタンを再表示
                if (int.TryParse(lastPart, out int lastNumber))
                {
                    foreach (var pair in numberButtonMap)
                    {
                        if (pair.Value == lastNumber && !pair.Key.gameObject.activeSelf)
                        {
                            pair.Key.gameObject.SetActive(true);
                            break;
                        }
                    }

                }
                // 数式から最後の部分を取り除く
                currentFormula = string.Join(" ", formulaParts, 0, formulaParts.Length - 1);
                formulaText.text = currentFormula.Replace("*", "×").Replace("/", "÷") + " ";
            }
        }
    }

    // 数式の結果を計算する（必要に応じて計算処理を追加）
    public void CalculateResult()
    {
        if (num1.gameObject.activeSelf | num2.gameObject.activeSelf | num3.gameObject.activeSelf | num4.gameObject.activeSelf)
        {
            Debug.Log("数字が足りません");
        }
        if (!num1.gameObject.activeSelf && !num2.gameObject.activeSelf && !num3.gameObject.activeSelf && !num4.gameObject.activeSelf)
        {
            // ここに数式の計算処理を実装（例：数式を解析し、結果を表示）
            double result = Calculator.EvaluateExpression(currentFormula);
            formulaText.text = " " + currentFormula.Replace("*", "×").Replace("/", "÷") + " " + "=" + " " + result.ToString();
            if (result == 10)
            {
                // Debug.Log("正解！");
                resultText.text = "<color=red>" + "\u3007" + " </color=red >";
                isSuccess = true;
                this.GetComponent<AudioSource>().PlayOneShot(success);
                count++;
                countText.text = count.ToString();
                StartCoroutine(successWait());
            }
            else
            {
                // Debug.Log("不正解！");
                resultText.text = "<color=blue>" + "×" + " </color=blue >";
                this.GetComponent<AudioSource>().PlayOneShot(failure);
                tmptext = formulaText.text.Replace("×", "*").Replace("÷", "/");
                string[] formulaParts = tmptext.Trim().Split(" ");
                currentFormula = string.Join(" ", formulaParts, 0, formulaParts.Length - 2);
                StartCoroutine(failureWait());
            }
        }
    }

    IEnumerator successWait()
    {
        yield return new WaitForSeconds(0.5f);
        num1.gameObject.SetActive(true);
        num2.gameObject.SetActive(true);
        num3.gameObject.SetActive(true);
        num4.gameObject.SetActive(true);
        formulaText.text = "";
        resultText.text = "";
    }

    IEnumerator failureWait()
    {
        yield return new WaitForSeconds(0.5f);
        resultText.text = "";
        formulaText.text = currentFormula.Replace("*", "×").Replace("/", "÷") + " ";
    }

    // 数式の妥当性を確認する関数
    private bool IsFormulaValid(string formula)
    {
        try
        {
            // 数式をチェックする（例：計算可能かどうか）
            double result = Calculator.EvaluateExpression(formula);
            return true;
        }
        catch
        {
            // 例外が発生した場合は無効な数式
            return false;
        }
    }

}
