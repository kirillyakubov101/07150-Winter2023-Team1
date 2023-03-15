using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TowerBuildingSystem : MonoBehaviour
{
    #region Building System Variables

    public static TowerBuildingSystem current; //Singleton reference of our script

    public GridLayout gridLayout;
    private Grid grid;
    private Vector3Int cellPos;
    private bool canBePlaced = false;

    [SerializeField] private LayerMask terrainLayer;

    [Header("Object Needed")]
    [SerializeField] private Tilemap towerTileMap;
    [SerializeField] private Camera playerCamera;

    private GameObject towerToPlace = null;
    private GameObject takenAreaTile = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (towerToPlace != null)
        {
            Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(cameraRay, out rayHit, 100f, terrainLayer))
            {
                cellPos = gridLayout.WorldToCell(rayHit.point);
                takenAreaTile.transform.position = SnapCoordinateToGrid(rayHit.point);

                if(rayHit.collider.CompareTag("Taken"))
                    canBePlaced = false;
                else
                    canBePlaced = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (canBePlaced)
                {
                    towerToPlace.transform.position = SnapCoordinateToGrid(rayHit.point);
                    takenAreaTile.GetComponent<BoxCollider>().enabled = true;
                    takenAreaTile = null;
                    towerToPlace = null;
                }
            }
        }
    }

    #endregion

    #region Buidling System Methods

    public void SetTakenTile(GameObject takenTile)
    {
        if (takenAreaTile == null)
            takenAreaTile = Instantiate(takenTile, Vector3.zero, takenTile.transform.rotation);
    }

    public void SetTowerToPlace(GameObject tower)
    {
        if(towerToPlace == null) 
            towerToPlace = Instantiate(tower, Vector3.zero, tower.transform.rotation);
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        position = grid.GetCellCenterWorld(cellPos);

        return position;
    }

    #endregion
}
