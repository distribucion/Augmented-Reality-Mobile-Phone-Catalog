using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPanel : MonoBehaviour
{
    public GameObject panelRotationXia;



    void Start()
    {
        panelRotationXia.SetActive(false);
    }

    public void panelActivarXia()
    {
        if (panelRotationXia != null)
        {
            bool IsActive = panelRotationXia.activeSelf;
            panelRotationXia.SetActive(!IsActive);


        }
    }
}
