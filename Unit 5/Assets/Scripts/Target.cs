using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticles;

    GameManager gameManager;
    CanvasManager canvasManager;

    BackgroundColor bgColor;

    private Rigidbody targetRb;
    private Collider targetCollider;

    [SerializeField] private float minSpeed = 8f, maxSpeed = 10f, minHeight = -1f;
    [SerializeField] private int bombScore = -300, score = 100;

    GameObject[] otherTargets;
    GameObject[] bombs;

    public int targetCutt;

    void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        gameManager = FindObjectOfType<GameManager>();

        bgColor = FindObjectOfType<BackgroundColor>();

        targetRb = GetComponent<Rigidbody>();
        targetCollider = GetComponent<Collider>();

        otherTargets = GameObject.FindGameObjectsWithTag("Target");
        bombs = GameObject.FindGameObjectsWithTag("Bomb");

        targetRb.AddForce(Vector3.up * RandomInitialSpeed(), ForceMode.Impulse);          
    }

    void Update()
    {
        ShouldDestroy();

        ShouldNotCollide();
    }

    //private void OnDisable()
    //{
    //    Debug.Log("Caiu");
    //    gameManager.HardModeCount();
    //}

    float RandomInitialSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        return randomSpeed;
    }

    void ShouldDestroy()
    {
        if (transform.position.y <= minHeight)
        {
            if (gameObject.CompareTag("Target"))
            {
                gameManager.HardModeCount();
            }
            Destroy(gameObject);
        }       
    }

    void ShouldNotCollide()
    {
        for (int i = 0; i < otherTargets.Length; i++)
        {
            if (otherTargets[i] != null)
            {
                Collider otherTargetsCollider = otherTargets[i].gameObject.GetComponent<Collider>();              
                Physics.IgnoreCollision(targetCollider, otherTargetsCollider, true);              
            }           
        }

        for (int i = 0; i < bombs.Length; i++)
        {
            if (bombs[i] != null)
            {
                Collider otherBombsCollider = bombs[i].gameObject.GetComponent<Collider>();
                Physics.IgnoreCollision(targetCollider, otherBombsCollider, true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);

            if (gameObject.CompareTag("Bomb"))
            {
                bgColor.BackGroundBomb();

                gameManager.UpdateScore(bombScore);
                gameManager.UpdateBomb();
            }
            else if (gameObject.CompareTag("Target"))
            {
                gameManager.UpdateScore(score);
                canvasManager.ComboTextInstantiate(transform.position);
            }
            
            Destroy(gameObject);
        }
    }
}
