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

    public Sprite spriteBase;

    private PlayerResources playerResources = new PlayerResources();
    private List<ResidentialModule> residentialModuleList = new List<ResidentialModule>();
    private List<WorkShop> workShopList = new List<WorkShop>();
    private Portal portal = new Portal();
    private Barracks barracks = new Barracks();
    private Walls walls = new Walls();
    private Production production = new Production();

    private List<Unit> unitAttacks = new List<Unit>();
    private List<Unit> unitDefense = new List<Unit>();
    private List<Unit> unitSpeed = new List<Unit>();

    private bool IsTraining = false;
    private UnityEvent ChangingUnitsSpot = new UnityEvent();

    private void Awake()
    {
        residentialModuleList.Add(new ResidentialModule());
        workShopList.Add(new WorkShop());
    }

    private void Start()
    {
        ChangingUnitsSpot.AddListener(UnitsOnBase);  
    }

    public void GameStep()
    {
        foreach (ResidentialModule residentialModule in residentialModuleList)
        {
            production.ProductionPeople(residentialModule, playerResources);
        }

        foreach (WorkShop workShop in workShopList)
        {
            production.ProductionGoods(workShop, portal, playerResources);
        }

        production.ProductionLoans(portal, playerResources);
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

    public void BuyLevelUpBuildings<T>(T building,int level)
    {
        int cost = Convert.ToInt32(100 + (level + 1) / 0.985f);

        if (playerResources.Goods >= cost && playerResources.Loans >= cost)
        {
            playerResources.Goods -= cost;
            playerResources.Loans -= cost;
        }

        //building.LevelUp();
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

            barracks.LimitUp(200);
            walls.LevelDown();
        }
    }

    private void BaseExpansionBlock()
    {
        Vector2 newBasePos = new Vector2();

        newBasePos.x = gameObject.transform.position.x;
        newBasePos.y = gameObject.transform.position.y + 1;

        Instantiate(spriteBase, newBasePos, Quaternion.identity);
    }
    
}
