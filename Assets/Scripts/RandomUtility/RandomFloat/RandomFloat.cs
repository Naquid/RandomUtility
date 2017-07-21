using UnityEngine;

namespace RandomUtility
{

    [System.Serializable]
    public struct RandomFloat
    {
        public float minValue;
        public float maxValue;

        [SerializeField, HideInInspector]
        float currentValue;

        [SerializeField, HideInInspector]
        bool isInitialized;

        public float GetValue()
        {
            return isInitialized ? currentValue : GetNewRandom();
        }

        public float GetNewRandom()
        {
            currentValue = Random.Range(minValue, maxValue);
            isInitialized = true;
            return currentValue;
        }

        public static implicit operator float(RandomFloat randomFloat)
        {
            return randomFloat.GetValue();
        }

        public override string ToString()
        {
            if(isInitialized)
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
