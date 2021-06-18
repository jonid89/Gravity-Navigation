using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;

    private AudioSource audioSource;
    private int levelCount;
    

    void Start()
    {
        levelCount = GameSettings.Instance.GetLevelCount();

        audioSource = GetComponent<AudioSource>();

        if (levelCount < 4) audioSource.clip = clip1;
        else if (levelCount < 7) audioSource.clip = clip2;
        else if (Random.Range(0, 2) == 0) audioSource.clip = clip1;
        else audioSource.clip = clip2;

        Debug.Log(audioSource.clip);

        audioSource.time = Random.Range(0, 4) * 20f;
        audioSource.Play();
        audioSource.volume = 0f;
        StartCoroutine(FadeIn(audioSource));


    }

    static IEnumerator FadeIn(AudioSource audioSrc)
    {
        float vol = 0f;

        while (vol < 0.2f)
        {
            audioSrc.volume = vol;
            vol += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }


}
