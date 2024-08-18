using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject[] shapePrefabs;
    public GameObject cube;
    public int width;
    public int height;
    public int startX;
    public int startY;
    private GameObject shape;
    private CubeDestroyer cd;

    void Start()
    {
        WallBuilder();
    }

    private void ResetWall()
    {
        if (shape != null)
        {
            Destroy(shape);
        }
    }

    private void WallBuilder()
    {
        int randomShapeIndex = Random.Range(0, shapePrefabs.Length);
        
        int spawnAreaOffsetX = 2;
        int spawnAreaOffsetY = 2;
        
        int spawnX = Random.Range(startX + spawnAreaOffsetX, startX + width - spawnAreaOffsetX);
        int spawnY = Random.Range(startY + spawnAreaOffsetY, startY + height - spawnAreaOffsetY);

        shape = Instantiate(shapePrefabs[randomShapeIndex], new Vector3(spawnX, spawnY, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);

        // Build wall
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Instantiate(cube, new Vector3(x + startX, y + startY, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
            }
        }

        if (cd != null && cd.cubesDestroyed < cd.childCount)
        {
            Debug.Log("Woopsie Daisy");
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
