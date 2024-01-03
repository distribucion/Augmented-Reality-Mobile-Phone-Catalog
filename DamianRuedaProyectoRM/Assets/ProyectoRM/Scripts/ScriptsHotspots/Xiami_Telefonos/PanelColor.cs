using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColor : MonoBehaviour
{
    public GameObject panelCambiarColorXiami;




    void Start()
    {
        panelCambiarColorXiami.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelCambiarColorXiami != null)
        {
            bool IsActive = panelCambiarColorXiami.activeSelf;
            panelCambiarColorXiami.SetActive(!IsActive);


        }
    }
}
