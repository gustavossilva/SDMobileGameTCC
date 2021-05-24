using systemItems = System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RouteGameManager : Singleton<RouteGameManager>
{
    public List<int> numbers = new List<int>(new int[4]);
    public List<string> numbersString = new List<string>(new string[4]);
    public List<TextMeshProUGUI> numbersText;
    public int destiny;
    public string destinyString;
    public int selectedRoutes = 0;
    public int maxRoutes = 10;
    public int errorsLimit = 2;
    public TextMeshProUGUI destinyText;
    public TextMeshProUGUI selectedRoutesText;
    public TextMeshProUGUI errorsText;
    public List<Button> interactableButtons;
    public Animator generateNewAnimator;

    public AudioClip successClip;
    public AudioClip failClip;
    public AudioSource audioSource;

    protected override void Awake() {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }
    
    void Start()
    {   
        this.selectedRoutesText.text = "Rotas selecionadas: " + this.selectedRoutes.ToString() + "/" + this.maxRoutes.ToString();
        this.errorsText.text = "Limite de erros: " + this.errorsLimit.ToString();
        audioSource = GetComponent<AudioSource>();
        GenerateNewDestiny();
    }

    public void GenerateNewDestiny() {
        ChangeButtonInteractable(true);
        bool destinyChoosed = false;
        for(int i = 0; i < this.numbers.Count ; i++) {
            int number = Random.Range(100, 9999);
            bool isDestiny = Random.Range(0.0f, 1.0f) > 0.5f;
            string firstDigit = NormalizeDigit(systemItems.Convert.ToString((int)(number / 1000), 2));
            string secondDigit = NormalizeDigit(systemItems.Convert.ToString((int)((number % 1000) / 100), 2));
            string thirdDigit = NormalizeDigit(systemItems.Convert.ToString((int)((number % 100) / 10), 2));
            string fourthDigit = NormalizeDigit(systemItems.Convert.ToString((int)(number % 10), 2));
            string route = firstDigit + "  " + secondDigit + "  " + thirdDigit + "  " + fourthDigit;
            if ((i == numbers.Count - 1 && !destinyChoosed) || (isDestiny && !destinyChoosed)) {
                destinyChoosed = true;
                this.destiny = number;
                string addZero = number < 1000 ? "0" : "";
                this.destinyText.text = "Rota destino: " + addZero + number.ToString();
                this.destinyString = route;
            }
            this.numbersString[i] = route;
            this.numbers[i] = number;
            this.numbersText[i].text = "Rota " + (i+1) + ": " + route;
        }
    }

    public void ChangeButtonInteractable(bool interactable) {
        for(int i = 0; i < interactableButtons.Count; i++) {
            interactableButtons[i].interactable = interactable;
        }
    }

    public void RouteClick(int position) {
        if (numbersString[position] == destinyString) {
            ChangeButtonInteractable(false);
            this.selectedRoutes++;
            this.selectedRoutesText.text = "Rotas selecionadas: " + this.selectedRoutes.ToString() + "/" + this.maxRoutes.ToString();
            if (selectedRoutes > maxRoutes) {
                PlayerPrefs.SetInt("finished3", 1);
                if (PlayerPrefs.GetInt("finished2", 0) > 0 && PlayerPrefs.GetInt("finished1", 0) > 0) {
                    SceneManager.LoadScene("Victory");
                } else {
                    SceneManager.LoadScene("CompleteRoute");
                }
            }
            audioSource.clip = successClip;
            audioSource.Play();
            generateNewAnimator.SetTrigger("GenerateNew");
        } else {
            CameraShake.Instance.ShaekCamera(0.5f);
            errorsLimit--;
            if (errorsLimit < 0) {
                SceneManager.LoadScene("GameOverRoute");
            }
            errorsText.text = "Limite de erros: "+errorsLimit.ToString();
            audioSource.clip = failClip;
            audioSource.Play();
        }
    }

    string NormalizeDigit(string digit) { 
        if (digit.Length < 4) {
            while(digit.Length < 4) {
                digit = "0" + digit;
            }
        }
        return digit;
    }
}
