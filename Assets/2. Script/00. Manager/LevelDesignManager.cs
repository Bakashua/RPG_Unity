using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignManager : MonoBehaviour
{
    [Header("CLASS")]
    public CharacterSpawner CS;
    public  static LevelDesignManager LDM;

    // The size of the grid in the X and Z dimensions
    public int gridX;
    public int gridZ;
    public int size;
    public Vector3 Offset;

    // The size of each cell in the grid
    public float cellSize = 1.0f;

    public List<GameObject> Tiles = new List<GameObject>();
    public List<GameObject> TilesAllied = new List<GameObject>();
    public List<GameObject> TilesEnemy = new List<GameObject>();
    //[HideInInspector] public List<GameObject> Cells = new List<GameObject>();

    public GameObject startingPoint;

    [Header("UI")]
    public GameObject TileBtn;
    public GameObject BtnParent;

    private void Awake()
    {
        if (LDM != null && LDM != this)
        {
            Destroy(this);
        }
        else
        {
            LDM = this;
        }
    }

    void Start()
    {
        size = gridX * gridZ;
        SetUpGrid();
    }

    void SetUpGrid()
    {
        int i = 0;
        // Loop through the grid in the X and Z dimensions
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                i++;
                // Create a cube primitive at the current grid cell
                GameObject tile = Instantiate(Tiles[0]);
                SetUpTiles(tile, x, z);
                SeparateTileInList(tile, i);
            }
        }
    }

    void SetUpTiles(GameObject tile, int x, int z)
    {
        tile.transform.SetParent(startingPoint.transform);
        tile.transform.localScale = new Vector3(cellSize, 0.05f, cellSize);
        tile.GetComponent<Combat_Tiles>().x = x;
        tile.GetComponent<Combat_Tiles>().z = z;

        // Set the position of the cube to the current grid cell plus the offset in x z and between them
        tile.transform.position = new Vector3(
            (x * Offset.x) + startingPoint.transform.position.x + (tile.transform.localScale.x - startingPoint.transform.localScale.x) / 2,
            1 * Offset.y,
            (z * Offset.z) + startingPoint.transform.position.z + +startingPoint.transform.localScale.x + (tile.transform.localScale.z - startingPoint.transform.localScale.z) / 2
            );
    }

    void SeparateTileInList(GameObject tile, int i)
    {
        Vector3 offsetFaction = new Vector3(5, 0, 0);
        if (i <= size/2)
        {
            TilesAllied.Add(tile);
            CS.heroSpawnPoint.Add(tile.transform);
            //TilesEnemy.Insert(0, tile);
            tile.transform.position = tile.transform.position - offsetFaction;
            TilesEnemy.Reverse();
            //tile.GetComponent<Combat_Tiles>().x = TilesEnemy.IndexOf(tile);
        }

        else if (i > size/2)
        {
            TilesEnemy.Add(tile);
            CS.monsterSpawnPoint.Add(tile.transform);
        }

    }

    public void SetupTileBtn(GameObject tile)
    {

    }


}

