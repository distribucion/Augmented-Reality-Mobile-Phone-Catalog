using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotacionIphone : MonoBehaviour
{
    public GameObject panelRotationIphone;



    void Start()
    {
        panelRotationIphone.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelRotationIphone != null)
        {
            bool IsActive = panelRotationIphone.activeSelf;
            panelRotationIphone.SetActive(!IsActive);


        }
    }
}
