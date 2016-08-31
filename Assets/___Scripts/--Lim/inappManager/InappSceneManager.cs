using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InappSceneManager : MonoBehaviour {

    Button gold100;
    Button gold50;

    Button back;
	// Use this for initialization
	void Start () {
        gold100 = GameObject.Find("gold100").GetComponent<Button>();
        gold50 = GameObject.Find("gold50").GetComponent<Button>();
        back = GameObject.Find("back").GetComponent<Button>();

        gold100.onClick.AddListener(gold100func);
        gold50.onClick.AddListener(gold50func);
        back.onClick.AddListener(backfunc);
    }
    void gold100func()
    {
        //InappManager.Instance.Buy100Gold();
    }
    void gold50func()
    {
        //InappManager.Instance.Buy50Gold();
    }
    void backfunc()
    {
        SceneManager.LoadScene(0);
    }
}
