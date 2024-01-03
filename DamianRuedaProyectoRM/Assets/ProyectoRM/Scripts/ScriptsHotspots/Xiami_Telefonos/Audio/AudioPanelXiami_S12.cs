using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPanelXiami_S12 : MonoBehaviour
{
    public GameObject panelAudio;



    void Start()
    {
        panelAudio.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelAudio != null)
        {
            bool IsActive = panelAudio.activeSelf;
            panelAudio.SetActive(!IsActive);


        }
    }
}
