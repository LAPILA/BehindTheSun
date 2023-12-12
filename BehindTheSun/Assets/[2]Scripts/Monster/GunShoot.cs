using System.Collections;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public AudioClip shootSound;
    public Sprite itemImage;
    public float bulletSpeed;
    public float reloadTime;
    public int maxBullet;
    public float fireTerm;
}

public class GunShoot : MonoBehaviour
{
    public string weaponType;
    public Transform gunShootPos;
    private AudioSource audioSource;
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
            // 플레이어의 위치를 가져옴
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                // 플레이어 위치에서 몬스터 위치를 뺀 방향 벡터 계산
                Vector3 direction = (player.transform.position - gunShootPos.position).normalized;

                // 총알 생성 및 발사
                GameObject bullet = Instantiate(bulletPrefab, gunShootPos.position, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

                // 총알에 방향 벡터를 이용하여 힘을 가함
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
                    shootSound = null,
                    itemImage = pistolImage,
                    bulletSpeed = 20f,
                    reloadTime = 2f,
                    maxBullet = 10,
                    fireTerm = 0.5f
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = pistolImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            case "Shotgun":
                currentWeapon = new Weapon
                {
                    shootSound = null,
                    itemImage = shotgunImage,
                    bulletSpeed = 10f,
                    reloadTime = 3f,
                    maxBullet = 6,
                    fireTerm = 4f
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = shotgunImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            case "MachineGun":
                currentWeapon = new Weapon
                {
                    shootSound = null,
                    itemImage = machineGunImage,
                    bulletSpeed = 30f,
                    reloadTime = 5f,
                    maxBullet = 30,
                    fireTerm = 0.2f
                };
                gunShootPos.GetComponent<SpriteRenderer>().sprite = machineGunImage;
                bulletCount = currentWeapon.maxBullet;
                break;
            default:
                Debug.LogError("Unknown weapon type");
                break;
        }
    }

    void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
