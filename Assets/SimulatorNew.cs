using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorNew : MonoBehaviour
{
    public GameObject p_creature;
    public int populationSize = 20;

    private Vector3 start = new Vector3(0, 2, 0);
    private CreatureNew.Gene bestGene;
    public List<GameObject> population = new List<GameObject>();

    void Start()
    {
        bestGene = new CreatureNew.Gene();
        bestGene.a = Random.Range(0, 1f);
        bestGene.b = Random.Range(0, 1f);
        bestGene.c = Random.Range(0, 1f);
        bestGene.d = Random.Range(0, 1f);

        InvokeRepeating("RunSimulation", 0, 5f);
    }

    void RunSimulation()
    {
        float bestFitness = 0;
        CreatureNew.Gene bestGene = new CreatureNew.Gene();
        // Debug.Log("pop: " + population.Count);
        // for (int i = 0; i < population.Count; i++)
        foreach(GameObject c in population)
        {
            // GameObject c = population[i];
            float fitness = c.GetComponent<CreatureNew>().GetFitness();
            if (fitness > bestFitness)
            {
                bestFitness = fitness;
                bestGene = c.GetComponent<CreatureNew>().gene;
            }
            // Debug.Log(fitness);
            Destroy(c);
        }
        population.Clear();

        Debug.Log("best: " + bestFitness);

        for (int i = 0; i < populationSize; i++)
        {
            GameObject creature = Instantiate(p_creature, start + new Vector3(-2 * i, 0, 0), Quaternion.identity);
            creature.GetComponent<CreatureNew>().gene = bestGene.Mutate();
            population.Add(creature);
        }

    }

    void Update()
    {

    }
}
