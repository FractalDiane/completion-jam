using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiPageScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject[] pages;
    private int currentPage = 0;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private UnityEvent onPageClosed;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        foreach(GameObject page in pages)
        {
            page.SetActive(false);
        }
        GoToPage(0);
    }

    public void GoToPage(int index)
    {
        pages[currentPage].SetActive(false);
        leftArrow.SetActive(index > 0);
        rightArrow.SetActive(index < pages.Length - 1);
        closeButton.SetActive(index >= pages.Length - 1);
        pages[index].SetActive(true);
        currentPage = index;
    }

    public void GoToPreviousPage()
    {
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
        if(currentPage > 0)
            GoToPage(currentPage-1);
    }

    public void GoToNextPage()
    {
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
        if(currentPage < pages.Length - 1)
            GoToPage(currentPage+1);
    }

    public void CloseButton()
    {
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
        canvas.SetActive(false);
        onPageClosed.Invoke();
    }
}
