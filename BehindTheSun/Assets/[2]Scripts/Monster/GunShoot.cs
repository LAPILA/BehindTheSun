using System.Collections;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public Sprite itemImage;
    public float bulletSpeed;
    public float reloadTime;
    public int maxBullet;
    public float fireTerm;
    public float damage;
}

public class GunShoot : MonoBehaviour
{
    private AudioSource audioSource;
    public string weaponType;
    public Transform gunShootPos;
    private Weapon currentWeapon;
    private int bulletCount;
    public Sprite pistolImage;
    public Sprite shotgunImage;
    public Sprite machineGunImage;
    public GameObject bulletPrefab;
    bool isReloading = false;
    bool isShooting = false;
    float lastShotTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeWeapon(weaponType);
    }

    public void Shoot()
    {
        if (!isShooting) {
        }
        if (bulletCount > 0 && !isShooting && Time.time >= lastShotTime + currentWeapon.fireTerm) {
            isShooting = true;
            StartCoroutine(ShootCoroutine());
        }
        else if (bulletCount <= 0) {
            Reload();
        }
    }

    IEnumerator ShootCoroutine()
    {

        while (isShooting) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                Vector3 direction = (player.transform.position - gunShootPos.position).normalized;
                audioSource.Play();
                GameObject bullet = Instantiate(bulletPrefab, gunShootPos.position, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = direction * currentWeapon.bulletSpeed;

                bulletCount--;
                lastShotTime = Time.time;

                yield return new WaitForSeconds(currentWeapon.fireTerm);
                isShooting = false;
            }
        }
    }



    public void Reload()
    {
        if (!isReloading && bulletCount <= 0) {
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        bulletCount = currentWeapon.maxBullet;
        isReloading = false;
        Shoot();
    }

    public void AimAtPlayer(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - gunShootPos.position;

        if (direction.x < 0) {
            gunShootPos.GetComponent<SpriteRenderer>().flipY = true;
        }
        else {
            gunShootPos.GetComponent<SpriteRenderer>().flipY = false;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunShootPos.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ChangeWeapon(string newWeaponType)
    {
        switch (newWeaponType) {
            case "Pistol":
                currentWeapon = new Weapon
                {
                    itemImage = pistolImage,
                    bulletSpeed = 20f,
                    reloadTime = 2f,
                    maxBullet = 10,
                    fireTerm = 0.8f,
                    damage = 20
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = pistolImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            case "Shotgun":
                currentWeapon = new Weapon
                {
                    itemImage = shotgunImage,
                    bulletSpeed = 10f,
                    reloadTime = 3f,
                    maxBullet = 6,
                    fireTerm = 0.5f,
                    damage = 5
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = shotgunImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            case "MachineGun":
                currentWeapon = new Weapon
                {
                    itemImage = machineGunImage,
                    bulletSpeed = 30f,
                    reloadTime = 5f,
                    maxBullet = 30,
                    fireTerm = 1.5f,
                    damage = 10
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = machineGunImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            default:
                Debug.LogError("Unknown weapon type");
                break;
        }
    }

}
