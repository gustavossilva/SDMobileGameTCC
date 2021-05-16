using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public int position = 0;
    public bool isOn = true;
    public GameObject switchOff;
    public GameObject switchOn;
    public GameObject xorImage;
    public GameObject trailActiveFinal;
    public GameObject trailActiveInit;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomSwitch();
    }

    public void GenerateRandomSwitch() {
        float randInit = Random.Range(0.0f, 1.0f);
        float randXor = Random.Range(0.0f, 1.0f); //enable xor with 0.5
        if (randInit > 0.5) {
            this.isOn = false;
            switchOff.SetActive(true);
            switchOn.SetActive(false);
              this.trailActiveInit.SetActive(false);
          if (randXor > 0.5) {
              this.xorImage.SetActive(true);
              this.trailActiveFinal.SetActive(true);
              ShipGameManager.Instance.SetGameArray(true, position);
          } else {
              this.xorImage.SetActive(false);
              this.trailActiveFinal.SetActive(false);
              ShipGameManager.Instance.SetGameArray(false, position);
          }
        } else {
            this.isOn = true;
            switchOff.SetActive(false);
            switchOn.SetActive(true);
            trailActiveInit.SetActive(true);
            if (randXor > 0.5) {
                this.xorImage.SetActive(true);
                this.trailActiveFinal.SetActive(false);
                ShipGameManager.Instance.SetGameArray(false, position);
            } else {
                this.xorImage.SetActive(false);
                this.trailActiveFinal.SetActive(true);
                ShipGameManager.Instance.SetGameArray(true, position);
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
        this.trailActiveFinal.SetActive(!this.trailActiveFinal.activeSelf);
        ShipGameManager.Instance.SetGameArray(this.trailActiveFinal.activeSelf, position);
        this.trailActiveInit.SetActive(!this.trailActiveInit.activeSelf);
        // Tells gameManager
    }
}
