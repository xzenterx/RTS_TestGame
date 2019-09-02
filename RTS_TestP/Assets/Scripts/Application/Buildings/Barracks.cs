using UnityEngine;
using System.Collections;
using Models;
using Models.Units;
using Application.Units.Fabrics;

namespace Application.Buildings
{
    public class Barracks : Building
    {   
        public int LimitUnits { get; private set; } = 300;

        public Barracks()
        {
            Name = "Барраки";
            Level = 1;
        }

        public override void LevelUp()
        {
            Level++;
            LimitUnits += 100;
        }

        public void LimitUp()
        {
            LimitUnits += 200;
        }

        public bool CheckPesourcesForCreateUnit(PlayerResources playerResources, int countUnit)
        {
            if (countUnit <= playerResources.People &&
                10 * countUnit <= playerResources.Goods &&
                10 * countUnit <= playerResources.Loans)
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
            playerResources.Loans -= countUnit * 10;
            playerResources.Goods -= countUnit * 10;
        }

        public Unit CreateUnit(IUnitFabric unitFabric)
        {
            return unitFabric.Create();
        }
        
    }
}