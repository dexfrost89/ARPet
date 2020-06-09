using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class forMenu : MonoBehaviour
{

    private GameObject loadingCanvas;

    public void OnClick(string scene)
    {
        loadingCanvas.SetActive(true);
        SceneManager.LoadScene(scene);
        gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        loadingCanvas = GameObject.Find("LoadingCanvas");
        DontDestroyOnLoad(loadingCanvas);
        loadingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
