using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainScript : MonoBehaviour
{

    public Animator AxeAnimator;


    public AudioSource audioSource;

    public TMP_Text score_text;
    public TMP_Text FixAxeButton;
    public TMP_Text UpdateAxeButton;

    public int scorettx = 0;

    public List<GameObject> axes;
    public int currentAxe = 0;
    public int timesFixed = 0;
    public List<AxeScriptableObject> AxeScriptableObjectList;

    public Image fillAmount;

    public int priceToFix=0;


    private void FixedUpdate()
    {
        checkCapacity();
        checkAxes();
        fillbuttons();
    }

    private void fillbuttons()
    {
        FixAxeButton.text = "Fix Axe:\n" + priceToFix.ToString();
        if (currentAxe < 6)
        {
            UpdateAxeButton.text = "Upgrade Axe:\n" + AxeScriptableObjectList[currentAxe + 1].price.ToString();
        }
        else
        {
            UpdateAxeButton.text = "NO AXES TO UPGRADE";
        }
    }

    private void checkAxes()
    {
        if (!axes[currentAxe].activeSelf) {
            axes[currentAxe].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!AxeAnimator.GetBool("Click"))
            {
                attack();
            }
            
        }
    }
    void initVariables()
    {
        priceToFix = (AxeScriptableObjectList[currentAxe].price / 2)-5;

    }
    void checkCapacity() {
        float currentAmountInProcent = (1f * (float)AxeScriptableObjectList[currentAxe].currentCapacity) / (float)AxeScriptableObjectList[currentAxe].maxCapacity;
        fillAmount.fillAmount = currentAmountInProcent;
    }

    void attack()
    {
        if (AxeScriptableObjectList[currentAxe].currentCapacity>0) {
            AxeAnimator.SetBool("Click", true);
            Invoke("playAudio", 0.15f);
            AxeScriptableObjectList[currentAxe].currentCapacity -= 1;
            scorettx += 1;
            score_text.text = "Score: " + scorettx.ToString();
            Invoke("disabler", 0.25f);
        }
    }
    void disabler()
    {
        AxeAnimator.SetBool("Click", false);
    }
    void playAudio()
    {
        Debug.Log("PLAYED");
        audioSource.Play();
    }

    public void FixAxe()
    {
        if (timesFixed == 0)
        {
            priceToFix = (int)(AxeScriptableObjectList[currentAxe].price / 2)-5;
            if (scorettx >= priceToFix)
            {
                scorettx -= priceToFix;
                AxeScriptableObjectList[currentAxe].currentCapacity = AxeScriptableObjectList[currentAxe].maxCapacity;
                timesFixed += 1;
            }
        }
        else
        {
            priceToFix = ((int)(AxeScriptableObjectList[currentAxe].price / 2) + (timesFixed * 10))-5;
            if (scorettx >= priceToFix)
            {
                scorettx -= priceToFix;
                AxeScriptableObjectList[currentAxe].currentCapacity = AxeScriptableObjectList[currentAxe].maxCapacity;
                timesFixed += 1;
            }

        }
    }
    public void updateAxe()
    {
        if (currentAxe < 6)
        {
            if (scorettx >= AxeScriptableObjectList[currentAxe + 1].price)
            {
                scorettx -= AxeScriptableObjectList[currentAxe + 1].price;
                axes[currentAxe].SetActive(false);
                currentAxe += 1;
                axes[currentAxe].SetActive(true);
                timesFixed = 0;
                initVariables();
            }
        }
        
    }
    public void ResetAllVars()
    {
        scorettx = 0;
        axes[currentAxe].SetActive(false);
        currentAxe = 0;
        axes[currentAxe].SetActive(true);
        timesFixed = 0;
        initVariables();
        foreach(AxeScriptableObject a in AxeScriptableObjectList)
        {
            a.currentCapacity = a.maxCapacity;
        }
    }
}
