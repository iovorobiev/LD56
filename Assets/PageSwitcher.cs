using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    public List<GameObject> page;
    public int defaultPageIndex = 0;
    private int currentPage = 0;
    
    public void ShowPage(int index)
    {
        page[currentPage].SetActive(false);
        currentPage = index;
        page[index].SetActive(true);
    }

    public void ShowDefault()
    {
        ShowPage(defaultPageIndex);
    }
}
