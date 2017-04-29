using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour {

    public int generations = 100;
    public float simulationTime = 15f;

    public float TimeScale = 10f;

    public IEnumerator Simulation()
    {
        for (int i = 0; i < generations; i++)
        {
            //StartCoroutine(CreateCreatures())
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
    public GameObject bestCreature = null;

    public Vector3 distance = new Vector3(2, 0, 0);
    public GameObject prefab;
    private List<Creature> creatures = new List<Creature>();

    private void Start()
    {
        bestGenome = Instantiate(prefab, new Vector3(0, 2, 0), Quaternion.identity).GetComponent<Genome>();
        // Randomise starting best genome
        bestGenome.Mutate();
        Destroy(bestGenome.gameObject);
        //bestGenome = new Creature().gameObject.GetComponent<Genome>();
        StartCoroutine(Simulation());
    }

    public void CreateCreatures()
    {
        for (int i = 0; i < variations; i++)
        {
            // Mutate the genome
            Genome genome = bestGenome.Clone().Mutate();

            // Instantiate the creature
            Vector3 position = new Vector3(0, 2, 0) + distance * i;
            //if(i%3 == 0)
                //yield return new WaitForEndOfFrame();
            Creature creature = Instantiate(prefab, position, Quaternion.identity).GetComponent<Creature>();

            creature.genome = genome;
            creatures.Add(creature);
        }
    }

    private void Update()
    {
        if (Time.timeScale != TimeScale)
            Time.timeScale = TimeScale;
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
        ///*
        float totalScore = 0;
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            totalScore += score;
        }

        float rand = Random.Range(0, totalScore);
        float currentScore = 0;
        
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            currentScore += score;
            Debug.Log(score);
            if (rand <= currentScore)
            {
                bestGenome = creature.genome.Clone();
                Debug.Log("Genome selected had score of " + score);
                Debug.Log("Percentage " + (score / totalScore) * 100);
                break;
            }
        }
        //*/

        /*
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
        Debug.Log("left m: " + bestGenome.legs[0].m);
        Debug.Log("left M: " + bestGenome.legs[0].M);
        Debug.Log("left o: " + bestGenome.legs[0].o);
        Debug.Log("left p: " + bestGenome.legs[0].p);
        if (bestCreature != null)
            Destroy(bestCreature);
        bestCreature = Instantiate(prefab, new Vector3(-10, 2, 0), Quaternion.identity);
        bestCreature.GetComponent<Creature>().genome = bestGenome;
        */
    }
}
