using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotacionHawei : MonoBehaviour
{
    public GameObject panelRotationHawei;



    void Start()
    {
        panelRotationHawei.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelRotationHawei != null)
        {
            bool IsActive = panelRotationHawei.activeSelf;
            panelRotationHawei.SetActive(!IsActive);


        }
    }
}
