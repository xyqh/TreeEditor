using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var node = TreeHelper.GetRootNodeFromAsset("TreeAsset/NewTree");
        TreeHelper.PrintTree(node);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
