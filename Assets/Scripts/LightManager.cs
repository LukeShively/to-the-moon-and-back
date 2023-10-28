using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    Vector3 newRotation;
    // Start is called before the first frame update
    void Start()
    {
        newRotation = new Vector3(0, ((3.0f / 6f)) % 360f, 0);
    }

    void FixedUpdate()
    {
        //newRotation.Set(0, , 0);
        this.transform.Rotate(newRotation);
    }
}
