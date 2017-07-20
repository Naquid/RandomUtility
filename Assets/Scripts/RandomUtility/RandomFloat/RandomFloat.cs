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

        [SerializeField, HideInInspector]
        bool isInitialized = false;

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
