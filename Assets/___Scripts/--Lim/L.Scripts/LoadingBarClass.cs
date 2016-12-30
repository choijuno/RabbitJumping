using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarClass : MonoBehaviour {

    public Image loadingBar;
    public Text tip;
    bool loadingchk = false;
	void Start ()
    {
        if (!ES2.Exists("Language"))
        {
            ES2.Save<bool>(false, "Language");
            tip.text = "Tip) You can tile version~";
        }
        if (ES2.Load<bool>("Language"))
        {
            tip.text = "Tip) 틸트 모드도 있어요~";
        }
        else
        {
            tip.text = "Tip) You can tile version~";
        }
        MusicManager.instance.MusicSelect(false);
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
                async = SceneManager.LoadSceneAsync(3);
                break;
            case 2:
                async = SceneManager.LoadSceneAsync(4);
                break;
            case 3:
                async = SceneManager.LoadSceneAsync(3);
                break;
            default:
                async = null;
                break;
        }

        while (loadingBar.fillAmount >= 0.5f)
        {
            loadingBar.fillAmount += 0.001f;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        loadingchk = true;
        if (loadingchk == true)
        {
            while (async.isDone == false)
            {
                loadingBar.fillAmount = async.progress * 100f;
                yield return true;
            }
        }
    }
}
