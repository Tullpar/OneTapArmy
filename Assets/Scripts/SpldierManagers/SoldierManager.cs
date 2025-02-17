using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierManager : MonoBehaviour
{

    public GameObject TargetParticle;
    public GameObject TargetLine;

    CastleBrain CastleBrain;

    public UnitSpawner UnitSpawner;

    public SoldierBrain.UnitType SelectedType;

    public bool SelectAll;

    public float sidewaysSpacing = 1f;
    public float rowSpacing = 1f;

    private void Start()
    {
        CastleBrain = GetComponentInParent<CastleBrain>();
    }

    void Update()
    {
        DeselectAll();
        List<SoldierBrain> soldiers = new List<SoldierBrain>();

        if (!SelectAll)
        {
            soldiers = UnitSpawner.GetUnitsByType(SelectedType);
        }
        else
        {
            soldiers = UnitSpawner.SpawnedSoldiers;
        }
        List<SoldierBrain> availableSoldiers = SelectAvailableSoldiers(soldiers);

        if (CastleBrain.IsPlayer)
        {

            if (Input.GetMouseButton(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject() && Camera.main.ScreenToViewportPoint(Input.mousePosition).y > 0.25f)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Ground")))
                    {
                        SendSoldiers(availableSoldiers, hit.point);
                    }
                }
            }
        }
        else if(UnitSpawner.SpawnedSoldiers.Count > 10)
        {
            SendSoldiers(availableSoldiers, CastleManager.instance.PlayerCastle.transform.position);
        }
    }

    List<SoldierBrain> SelectAvailableSoldiers(List<SoldierBrain> soldiers)
    {
        List<SoldierBrain> availableSoldiers = new List<SoldierBrain>();
        for (int i = 0; i < soldiers.Count; i++)
        {
            if (soldiers[i].IsIdle())
            {
                availableSoldiers.Add(soldiers[i]);
                soldiers[i].Select();
            }
        }
        return availableSoldiers;
    }


    public void SendSoldiers(List<SoldierBrain> soldiers,Vector3 position)
    {
        if (CastleBrain.IsPlayer)
        {
            TargetParticle.transform.position = position;
            TargetLine.transform.position = new Vector3(TargetLine.transform.position.x,TargetParticle.transform.position.y,TargetLine.transform.position.z);
            TargetLine.transform.LookAt(position);
            TargetLine.transform.localScale = Vector3.one * Vector3.Distance(position, TargetLine.transform.position);
        }
        List<SoldierBrain> soldiersToSend = new List<SoldierBrain>(soldiers);
        for (int i = 0; i < soldiersToSend.Count; i++)
        {
            UnitSpawner.RemoveFromFreshSpawn(soldiersToSend[i]);
            if (soldiersToSend[i].IsIdle())
            {
                Vector3 soldierTargetPosition = GetGridPosition(position, i);
                soldiersToSend[i].Target.SetTarget(soldierTargetPosition);
            }
        }
    }

    public void SendSpawnPosition(List<SoldierBrain> soldiers, Vector3 position)
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            if (soldiers[i].IsIdle())
            {
                Vector3 soldierTargetPosition = GetGridPosition(position, i);
                soldiers[i].Target.SetTarget(soldierTargetPosition);
            }
        }

    }

    public void DeselectAll()
    {
        for (int i = 0; i < UnitSpawner.SpawnedSoldiers.Count; i++)
        {
            UnitSpawner.SpawnedSoldiers[i].Deselect();
        }
    }

    public void SelectType(int unitTypeIndex)
    {
        SelectedType = (SoldierBrain.UnitType)unitTypeIndex;
        SelectAll = false;
    }

    public void SelectAllTypes()
    {
        SelectAll = true;
    }


    Vector3 GetGridPosition(Vector3 targetPosition,int index)
    {

        int rowSize = 8;

        int row = Mathf.FloorToInt((float)index / (float)rowSize);

        int indexInRow = index - row * rowSize;
        int side = indexInRow % 2 == 0 ? 1 : -1;


        return targetPosition + (transform.right * side * sidewaysSpacing * indexInRow / 2) - transform.forward * row * rowSpacing;

    }
}
