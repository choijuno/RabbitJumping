using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarClass : MonoBehaviour {

    public Image loadingBar;
    bool loadingchk = false;
	void Start ()
    {
        StartCoroutine(NextScene());
	}
	
    IEnumerator NextScene()
    {
        //1은 메인에서 넘길때, 2는 셀렉트에서 넘길때, 3은 게임에서 넘길때
        // 0 메인
        // 1 로딩
        // 2 선택
        // 3 게임
        //
        AsyncOperation async;
        switch (mainSceneManager.SceneIndex)
        {
            case 1:
                async = SceneManager.LoadSceneAsync(2);
                break;
            case 2:
                async = SceneManager.LoadSceneAsync(3);
                break;
            case 3:
                async = SceneManager.LoadSceneAsync(2);
                break;
            default:
                async = null;
                break;
        }

        while (loadingBar.fillAmount >= 0.5f)
        {
            loadingBar.fillAmount += 0.0001f;

            yield return null;
            
        }
        loadingchk = true;
        if (loadingchk == true)
        {
            while (async.isDone == false)
            {
                loadingBar.fillAmount = async.progress * 10f;
                Debug.Log(async.progress);
                Debug.Log(async.isDone);

                yield return true;
            }
        }
    }
}
