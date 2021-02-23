using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public int current, next;

    public Animation animation;
    public string intro = "SplashIn";

    void Start()
    {
        UnityEngine.XR.InputTracking.Recenter();

        //play intro
        animation.Play(intro);

        Invoke("changeScene", 10);
    }

    void changeScene()
    {
        SceneManager.LoadSceneAsync(next);
        SceneManager.UnloadSceneAsync(current);
    }

}
