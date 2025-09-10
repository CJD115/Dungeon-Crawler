using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    private int currentTab = 0;
    private bool forceTab = false;

    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabNo)
    {
        currentTab = tabNo;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
        forceTab = false;
    }

    // Call this from MenuController to force tab 0
    public void ForceTab0()
    {
        forceTab = true;
        ActivateTab(0);
    }
}
