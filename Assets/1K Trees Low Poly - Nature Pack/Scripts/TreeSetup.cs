// MARCIN'S Assets ( https://assetstore.unity.com/publishers/6702 )
// 1K Trees Low Poly - Nature Pack
// Copyright 2021 

using UnityEngine;

public class TreeSetup : MonoBehaviour
{
    [Range(0,9)]
    public int Genre = 0;
    [Range(0, 9)]
    public int TreeType = 0;
    [Range(0, 9)]
    public int StartIndex = 0;
    public float GrowSpeed = 1.0f;

    private GameObject treeObj;
    
    void Start()
    {
        string str = "G" + Genre + "_T" + TreeType + "_ID_" + StartIndex;
        GameObject gameObject = Resources.Load<GameObject>(str) as GameObject;
        treeObj  = Instantiate(gameObject, transform);

        treeObj.name = str;
        treeObj.AddComponent<TreeGrowCtrl>();
        TreeGrowCtrl treeGrow = treeObj.GetComponent<TreeGrowCtrl>();
        treeGrow.Genre = Genre;
        treeGrow.TreeType = TreeType;
        treeGrow.ID = StartIndex;
        treeGrow.t = GrowSpeed; 
    }
}
