using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;
   

    [SerializeField]
    private GameObject go_BulletHUD; // 필요하면 HUD 호출, 필요없으면 HUD 비활

    [SerializeField]
    private Text[] text_Bullet;

    void Start()
    {
        currentGun = GetComponent<Gun>();
    }

    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.currentGun;
        text_Bullet[0].text = currentGun.currentBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();

    }

    
    
}
