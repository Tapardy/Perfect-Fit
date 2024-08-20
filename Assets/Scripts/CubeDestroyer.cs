using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public int cubesDestroyed;
    public int childCount;
    public int score;
    private int _playerCollisionCount;
    private bool _isPerfectFit = false;
    private int _accumulatedPoints = 0;

    [SerializeField] private LayerMask gridCellLayerMask;  
    private HashSet<SpriteRenderer> _currentlyAffectedCells = new HashSet<SpriteRenderer>();
    private HighlightCells _highlightedCells;

    private void Start()
    {
        ChildChecker();
        Debug.Log(cubesDestroyed + " cubes, " + childCount + " children");
    }

    private void Update()
    {
        // Perform raycasts for each child
        foreach (Transform child in transform)
        {
            PerformRaycast(child);
        }
    }

    private void PerformRaycast(Transform child)
    {
        RaycastHit hit;
        Ray ray = new Ray(child.position, -child.forward);

        // Draw the ray in the Scene view for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridCellLayerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            HighlightCells highlight = hit.collider.GetComponentInParent<HighlightCells>();
            SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();

            if (highlight != null && spriteRenderer != null)
            {
                if (!_currentlyAffectedCells.Contains(spriteRenderer))
                {
                    highlight.StartBlinking(spriteRenderer);
                    _currentlyAffectedCells.Add(spriteRenderer);
                    Debug.Log("Started blinking on: " + spriteRenderer.gameObject.name);
                }
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 200f, Color.yellow);

            foreach (var spriteRenderer in _currentlyAffectedCells)
            {
                if (spriteRenderer != null)
                {
                    var highlight = spriteRenderer.GetComponentInParent<HighlightCells>();
                    if (highlight != null)
                    {
                        highlight.StopBlinking(spriteRenderer);
                        Debug.Log("Stopped blinking on: " + spriteRenderer.gameObject.name);
                    }
                }
            }

            _currentlyAffectedCells.Clear();
        }
    }

    public void ChildChecker()
    {
        childCount = gameObject.transform.childCount;
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
        else if (other.CompareTag("GridCell"))
        {
            return;
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
