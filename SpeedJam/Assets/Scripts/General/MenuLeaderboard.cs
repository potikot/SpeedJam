using LootLocker.Requests;
using TMPro;
using UnityEngine;

public class MenuLeaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] leaderboardTexts;
    
    private readonly string leaderboardID = "11866";
    private string memberID;

    private void Start()
    {
        StartGuestSession();
    }

    private void StartGuestSession()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful session");
                memberID = response.player_id.ToString();
                UpdateLocalLeaderboard();
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });
    }

    public void UpdateLocalLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, leaderboardTexts.Length, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful get list");

                for (int i = 0; i < response.items.Length; i++)
                {
                    leaderboardTexts[i].text = string.Empty;
                    leaderboardTexts[i].text += $"{response.items[i].rank}. {response.items[i].metadata} - {response.items[i].score}\n";
                }
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });
    }
}