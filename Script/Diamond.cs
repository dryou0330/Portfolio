using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private Rigidbody rigid;
    public float upPower;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(Vector3.up * upPower);
    }
}
