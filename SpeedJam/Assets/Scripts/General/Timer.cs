using UnityEngine;

public class Timer : MonoBehaviour
{
    private static readonly int precise = 100;
    
    private static float value;
    public static float Value
    {
        get { return (int)(value * precise) / (float)precise; }
        private set { }
    }
    public static int IntValue
    {
        get { return (int)(value * precise); }
        private set { }
    }

    private void Update()
    {
        value += Time.deltaTime;
    }

    public static void ResetTimer()
    {
        value = 0f;
    }

    public static float ToFloat(int intTime)
    {
        return (float)intTime / precise;
    }
}