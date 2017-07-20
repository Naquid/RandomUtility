using UnityEngine;
using UnityEngine.Assertions;

namespace RandomUtility
{

    public enum DistributionMode
    {
        Normal,
        Slope,
        Curve,
        Exp,
        List
    }

    [System.Serializable]
    public struct ProbabilityValue
    {
        public float value;
        public float probability;
    }

    [System.Serializable]
    public class RandomFloatDistribution
    {
        public DistributionMode mode = DistributionMode.Curve;

        [SerializeField]
        float minValue = 0.0f;
        [SerializeField]
        float maxValue = 0.0f;

        public AnimationCurve curve = null;
        public float exp = 0.0f;
        public float slope = 0.0f;
        public float stdDev = 0.1f;
        public float mean = 0.5f;

        [SerializeField]
        ProbabilityValue[] probabilityList; //Cache values from list so don't allow to directly change it
        float totalProbability = 0.0f;
        bool totalProbabilityIsSet = false;

        [SerializeField, HideInInspector]
        float currentValue = 0.0f;

        bool isInitialized = false;

        public float Value
        {
            get { return isInitialized ? currentValue : GetNewRandom(); }
        }

        public static implicit operator float(RandomFloatDistribution randomFloat)
        {
            return randomFloat.Value;
        }

        public float MinValue
        {
            get
            {
                if (isInitialized)
                {
                    return minValue;
                }
                else
                {
                    GetNewRandom(); //Force update of minMax if using probabilityList
                    return minValue;
                }
            }
        }

        public float MaxValue
        {
            get
            {
                if (isInitialized)
                {
                    return maxValue;
                }
                else
                {
                    GetNewRandom(); //Force update of minMax if using probabilityList
                    return maxValue;
                }
            }
        }

        public float GetNewRandom()
        {
            switch (mode)
            {
                case DistributionMode.Normal:
                    bool foundValueNormal = false;
                    while (!foundValueNormal)
                    {
                        float valueNormal = RemapValue((GetNormalDistributionRandom() * stdDev) + Mathf.Clamp01(mean));
                        if (valueNormal >= minValue && valueNormal <= maxValue)
                        {
                            currentValue = valueNormal;
                            foundValueNormal = true;
                        }
                    }
                    break;

                case DistributionMode.Slope:
                    currentValue = RemapValue(RandomFromSlope(slope));
                    break;

                case DistributionMode.Curve:
                    Assert.IsTrue(curve != null, "No curve found!");

                    bool foundValueCurve = false;
                    while (!foundValueCurve)
                    {
                        float randomValue = Random.value;
                        float randomCurveValue = Random.value;

                        float probability = curve.Evaluate(randomValue);
                        if (randomCurveValue > (1.0f - probability))
                        {
                            currentValue = RemapValue(randomValue);
                            foundValueCurve = true;
                        }
                    }
                    break;

                case DistributionMode.Exp:
                    bool foundValueExp = false;
                    while (!foundValueExp)
                    {
                        float random1 = Random.value;
                        float random2 = Random.value;
                        float y = Mathf.Pow(random1, Mathf.Abs(exp));
                        if (random2 < y)
                        {
                            currentValue = exp >= 0.0f ? RemapValue(random1) : RemapValue(1.0f - random1);
                            foundValueExp = true;
                        }
                    }
                    break;

                case DistributionMode.List:
                    Assert.IsTrue(probabilityList != null && probabilityList.Length > 0, "No probabilities found in list!");

                    currentValue = RandomFromList();
                    break;

                default:
                    currentValue = Random.Range(minValue, maxValue);
                    break;
            }

            isInitialized = true;
            return currentValue;
        }

        public override string ToString()
        {
            if (isInitialized)
            {
                return currentValue.ToString();
            }
            else
            {
                GetNewRandom();
                return currentValue.ToString();
            }
        }

        float RemapValue(float value)
        {
            return minValue + (maxValue - minValue) * value;
        }

        float GetNormalDistributionRandom()
        {
            float random1, random2, result;

            do
            {
                random1 = 2.0f * Random.value - 1.0f;
                random2 = 2.0f * Random.value - 1.0f;
                result = random1 * random1 + random2 * random2;
            } while (result >= 1.0);

            result = Mathf.Sqrt((-2.0f * Mathf.Log(result)) / result);
            return random1 * result;
        }

        float RandomFromSlope(float slope)
        {
            float absSlope = Mathf.Abs(slope);
            if (absSlope == 0)
            {
                return Random.Range(0.0f, 1.0f);
            }

            float x, y;
            do
            {
                x = Random.Range(0.0f, 1.0f);
                y = Random.Range(0.0f, 1.0f);
                if (absSlope < 1)
                {
                    y -= (1 - absSlope) / 2.0f;
                }
            } while (y > x * absSlope);

            return slope < 0 ? 1.0f - x : x;
        }

        float RandomFromList()
        {
            float totalProbability = GetTotalProbability();

            float randomValue = Random.Range(0.0f, totalProbability);

            for (int i = 0; i < probabilityList.Length; i++)
            {
                if (randomValue < probabilityList[i].probability)
                {
                    return probabilityList[i].value;
                }
                else
                {
                    randomValue -= probabilityList[i].probability;
                }
            }

            return probabilityList[probabilityList.Length - 1].value;
        }

        float GetTotalProbability()
        {
            if (totalProbabilityIsSet)
            {
                return totalProbability;
            }
            else
            {
                minValue = float.MaxValue;
                maxValue = float.MinValue;

                totalProbability = 0.0f;
                for (int i = 0; i < probabilityList.Length; ++i)
                {
                    totalProbability += probabilityList[i].probability;
                    UpdateMinMaxValues(probabilityList[i].value);
                }

                totalProbabilityIsSet = true;
                Assert.IsTrue(totalProbability >= 0.0f, "Total probability is 0!");
                return totalProbability;
            }
        }

        void UpdateMinMaxValues(float value)
        {
            minValue = value < minValue ? value : minValue;
            maxValue = value > maxValue ? value : maxValue;
        }
    }

}