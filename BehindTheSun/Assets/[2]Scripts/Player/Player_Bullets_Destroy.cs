using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullets_Destroy : MonoBehaviour
{
    private void Update()
    {
        DestroyBulletIfOffscreen();
    }
    void DestroyBulletIfOffscreen()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Player_Bullets");

        foreach (GameObject bullet in bullets)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(bullet.transform.position);

            // ȭ���� ��� ���
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                Destroy(bullet); // �Ѿ� ����
            }
        }
    }
}
