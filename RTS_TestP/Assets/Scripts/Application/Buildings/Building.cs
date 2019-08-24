using UnityEngine;
using System.Collections;

namespace Application.Buildings
{
    public abstract class Building
    {
        public int Level { get; set; }

        public abstract void LevelUp();
    }
}


