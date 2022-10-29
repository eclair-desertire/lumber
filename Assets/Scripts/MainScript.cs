using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainScript : MonoBehaviour
{
    private int[] axehps;

    public Animator AxeAnimator;

    public GameObject WinterTerrain;
    public GameObject StandardTerrain;
    public GameObject RainPart;
    public GameObject SnowPart; 

    public AudioSource chopSound;
    public AudioSource successSound;

    public TMP_Text UpdateAxeButton;
    public TMP_Text trees_choped;

    public int scorettx = 0;

    public List<GameObject> axes;
    public int currentAxe = 0;

    public int currentTree = 0;
    public List<GameObject> trees;
    public int treeHP=0;
    public int treeScore=0;

    public ParticleSystem axeParticle;
    public ParticleSystem treeParticle;

    private void FixedUpdate()
    {
        checkAxes();
        fillbuttons();
        checkTree();
    }

    private void checkTree()
    {
        Debug.Log("Tree HP: "+treeHP);
        Debug.Log("Current AXE: "+ currentAxe);
        Debug.Log("Current Tree: "+currentTree);
        Debug.Log("AXEHPS: " + axehps[currentAxe]);
        Debug.Log("AXEHPS NEXT: " + axehps[currentAxe+1]);
        if (treeHP <= 0)
        {
            treeHP = UnityEngine.Random.Range(100, 200);
            treeScore += 1;
            trees_choped.text = "Score: " + treeScore.ToString();
            occurency();
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
        if (currentAxe < 7)
        {
            UpdateAxeButton.text = axehps[currentAxe+1].ToString();// ���������������� ���� � �� ���������;
        }
        else
        {
            UpdateAxeButton.text = "NO AXES";
        }
    }

    private void checkAxes()
    {
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
        initTreeHp();
    }

    private void initTreeHp()
    {
        axehps = new int[] { 0,10, 30, 80, 150, 250, 400, 700 };
        treeHP = UnityEngine.Random.Range(70, 120);

    }

    public void ClickButton()
    {
        if (!AxeAnimator.GetBool("Click"))
        {
            attack();
        }
    }
    void attack()
    {
            AxeAnimator.SetBool("Click", true);
            Invoke("playAudio", 0.15f);
            treeHP -= currentAxe + 1;
            Invoke("disabler", 0.08f);
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

    public void updateAxe()
    {
        if (currentAxe < 7)
        {
            if (treeScore >= axehps[currentAxe + 1])
            {
                successSound.Play();
                axeParticle.Play();
                treeScore -= axehps[currentAxe + 1];
                axes[currentAxe].SetActive(false);
                currentAxe += 1;
                axes[currentAxe].SetActive(true);
            }
        }
        
    }
    public void ResetAllVars()
    {
        scorettx = 0;
        axes[currentAxe].SetActive(false);
        trees[currentTree].SetActive(false);
        treeScore = 0;
        currentAxe = 0;
        currentTree = 0;
        axes[currentAxe].SetActive(true);
        trees[currentTree].SetActive(true);
    }
    void occurency()
    {
        int i= UnityEngine.Random.Range(1, 10);
        int j = UnityEngine.Random.Range(1, 3);
        resetWeather();
        resetAllenv();
        occur(i, j);
    }

    void resetAllenv()
    {
        WinterTerrain.SetActive(false);
        StandardTerrain.SetActive(false);
    }

    void resetWeather()
    {
        SnowPart.SetActive(false);
        RainPart.SetActive(false);
    }

    void occur(int i, int j)
    {
        if (i %2== 0){
            StandardTerrain.SetActive(true);
        }
        else if (i%2!=0)
        {
            WinterTerrain.SetActive(true);
        }

        if (j == 1)
        {
            resetWeather();
        }
        else if (j == 2)
        {
            SnowPart.SetActive(true);
        }
        else if (j == 3)
        {
            RainPart.SetActive(true);
        }
    }
}
