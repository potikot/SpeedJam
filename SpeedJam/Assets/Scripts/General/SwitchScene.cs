using LootLocker.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadScene(string name)
    {
        LootLockerSDKManager.EndSession((response) => 
        {
            if (response.success)
            {
                Debug.Log("Successful end session");
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }

            SceneManager.LoadScene(name);
        });
    }
}
