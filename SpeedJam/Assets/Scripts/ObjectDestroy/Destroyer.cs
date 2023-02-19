using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<MeshDestroy>()?.DestroyMesh();
    }
}
