<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "MainStage"; // 다음씬으로 이동할 이름 

    public void ClickStart()
    {
        Debug.Log("게임 스타트 로딩중 ..");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("Load 중...");
    }

}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "MainStage"; // 다음씬으로 이동할 이름 

    public void ClickStart()
    {
        Debug.Log("게임 스타트 로딩중 ..");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("Load 중...");
    }

}
>>>>>>> 6c436f29386b55cbc0c7cc394c95d1e7d24ff50b
