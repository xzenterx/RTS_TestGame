using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Models;
using Models.Units;
using Application.Buildings;
using Application;
using Application.Units.Fabrics;

public class BaseScript : MonoBehaviour
{
    public GameObject spriteBase;

    public PlayerResources playerResources = new PlayerResources();
    public List<ResidentialModule> residentialModuleList = new List<ResidentialModule>();
    public List<WorkShop> workShopList = new List<WorkShop>();
    public Portal portal = new Portal();
    public Barracks barracks = new Barracks();
    public Walls walls = new Walls();
    public Production production = new Production();

    public List<Unit> unitAttacks = new List<Unit>();
    public List<Unit> unitDefense = new List<Unit>();
    public List<Unit> unitSpeed = new List<Unit>();

    private UnityEvent ChangingUnitsSpot = new UnityEvent();

    public UnityEvent ChangingResources = new UnityEvent();
    public UnityEvent ChangingLevelBuildingEvent = new UnityEvent();

    private void Awake()
    {
        if (residentialModuleList.Count == 0) InitModuls();
    }

    public void Start()
    {
        ChangingUnitsSpot.AddListener(UnitsOnBase);
    }

    public void GameStep()
    {

        production.ProductionPeople(residentialModuleList, playerResources);

        foreach (WorkShop workShop in workShopList)
        {
            production.ProductionGoods(workShop, portal, playerResources);
        }

        production.ProductionLoans(portal, playerResources);

        ChangingResources.Invoke();

    }

    private void InitModuls()
    {
        residentialModuleList.Add(new ResidentialModule());
        workShopList.Add(new WorkShop());
    }

    public void CreateUnit(int countUnitsAttacks, int countUnitsDefense, int countUnitsSpeed)
    {
        if (countUnitsAttacks > 0 && barracks.CheckPesourcesForCreateUnit(playerResources, countUnitsAttacks))
        {
            for (int i = 0; i < countUnitsAttacks; i++)
            {
                unitAttacks.Add(barracks.CreateUnit(new UnitAttackFactory()));
            }
            barracks.BuyUnits(playerResources, countUnitsAttacks);
        }

        if (countUnitsDefense > 0 && barracks.CheckPesourcesForCreateUnit(playerResources, countUnitsDefense))
        {
            for (int i = 0; i < countUnitsDefense; i++)
            {
                unitDefense.Add(barracks.CreateUnit(new UnitDefenseFactory()));
            }
            barracks.BuyUnits(playerResources, countUnitsDefense);
        }

        if (countUnitsSpeed > 0 && barracks.CheckPesourcesForCreateUnit(playerResources, countUnitsSpeed))
        {
            for (int i = 0; i < countUnitsSpeed; i++)
            {
                unitSpeed.Add(barracks.CreateUnit(new UnitSpeedFactory()));
            }
            barracks.BuyUnits(playerResources, countUnitsSpeed);
        }
        ChangingUnitsSpot.Invoke();
    }

    public void UnitsOnBase()
    {
        for (int i = 0; i < unitAttacks.Count; i++)
        {
            if (unitAttacks[i].OnBase)
            {
                unitAttacks[i].Defense += walls.DefenseUnitsBase;
            }
            else
            {
                unitAttacks[i].Defense -= walls.DefenseUnitsBase;
            }
        }

        for (int i = 0; i < unitDefense.Count; i++)
        {
            if (unitDefense[i].OnBase)
            {
                unitDefense[i].Defense += walls.DefenseUnitsBase;
            }
            else
            {
                unitDefense[i].Defense -= walls.DefenseUnitsBase;
            }
        }

        for (int i = 0; i < unitSpeed.Count; i++)
        {
            if (unitSpeed[i].OnBase)
            {
                unitSpeed[i].Defense += walls.DefenseUnitsBase;
            }
            else
            {
                unitSpeed[i].Defense -= walls.DefenseUnitsBase;
            }
        }
    }

    public void BuyLevelUpBuildings(Building building)
    {
        int cost = Convert.ToInt32(100 + (building.Level + 1) / 0.985f);

        if (playerResources.Goods >= cost && playerResources.Loans >= cost)
        {
            playerResources.Goods -= cost;
            playerResources.Loans -= cost;
            building.LevelUp();
            ChangingLevelBuildingEvent.Invoke();
        }

    }

    public void BaseExpansion()
    {
        if (playerResources.Goods >= 1000 && playerResources.Loans >= 1000 && playerResources.People >= 500)
        {
            playerResources.Goods -= 1000;
            playerResources.Loans -= 1000;
            playerResources.People -= 500;

            BaseExpansionBlock();

            residentialModuleList.Add(new ResidentialModule());
            workShopList.Add(new WorkShop());

            barracks.LimitUp();
            walls.LevelDown();

            ChangingLevelBuildingEvent.Invoke();
        }
    }

    private void BaseExpansionBlock()
    {
        Vector3 newBasePos;

        newBasePos.x = gameObject.transform.position.x + UnityEngine.Random.Range(-1, 1);
        newBasePos.y = gameObject.transform.position.y + UnityEngine.Random.Range(-1, 1);
        newBasePos.z = 0;

        GameObject obj = Instantiate(spriteBase, newBasePos, Quaternion.identity);
        obj.transform.position = newBasePos;
    }

}
