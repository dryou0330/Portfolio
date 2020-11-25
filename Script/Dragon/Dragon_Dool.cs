using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dragon_Dool : MonoBehaviour
{
    [SerializeField]
    private GameObject player; // 플레이어 벡터값 받아오기 
    [SerializeField]
    private GameObject move_Position; // 옮겨질 위치 
    [SerializeField]
    private GameObject dragon_First;

    private void Start()
    {
        dragon_First.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.transform.position = move_Position.transform.position;
            dragon_First.SetActive(true);
        }
    }
}
