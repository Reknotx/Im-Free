using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    //Level Ref
    public int level = 1;
    //Lighting Prefabs Ref
    public GameObject LabL;
    public GameObject ZooL;
    public GameObject MilitaryL;
    public GameObject ForestL;
    //Switches on func for prefabs to spawn if false, if true: does not spawn prefabs
    public bool ZooOn = false;
    public bool MilitaryOn = false;
    public bool ForestOn = false;

    //Activates Light Manager, Finds Lab Lighting to ref
    private void Start()
    {
        Light();
        LabL = GameObject.Find("LabLighting");
    }

    //Func to check lighting when player collides
    public void Light()
    {
        switch (level)
        {
            case 4:
                print("Currenty in Forest Level");
                //LabL.SetActive(false);
                MilitaryL = GameObject.FindWithTag("MilitaryLighting");
                if (MilitaryL.activeInHierarchy)
                {
                    MilitaryL.SetActive(false);
                    //Debug.Log("Passed through activeinHiearchy");
                }
                if (ForestOn == false)
                {
                    ForestOn = true;
                    Instantiate(ForestL);
                    //Debug.Log("Passed through Instantiate");
                }
                break;
            case 3:
                print("Currenty in Military Level");
                //LabL.SetActive(false);
                ZooL = GameObject.FindWithTag("ZooLighting");
                if (ZooL.activeInHierarchy)
                {
                    ZooL.SetActive(false);
                    //Debug.Log("Passed through activeinHiearchy");
                }
                if (MilitaryOn == false)
                {
                    MilitaryOn = true;
                    Instantiate(MilitaryL);
                    //Debug.Log("Passed through Instantiate");
                }
                break;
            case 2:
                print("Currently in Zoo Level");
                LabL = GameObject.Find("LabLighting");
                if (LabL.activeInHierarchy)
                {
                    LabL.SetActive(false);
                }
                if (ZooOn == false)
                {
                    ZooOn = true;
                    Instantiate(ZooL);
                }
                break;
            case 1:
                print("Currently in Lab Level");
                LabL = GameObject.Find("LabLighting");
                if (LabL.activeInHierarchy == false)
                {
                    LabL.SetActive(true);
                }
                break;
            default:
                print("Error");
                break;
        }
    }

}
