using UnityEngine;

public class MeshDissolve : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}
