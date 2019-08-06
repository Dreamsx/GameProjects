using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class BulletAttr : MonoBehaviour 
{
    public int speed;
    public int damage;
    public GameObject WebPerfab;

    //子弹打到边界销毁自身，打到鱼销毁自身并且生成网
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Fish")
        {
            GameObject Web = Instantiate(WebPerfab);
            Web.transform.SetParent(gameObject.transform.parent, false);
            Web.transform.position = gameObject.transform.position;
            Debug.Log("aaa");
            Web.GetComponent<WebAttr>().damage = damage;
            Debug.Log("bbb");
            Destroy(gameObject);
            Debug.Log("ccc");
        }
    }
}
