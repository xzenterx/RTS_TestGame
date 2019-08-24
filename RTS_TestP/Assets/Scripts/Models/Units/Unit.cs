using UnityEngine;
using System.Collections;

namespace Models.Units
{
    public abstract class Unit
    {
        public int Speed { get; set; }
        public int Attack { get; set; }
        public float Defense { get; set; }
        public bool OnBase { get; set; }
    }
}

