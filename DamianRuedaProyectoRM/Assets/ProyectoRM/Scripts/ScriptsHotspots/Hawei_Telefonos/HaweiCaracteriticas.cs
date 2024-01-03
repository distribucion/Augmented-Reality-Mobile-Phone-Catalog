using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaweiCaracteriticas : MonoBehaviour
{
    public GameObject panelCarcteristicasHawei;



    void Start()
    {
        panelCarcteristicasHawei.SetActive(false);
    }

    public void panelActivar1()
    {
        if (panelCarcteristicasHawei != null)
        {
            bool IsActive = panelCarcteristicasHawei.activeSelf;
            panelCarcteristicasHawei.SetActive(!IsActive);


        }
    }
}
