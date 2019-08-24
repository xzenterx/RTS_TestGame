using Models.Units;
using UnityEngine;

namespace Application.Units.Fabrics
{
    public class UnitSpeedFactory : IUnitFabric
    {
        public Unit Create()
        {
            Debug.Log("UnitSpeedFactory-Create");
            return new UnitSpeed();
        }
    }
}