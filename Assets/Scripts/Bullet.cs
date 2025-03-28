using UnityEngine;

public class BulletScript : MonoBehaviour
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
            FindObjectOfType<XPManager>().AddXP(1);
        }

        Destroy(gameObject);
    }
}