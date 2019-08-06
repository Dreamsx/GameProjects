using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class WebAttr : MonoBehaviour 
{
    public float disapperTime;

    public int damage;

    private void Start()
    {
        Destroy(gameObject, disapperTime);
    }

}
