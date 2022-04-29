using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveComboUI : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor;
    [SerializeField] private float fadeTimer;

    void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();

        textColor = textMesh.color;
        
        fadeTimer = 1f;

        StartCoroutine(DestroyComboText());
    }

    void Update()
    {       
        float moveYUp = 0.8f;

        transform.position += new Vector3(0, moveYUp, 0) * Time.deltaTime;

        fadeTimer -= Time.deltaTime;
        if (fadeTimer < 0)
        {
            float fadeSpeed = 3f;
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }
    }

    IEnumerator DestroyComboText()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
