﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FristSceneClass : MonoBehaviour {
    
	void Start ()
    {
        StartCoroutine(WaitSceneControll());
	}
    IEnumerator WaitSceneControll()
    {
        yield return new WaitForSeconds(3);
        /*CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 1.2f;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;*/
        SceneManager.LoadScene(1);
    }
}
