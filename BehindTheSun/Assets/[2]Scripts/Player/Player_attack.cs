using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
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
        audioSource.Play();
        GameObject bullet = Instantiate(bulletPrefab, gunShootPos.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        if(transform.localScale.x == -1f)
        {
            bulletRB.velocity = new Vector2(-1, 0) * 40f;
        }
        else
        {
            bulletRB.velocity = new Vector2(1, 0) * 40f;
        }
    }

    public void AimAtPlayer(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - gunShootPos.position;

        if (direction.x < 0)
        {
            gunShootPos.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gunShootPos.GetComponent<SpriteRenderer>().flipY = false;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunShootPos.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ChangeWeapon(string newWeaponType)
    {
        switch (newWeaponType)
        {
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
