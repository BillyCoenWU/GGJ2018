using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ;
public class Batima : MonoBehaviour
{
    public Sprite sprite;
    public Vector3 maxScale;
    public int maxSonarCount;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SonarPool.Instance.Load().Set(sprite, transform.position, maxScale, maxSonarCount,true);
        }
    }
}
