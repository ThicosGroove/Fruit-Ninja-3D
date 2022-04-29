using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;

    GameObject currentBladeTrail;

    Rigidbody rb;
    Camera cam;
    SphereCollider circleCollider;

    Vector2 previousPosition;
    Vector2 followPosition;
    bool isCutting = false;
    public bool isSameBlade;
    [SerializeField] private float maxTimeForCombo = 0.5f;
    [SerializeField] private float minCuttingVelocity = 0.01f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        circleCollider = GetComponent<SphereCollider>();

        circleCollider.enabled = false;
    }

    void Update()
    {
        followPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        if (velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
            currentBladeTrail.SetActive(true);
        }
        else
        {
            circleCollider.enabled = false;
        }
        rb.position = newPosition;
    }

    void StartCutting()
    {
        circleCollider.enabled = false;
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        currentBladeTrail.SetActive(false);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
       
        StartCoroutine(SameBladeForCombo());
    }

    void StopCutting()
    {
        isSameBlade = false;
        circleCollider.enabled = false;
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        previousPosition = followPosition;
        Destroy(currentBladeTrail, 2f);
    }

    IEnumerator SameBladeForCombo()
    {
        isSameBlade = true;
        yield return new WaitForSeconds(maxTimeForCombo);

        isSameBlade = false;
    }
}
