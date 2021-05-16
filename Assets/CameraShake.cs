using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{

    public Transform cameraTransform;
    private Vector3 originalCameraPos;

    public float shakeDuration = 2f;
    public float shakeAmount = 0.7f;

    private bool canShake = false;

    protected override void Awake()
    {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }

    public void ShaekCamera(float _shakeDuration = 0.0f) {
        canShake = true;
        Debug.Log(_shakeDuration);
        shakeDuration = _shakeDuration > 0.0f ? _shakeDuration : 2f;
        Debug.Log(shakeDuration);
    }

    void Start() {
        originalCameraPos = cameraTransform.position;
    }

    void Update() {
        if (shakeDuration > 0 && canShake) {
            Vector3 RandVector = Random.insideUnitSphere;
            cameraTransform.position = originalCameraPos + (RandVector * Random.Range(0.0f, 3.0f)) * shakeAmount;
            shakeDuration -= Time.deltaTime;
        } else {
            shakeDuration = 0f;
            cameraTransform.position = originalCameraPos;
        }
    }
}
