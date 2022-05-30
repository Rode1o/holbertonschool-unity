using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
   public static score instance;

    public Text scoreText;
    public int scoreValue;

	private void Start()
	{
		instance = this;
	}

	public void ScoreUpdate(int value)
	{
		scoreValue += value;
		scoreText.text = scoreValue.ToString();
	}

	public void ScoreReset()
	{
		scoreText.gameObject.SetActive(true);
		scoreValue = 0;
		scoreText.text = scoreValue.ToString();
	}
}
