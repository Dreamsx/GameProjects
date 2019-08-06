using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour 
{
    public GameObject[] gunGos;

    public int Lev = 0;

    public Transform bulletHolder;

    //存放枪的子弹
    public GameObject[] Bullet1Gos;
    public GameObject[] Bullet2Gos;
    public GameObject[] Bullet3Gos;
    public GameObject[] Bullet4Gos;
    public GameObject[] Bullet5Gos;

    //每一炮所需的金钱数和打出的伤害值
    private int[] oneShootCost = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    //使用的是第几档的炮弹
    private int CostIndex = 0;

    public Text oneShootCostText;

    private void Update()
    {
        Fire();
        ChangeBulletCost();
    }

    //滚轮切换子弹
    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            OnButtonMDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }
    }

    public void OnButtonPDown()
    {
        gunGos[CostIndex / 4].SetActive(false);
        CostIndex++;
        CostIndex = (CostIndex > oneShootCost.Length - 1) ? 0 : CostIndex;
        gunGos[CostIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[CostIndex];
    }

    public void OnButtonMDown()
    {
        gunGos[CostIndex / 4].SetActive(false);
        CostIndex--;
        CostIndex = (CostIndex < 0) ? oneShootCost.Length - 1 : CostIndex;
        gunGos[CostIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[CostIndex];
    }

    //开火的方法
    void Fire()
    {
        GameObject[] useBullets = Bullet5Gos;
        int bulletsIndex;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            switch (CostIndex/4)
            {
                case 0:
                    useBullets = Bullet1Gos;
                    break;
                case 1:
                    useBullets = Bullet2Gos;
                    break;
                case 2:
                    useBullets = Bullet3Gos;
                    break;
                case 3:
                    useBullets = Bullet4Gos;
                    break;
                case 4:
                    useBullets = Bullet5Gos;
                    break;
            }

            bulletsIndex = (Lev % 10 > 9) ? 9 : Lev;

            GameObject bullet = Instantiate(useBullets[bulletsIndex]);
            bullet.transform.SetParent(bulletHolder, false);
            bullet.transform.position = gunGos[CostIndex / 4].transform.Find("FirePos").transform.position;
            bullet.transform.rotation = gunGos[CostIndex / 4].transform.rotation;
            bullet.GetComponent<BulletAttr>().damage = oneShootCost[CostIndex];
            bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
            bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
        }
    }
}
