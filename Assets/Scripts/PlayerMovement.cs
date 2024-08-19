using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int maxCubes = 5;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float initialHoldMoveInterval = 0.4f; 
    [SerializeField] private float subsequentHoldMoveInterval = 0.15f; 

    private readonly List<GameObject> _cubes = new List<GameObject>();
    private Vector2 _movementDirection;
    private Coroutine _holdMovementCoroutine;
    [SerializeField] private float _playerPosZ;
    void Start()
    {
        SpawnCube(transform.position);
        UpdateCubeColors(true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();

        if (moveDirection != Vector2.zero)
        {
            if (Mathf.Abs(moveDirection.x) >= Mathf.Abs(moveDirection.y))
            {
                moveDirection = new Vector2(Mathf.Sign(moveDirection.x), 0);
            }
            else
            {
                moveDirection = new Vector2(0, Mathf.Sign(moveDirection.y));
            }

            _movementDirection = moveDirection;

            if (context.phase == InputActionPhase.Performed)
            {
                Move(_movementDirection); 
                if (_holdMovementCoroutine == null)
                {
                    _holdMovementCoroutine = StartCoroutine(HoldMovement());
                }
            }
        }
        
        if (context.phase == InputActionPhase.Canceled)
        {
            if (_holdMovementCoroutine != null)
            {
                StopCoroutine(_holdMovementCoroutine);
                _holdMovementCoroutine = null;
            }
        }
    }

    private IEnumerator HoldMovement()
    {
        yield return new WaitForSeconds(initialHoldMoveInterval);
        while (true)
        {
            Move(_movementDirection); 
            yield return new WaitForSeconds(subsequentHoldMoveInterval);
        }
    }

    private void Move(Vector2 direction)
    {
        Vector2 currentPosition = transform.position;
        Vector2 futurePosition = currentPosition + direction;

        if (IsBlockedByWall(direction))
        {
            StartCoroutine(FlashRed());
            return;
        }

        GameObject existingCube = _cubes.Find(cube => (Vector2)cube.transform.position == futurePosition);

        if (existingCube != null)
        {
            int index = _cubes.IndexOf(existingCube);

            if (index == _cubes.Count - 2)
            {
                AudioSource[] audioSources = _cubes[index].GetComponents<AudioSource>();
                audioSources[1].Play(); 
                Destroy(_cubes[_cubes.Count - 1]);
                _cubes.RemoveAt(_cubes.Count - 1);
                transform.position = futurePosition;
                UpdateCubeColors(true);
            }
            else
            {
                StartCoroutine(FlashRed());
            }
        }
        else
        {
            if (_cubes.Count >= maxCubes)
            {
                StartCoroutine(DeleteOldestCube());
            }
            SpawnCube(futurePosition);
            transform.position = futurePosition;

            UpdateCubeColors(true);
        }
    }

    private bool IsBlockedByWall(Vector2 direction)
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = new Vector3(direction.x, direction.y, 0).normalized;

        // Draw the ray in the scene view for debugging
        Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, raycastDistance, wallLayerMask))
        {
            Debug.Log("Wall detected: " + hit.collider.name);
            return true;
        }

        return false;
    }


    private void UpdateCubeColors(bool possible)
    {
        for (int i = 0; i < _cubes.Count; i++)
        {
            Renderer cubeRenderer = _cubes[i].GetComponent<Renderer>();
            if (i == _cubes.Count - 1)
            {
                cubeRenderer.material.color = possible ? new Color(0,1,0,0.75f) : Color.red;
            }
            else if (i == 0)
            {
                cubeRenderer.material.color = new Color(1f, 0.647f, 0f, 0.8f);
            }
            else
            {
                cubeRenderer.material.color = Color.yellow;
            }
        }
    }

    private IEnumerator FlashRed()
    {
        UpdateCubeColors(false);
        GameObject newestCube = _cubes[_cubes.Count - 1];

        AudioSource[] audioSources = newestCube.GetComponents<AudioSource>();
        audioSources[2].Play();  
        
        yield return new WaitForSeconds(0.2f);
        UpdateCubeColors(true);
    }

    private IEnumerator DeleteOldestCube()
    {
        if (_cubes.Count > 0)
        {
            GameObject oldestCube = _cubes[0];
            Animation cubeAnimation = oldestCube.GetComponent<Animation>();
            
            _cubes.RemoveAt(0);
            
            if (cubeAnimation != null && cubeAnimation.GetClip("delete") != null)
            {
                cubeAnimation.Play("delete");
                yield return new WaitForSeconds(cubeAnimation.GetClip("delete").length);
            }
            else
            {
                yield return new WaitForSeconds(0.15f);
            }

            Destroy(oldestCube);
        }
        else
        {
            yield return null;
        }
    }

    private void SpawnCube(Vector2 position)
    {
        GameObject newCube = Instantiate(cubePrefab, new Vector3(position.x, position.y, _playerPosZ), Quaternion.identity);
        _cubes.Add(newCube);
        UpdateCubeColors(true);
    }
}
