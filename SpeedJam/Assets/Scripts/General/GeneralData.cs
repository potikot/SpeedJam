using UnityEngine;

public class GeneralData : MonoBehaviour
{
    public static string PlayerName;
    public static float MasterVolume;

    public static void SetVolume(float volume)
    {
        MasterVolume = volume;
    }
}