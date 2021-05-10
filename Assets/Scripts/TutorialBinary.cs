using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBinary : Singleton<TutorialBinary>
{
    public bool isOpen = true;
    public int actualPage = 0;
    public int nextPage = 1;
    public int lastPage = 10;
    public List<GameObject> parts = new List<GameObject>();
    public CanvasGroup panel;
    public GameObject backButton;
    public GameObject closeButton;
    public GameObject nextButton;
    protected override void Awake()
    {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }

    public void NextPage() {
        parts[this.actualPage].SetActive(false);
        parts[this.nextPage].SetActive(true);
        this.actualPage++;
        this.nextPage++;
        if (this.actualPage != 0 && this.actualPage != lastPage) {
            backButton.SetActive(true);
            nextButton.SetActive(true);
            closeButton.SetActive(false);
        }
        if (this.actualPage == lastPage) {
            nextButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }

    public void BackPage() {
        if (this.actualPage != 0) {
            parts[this.actualPage].SetActive(false);
            parts[this.actualPage - 1].SetActive(true);
            this.actualPage--;
            this.nextPage--;
            if (this.actualPage == 0) {
                backButton.SetActive(false);
            }
            if (this.actualPage != 0 && this.actualPage != lastPage) {
            backButton.SetActive(true);
            nextButton.SetActive(true);
            closeButton.SetActive(false);
        }
        }
    }

    public void CloseTutorial() {
        this.parts[actualPage].SetActive(false);
        this.actualPage = 0;
        this.nextPage = 1;
        this.backButton.SetActive(false);
        this.closeButton.SetActive(false);
        this.nextButton.SetActive(true);
        this.panel.interactable = false;
        this.panel.blocksRaycasts = false;
        this.panel.alpha = 0;
        this.isOpen = false;
    }

    public void OpenTutorial() {
        if (!isOpen) {
            this.panel.interactable = true;
            this.panel.blocksRaycasts = true;
            this.panel.alpha = 1;
            this.parts[0].SetActive(true);
            this.isOpen = true;
        }
    }
}
