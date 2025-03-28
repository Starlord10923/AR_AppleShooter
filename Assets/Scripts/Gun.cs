using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform firePoint;
    public float shootForce;
    private int currentGunIndex = 0;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        Destroy(muzzleFlash, 0.75f);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        Debug.Log($"Shooting {bullet.name} from gun {currentGunIndex} : {gameObject.name}, with force {shootForce}");
    }
}
