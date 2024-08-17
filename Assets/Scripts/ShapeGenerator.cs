using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject[] shapePrefabs;
    private GameObject shape;
    public GameObject cube;
    public int width;
    public int height;
    public int startX;
    public int startY;
    private CubeDestroyer cd;
    

    void Start()
    {
        WallBuilder();
    }

    private void ResetWall()
    {
        Destroy(shape);
    }

    private void WallBuilder()
    {
        int randomShape = Random.Range(0, shapePrefabs.Length);
        shape = Instantiate(shapePrefabs[randomShape], new Vector3(Random.Range(0, width - 1 + startX), Random.Range(0, height - 1 + startY), gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
        //Debug.Log(cd.cubesDestroyed);
        //Debug.Log(cd.childCount);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Instantiate(cube, new Vector3(x + startX, y + startY, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
            }
        }
        if (cd.cubesDestroyed < cd.childCount)
        {
            Debug.Log("Fuck");
            //Start();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetWall();
            WallBuilder();
        }
    }
}
