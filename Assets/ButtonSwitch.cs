using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public int position = 0;
    public bool isOn = true;
    private Image _image;
    public Sprite switchOn;
    public GameObject xorImage;
    public GameObject trailActiveFinal;
    public GameObject trailActiveInit;
    public Sprite switchOff;
    // Start is called before the first frame update
    void Start()
    {
        this._image = GetComponent<Image>();
        GenerateRandomSwitch();
    }

    public void GenerateRandomSwitch() {
        float randInit = Random.Range(0.0f, 1.0f);
        float randXor = Random.Range(0.0f, 1.0f); //enable xor with 0.5
        if (randInit > 0.5) {
            this.isOn = false;
            this._image.sprite = this.switchOff;
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
            this._image.sprite = this.switchOn;
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
        this._image.sprite = this.isOn ? this.switchOn : this.switchOff;
        this.trailActiveFinal.SetActive(!this.trailActiveFinal.activeSelf);
        ShipGameManager.Instance.SetGameArray(this.trailActiveFinal.activeSelf, position);
        this.trailActiveInit.SetActive(!this.trailActiveInit.activeSelf);
        // Tells gameManager
    }
}
