using Models.Units;
using UnityEngine;

namespace Application.Units.Fabrics
{
    public class UnitAttackFactory : IUnitFabric
    {
        public Unit Create()
        {
            Debug.Log("UnitAttackFactory-Create");
            return new UnitAttack();
        }
    }
}