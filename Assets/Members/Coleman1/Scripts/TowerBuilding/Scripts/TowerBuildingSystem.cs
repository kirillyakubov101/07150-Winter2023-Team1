using OurGame.Spawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TowerBuildingSystem : MonoBehaviour
{
    #region Building System Variables

    public static TowerBuildingSystem current; //Singleton reference of our script

    RaycastHit rayHit;

    public GridLayout gridLayout;
    private Grid grid;
    private Vector3Int cellPos;
    public static bool canBePlaced = false;
    private bool isTaken = false;

    [SerializeField] private LayerMask layerMask;

    [Header("Object Needed")]
    [SerializeField] private Tilemap towerTileMap;

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
            Ray cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); //playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out rayHit, 100f, layerMask))
            {
                cellPos = gridLayout.WorldToCell(rayHit.point);
                takenAreaTile.transform.position = SnapCoordinateToGrid(rayHit.point);

                if(rayHit.collider.CompareTag("Taken"))
                    isTaken = true;
                else
                    isTaken = false;
            }

            if (!canBePlaced && Input.GetKeyDown(KeyCode.Mouse0) && !isTaken)
            {
                towerToPlace.transform.position = SnapCoordinateToGrid(rayHit.point);
                takenAreaTile.GetComponent<BoxCollider>().enabled = true;
                takenAreaTile = null;
                towerToPlace = null;
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
