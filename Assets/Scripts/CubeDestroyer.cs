using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public int cubesDestroyed;
    public int childCount;
    public int score;
    private int playerCollisionCount;
    private bool isPerfectFit = false;

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
        if (other.CompareTag("Player"))
        {
            playerCollisionCount++;
            if (playerCollisionCount == childCount)
            {
                isPerfectFit = true;
            }
        }
        else
        {
            cubesDestroyed++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPerfectFit)
            {
                score += 1000;
                Debug.Log("Perfect Fit! Added 1000 points. Total score: " + score);
                isPerfectFit = false;
            }
            else
            {
                AddScore();
            }
            playerCollisionCount = 0; // Reset the count for the next interaction
        }
    }

    private void AddScore()
    {
        int pointsToAdd = 0;

        switch (playerCollisionCount)
        {
            case 1:
                pointsToAdd = 100;
                break;
            case 2:
                pointsToAdd = 300;
                break;
            case 3:
                pointsToAdd = 500;
                break;
            case 4:
                pointsToAdd = 700;
                break;
            case 5:
                pointsToAdd = 1000;
                break;
        }

        score += pointsToAdd;
        Debug.Log("Added " + pointsToAdd + " points. Total score: " + score);
    }
}
