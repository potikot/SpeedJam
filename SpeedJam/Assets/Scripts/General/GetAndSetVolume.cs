using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GetAndSetVolume : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().volume = GeneralData.MasterVolume;
    }
}