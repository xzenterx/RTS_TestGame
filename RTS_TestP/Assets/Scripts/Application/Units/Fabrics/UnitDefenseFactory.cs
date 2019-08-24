using Models.Units;
using UnityEngine;

namespace Application.Units.Fabrics
{
    public class UnitDefenseFactory : IUnitFabric
    {
        public Unit Create()
        {
            Debug.Log("UnitDefenseFactory-Create");
            return new UnitDefense();
        }
    }
}