using UnityEngine;
using System.Collections;
using Models;

namespace Application.Buildings
{
    public class WorkShop : Building
    {                              
        public float ProductionGoodsPercent { get; private set; }     
        public int EveryGSGoods { get; private set; } = 100;        
        
        public WorkShop()
        {
            Level = 1;
        }

        public override void LevelUp()
        {
            Level++;
            ProductionGoodsPercent += 1.75f;
        }

    }
}
