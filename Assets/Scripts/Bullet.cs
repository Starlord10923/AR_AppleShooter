using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 6f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Target"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.xpManager.AddXP(10);
        }

        Destroy(gameObject);
    }
}