using LootLocker.Requests;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject finalMessageUI;
    [SerializeField] private TextMeshProUGUI currentTimeUI;
    [SerializeField] private TextMeshProUGUI bestTimeUI;

    private string leaderboardID = "11866";
    private string memberID;

    private float bestTime;

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
                GetBestTime();
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });
    }

    public void StartRun()
    {
        Timer.ResetTimer();
    }

    public void StopRun()
    {
        if (Timer.Value < bestTime)
        {
            SubmitTime();
            bestTime = Timer.Value;
        }

        finalMessageUI.SetActive(true);
        currentTimeUI.text = $"Current time: {Timer.Value}";
        bestTimeUI.text = $"Best time: {(bestTime < Timer.Value ? bestTime : Timer.Value)}";
    }

    public void SubmitTime()
    {
        LootLockerSDKManager.SubmitScore(memberID, Timer.IntValue, leaderboardID, GeneralData.PlayerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful submit");
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });
    }

    public void GetBestTime()
    {
        LootLockerSDKManager.GetMemberRank(leaderboardID, memberID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful get");
                bestTime = Timer.ToFloat(response.score);
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });
    }
}