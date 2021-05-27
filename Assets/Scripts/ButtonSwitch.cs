using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public int position = 0;
    public bool isOn = true;
    public bool isXor = false;
    public GameObject switchOff;
    public GameObject switchOn;
    public GameObject xorImage;
    public bool finalIsActive = false;
    public GameObject trailActiveFinal;
    public GameObject trailActiveInit;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomSwitch();
    }

    public void GenerateRandomSwitch() {
        this.trailActiveInit.SetActive(false);
        this.trailActiveFinal.SetActive(false);
        float randInit = Random.Range(0.0f, 1.0f);
        float randXor = Random.Range(0.0f, 1.0f); //enable xor with 0.5
        if (randInit > 0.5) {
            this.isOn = false;
            switchOff.SetActive(true);
            switchOn.SetActive(false);
            //this.trailActiveInit.SetActive(false);
          if (randXor > 0.5) {
                this.isXor = true;
                this.xorImage.SetActive(true);
                this.finalIsActive = true;
                //this.trailActiveFinal.SetActive(true);
                ShipGameManager.Instance.SetGameArray(true, position, true);
          } else {
                this.isXor = false;
                this.xorImage.SetActive(false);
                this.finalIsActive = false;
                //this.trailActiveFinal.SetActive(false);
                ShipGameManager.Instance.SetGameArray(false, position, false);
          }
        } else {
            this.isOn = true;
            switchOff.SetActive(false);
            switchOn.SetActive(true);
            //trailActiveInit.SetActive(true);
            if (randXor > 0.5) {
                this.isXor = true;
                this.xorImage.SetActive(true);
                //this.trailActiveFinal.SetActive(false);
                this.finalIsActive = false;
                ShipGameManager.Instance.SetGameArray(false, position, true);
            } else {
                this.isXor = false;
                this.xorImage.SetActive(false);
                //this.trailActiveFinal.SetActive(true);
                this.finalIsActive = true;
                ShipGameManager.Instance.SetGameArray(true, position, false);
            }
        }
    }

    public void SwitchClick() {
        this.isOn = !this.isOn;
        if (isOn) {
            this.switchOn.SetActive(true);
            this.switchOff.SetActive(false);
        } else {
            
            this.switchOn.SetActive(false);
            this.switchOff.SetActive(true);
        }
        this.finalIsActive = !this.finalIsActive;
        //this.trailActiveFinal.SetActive(!this.trailActiveFinal.activeSelf);
        ShipGameManager.Instance.SetGameArray(this.finalIsActive, position, this.isXor);
        //this.trailActiveInit.SetActive(!this.trailActiveInit.activeSelf);
    }
}
