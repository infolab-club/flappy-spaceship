using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool startedGame = false;
	public bool stillAlive = true;

    public GameObject pressText;
    public Text scoreText;
    public Text maxScoreText;
    public BirdMovement birdMovementScript;
    public GameObject[] movableObjectParents;

    private void Update(){
        if (!startedGame && Input.GetMouseButtonUp(0)){
            pressText.SetActive(false);
            startedGame = true;
        }
        if (startedGame){
            scoreText.text = "" + GameData.Score;
            maxScoreText.text = "МАКСИМУМ: " + GameData.MaxScore;
        }

        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0) && !stillAlive && startedGame) {
            ResetGame();
        }
    }

    public void AddPoints(int points) {
        GameData.Score += points;
    }

    public void ResetGame(){
        scoreText.text = "" + GameData.Score;
        startedGame = false;
        stillAlive = true;
        pressText.SetActive(true);
        foreach (GameObject gb in movableObjectParents) {
            foreach (Transform child in gb.transform) {
                Destroy(child.gameObject);
            }
            gb.GetComponent<ObjectsMoveManager>().SpawnCall();
        }
        birdMovementScript.ResetBird();
        if (GameData.Score > GameData.MaxScore)
        {
            GameData.MaxScore = GameData.Score;
        }
        GameData.Score = 0;
    }
}
