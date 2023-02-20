using UnityEngine;

[RequireComponent(typeof(MeshDestroy))]
public class Destroyable : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float decelerationMultiplier;

    [SerializeField] private bool switchOnHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (switchOnHit)
                GameManager.SwitchMovement.Switch();

            collision.gameObject.GetComponent<WheelLocomotion>()?.Deceleration(decelerationMultiplier);
            GetComponent<MeshDestroy>().DestroyMesh(collision.contacts[0].point);
        }
    }
}