using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Ef_destorySelf : MonoBehaviour 
{
    //鱼死亡动画销毁的时间
    public float delay = 1.0f;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
