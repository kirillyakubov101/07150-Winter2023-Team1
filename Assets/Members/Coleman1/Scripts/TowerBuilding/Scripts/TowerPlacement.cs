using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject currentTower;
    [SerializeField] private Camera playerCamera;

    public void SetTowerToPlace(GameObject tower)
    {
        currentTower = Instantiate(tower, Vector3.zero, Quaternion.Euler(0, -90, 0));
    }

    #region Default Unity Methods

    private void FixedUpdate()
    {
        if (currentTower != null)
        {
            Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit rayHitInfo, 100f))
            {
                currentTower.transform.position = rayHitInfo.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentTower = null;
            }
        }
    }

    #endregion
}
