using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IphoneColor : MonoBehaviour
{
     public GameObject panelColorIphone;



    void Start()
    {
        panelColorIphone.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelColorIphone != null)
        {
            bool IsActive = panelColorIphone.activeSelf;
            panelColorIphone.SetActive(!IsActive);


        }
    }
}
