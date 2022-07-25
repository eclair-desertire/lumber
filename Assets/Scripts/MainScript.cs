using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainScript : MonoBehaviour
{

    public Animator AxeAnimator;


    public AudioSource chopSound;
    public AudioSource successSound;

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

    public int currentTree = 0;
    public List<GameObject> trees;
    public int treeHP=0;

    public ParticleSystem axeParticle;
    public ParticleSystem treeParticle;
    private void FixedUpdate()
    {
        checkCapacity();
        checkAxes();
        fillbuttons();
        checkTree();
    }

    private void checkTree()
    {
        if (treeHP <= 0)
        {
            treeHP = 50;
            if (currentTree >= 7)
            {
                successSound.Play();
                treeParticle.Play();
                trees[currentTree].SetActive(false);
                currentTree = 0;
                trees[currentTree].SetActive(true);
            }
            else
            {
                successSound.Play();
                treeParticle.Play();
                trees[currentTree].SetActive(false);
                currentTree += 1;
                trees[currentTree].SetActive(true);
            }
            
        }
        
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
        //Debug.Log(Random.RandomRange(100f, 500f));
        if (!axes[currentAxe].activeSelf) {
            axes[currentAxe].SetActive(true);
        }
        if (!trees[currentTree].activeSelf)
        {
            trees[currentTree].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initVariables();
        initTreeHp();
    }

    private void initTreeHp()
    {
        //treeHP = UnityEngine.Random.Range(200, 500);
        treeHP = 50;
    }

    public void ClickButton()
    {
        if (!AxeAnimator.GetBool("Click"))
        {
            attack();
        }
    }
    void initVariables()
    {
        
        priceToFix = (AxeScriptableObjectList[currentAxe].price / 3);

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
            scorettx += currentAxe+1;
            treeHP -= currentAxe + 1;
            score_text.text = "Score: " + scorettx.ToString();
            Invoke("disabler", 0.08f);
        }
    }
    void disabler()
    {
        AxeAnimator.SetBool("Click", false);
    }
    void playAudio()
    {
        Debug.Log("PLAYED");
        chopSound.Play();
    }

    public void FixAxe()
    {
        if (timesFixed == 0)
        {
            priceToFix = (int)(AxeScriptableObjectList[currentAxe].price / 3)+10;
            if (scorettx >= priceToFix)
            {
                scorettx -= priceToFix;
                AxeScriptableObjectList[currentAxe].currentCapacity = AxeScriptableObjectList[currentAxe].maxCapacity;
                timesFixed += 1;
            }
        }
        else
        {
            priceToFix = ((int)(AxeScriptableObjectList[currentAxe].price / 3) + (timesFixed * 8))+10;
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
                successSound.Play();
                axeParticle.Play();
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
        trees[currentTree].SetActive(false);
        currentAxe = 0;
        currentTree = 0;
        axes[currentAxe].SetActive(true);
        trees[currentTree].SetActive(true);
        timesFixed = 0;
        initVariables();
        foreach(AxeScriptableObject a in AxeScriptableObjectList)
        {
            a.currentCapacity = a.maxCapacity;
        }
    }
}
