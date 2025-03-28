using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 6f);
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     Debug.Log("Bullet collided with " + col.collider.name);
    //     if (col.collider.CompareTag("Target"))
    //     {
    //         Debug.Log("Bullet collided with appletarget");
    //         Destroy(col.gameObject);
    //         GameManager.Instance.AppleDestroyed();
    //     }
    //     else if (col.collider.CompareTag("Human"))
    //     {
    //         Debug.Log("Bullet collided with human");
    //         GameManager.Instance.GameWin();
    //         Destroy(gameObject);
    //     }

    //     Destroy(gameObject);
    // }

    // private void OnTriggerEnter(Collider col)
    // {
    //     Debug.Log("Bullet collided with " + col.name);
    //     if (col.CompareTag("Target"))
    //     {
    //         Debug.Log("Bullet collided with appletarget");
    //         Destroy(col.gameObject);
    //         GameManager.Instance.AppleDestroyed();
    //     }
    //     else if (col.CompareTag("Human"))
    //     {
    //         Debug.Log("Bullet collided with human");
    //         GameManager.Instance.GameWin();
    //         Destroy(gameObject);
    //     }

    //     Destroy(gameObject);
    // }
}
