using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarningScene : MonoBehaviour
{

	void Start ()
    {
        StartCoroutine(ChangeScene());
	}


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMenu");
    }
}
