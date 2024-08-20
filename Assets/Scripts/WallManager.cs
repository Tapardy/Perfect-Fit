using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabWall;
    private GameObject wall;

    private Vector3 spawnPos;
    private bool canSpawn = true;

    public float speed;
    [SerializeField]private float acceleration = 1f;

    [SerializeField] private float maxSpeed = 50;
    //private float[] rotation = new float[4] { 0f, 90f, 180f, 270f };
    [SerializeField] private float distance = 220f;
    [SerializeField] private float distanceSpawn = 200f;

    private List<GameObject> wallObjects = new List<GameObject>();

    //private float GetRandomRotation()
    //{
    //    int randomIndex = Random.Range(0, rotation.Length);
    //    return rotation[randomIndex];
    //}


    private void DecreaseSpawnDelay()
    {

    }

    private void SpawnWall()
    {
        //float randomRotation = GetRandomRotation();
        wall = Instantiate(prefabWall, gameObject.transform);
        Debug.Log(wall.transform.rotation);
        wallObjects.Add(wall);
    }

    private void Start()
    {
        spawnPos = gameObject.transform.position;
        SpawnWall();
    }

    void Update()
    {
        speed += acceleration * Time.deltaTime/2;
        speed = Mathf.Clamp(speed, 20, maxSpeed);
        //wall.transform.position += Vector3.forward * speed * Time.deltaTime;

        for (int i = wallObjects.Count - 1; i >= 0; i--)
        {
            GameObject wall = wallObjects[i];
            wall.transform.position += Vector3.forward * -speed * Time.deltaTime;

            float traveledDistance = Vector3.Distance(gameObject.transform.position, wall.transform.position);

            if (traveledDistance >= distanceSpawn && canSpawn)
            {
                SpawnWall();
                canSpawn = false;
            }

            if (traveledDistance >= distance)
            {
                Destroy(wall);
                canSpawn = true;
                wallObjects.RemoveAt(i);
            }
        }
    }
}
