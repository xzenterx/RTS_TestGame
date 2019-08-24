using UnityEngine;
using System.Collections;
using System;
using Models;

namespace Application.Buildings
{
    public class ResidentialModule
    {
        public int Level { get; private set; } = 1;
        public int LimitBase { get; private set; } = 1000;
        public float PopulationGrowthPercent { get; private set; }
        public int EveryGSPopulation { get; private set; } = 100;

        public void LevelUp()
        {
            Level += 1;
            LimitBase += 200;
            PopulationGrowthPercent += 0.05f;
        }
        
    }
}
