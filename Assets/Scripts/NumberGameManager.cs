using systemItems = System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberGameManager :  Singleton<NumberGameManager>
{
    public int life = 3;
    public int sendedMsg = 0;
    public int maxPoints = 20;
    public int min = 0;
    public int max = 31;
    public int decimalValue; 
    public string expectedString;

    // Signal References
    public bool useSignalBit = false;
    public int maxSignalBit = 5;
    private int signalBitTimes = 0;

    //Ui Elements
    public TextMeshProUGUI decimalText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI sendedText;
    public AudioClip failClip;
    public AudioClip successClip;
    public AudioSource audioSource;
    public List<string> values = new List<string>();
    public List<TextMeshProUGUI> buttonsText = new List<TextMeshProUGUI>();
    public Button signalButton;
    public List<Button> interactableButtons = new List<Button>();
    public Animator generateNewAnimator;
    // Start is called before the first frame update

    protected override void Awake() {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GenerateNewNumber(true);
    }

    public void GenerateNewNumber(bool firstTime = false) {
        GetSignalBit(firstTime);
        this.decimalValue = this.useSignalBit ? Random.Range(-max, max) : Random.Range(min, max);
        GetExpectedString(this.decimalValue);
        this.decimalText.text = "Decimal: " + this.decimalValue.ToString();
    }

    public void GetSignalBit(bool firstTime) {
        bool signalRand = Random.Range(0.0f, 1.0f) > 0.5f;
        if (!firstTime && signalRand) {
            this.signalBitTimes++;
        }
        this.useSignalBit = firstTime || this.signalBitTimes >= this.maxSignalBit ? false : signalRand;
    }

    public void GetExpectedString(int decValue) {
        bool isNegative = false;
        if (decValue < 0) {
            decValue = decValue * (-1);
            isNegative = true;
        }
        string expectedString = systemItems.Convert.ToString(decValue, 2);
        if (expectedString.Length < 5) {
            while(expectedString.Length < 5) {
                expectedString = "0" + expectedString;
            }
        }
        signalButton.interactable = this.useSignalBit;
        if (this.useSignalBit) {
            expectedString = isNegative ? "1" + expectedString : "0" + expectedString;
        }
        this.expectedString = expectedString;
    }

    public void ButtonClick(int position) {
        TextMeshProUGUI buttonText = buttonsText[position];
        if (!buttonText) {
            return;
        }
        if (buttonText.text == "0") {
            buttonText.text = "1";
            values[position] = "1";
        } else {
            buttonText.text = "0";
            values[position] = "0";
        }
    }

    public void Reset() {
        for(int i = 0; i < values.Count; i++) {
            values[i] = "0";
            buttonsText[i].text = "0";
        }
        GenerateNewNumber();
        ChangeButtonInteractable(true);
    }

    public void ChangeButtonInteractable(bool interactable) {
        for(int i = 0; i < interactableButtons.Count; i++) {
            interactableButtons[i].interactable = interactable;
        }
    }

    public void Send() {
        string finalValue = "";
        int initialValue = this.useSignalBit ? 0 : 1;
        for (int i = initialValue; i < values.Count; i++) {
            finalValue += values[i];
        }
        if (finalValue == expectedString) {
            ChangeButtonInteractable(false);
            this.sendedMsg++;
            this.sendedText.text = "Mensagens enviadas: " + this.sendedMsg + "/" + this.maxPoints;
            if (sendedMsg == 20) {
                // Set the game as completed on save file
                SceneManager.LoadScene("CompleteBinary");
            }
            audioSource.clip = successClip;
            audioSource.Play();
            generateNewAnimator.SetTrigger("GenerateNew");

        } else {
            CameraShake.Instance.ShaekCamera(0.5f);
            life--;
            if (life <= 0) {
                SceneManager.LoadScene("GameOverBinary");
            }
            errorText.text = "Limite de erros: "+life.ToString();
            audioSource.clip = failClip;
            audioSource.Play();
        }
    }
}
