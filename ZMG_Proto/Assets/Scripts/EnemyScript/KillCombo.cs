using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCombo : MonoBehaviour
{

    public Slider barTimer;
    public float maxTime = 5f;
    private float timeLeft;
    public EnemyKillCount killCounter;

    // Start is called before the first frame update
    void Start()
    {
        barTimer = GetComponent<Slider>();
        timeLeft = maxTime;
        barTimer.maxValue = maxTime;
    }

    private void Update()
    {
        if(timeLeft > 0 && killCounter.kills !=0)
        {
            timeLeft -= Time.deltaTime;
            barTimer.value = timeLeft;
        }
        if (timeLeft  < 0)
        {
            killCounter.RemoveKill();
        }
    }

    public void Reset()
    {
        timeLeft = maxTime;
    }


}
