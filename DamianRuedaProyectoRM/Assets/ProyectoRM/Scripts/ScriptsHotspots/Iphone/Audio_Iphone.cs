using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Iphone : MonoBehaviour
{
    public GameObject panelAudioIphone;



    void Start()
    {
        panelAudioIphone.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelAudioIphone != null)
        {
            bool IsActive = panelAudioIphone.activeSelf;
            panelAudioIphone.SetActive(!IsActive);


        }
    }
}
