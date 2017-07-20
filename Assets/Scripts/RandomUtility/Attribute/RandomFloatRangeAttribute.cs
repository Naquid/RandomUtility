using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomUtility
{

    public class RandomFloatRangeAttribute : PropertyAttribute
    {
        public float minLimit;
        public float maxLimit;

        public RandomFloatRangeAttribute(float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
    }

}