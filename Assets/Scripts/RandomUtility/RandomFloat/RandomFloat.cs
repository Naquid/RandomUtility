using UnityEngine;

namespace RandomUtility
{

    [System.Serializable]
    public class RandomFloat
    {
        public float minValue = 0.0f;
        public float maxValue = 0.0f;

        [SerializeField, HideInInspector]
        float currentValue = 0.0f;
        bool valueIsSet = false;

        public float GetValue()
        {
            return valueIsSet ? currentValue : GetNewRandom();
        }

        public float GetNewRandom()
        {
            currentValue = Random.Range(minValue, maxValue);
            valueIsSet = true;
            return currentValue;
        }

        public static implicit operator float(RandomFloat randomFloat)
        {
            return randomFloat.GetValue();
        }

        public override string ToString()
        {
            if(valueIsSet)
            {
                return currentValue.ToString();
            }
            else
            {
                GetNewRandom();
                return currentValue.ToString();
            }
        }
    }

}
