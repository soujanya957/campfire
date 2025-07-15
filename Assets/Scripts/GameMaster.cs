using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

  
    public GameObject restartPanel;
    public GameObject wonPanel;
    public GameObject instructionsPanel;
    public Text score; 
    public Text lives;
    private bool hasLost;
    private bool hasWon;

    public void Update()
    {
        if (!hasLost && score != null)
        {
            score.text = Time.time.ToString("F0");
        }
    }


    public void LostGameOver() {
        hasLost = true;
        Invoke("DelayLost", 1.5f);
    }

    public void WinGameOver() {
        hasWon = true;
        Invoke("DelayWon", 1.5f);
    }

    void DelayLost() {
        restartPanel.SetActive(true); 
        
    }
    void DelayWon() {
        wonPanel.SetActive(true); 
    }

    public void GoToGameScene() {
        SceneManager.LoadScene("GamePlayAnimation");
    }

    public void LoadInstructions() {
        instructionsPanel.SetActive(true);
    }


    public void Restart() {
        LifeScript.scoreValue = 5;
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMainMenu(){
        LifeScript.scoreValue = 5;
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene("MainMenu");
    }



}
