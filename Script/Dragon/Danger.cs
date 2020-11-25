using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField]
    EnemyAI enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemy.check)
        {
            enemy.SetTarger(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && enemy.check)
        {
            enemy.RemoveTarger();
        }
    }

}
