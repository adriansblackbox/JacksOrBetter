using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;

		[SerializeField]
		private Button betButton = null;
		public bool gameReset;
		private GameManager gm;
		public float points, totalPoints;

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			betButton.onClick.AddListener(OnBetButtonPressed);
			gm = FindObjectOfType<GameManager>();
			gameReset = true;
			winningText.enabled = false;
			points = totalPoints = 0;
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			if(gameReset) {
				winningText.enabled = false;
				gm.StartGame();
				gameReset = false;
			}
			else {
				winningText.enabled = true;
				gm.DealCards();
				gameReset = true;
			}

		}

		public void DisplayPoints(string intro, int points) {
			if(points > 0) {
				winningText.text = intro + "! You won " + points.ToString() + " credits!";
				totalPoints += points;
				currentBalanceText.text = "Balance: " + totalPoints + " Credits";
			} else {
				winningText.text = "Game Over!";
			}
		}
	}
}