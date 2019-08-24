using UnityEngine;
using System.Collections;

namespace Application.Buildings
{
    public class Portal
    {
        public int Level { get; private set; } = 1;
        public float BuyGoodsRate { get; private set; } = 1.5f;
        public float SellGoodsRate { get; private set; } = 0.5f;
        public float ProductionLoansPercent { get; private set; }
        public float ProductionGoodsPercent { get; private set; }

        public void LevelUp()
        {
            Level += 1;
            ProductionLoansPercent += 0.0025f;
            ProductionLoansPercent += 0.0025f;

            float diffPercent = 0;
            diffPercent = (BuyGoodsRate - SellGoodsRate) * 0.01f;

            if (BuyGoodsRate > 0)
                BuyGoodsRate -= diffPercent;
            if (SellGoodsRate > 0)
                SellGoodsRate += diffPercent;
        }

        public void BuyGoods(int countGoods, ref float goods, ref float loans)
        {
            string msg = null;

            float needLoans = countGoods * BuyGoodsRate;

            if (needLoans > loans)
            {
                msg = "Not enough loans";
                Debug.Log(msg);
            }
            else
            {
                loans -= needLoans;
                goods += countGoods;

                msg = "The purchase is successful";
                Debug.Log(msg);
            }
        }

        public void SellGoods(int countGoods, ref float goods, ref float loans)
        {
            string msg = null;

            if (countGoods > goods)
            {
                msg = "Not enough goods";
                Debug.Log(msg);
            }
            else
            {
                loans += countGoods * SellGoodsRate;
                goods -= countGoods;

                msg = "Sale successful";
                Debug.Log(msg);
            }
        }

    }
}
