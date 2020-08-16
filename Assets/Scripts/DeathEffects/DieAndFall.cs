using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndFall : MonoBehaviour
{
    public float Duration;
    public float time_left;
    private const float BURY_DEPTH = 5;
    // Start is called before the first frame update
    void Start()
    {
        time_left = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        //rotate the model on the x axis based on duration
        if(time_left > 3*(Duration/4))
        {
            transform.Rotate(new Vector3(180.0f * (Time.deltaTime/(3 * Duration /4)),0,0));
        }
        else if(time_left > Duration/2)
        {
            //do nothing
        }
        else if(time_left > 0)
        {
            //move the model down
            //transform.Translate(new Vector3(0, -BURY_DEPTH * (Time.deltaTime / (Duration / 2)), 0));
            transform.position = transform.position + new Vector3(0, -BURY_DEPTH * (Time.deltaTime / (Duration / 2)), 0);
        }
        else
        {
            //destroy self
            Destroy(gameObject);
        }
    }
}
