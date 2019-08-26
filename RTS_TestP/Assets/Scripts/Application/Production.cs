using UnityEngine;
using System;
using System.Collections;
using Models;
using Application.Buildings;
using System.Collections.Generic;

namespace Application
{
    public class Production
    {
        public void ProductionPeople(List<ResidentialModule> residentialModuleList, PlayerResources playerResources)
        {

            int limitBase = 0;

            foreach (ResidentialModule residentialModule in residentialModuleList)
            {
                limitBase += residentialModule.LimitBase;
            }

            foreach (ResidentialModule residentialModule in residentialModuleList)
            {
                if (playerResources.People < limitBase)
                {
                    playerResources.People += residentialModule.EveryGSPopulation + Convert.ToInt32(residentialModule.EveryGSPopulation * residentialModule.PopulationGrowthPercent);
                }
            }


            Debug.Log("People: " + playerResources.People);
        }

        public void ProductionGoods(WorkShop workShop, Portal portal, PlayerResources playerResources)
        {
            playerResources.Goods += workShop.EveryGSGoods + (workShop.EveryGSGoods * (workShop.ProductionGoodsPercent + portal.ProductionGoodsPercent));
            Debug.Log("Goods: " + playerResources.Goods);
        }

        public void ProductionLoans(Portal portal, PlayerResources playerResources)
        {
            playerResources.Loans += playerResources.People * (0.1f + portal.ProductionLoansPercent);
            Debug.Log("Loans: " + playerResources.Loans);                                                                           
        }
    }
}

