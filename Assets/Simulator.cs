using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour {

    public int generations = 100;
    public float simulationTime = 15f;

    public IEnumerator Simulation()
    {
        for (int i = 0; i < generations; i++)
        {
            CreateCreatures();
            StartSimulation();

            yield return new WaitForSeconds(simulationTime);

            StopSimulation();
            EvaluateScore();

            DestroyCreatures();

            yield return new WaitForSeconds(1);
        }
    }

    public int variations = 100;
    private Genome bestGenome;

    public Vector3 distance = new Vector3(2, 0, 0);
    public GameObject prefab;
    private List<Creature> creatures = new List<Creature>();

    private void Start()
    {
        bestGenome = Instantiate(prefab, new Vector3(0, 2, 0), Quaternion.identity).GetComponent<Genome>();
        Destroy(bestGenome.gameObject);
        //bestGenome = new Creature().gameObject.GetComponent<Genome>();
        StartCoroutine(Simulation());
        Time.timeScale = 10;
    }

    public void CreateCreatures()
    {
        for (int i = 0; i < variations; i++)
        {
            // Mutate the genome
            Genome genome = bestGenome.Clone().Mutate();

            // Instantiate the creature
            Vector3 position = new Vector3(0, 2, 0) + distance * i;
            Creature creature = Instantiate(prefab, position, Quaternion.identity).GetComponent<Creature>();

            creature.genome = genome;
            creatures.Add(creature);
        }
    }

    public void StartSimulation()
    {
        foreach (Creature creature in creatures)
            creature.enabled = true;
    }

    public void StopSimulation()
    {
        foreach (Creature creature in creatures)
            creature.enabled = false;
    }

    public void DestroyCreatures()
    {
        foreach (Creature creature in creatures)
            Destroy(creature.gameObject);

        creatures.Clear();
    }

    private float bestScore = 0;

    public void EvaluateScore()
    {
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            //Debug.Log("score: " + score);
            if (score > bestScore)
            {
                bestScore = score;
                bestGenome = creature.genome.Clone();
                //Debug.Log("chose nest genome with m: " + bestGenome.left.m);
            }
        }
        Debug.Log("Best genome in generation had a score of " + bestScore);
    }
}
