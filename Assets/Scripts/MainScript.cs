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
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            attack();
        }
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
