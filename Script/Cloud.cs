using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float CloudSpeed;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * CloudSpeed * Time.deltaTime);
    }
}
