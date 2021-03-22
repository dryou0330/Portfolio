<<<<<<< HEAD
﻿using System.Collections;
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
=======
﻿using System.Collections;
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
>>>>>>> 6c436f29386b55cbc0c7cc394c95d1e7d24ff50b
