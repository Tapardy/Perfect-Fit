using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public int cubesDestroyed;
    public int childCount;
    public int score;
    private int _playerCollisionCount;
    private bool _isPerfectFit = false;
    private int _accumulatedPoints = 0;  // New field to accumulate points

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
            _playerCollisionCount++;
            if (_playerCollisionCount >= childCount)
            {
                _isPerfectFit = true;
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
            if (_isPerfectFit)
            {
                _accumulatedPoints += 1000;
                Debug.Log("Perfect Fit! Accumulated 1000 points.");
                _isPerfectFit = false;
            }
            else
            {
                AddScore();
            }

            if (_accumulatedPoints > 0)
            {
                ScoreManager.Instance.AddPoints(_accumulatedPoints);
                Debug.Log("Added accumulated points: " + _accumulatedPoints);
                _accumulatedPoints = 0;
            }

            _playerCollisionCount = 0;
        }
    }

    private void AddScore()
    {
        int pointsToAdd = 0;

        switch (_playerCollisionCount)
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

        _accumulatedPoints += pointsToAdd;
        Debug.Log("Accumulated " + pointsToAdd + " points.");
    }
}
