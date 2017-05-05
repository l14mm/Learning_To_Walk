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
            //DestroyOldCreatures();
            oldCreatures.Clear();
            yield return new WaitForSeconds(1);
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

    public Vector3 start = new Vector3(0, 2, 0);
    public Vector3 distance = new Vector3(2, 0, 0);
    public GameObject prefab;
    private List<Creature> creatures = new List<Creature>();
    private List<Creature> oldCreatures = new List<Creature>();

    private void Start()
    {
        bestGenome = Instantiate(prefab, start + new Vector3(-2, 2, 0), Quaternion.identity).GetComponent<Genome>();
        // Randomise starting best genome
        bestGenome.Mutate();
        Destroy(bestGenome.gameObject);
        //bestGenome = new Creature().gameObject.GetComponent<Genome>();
        StartCoroutine(Simulation());
    }

    public Genome SelectGenome()
    {
        Genome g = bestGenome.Clone();
        float totalScore = 0;
        foreach (Creature creature in oldCreatures)
        {
            float score = creature.GetScore();
            //Debug.Log("score: " + score);
            totalScore += score * score;
        }

        float rand = Random.Range(0, totalScore);
        float currentScore = 0;

        foreach (Creature creature in oldCreatures)
        {
            float score = creature.GetScore();
            currentScore += score * score;
            if (rand <= currentScore)
            {
                g = creature.genome.Clone();
                Debug.Log(score);
                //Debug.Log("Genome selected had score of " + score);
                //Debug.Log("Percentage " + (score * score / totalScore) * 100);
                break;
            }
        }
        return g;
    }

    public void CreateCreatures()
    {
        for (int i = 0; i < variations; i++)
        {
            // Mutate the genome
            //Genome genome = bestGenome.Clone().Mutate();
            Genome genome = SelectGenome();

            // Instantiate the creature
            Vector3 position = start + distance * i;
            Creature creature = Instantiate(prefab, position, Quaternion.identity).GetComponent<Creature>();
            creature.head.GetComponent<Rigidbody>().isKinematic = true;

            creature.genome = genome;
            creatures.Add(creature);
            Creature clone = creature;
            oldCreatures.Add(clone);
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
        {
            creature.enabled = true;
            creature.head.GetComponent<Rigidbody>().isKinematic = false;
        }
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

    public void DestroyOldCreatures()
    {
        foreach (Creature creature in oldCreatures)
            Destroy(creature.gameObject);

        oldCreatures.Clear();
    }

    private float bestScore = 0;

    public void EvaluateScore()
    {
        /*
        float totalScore = 0;
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            totalScore += score * score;
            //Debug.Log(score);
        }

        float rand = Random.Range(0, totalScore);
        float currentScore = 0;
        
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            currentScore += score * score;
            //Debug.Log(score);
            if (rand <= currentScore)
            {
                bestGenome = creature.genome.Clone();
                Debug.Log("Genome selected had score of " + score);
                Debug.Log("Percentage " + (score * score / totalScore) * 100);
                break;
            }
        }
        */

        ///*
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
        if (bestCreature != null)
            Destroy(bestCreature);
        //bestCreature = Instantiate(prefab, new Vector3(-10, 2, 0), Quaternion.identity);
        //bestCreature.GetComponent<Creature>().genome = bestGenome;
        //*/
    }
}
