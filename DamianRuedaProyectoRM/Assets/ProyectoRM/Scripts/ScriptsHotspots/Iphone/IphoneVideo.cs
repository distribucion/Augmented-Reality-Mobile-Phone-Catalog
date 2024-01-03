using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IphoneVideo : MonoBehaviour
{
    public GameObject panelVideoIphone;



    void Start()
    {
        panelVideoIphone.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelVideoIphone != null)
        {
            bool IsActive = panelVideoIphone.activeSelf;
            panelVideoIphone.SetActive(!IsActive);


        }
    }
}
