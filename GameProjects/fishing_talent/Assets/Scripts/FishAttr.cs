using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class FishAttr : MonoBehaviour 
{
    //鱼的生命值
    public int hp;
    public int maxNum;
    public int maxSpeed;

    public GameObject diePerfab;

    //鱼碰到边界自动销毁
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    //鱼受伤
    void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            GameObject die = Instantiate(diePerfab);
            die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
}
