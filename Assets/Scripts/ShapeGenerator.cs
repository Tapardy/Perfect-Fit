using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject shape;
    public GameObject cube;
    public int width;
    public int height;
    public int startX;
    public int startY;

    void Start()
    {
        Instantiate(shape, new Vector3(Random.Range(0, width-1 + startX), Random.Range(0, height-1 + startY), gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Instantiate(cube, new Vector3(x + startX, y + startY, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
            }
        }
    }
}
