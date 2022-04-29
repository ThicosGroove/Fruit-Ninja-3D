using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    Color colorStart = Color.white;
    Color colorEnd = Color.red;

    GameManager gameManager;
    Renderer render;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        render = GameObject.Find("Background").GetComponent<Renderer>();   
    }

    void Update()
    {
        if (gameManager.isGameOver)
        {
            render.material.color = colorEnd;
        }
        else if (render.material.color == colorEnd)
        {
            StartCoroutine(ReturnStartColor());
        }
    }

    public void BackGroundBomb()
    {
        render.material.color = colorEnd;
    }

    IEnumerator ReturnStartColor()
    {
        yield return new WaitForSeconds(0.3f);

        render.material.color = colorStart;
    }
}
