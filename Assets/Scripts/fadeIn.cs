using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeIn : MonoBehaviour {
    public Image white;
    Scene currentScene;

    // Start is called before the first frame update
    void Start() {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "museum") {
            Fade(0);
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public void Fade(float targetParam) {
        white.CrossFadeAlpha(targetParam, 1, false);
    }
}
