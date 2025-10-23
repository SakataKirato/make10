// using UnityEngine;
// using PlayFab;
// using PlayFab.ClientModels;

// public class Script : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
//         {
//             StatisticName = "Highscore",
//             StartPosition = 0,    // 取得する開始位置
//             MaxResultsCount = 9 // 最大取得数
//         },
//                 (result) =>
//                 {
//                     // 取得したランキング情報
//                     if (result?.Leaderboard != null)
//                     {
//                         for (var i = 0; i < result.Leaderboard.Count; i++)
//                         {

//                             var entry = result.Leaderboard[i];
//                             Debug.Log(entry.Position);    // 順位
//                             Debug.Log(entry.DisplayName); // プレイヤー名
//                             Debug.Log(entry.StatValue);   // スコア
//                         }
//                     }
//                     Debug.Log("Get Leader Board Success!!");
//                 },
//                 (error) =>
//                 {
//                     Debug.LogError("Get Leader Board Failed...");
//                     Debug.LogError(error.GenerateErrorReport());
//                 });
//     }

//     // Update is called once per frame

// }
