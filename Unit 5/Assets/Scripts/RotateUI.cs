using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.5f, 0), Space.Self);
    }
}
