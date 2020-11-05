using UnityEngine;
using System.Collections;
using DevelopEngine;
using UnityEngine.SceneManagement;
public class GameMain : MonoSingleton<GameMain> {
    AsyncOperation asc;
    public readonly string config = "/config.txt";
    string gameStartName;
	IEnumerator Start () {
        DontDestroyOnLoad(gameObject);
        SkillManager.Instance.InitSkill();
        Configuration.LoadConfig(config);
        while (!Configuration.IsDone)
            yield return null;
        gameStartName = Configuration.GetContent("Scene", "LoadGameStart");        
       // Debug.Log(gameStartName);
        StartCoroutine(LoadUIScene(gameStartName));

	}
    /// <summary>
    /// 加载进度条场景
    /// </summary>
    /// <returns></returns>
    IEnumerator Loading()
    {
        asc = SceneManager.LoadSceneAsync("Loading");
        yield return asc;
        
    }
	public IEnumerator LoadUIScene(string uiSceneName)
    {
        Global.LoadUIName = uiSceneName;
        
        yield return StartCoroutine(Loading());
    }
    public IEnumerator LoadAllScene(string uiSceneName, string sceneName)
    {
        Global.Contain3DScene = true;
        Global.LoadUIName = uiSceneName;
        Global.LoadSceneName = sceneName;
        yield return StartCoroutine(Loading());
    }
	
	
}
