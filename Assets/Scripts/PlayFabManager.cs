using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class PlayFabManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TMP_InputField nameImputField;
    public static TMP_InputField nameImput;
    [SerializeField] private Button SubmitButton;
    public static Button submitButton;
    public Button rankingButton;
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI[] nameText;

    private const string TitleId = "10を作ろう";
    private int position;

    void Start()
    {
        nameImput = nameImputField;
        submitButton = SubmitButton;
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = TitleId;
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = "aaaaaaaaaaaa",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        // (result) =>
        // {
        //     // 既に作成済みだった場合
        //     if (!result.NewlyCreated)
        //     {
        //         Debug.LogWarning("already account");

        //         // 再度IDを取得し直してログイン
        //         var newId = CreateNewPlayerId();
        //         DoLogin(newId); // ログイン処理
        //         return;
        //     }

        //     // アカウント作成完了
        //     Debug.Log("Create Account Success!!");
        // },
        // (error) =>
        // {
        //     Debug.LogError("Create Account Failed...");
        //     Debug.LogError(error.GenerateErrorReport());
        // });
        submitButton.onClick.AddListener(() => SetUserName(nameImput.text));
        // rankingButton.onClick.AddListener(() => GetRanking());
        rankingButton.onClick.AddListener(() => GetLeaderboard());
    }

    private void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "Highscore",
            StartPosition = 0,    // 取得する開始位置
            MaxResultsCount = 10 // 最大取得数
        },
                (result) =>
                {
                    // 取得したランキング情報
                    if (result?.Leaderboard != null)
                    {
                        for (var i = 0; i < result.Leaderboard.Count; i++)
                        {

                            var entry = result.Leaderboard[i];
                            Debug.Log(entry.Position);    // 順位
                            Debug.Log(entry.DisplayName); // プレイヤー名
                            Debug.Log(entry.StatValue);   // スコア
                            if (entry.DisplayName == null)
                            {
                                // position = entry.Position + 1;
                                nameText[i].text = "";
                                scoreText[i].text = "";
                            }
                            else
                            {
                                // position = entry.Position + 1;
                                nameText[i].text = entry.DisplayName;
                                scoreText[i].text = entry.StatValue.ToString();
                            }
                        }
                        Debug.Log("Get Leader Board Success!!");
                    }
                },
                (error) =>
                {
                    Debug.LogError("Get Leader Board Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });
    }


    private void OnLoginSuccess(LoginResult result)
    {

        Debug.Log("Login Success: " + result.PlayFabId);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login Failed.");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void DoLogin(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private string CreateNewPlayerId()
    {
        return Guid.NewGuid().ToString("N");
    }

    private void SetUserName(string userName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
                new UpdateUserTitleDisplayNameRequest { DisplayName = userName },
                (result) =>
                {
                    Debug.Log("Save Display Name Success!!");
                },
                (error) =>
                {
                    Debug.LogError("Save Display Name Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });
        SceneManager.LoadScene("game");
    }

    public static void SendPlayScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName = "Highscore",
                            Value = score// スコア
                        }
                    }
        },
                (result) =>
                {
                    // スコア送信完了
                    Debug.Log("Send Ranking Score Success!!");
                },
                (error) =>
                {
                    Debug.LogError("Send Ranking Score Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });
    }
}
