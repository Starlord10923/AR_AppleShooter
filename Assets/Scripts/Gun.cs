using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootForce;
    private int currentGunIndex = 0;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
    }
}
