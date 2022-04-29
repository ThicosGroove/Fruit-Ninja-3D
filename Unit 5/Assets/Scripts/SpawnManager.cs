using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetsInfo
{
    public string name;
    public GameObject prefab;
    public float minPosX = -4.3f, maxPosX = 4.3f;
    public float minTorque = 50, maxTorque = 200;    
}


public class SpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public TargetsInfo[] targets;


    [SerializeField] private float respawnRate = 0.6f, initialSpawWait = 2f;
    [SerializeField] private float leftSide = -1f, rightSide = 1f;
    [SerializeField] private float spinMinForce = 1f, spinMaxForce = 2f;

    private GameObject newPrefab;

    public void SpawnObjects()
    {
        if (gameManager.isEasyMode || gameManager.isHardMode)
        {
            InvokeRepeating("RandomSelectPrefab", initialSpawWait, respawnRate);
        }
    }

    void RandomSelectPrefab()
    {
        if (!gameManager.isGameOver)
        {
            int randomPrefab = Random.Range(0, targets.Length);

            float randomPos = Random.Range(targets[randomPrefab].minPosX, targets[randomPrefab].maxPosX);

            Vector3 newPos = new Vector3(randomPos, 0, 0);

            newPrefab = Instantiate(targets[randomPrefab].prefab, newPos, Quaternion.identity);
            newPrefab.GetComponent<Rigidbody>().AddTorque(RandomTorque());
            newPrefab.GetComponent<Rigidbody>().AddForce(RandomSideMovement());
        }
    }

    Vector3 RandomTorque() //Add spin to itself
    {
        int randomPrefab = Random.Range(0, targets.Length);

        float randomTorque = Random.Range(targets[randomPrefab].minTorque, targets[randomPrefab].maxTorque);

        Vector3 newTorque = new Vector3(randomTorque, randomTorque, randomTorque);

        return newTorque;
    }

    Vector3 RandomSideMovement() //Add side movement to the screen
    {
        float spinForce = Random.Range(spinMinForce, spinMaxForce);
        Vector3 newSpin = Vector3.zero;

        if (newPrefab.transform.position.x <= leftSide)
        {
            newSpin = new Vector3(spinForce, 0f, 0f);
        }
        else if (newPrefab.transform.position.x >= rightSide)
        {
            spinForce *= -1;
            newSpin = new Vector3(spinForce, 0f, 0f);
        }
        return newSpin;
    }
}
