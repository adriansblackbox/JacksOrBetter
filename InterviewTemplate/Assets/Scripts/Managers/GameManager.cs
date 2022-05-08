using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public GameObject Card_1, Card_2, Card_3, Card_4, Card_5;
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}
		public void StartGame() {
			//test run
			Debug.Log("Started Game!");
			Card_1.GetComponent<CardScript>().ResetCard();
			Card_2.GetComponent<CardScript>().ResetCard();
			Card_3.GetComponent<CardScript>().ResetCard();
			Card_4.GetComponent<CardScript>().ResetCard();
			Card_5.GetComponent<CardScript>().ResetCard();
		}
		public void DealCards() {
			Debug.Log("Game Over!");
		}
	}
}