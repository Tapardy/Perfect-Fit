using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    
    private Vector2 _lastMoveDirection;
    private const int MaxCubes = 5;
    private readonly List<GameObject> _cubes = new List<GameObject>();
    private Color _originalColor = Color.black;

    void Start()
    {
        SpawnCube(transform.position);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
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

                Move(moveDirection);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)transform.position + direction;
        
        GameObject existingCube = _cubes.Find(cube => (Vector2)cube.transform.position == newPosition);
        
        if (existingCube != null)
        {
            int index = _cubes.IndexOf(existingCube);
            
            if (index == _cubes.Count - 2) 
            {
                Destroy(_cubes[_cubes.Count - 1]);
                _cubes.RemoveAt(_cubes.Count - 1);
                transform.position = newPosition;
                _lastMoveDirection = direction;
                
                UpdateCubeColors(true);
            }
            else
            {
                StartCoroutine(FlashRed());
            }
        }
        else
        {
            if (_cubes.Count >= MaxCubes)
            {
                Destroy(_cubes[0]);
                _cubes.RemoveAt(0);
            }
            SpawnCube(newPosition);
            transform.position = newPosition;
            _lastMoveDirection = direction;
            
            UpdateCubeColors(true);
        }
    }

    private void UpdateCubeColors(bool possible)
    {
        for (int i = 0; i < _cubes.Count; i++)
        {
            _cubes[i].GetComponent<Renderer>().material.color = (i == _cubes.Count - 1) ? (possible ? Color.green : Color.red) : _originalColor;
        }
    }

    private IEnumerator FlashRed()
    {
        UpdateCubeColors(false);
        yield return new WaitForSeconds(0.2f);
        UpdateCubeColors(true);
    }

    private void SpawnCube(Vector2 position)
    {
        GameObject newCube = Instantiate(cubePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        _cubes.Add(newCube);
    }
}