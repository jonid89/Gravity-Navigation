using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour, IPointerClickHandler
{
    public Text slashTxt;
    private AudioSource audioSource;
    
    private bool soundOn;

    void Start()
    {
        soundOn = GameSettings.Instance.GetSoundState();

        audioSource = GetComponent<AudioSource>();

        if (soundOn)
        {
            AudioListener.pause = false;
            slashTxt.enabled = false;
        }
        else
        {
            AudioListener.pause = true;
            slashTxt.enabled = true;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        HandleClick();
    }

    private void HandleClick()
    {
        soundOn = !soundOn;

        if (soundOn)
        {
            AudioListener.pause = false;
            slashTxt.enabled = false;
            audioSource.Play();
        }
        else
        {
            AudioListener.pause = true;
            slashTxt.enabled = true;
        }


        GameSettings.Instance.SetSoundState(soundOn);

    }
}
