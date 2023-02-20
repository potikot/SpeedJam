using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public TextMeshProUGUI text;

    public int count = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        
        if (pickup)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!isFull[i])
                {
                    isFull[i] = true;
                    //Instantiate(pickup.slotButton, slots[i].transform);
                    Destroy(pickup.gameObject);
                    count++;
                    text.text = count.ToString() + "/5";
                    if (count >= isFull.Length)
                        FindObjectOfType<GameManager>().StopRun();
                    break;
                }
            }
        }
    }
}