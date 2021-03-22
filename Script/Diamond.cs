<<<<<<< HEAD
﻿using System.Collections;
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
=======
﻿using System.Collections;
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
>>>>>>> 6c436f29386b55cbc0c7cc394c95d1e7d24ff50b
