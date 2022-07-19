using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public List<GameObject> trees;
    public List<GameObject> axes;

    [SerializeField]
    private int score = 0;

    [SerializeField]
    private int axe = 1;

    [SerializeField]
    private int money = 100;


    public Animator AxeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!AxeAnimator.GetBool("Click"))
            {
                attack();
            }
            
        }
        Debug.Log(AxeAnimator.GetBool("Click"));
    }

    void attack()
    {
        AxeAnimator.SetBool("Click", true);
        Invoke("disabler", 1f);
    }
    void disabler()
    {
        AxeAnimator.SetBool("Click", false);
    }
}
