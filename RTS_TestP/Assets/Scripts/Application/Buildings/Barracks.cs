using UnityEngine;
using System.Collections;
using Models;
using Models.Units;
using Application.Units.Fabrics;

namespace Application.Buildings
{
    public class Barracks
    {
        public int Level { get; private set; } = 1;
        public int LimitUnits { get; private set; } = 300;

        public void LevelUp()
        {
            Level += 1;
            LimitUnits += 100;
        }

        public void LimitUp(int limit)
        {
            LimitUnits += limit;
        }

        public bool CheckPesourcesForCreateUnit(PlayerResources playerResources, int countUnit)
        {
            if (playerResources.People * countUnit <= playerResources.People &&
                playerResources.Goods * countUnit <= playerResources.Goods &&
                playerResources.Loans * countUnit <= playerResources.Loans)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void BuyUnits(PlayerResources playerResources, int countUnit)
        {
            playerResources.People -= countUnit;
            playerResources.Loans -= playerResources.Loans * 10;
            playerResources.Goods -= playerResources.Goods * 10;
        }

        public Unit CreateUnit(IUnitFabric unitFabric)
        {
            return unitFabric.Create();
        }
    }
}