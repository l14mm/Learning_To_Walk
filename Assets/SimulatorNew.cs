using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorNew : MonoBehaviour
{
    public GameObject p_creature;
    // Number of creatures in each population
    public int populationSize = 20;

    // How long each population should be evaulated for before creating a new one
    public int simulationTime = 10;

    private Vector3 start = new Vector3(0, 2, 0);
    private CreatureNew.Gene parentGene;
    public List<GameObject> population = new List<GameObject>();

    void Start()
    {
        // Creature random parent for initial population
        parentGene = new CreatureNew.Gene();
        parentGene.Init();

        InvokeRepeating("RunSimulation", 0, simulationTime);
    }

    void RunSimulation()
    {
        float bestFitness = 0;
        CreatureNew.Gene bestGene = parentGene;

        // Find creature with best gene/fitness
        // then remove population
        foreach(GameObject c in population)
        {
            float fitness = c.GetComponent<CreatureNew>().GetFitness();
            if (fitness > bestFitness)
            {
                bestFitness = fitness;
                bestGene = c.GetComponent<CreatureNew>().gene;
            }
            Destroy(c);
        }
        population.Clear();

        Debug.Log("bestfitness: " + bestFitness);

        // Create new population mutated from best creature/gene from last population
        for (int i = 0; i < populationSize; i++)
        {
            GameObject creature = Instantiate(p_creature, start + new Vector3(4 * i, 0, 0), Quaternion.identity);
            creature.GetComponent<CreatureNew>().gene = bestGene.Mutate();
            population.Add(creature);
        }

    }
}
