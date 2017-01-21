﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VRSlider : MonoBehaviour
{
    // how long it takes the fill the slider
    public float fillTime = 2f;
    public int Scene = 1;
    // private vars
    private Slider mySlider;
    private float timer;
    private bool gazedAt;
    private Coroutine fillBarRoutine;

    // Use this for initialization
    void Start()
    {
        mySlider = GetComponent<Slider>();
        if (mySlider == null) Debug.Log("Please Add a slider component to this GameObject");
    }

    // PointerEnter
    public void PointerEnter()
    {
        gazedAt = true;
        fillBarRoutine = StartCoroutine(FillBar());
    }


    // PointerExit
    public void PointerExit()
    {
        gazedAt = false;
        if (fillBarRoutine!=null)
        {
            StopCoroutine(fillBarRoutine);
        }
        timer = 0f;
        mySlider.value = 0f;
    }
    // Fill the bar
    private IEnumerator FillBar()
    {
        //when the bar starts to fill, reset the timer.
        timer = 0f;
        // until the timer greater than fill time...
        while (timer < fillTime)
        {
            //... add the timer the difference between frames.
            timer += Time.deltaTime;
            // set the value of the slider 
            mySlider.value = timer / fillTime;
            //wait until next frame.
            yield return null;
            
            // if the user still looking at the bar, go on the next iteration of the loop.
            if (gazedAt)
            {
                continue;
            }
            // if the user is no longer looking at the bar, reset the timer and bar and leave the function.
            timer = 0f;
            mySlider.value = 0f;
            yield break;
        }
        // the bar has been filled
        OnBarFilled();
    }

    private void OnBarFilled()
    {
        Debug.Log("Open Scene: "+Scene);
        
    }

}
