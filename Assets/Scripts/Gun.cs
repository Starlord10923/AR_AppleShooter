using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform firePoint;
    public float shootForce = 30f;
    public float raycastRange = 20f;

    [Header("Crosshair UI")]
    public RectTransform crosshairUI;

    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
        if (arCamera == null)
            Debug.LogError("AR Camera not found! Tag your AR camera as 'MainCamera'");
    }

    public void Shoot()
    {
        if (arCamera == null || crosshairUI == null) return;

        Vector3 screenPos = crosshairUI.position;
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        Vector3 shootDirection = ray.direction;

        // Visual bullet + muzzle flash
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position - shootDirection * 0.2f,
            Quaternion.LookRotation(shootDirection)
        );
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce, ForceMode.Impulse);

        GameObject muzzleFlash = Instantiate(
            muzzleFlashPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );
        Destroy(muzzleFlash, 0.75f);

        // === Raycast logic ===
        if (Physics.Raycast(ray, out RaycastHit hit, raycastRange))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            if (hit.collider.CompareTag("Target"))
            {
                Debug.Log("Hit Apple Target!");
                Destroy(hit.collider.gameObject);
                GameManager.Instance.AppleDestroyed();
            }
            else if (hit.collider.CompareTag("Human"))
            {
                Debug.Log("Hit Human!");
                GameManager.Instance.GameWin();
                // Optionally: apply feedback on the Human object
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }
}
