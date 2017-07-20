using UnityEngine;
using RandomUtility;


public class RandomFloatDistribution_Tester : MonoBehaviour
{
    public RandomFloatDistribution randomFloat;

    public bool update = false;
    public int repetitions = 1000;

    private GameObject graph;

    // Use this for initialization
    void Start()
    {
        graph = CreateGraph();
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            Destroy(graph);
            graph = CreateGraph();
        }
    }


    private GameObject CreateGraph()
    {
        int[] buckets = new int[Mathf.RoundToInt(randomFloat.MaxValue) + 1 - Mathf.RoundToInt(randomFloat.MinValue)]; // add one, because RandomRangeNormalDistribution is inclusive.
        for (int i = 0; i < buckets.Length; ++i)
        {
            buckets[i] = 0;
        }

        for (int i = 0; i < repetitions; ++i)
        {
            float randomNumber = GetRandomNumber();
            buckets[Mathf.RoundToInt(randomNumber) - Mathf.RoundToInt(randomFloat.MinValue)]++;
        }

        // Display how many times each bucket was drawn by creating a bunch of dots in the scene. 
        GameObject graph = new GameObject("Graph");
        graph.transform.parent = transform;
        graph.transform.localPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < buckets.Length; ++i)
        {
            float height = buckets[i] + 1;
            GameObject new_dot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            new_dot.transform.parent = graph.transform;
            new_dot.transform.localPosition = new Vector3(i, height / 2.0f, 0);
            new_dot.transform.localScale = new Vector3(1, height, 1);
        }
        return graph;
    }

    float GetRandomNumber()
    {
        return randomFloat.GetNewRandom();
    }

}