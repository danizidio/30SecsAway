using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsMenu : MonoBehaviour {

   public bool vai;

    public void OpenConfig()
    {
        if(!vai)
        {
            GetComponent<Animator>().SetBool("show",!vai);
            vai = true;
        }
        else
        {   
            GetComponent<Animator>().SetBool("show", !vai);
            vai = false;
        }
    }
}
