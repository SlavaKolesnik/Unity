using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Waves _waves;
    public Text textWaves;
    // Start is called before the first frame update
    void Start()
    {
        _waves = FindObjectOfType<Waves>();
        textWaves = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        WavesUIPrint();
    }
    void WavesUIPrint()
    {
        textWaves.text = "" + _waves.WavesCount;
    }
}
