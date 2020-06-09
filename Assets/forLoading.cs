using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forLoading : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Text text;
    // Start is called before the first frame update
    private int num_points = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(num_points == 3)
        {
            num_points = 0;
            text.text = "     Loading";
        } else
        {
            num_points += 1;
            text.text = text.text + ".";
        }
    }
}
