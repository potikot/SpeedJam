using LootLocker.Requests;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject finalMessageUI;
    [SerializeField] private TextMeshProUGUI currentTimeUI;
    [SerializeField] private TextMeshProUGUI bestTimeUI;

    private readonly string leaderboardID = "11866";
    private string memberID;

    private float bestTime;
    
    public static SwitchMovement SwitchMovement;

    public static bool isWin = false;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        StartGuestSession();
        SwitchMovement = FindObjectOfType<SwitchMovement>();
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
        Time.timeScale = 1f;
        Timer.ResetTimer();
    }

    public void StopRun()
    {
        Time.timeScale = 0f;

        if (isWin && Timer.Value < bestTime)
        {
            SubmitTime();
            bestTime = Timer.Value;
        }

        finalMessageUI.SetActive(true);
        currentTimeUI.text = $"Current time: {Timer.Value}";
        bestTimeUI.text = $"Best time: {bestTime}";
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