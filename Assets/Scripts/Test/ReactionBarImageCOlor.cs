using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionBarImageCOlor : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Gradient gradient;

    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gradient.colorKeys[1].time -= Time.deltaTime * 3f;
        color = gradient.Evaluate(0.25f);
        Debug.Log(gradient.Evaluate(gradient.colorKeys[1].time));
        image.color = color;

    }
}
