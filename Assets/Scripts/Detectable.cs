using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Detectable : MonoBehaviour
{
    //public enum DetectionType { Discrete, Continuous };

    float currentDetection = 0f;
    float maxDetection = 10f;
    [Range(1, 5)]
    [SerializeField] float detectionDecreseRate = 1f;
    [SerializeField] bool isContinouslyDetectable = false;

    [SerializeField] AudioClip detectionClip;

    Enemy currentDetector;

    PlayerMovement playerMovement;

    bool isDetected = false;
    bool isDetectionStarted = false;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            currentDetection = 0;
    }

    void DetectionHit(Enemy enemy)
    {
        if (currentDetection < maxDetection)
        {
            currentDetection += enemy.DetectionDifficulty;
        }

        if (false == isDetectionStarted)
        {
            isDetectionStarted = true;
            StartCoroutine(DetectionCountDown(enemy));
        }

        if (true == isDetected)
        {
            switch (isContinouslyDetectable)
            {
                case true:
                    StartCoroutine(DetectContinously(enemy));
                    Debug.Log("Detection started");
                    SoundManager.Instance.SwapAmbience(detectionClip);
                    break;
                case false:
                    enemy.Detect(this);
                    break;
            }
        }
    }

    public void ShootDetectionHit(Watcher enemy)
    {
        DetectionHit(enemy);
    }

    public void ShootDetectionHit(Listener enemy)
    {
        if (false == playerMovement.IsMoving)
            return;

        if (true == playerMovement.IsMoving && false == playerMovement.IsRunning)
            return;

        DetectionHit(enemy);
    }

    IEnumerator DetectContinously(Enemy detectorEnemy)
    {
        while (isDetected)
        {
            detectorEnemy.Detect(this);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator DetectionCountDown(Enemy detectorEnemy)
    {
        SoundManager.Instance.PlayClip(detectorEnemy.detectionStartClip);
        while (currentDetection > 0)
        {
            isDetected = currentDetection >= maxDetection;
            currentDetection -= detectionDecreseRate;
            yield return new WaitForSeconds(1);
        }

        isDetected = false;
        isDetectionStarted = false;
        SoundManager.Instance.PlayMainAmbience();
    }
}
