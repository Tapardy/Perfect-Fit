using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject cube;
    public int width = 10;
    public int height = 4;
    void Start()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Instantiate(cube, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
