using UnityEngine;

public class SwitchMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerInWheel;
    [SerializeField] private GameObject playerOutWheel;
    [SerializeField] private CameraFollow cameraFollow;

    [SerializeField] private bool switchOnSpeed;
    [SerializeField] private float speedToInWheel;
    [SerializeField] private float speedToOutWheel;

    private WheelLocomotion wheelLocomotion;
    private PlayerLocomotion playerLocomotion;

    private bool inWheel = true;

    private void Start()
    {
        wheelLocomotion = playerInWheel.GetComponent<WheelLocomotion>();
        playerLocomotion = playerOutWheel.GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        if (switchOnSpeed)
        {
            if (inWheel)
            {
                if (wheelLocomotion.LastVelocityXZ.magnitude <= speedToOutWheel)
                {
                    Switch();
                }
            }
            else
            {
                if (playerLocomotion.LastVelocityXZ.magnitude >= speedToInWheel)
                {
                    Switch();
                }
            }
        }
    }

    public void Switch()
    {
        if (inWheel)
        {
            playerInWheel.SetActive(false);
            playerOutWheel.SetActive(true);

            cameraFollow.target = playerOutWheel.transform;
            playerOutWheel.transform.position = playerInWheel.transform.position;
        }
        else
        {
            playerInWheel.SetActive(true);
            playerOutWheel.SetActive(false);

            cameraFollow.target = playerInWheel.transform;
            playerInWheel.transform.position = playerOutWheel.transform.position + Vector3.up;
        }

        inWheel = !inWheel;
    }
}