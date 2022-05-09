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
			int rank = Random.Range(1,14);
			Card curCard = new Card(suit, rank);
			hand.Add(curCard);
			Cards[0].GetComponent<CardScript>().ChangeSprite(suit, rank);
			for(int i = 1; i < 5; ++i) {
				curCard.Suit = Random.Range(0,4);
				curCard.Rank = Random.Range(1,14);
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
			for(int i = 0; i < 5; ++i) {
				if(Cards[i].GetComponent<CardScript>().hold) {
					continue;
				}
				int suit = Random.Range(0,4);
				int rank = Random.Range(1,14);
				Card curCard = new Card(suit, rank);
				if(!CheckCards(curCard, i)) {
					hand[i] = curCard;
					Cards[i].GetComponent<CardScript>().ChangeSprite(curCard.Suit, curCard.Rank);
				}else {
					i--;
				}
			}
			CalculatePoints();
		}
		private void CalculatePoints() {
			int points = 0;
			string intro = "";
			if(JacksOrBetter(hand)) {
				points = 1;
				intro = "Jacks Or Better";
			}
			if(CheckThree(hand)) {
				points = 3;
				intro = "Three Of A Kind";
			}
			if(CheckTwoPair(hand)) {
				points = 2;
				intro = "Two Pairs";
			}
			if(CheckStraight(hand)) {
				points = 4;
				intro = "Straight";
			}
			if(CheckFlush(hand)) {
				points = 6;
				intro = "Flush";
			}
			if(CheckFullHouse(hand)) {
				points = 9;
				intro = "Full House";
			}
			if(CheckFour(hand)) {
				points = 25;
				intro = "Four Of A Kind";
			}
			if(CheckStraightFlush(hand)) {
				points = 50;
				intro = "Straight Flush";
			}
			if(CheckRoyalFlush(hand)) {
				points = 800;
				intro = "ROYAL FLUSH";
			}
			UIManager.DisplayPoints(intro, points);
		}
		///////////////////////////////////////////////////////////////////////////////////
		// Helper functions for calculating score
		private bool JacksOrBetter(List<Card> cards) {
			List<int> cardRanks = new List<int>();
			foreach (Card card in cards) {
				cardRanks.Add(card.Rank);
			}
			IEnumerable<int> duplicates = cardRanks.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key);
			if(duplicates.Count() == 1 && (duplicates.ElementAt(0) >= 10))
				return true;
				
			return false;
		}
		private bool CheckTwoPair(List<Card> cards) {
			List<int> cardRanks = new List<int>();
			foreach (Card card in cards) {
				cardRanks.Add(card.Rank);
			}
			IEnumerable<int> duplicates = cardRanks.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key);
			if(duplicates.Count() != 2)
				return false;

			return true;
		}
		private bool CheckThree(List<Card> cards) {
			List<int> cardRanks = new List<int>();
			foreach (Card card in cards) {
				cardRanks.Add(card.Rank);
			}
			Debug.Log(cardRanks.Distinct().Count());
			if(cardRanks.Distinct().Count() == 3)
				return true;
			
			return false;
		}
		private bool CheckFour(List<Card> cards) {
			List<int> cardRanks = new List<int>();
			foreach (Card card in cards) {
				cardRanks.Add(card.Rank);
			}
			if(cardRanks.Distinct().Count() == 2)
				return true;
			else
				return false;
		}
		
		private bool CheckStraight(List<Card> cards) {
				for(int i = 1; i < cards.Count; i++) {
					if(cards[i].Rank != cards[i-1].Rank-1)
						return false;
				}
			return true;
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
			if(!CheckStraight(cards))
				return false;
			return true;
		}
		private bool CheckFullHouse(List<Card> cards) {
			List<int> cardRanks = new List<int>();
			foreach (Card card in cards) {
				cardRanks.Add(card.Rank);
			}
			if(cardRanks.Distinct().Count() != 5)
				return false;
			if(!CheckThree(cards))
				return false;
			return true;
		}
		private bool CheckRoyalFlush(List<Card> cards) {
			if(!CheckFlush(cards))
				return false;
			foreach (Card card in cards) {
				if(card.Rank <= 10)
					return false;
			}
			return true;
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