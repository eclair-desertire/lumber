using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainScript : MonoBehaviour
{


    [SerializeField]
    private int score = 0;


    [SerializeField]
    private int money = 100;


    public List<Animator> AxeAnimator;


    public AudioSource audioSource;

    public TMP_Text score_text;

    public int scorettx = 0;

    public List<GameObject> axes;
    public int currentAxe = 0;
    public int timesFixed = 0;
    public List<AxeScriptableObject> AxeScriptableObject;

    public Image fillAmount;

    private void FixedUpdate()
    {
        checkCapacity();
        checkAxes();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!AxeAnimator[currentAxe].GetBool("Click"))
            {
                attack();
            }
            
        }
    }
    void checkCapacity() {
        float currentAmountInProcent = (1f * (float)AxeScriptableObject[currentAxe].currentCapacity) / (float)AxeScriptableObject[currentAxe].maxCapacity;
        fillAmount.fillAmount = currentAmountInProcent;
    }

    void attack()
    {
        if (AxeScriptableObject[currentAxe].currentCapacity>0) {
            AxeAnimator[currentAxe].SetBool("Click", true);
            Invoke("playAudio", 0.3f);
            AxeScriptableObject[currentAxe].currentCapacity -= 1;
            scorettx += 1;
            score_text.text = "Score: " + scorettx.ToString();
            Invoke("disabler", 1f);
        }
    }
    void disabler()
    {
        AxeAnimator[currentAxe].SetBool("Click", false);
    }
    void playAudio()
    {
        Debug.Log("PLAYED");
        audioSource.Play();
    }
}
