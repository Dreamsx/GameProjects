using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Ef_AutoMove : MonoBehaviour 
{
    public float speed = 0.1f;
    public Vector3 dir = Vector3.right;

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

}
