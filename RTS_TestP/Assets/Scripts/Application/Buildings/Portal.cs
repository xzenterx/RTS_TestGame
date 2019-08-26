using UnityEngine;
using System.Collections;
using Models;

namespace Application.Buildings
{
    public class Portal : Building
    {
        public float BuyGoodsRate { get; private set; } = 1.5f;
        public float SellGoodsRate { get; private set; } = 0.5f;
        public float ProductionLoansPercent { get; private set; }
        public float ProductionGoodsPercent { get; private set; }

        public Portal()
        {
            Name = "Портал";
            Level = 1;
        }

        public override void LevelUp()
        {
            Level ++;
            ProductionLoansPercent += 0.0025f;
            ProductionLoansPercent += 0.0025f;

            float diffPercent = 0;
            diffPercent = (BuyGoodsRate - SellGoodsRate) * 0.01f;

            if (BuyGoodsRate > 0)
                BuyGoodsRate -= diffPercent;
            if (SellGoodsRate > 0)
                SellGoodsRate += diffPercent;
        }

        public void BuyGoods(int countGoods, PlayerResources playerResources)
        {
            string msg = null;

            float needLoans = countGoods * BuyGoodsRate;

            if (needLoans > playerResources.Loans)
            {
                msg = "Not enough loans";
                Debug.Log(msg);
            }
            else
            {
                playerResources.Loans -= needLoans;
                playerResources.Goods += countGoods;

                msg = "The purchase is successful";
                Debug.Log(msg);
            }
        }

        public void SellGoods(int countGoods, PlayerResources playerResources)
        {
            string msg = null;

            if (countGoods > playerResources.Goods)
            {
                msg = "Not enough goods";
                Debug.Log(msg);
            }
            else
            {
                playerResources.Loans += countGoods * SellGoodsRate;
                playerResources.Goods -= countGoods;

                msg = "Sale successful";
                Debug.Log(msg);
            }
        }

    }
}
