using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	///
	struct Card 
	{
		public int Suit;
		public int Rank;
		public Card (int suit, int rank) {
			Suit = suit;
			Rank = rank;
		}
	}
	public class GameManager : MonoBehaviour
	{
		public GameObject[] Cards;
		private List<Card> hand = new List<Card>();
		private UIManager UIManager;
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			UIManager = FindObjectOfType<UIManager>();
			CardButtons(false);
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}
		// StartGame() get's called both at the start of the game, and when the player 
		public void StartGame() {
			ResetCards();
			CardButtons(true);
			hand.Clear();
			int suit = Random.Range(0,4);
			int rank = Random.Range(0,13);
			Card curCard = new Card(suit, rank);
			hand.Add(curCard);
			Cards[0].GetComponent<CardScript>().ChangeSprite(suit, rank);
			for(int i = 1; i < 5; ++i) {
				curCard.Suit = Random.Range(0,4);
				curCard.Rank = Random.Range(0,13);
				if(!CheckCards(curCard, i)) {
					hand.Add(curCard);
					Cards[i].GetComponent<CardScript>().ChangeSprite(curCard.Suit, curCard.Rank);
				}else {
					i--;
				}
			}
		}
		// DealCards() is called if the player presses the button after they've been
		// delt their first hand
		public void DealCards() {
			CardButtons(false);
			Card curCard = new Card(0, 0);
			for(int i = 0; i < 5; ++i) {
				if(Cards[i].GetComponent<CardScript>().hold) {
					continue;
				}
				curCard.Suit = Random.Range(0,4);
				curCard.Rank = Random.Range(0,13);
				if(!CheckCards(curCard, i)) {
					hand.RemoveAt(i);
					hand.Add(curCard);
					Cards[i].GetComponent<CardScript>().ChangeSprite(curCard.Suit, curCard.Rank);
				}else {
					i--;
				}
			}
			CalculatePoints();
		}
		private void CalculatePoints() {
			int points = 0;
			if(CheckFlush(hand))
				points = 6;
			if(CheckStraightFlush(hand))
				points = 50;
			UIManager.DisplayPoints(points);
		}
		///////////////////////////////////////////////////////////////////////////////////
		// Helper functions for calculating score
		private bool CheckTwoPair(List<Card> cards) {

			return false;
		}
		private bool CheckThree(List<Card> cards) {
			return false;
		}
		private bool CheckFour(List<Card> cards) {
			return false;
		}
		
		private bool CheckStraight(List<Card> cards) {
			return false;
		}
		private bool CheckFlush(List<Card> cards) {
			int firstsuit = cards[0].Suit;
			foreach (Card card in cards) {
				if(card.Suit != firstsuit)
					return false;
			}
			return true;
		}
		private bool CheckStraightFlush(List<Card> cards) {
			if(!CheckFlush(cards))
				return false;
			if(cards[0].Rank == cards[1].Rank+1) {
				for(int i = 1; i < cards.Count; i++) {
					if(cards[i].Rank != cards[i-1].Rank-1)
						return false;
				}
			}else {
				for(int i = 1; i < cards.Count; i++) {
					if(cards[i].Rank != cards[i-1].Rank+1)
						return false;
				}
			}
			return true;
		}
		private bool CheckFullHouse(List<Card> cards) {
			return false;
		}
		private bool CheckRoyalFlush(List<Card> cards) {
			return false;
		}
		///////////////////////////////////////////////////////////////////////////////////

		// Resets the cards to their default image & removes hold
		private void ResetCards() {
			for(int i = 0; i < Cards.Length; i++) {
				Cards[i].GetComponent<CardScript>().ResetCard();
			}
		}
		// Turns the card buttons off if the boolean passed is false, and vice versa
		private void CardButtons(bool enabled) {
			for(int i = 0; i < Cards.Length; i++) {
				Cards[i].GetComponent<Button>().enabled = enabled;
			}
		}
		// Returns true if the given suit and rank combination matches with
		// a card in the player's hand
		private bool CheckCards(Card card, int index) {
			for(int i = 0; i < hand.Count; i++) {
				if(index == i) 
					continue;
				if(hand[i].Suit == card.Suit && hand[i].Rank == card.Rank)
					return true;
			}
			return false;
		}
	}
}