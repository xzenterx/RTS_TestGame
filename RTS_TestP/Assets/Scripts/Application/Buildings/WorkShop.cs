using UnityEngine;
using System.Collections;
using Models;

namespace Application.Buildings
{
    public class WorkShop
    {
        public int Level { get; private set; } = 1;                               
        public float ProductionGoodsPercent { get; private set; }     
        public int EveryGSGoods { get; private set; } = 100;                       

        public void LevelUp()
        {
            Level += 1;
            ProductionGoodsPercent += 1.75f;
        }

    }
}
