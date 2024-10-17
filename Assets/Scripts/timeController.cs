using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeController : MonoBehaviour
{
    // minutes from midnight
    public float startTime;
    //current exact time
    private float currentTime;
    //time shown on the clock (rounded to the nearest minute)
    public float shownTime;
    //time speed (how many in-game minutes pass per real second)
    public float timeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;
        shownTime = Mathf.Round(currentTime);
    }
}
