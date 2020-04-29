using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public Transform minX;
    public Transform maxX;
    public Animator canvasAnim;

    // Start is called before the first frame update
    void Awake()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        //RESTART
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Revive()
    {
        canvasAnim.SetTrigger("ReviveTrigger");
        PlayerController.pc.GettingRevived();
    }

    public void StartLevel()
    {
        canvasAnim.SetTrigger("StartLevelTrigger");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void WinLevel()
    {
        canvasAnim.SetTrigger("WinLevelTrigger");
        PlayerController.pc.SetMovementLock(true, true);
        PlayerController.pc.SetMovementLock(false, true);
    }

    public void FailLevel()
    {
        canvasAnim.SetTrigger("FailLevelTrigger");
        PlayerController.pc.SetMovementLock(true, true);
        PlayerController.pc.SetMovementLock(false, true);
    }

    public float RemapValues(float _value, float _low1, float _max1, float _low2, float _max2)
    {
        float result = _low2 + (_value - _low1) * (_max2 - _low2) / (_max1 - _low1);
        return result;
    }
}
