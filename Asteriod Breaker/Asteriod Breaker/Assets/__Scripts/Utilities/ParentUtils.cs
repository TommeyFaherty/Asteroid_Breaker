using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utilities
{
    public class ParentUtils
    {
        public static GameObject GetEnemyParent()
        {
            return GetParent("EnemyParent");
        }

        public static GameObject GetBulletParent()
        {
            return GetParent("BulletParent");
        }

        // method to find parent of enemies
        public static GameObject GetParent(string requiredParent)
        {
            var parent = GameObject.Find(requiredParent);
            if( !parent)
            {
                parent = new GameObject(requiredParent);
            }
            return parent;
        }

    }
}

