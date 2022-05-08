using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public GameObject[] Cards;
		private List<int> hand_suits = new List<int>();
		private List<int> hand_values = new List<int>();
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			CardButtons(false);
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}
		public void StartGame() {
			ResetCards();
			CardButtons(true);
			hand_suits.Clear();
			hand_values.Clear();
			int suit = Random.Range(0,4);
			int value = Random.Range(0,13);
			hand_suits.Add(suit);
			hand_values.Add(value);
			Cards[0].GetComponent<CardScript>().ChangeSprite(suit, value);
			for(int i = 1; i < 5; ++i) {
				suit = Random.Range(0,4);
				value = Random.Range(0,13);
				if(!CheckCards(suit, value, i)) {
					hand_suits.Add(suit);
					hand_values.Add(value);
					Cards[i].GetComponent<CardScript>().ChangeSprite(suit, value);
				}else {
					i--;
				}
			}
		}
		public void DealCards() {
			CardButtons(false);
			//iterate over the hand, and check if it's held or not
			//if it is, leave it alone.
			//if not, remove the array index from both the suite and value lists
			for(int i = 1; i < 5; ++i) {
				if(Cards[0].GetComponent<CardScript>().hold) {
					continue;
				}
				int suit = Random.Range(0,4);
				int value = Random.Range(0,13);
				if(!CheckCards(suit, value, i)) {
					hand_suits.Add(suit);
					hand_values.Add(value);
					Cards[i].GetComponent<CardScript>().ChangeSprite(suit, value);
				}else {
					i--;
				}
			}
		}
		private void ResetCards() {
			for(int i = 0; i < Cards.Length; i++) {
				Cards[i].GetComponent<CardScript>().ResetCard();
			}
		}
		private void CardButtons(bool enabled) {
			for(int i = 0; i < Cards.Length; i++) {
				Cards[i].GetComponent<Button>().enabled = enabled;
			}
		}
		// Returns true if the given suit and value combination matches with
		// a card in the player's hand
		private bool CheckCards(int suit, int value, int index) {
			for(int i = 0; i < hand_suits.Count; i++) {
				if(index == i) 
					continue;
				if(hand_suits[i] == suit && hand_values[i] == value)
					return true;
			}
			return false;
		}
	}
}