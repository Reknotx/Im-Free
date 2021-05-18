using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    //Level Ref
    public int level;
    //Lighting Prefabs Ref
    public GameObject LabL;
    public GameObject ZooL;
    public GameObject MilitaryL;
    public GameObject ForestL;
    public GameObject CurrentLight;
    //Switches on func for prefabs to spawn if false, if true: does not spawn prefabs
    public bool LabOn;
    public bool ZooOn;
    public bool MilitaryOn;
    public bool ForestOn;

    Destroy DestroyScript;

    //Activates Light Manager, Finds Lab Lighting to ref
    private void Start()
    {
        LabOn = true;
        ZooOn = false;
        MilitaryOn = false;
        ForestOn = false;
        level = 1;
        //Light();
        //LabL = GameObject.FindWithTag("LabLighting");
        DestroyScript = CurrentLight.GetComponent<Destroy>();
    }

    //Func to check lighting when player collides
    public void Light()
    {
        switch (level)
        {
            case 4:
                print("Currenty in Forest Level");
                //MilitaryL = GameObject.FindWithTag("MilitaryLighting");
                if (MilitaryOn == true)
                {
                    DestroyScript.Delete();
                    //Destroy(MilitaryL);
                    MilitaryOn = false;
                    //MilitaryL.SetActive(false);
                    //Debug.Log("Passed through activeinHiearchy");
                }
                if (ForestOn == false)
                {
                    ForestOn = true;
                    //Instantiate(ForestL);
                    CurrentLight = Instantiate(ForestL);
                    DestroyScript = CurrentLight.GetComponent<Destroy>();
                    //Debug.Log("Passed through Instantiate");
                }
                return;
            case 3:
                print("Currenty in Military Level");
                //ZooL = GameObject.FindWithTag("ZooLighting");
                if (ZooOn == true)
                {
                    DestroyScript.Delete();
                    //Destroy(ZooL);
                    ZooOn = false;
                    //ZooL.SetActive(false);
                    //Debug.Log("Passed through activeinHiearchy");
                }
                if (MilitaryOn == false)
                {
                    MilitaryOn = true;
                    //Instantiate(MilitaryL);
                    CurrentLight = Instantiate(MilitaryL);
                    DestroyScript = CurrentLight.GetComponent<Destroy>();
                    //Debug.Log("Passed through Instantiate");
                }
                return;
            case 2:
                print("Currently in Zoo Level");
                //LabL = GameObject.FindWithTag("LabLighting");
                if (LabOn == true)
                {
                    DestroyScript.Delete();
                    //Destroy(LabL);
                    LabOn = false;
                    //LabL.SetActive(false);
                }
                if (ZooOn == false)
                {
                    ZooOn = true;
                    //Instantiate(ZooL);
                    CurrentLight = Instantiate(ZooL);
                    DestroyScript = CurrentLight.GetComponent<Destroy>();
                }
                return;
            case 1:
                print("Currently in Lab Level");
                //ForestL = GameObject.FindWithTag("ForestLighting");
                if (LabOn == false && ForestOn == true)
                {
                    DestroyScript.Delete();
                    CurrentLight = Instantiate(LabL);
                    DestroyScript = CurrentLight.GetComponent<Destroy>();
                    LabOn = true;
                    ForestOn = false;
                    //Debug.Log("Passed through activeinHiearchy");
                }
                return;
            default:
                print("Error");
                break;
        }
    }

}
