using UnityEngine;
using System.Collections;

namespace Application.Buildings
{
    public class Walls
    {
        public int Level { get; private set; } = 1;
        public float DefenseUnitsBase { get; private set; }

        public void LevelUp()
        {
            Level += 1;
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
