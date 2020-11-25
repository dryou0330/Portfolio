using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Sound1 : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip dragon_dool;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    private void OnTriggerEnter(Collider other)
    {
        SoundSE(dragon_dool);
        StartCoroutine("lightStart");
    }

    IEnumerator lightStart()
    {
        yield return new WaitForSeconds(1.0f);
        light1.SetActive(true);
        yield return new WaitForSeconds(2f);
        light2.SetActive(true);
        yield return new WaitForSeconds(2f);
        light3.SetActive(true);
        yield return null;

    }

    public void SoundSE(AudioClip _clip)
    {
        audio.clip = _clip;
        audio.Play();
    }
}
