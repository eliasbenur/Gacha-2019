using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{

    public Material whiteMat;
    public Material blackMat;
    public Material skyMatWhite;

    // Start is called before the first frame update
    public ResourceLoader()
    {
        whiteMat = Resources.Load<Material>("WhiteMat");
        blackMat = Resources.Load<Material>("BlackMat");
        skyMatWhite = Resources.Load<Material>("SkyWhite");
    }

}
