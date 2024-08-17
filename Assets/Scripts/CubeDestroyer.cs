using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public int cubesDestroyed;
    public int childCount;
    
    public void ChildChecker()
    {
        childCount = gameObject.transform.childCount;
    }

    private void Start()
    {
        ChildChecker();
        Debug.Log(cubesDestroyed + "cubes" + childCount + "children");
    }

    private void OnTriggerEnter(Collider other)
    {
        cubesDestroyed++;
        Destroy(other.gameObject);
    }
}
