using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaweiAudio : MonoBehaviour
{
    public GameObject panelAudioHawei;



    void Start()
    {
        panelAudioHawei.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelAudioHawei != null)
        {
            bool IsActive = panelAudioHawei.activeSelf;
            panelAudioHawei.SetActive(!IsActive);


        }
    }
}
