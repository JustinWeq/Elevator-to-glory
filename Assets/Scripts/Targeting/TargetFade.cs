using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFade : MonoBehaviour
{
    public float Duration = 3.0f;
    private float time_left;
    private Renderer renderer;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        time_left = Duration;
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease duration
        time_left -= Time.deltaTime;
        if(time_left < 0)
        {
            Destroy(gameObject);
        }
        string[] propertys = renderer.material.GetTexturePropertyNames();
        //make the material more transparent
        renderer.material.color = originalColor * new Color(1, 1, 1, time_left / Duration);
        
    }
}
