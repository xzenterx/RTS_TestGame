using UnityEngine;
using System.Collections;

namespace Application.Buildings
{
    public class Walls : Building
    {
        public float DefenseUnitsBase { get; private set; }

        public Walls()
        {
            Name = "Стена";
            Level = 1;
        }

        public override void LevelUp()
        {
            Level++;
            DefenseUnitsBase += 0.05f;
        }


        public void LevelDown()
        {
            if (Level - 5 >= 1)
            {
                Level -= 5;
            }
            else
            {
                Level = 1;
            }
        }

    }
}
