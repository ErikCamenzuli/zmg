using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyKillCount : MonoBehaviour
{
    public static EnemyKillCount instance;
    public KillCombo killComboBar;
    public TextMeshProUGUI counterText;
    public int kills;

    // Update is called once per frame
    void Update()
    {
        ShowKills();
    }

    private void ShowKills()
    {
        counterText.text = kills.ToString() + "X";
    }

    public void AddKill()
    {
        kills++;
        killComboBar.Reset();
    }

    public void RemoveKill()
    {
        kills--;
        killComboBar.Reset();
    }
}
