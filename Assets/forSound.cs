using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSound : MonoBehaviour
{
    public AudioSource twinkle;

    public void onCLick()
    {
        twinkle.Play();
    }

    public void stop()
    {
        twinkle.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
