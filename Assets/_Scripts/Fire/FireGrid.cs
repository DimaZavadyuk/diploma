using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireGrid : MonoBehaviour
{
    public BoxCollider fireAreaCollider; 
    public GameObject firePrefab;
    public float cellSize = 1f;  
    public float minSpreadDelay = 0.5f; 
    public float maxSpreadDelay = 2f;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Transform _vfxFirstpos;
    [SerializeField] private List<Vector2Int> startFires;  // List of starting positions
    private FireCell[,] fireGrid;
    private int gridWidth, gridHeight;
    private Vector3 gridOrigin;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        Vector3 colliderSize = fireAreaCollider.size;
        gridOrigin = fireAreaCollider.bounds.min;

        gridWidth = Mathf.CeilToInt(colliderSize.x / cellSize);
        gridHeight = Mathf.CeilToInt(colliderSize.z / cellSize);

        fireGrid = new FireCell[gridWidth, gridHeight];
    }

    void StartFire(int x, int y)
    {
        Instantiate(_vfx, _vfxFirstpos.position, quaternion.identity);
        if (IsWithinGrid(x, y))
        {
            StartCoroutine(SpreadFire(x, y)); 
        }
        else
        {
            Debug.LogError($"StartFire coordinates ({x}, {y}) are out of bounds.");
        }
    }

    IEnumerator SpreadFire(int x, int y)
    {
        if (fireGrid[x, y] != null && fireGrid[x, y].IsBurning())
            yield break;  

        Vector3 cellPosition = gridOrigin + new Vector3(x * cellSize + cellSize / 2, 0, y * cellSize + cellSize / 2);
        GameObject fireObject = Instantiate(firePrefab, cellPosition, Quaternion.identity);
        FireCell fireCell = fireObject.GetComponent<FireCell>();
        if (fireCell != null)
        {
            fireCell.StartFire(fireObject);  
            fireGrid[x, y] = fireCell;  

            float delay = Random.Range(minSpreadDelay, maxSpreadDelay);
            yield return new WaitForSeconds(delay);

            // Spread to adjacent cells independently
            StartCoroutine(SpreadToAdjacentCells(x, y));
        }
        else
        {
            Debug.LogError("FireCell component missing on firePrefab.");
        }
    }

    IEnumerator SpreadToAdjacentCells(int x, int y)
    {
        // Spread to adjacent cells
        if (IsWithinGrid(x + 1, y)) 
        {
            if (fireGrid[x, y] == null || !fireGrid[x, y].IsBurning())
                yield break; // Exit if the fire object is destroyed
            yield return StartCoroutine(SpreadFire(x + 1, y));  // Right
        }

        if (IsWithinGrid(x - 1, y)) 
        {
            if (fireGrid[x, y] == null || !fireGrid[x, y].IsBurning())
                yield break; // Exit if the fire object is destroyed
            yield return StartCoroutine(SpreadFire(x - 1, y));  // Left
        }

        if (IsWithinGrid(x, y + 1)) 
        {
            if (fireGrid[x, y] == null || !fireGrid[x, y].IsBurning())
                yield break; // Exit if the fire object is destroyed
            yield return StartCoroutine(SpreadFire(x, y + 1));  // Up
        }

        if (IsWithinGrid(x, y - 1)) 
        {
            if (fireGrid[x, y] == null || !fireGrid[x, y].IsBurning())
                yield break; // Exit if the fire object is destroyed
            yield return StartCoroutine(SpreadFire(x, y - 1));  // Down
        }
    }

    bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    public void RefreshFire()
    {
        foreach (var start in startFires)
        {
            StartFire(start.x, start.y);
        }
    }

    public void ClearAllFires()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (fireGrid[x, y] != null && fireGrid[x, y].IsBurning())
                {
                    Destroy(fireGrid[x, y].gameObject);
                    fireGrid[x, y] = null; 
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (fireAreaCollider == null) return;

        Vector3 colliderSize = fireAreaCollider.size;
        Vector3 gridOrigin = fireAreaCollider.bounds.min;

        int gridWidth = Mathf.CeilToInt(colliderSize.x / cellSize);
        int gridHeight = Mathf.CeilToInt(colliderSize.z / cellSize);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = gridOrigin + new Vector3(x * cellSize + cellSize / 2, 0, y * cellSize + cellSize / 2);
                Vector3 cellSizeVector = new Vector3(cellSize, 0.1f, cellSize); // Adjust the height for visibility

                // Draw start cells in green
                if (startFires.Contains(new Vector2Int(x, y)))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawWireCube(cellPosition, cellSizeVector);
            }
        }
    }
}
