using UnityEngine;
using System.Collections;

namespace Application.Buildings
{
    public abstract class Building
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public abstract void LevelUp();

    }
}


