using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lingo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		int _score = 0;
		int nextRow = 0;
		Random random;
		bool IsGameWon = false;
		string _WordToGuess = String.Empty;
		string[] _WordToGuessList = new string[] { "Abuse", "Adult", "Agent", "Anger", "Apple", "Award", "Basis", "Beach", "Birth", "Block", "Blood", "Board", "Brain", "Bread", "Break", "Brown", "Buyer", "Cause", "Chain", "Chair", "Chest", "Chief", "Child", "China", "Claim", "Class", "Clock", "Coach", "Coast", "Court", "Cover", "Cream", "Crime", "Cross", "Crowd", "Crown", "Cycle", "Dance", "Death", "Depth", "Doubt", "Draft", "Drama", "Dream", "Dress", "Drink", "Drive", "Earth", "Enemy", "Entry", "Error", "Event", "Faith", "Fault", "Field", "Fight", "Final", "Floor", "Focus", "Force", "Frame", "Frank", "Front", "Fruit", "Glass", "Grant", "Grass", "Green", "Group", "Guide", "Heart", "Henry", "Horse", "Hotel", "House", "Image", "Index", "Input", "Issue", "Japan", "Jones", "Judge", "Knife", "Laura", "Layer", "Level", "Lewis", "Light", "Limit", "Lunch", "Major", "March", "Match", "Metal", "Model", "Money", "Month", "Motor", "Mouth", "Music", "Night", "Noise", "North", "Novel", "Nurse", "Offer", "Order", "Other", "Owner", "Panel", "Paper", "Party", "Peace", "Peter", "Phase", "Phone", "Piece", "Pilot", "Pitch", "Place", "Plane", "Plant", "Plate", "Point", "Pound", "Power", "Press", "Price", "Pride", "Prize", "Proof", "Queen", "Radio", "Range", "Ratio", "Reply", "Right", "River", "Round", "Route", "Rugby", "Scale", "Scene", "Scope", "Score", "Sense", "Shape", "Share", "Sheep", "Sheet", "Shift", "Shirt", "Shock", "Sight", "Simon", "Skill", "Sleep", "Smile", "Smith", "Smoke", "Sound", "South", "Space", "Speed", "Spite", "Sport", "Squad", "Staff", "Stage", "Start", "State", "Steam", "Steel", "Stock", "Stone", "Store", "Study", "Stuff", "Style", "Sugar", "Table", "Taste", "Terry", "Theme", "Thing", "Title", "Total", "Touch", "Tower", "Track", "Trade", "Train", "Trend", "Trial", "Trust", "Truth", "Uncle", "Union", "Unity", "Value", "Video", "Visit", "Voice", "Waste", "Watch", "Water", "While", "White", "Whole", "Woman", "World", "Youth"};
		public MainWindow()
		{
			InitializeComponent();
			
			random = new Random();
			int word_indext = random.Next(0, _WordToGuessList.Length);

			 _WordToGuess = _WordToGuessList[word_indext].ToUpper();
			populateWord(_WordToGuess.ToUpper(), nextRow, true);
		}

		public void UpdateScore(int increase)
		{
			int current_score=0;
			Int32.TryParse(Score.Content.ToString(), out current_score);

			//int current_Score = (int)Score.Content;
			int newScore = current_score + increase;
			Score.Content =$"{newScore}";
		}

		public void ClearBoard()
		{
			WordGrid.Children.Clear();
			nextRow = 0;
			UserWord.Text = String.Empty;
		}
		public MessageBoxResult GameWon()
		{
			MessageBoxResult _messagePromptResult = MessageBox.Show("You Won! Play again?", "Want To Play Again?", MessageBoxButton.YesNo, MessageBoxImage.Question);
			return _messagePromptResult;
		}
		public MessageBoxResult GameLost()
		{
			MessageBoxResult _messagePromptResult = MessageBox.Show("You Lost! Play again?", "Want To Play Again?", MessageBoxButton.YesNo, MessageBoxImage.Question);
			return _messagePromptResult;
		}
		public void ResetGame()
		{
			ClearBoard();
			int word_indext = random.Next(0, _WordToGuessList.Length);

			_WordToGuess = _WordToGuessList[word_indext].ToUpper();
			populateWord(_WordToGuess.ToUpper(), nextRow, true);
		}

		private void UserWord_KeyDown(object sender, KeyEventArgs e)
		{
		
			
			if (e.Key == Key.Enter)
			{
				if (nextRow + 1 > 5)
				{
					if (GameLost() == MessageBoxResult.Yes)
					{
						ResetGame();


					}
				}

				if (!IsGameWon)
				{
					if (UserWord.Text.Length != _WordToGuess.Length)
					{
						return;
					}



					populateWord(UserWord.Text, nextRow);


					if (UserWord.Text.ToUpper().Equals(_WordToGuess.ToUpper()))
					{
						IsGameWon = true;
						switch (nextRow)
						{
							case 1:
								UpdateScore(5);
								break;
							case 2:
								UpdateScore(4);
								break;
							case 3:
								UpdateScore(3);
								break;
							case 4:
								UpdateScore(2);
								break;
							case 5:
								UpdateScore(1);
								break;
							
							default:
								UpdateScore(0);
								break;
						}
						//populateWord(UserWord.Text, nextRow);

						if (GameWon() == MessageBoxResult.Yes)
						{
							ResetGame();
						}
					}
					UserWord.Text = String.Empty;
				}
				else
				{
					if (GameWon() == MessageBoxResult.Yes)
					{
						ResetGame();
					}
				}
			}

		
			
		}

		private int[] letterPosition()
		{
			 random = new Random();
			int[] positions = new int[2]
			{
				random.Next(0, 5),
				random.Next(0, 5)
		};

			return positions;
		}

		private bool LetterPositionMatch(string word, char c, int position)
		{
			

			if (word[position].ToString().ToUpper() == c.ToString().ToUpper()){
				return true;
			}
			else
			{
				return false;
			}

			
		}
		private bool LetterExist(string word, char c)
		{


			
			return word.Contains(c);


		}
		private int letterCountMatch(string word, char c)
		{
			var count = word.Count(x => x == c);
			return count;
		}


		public void populateWord(string word, int _gridRow,bool _default = false)
		{
			
			if (!_default)
			{
				

				int _gridColumn = 0;
				for (int i = 0; i < word.Length; i++) { 
				
					TextBlock textBlock = new TextBlock();
					textBlock.VerticalAlignment = VerticalAlignment.Stretch;
					textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
					textBlock.Padding = new Thickness(20, 0, 0, 0);
					textBlock.FontSize = 30;


					if (LetterPositionMatch(_WordToGuess, word[i], i))
					{
						textBlock.Background = Brushes.LightGreen;
						
					}
					if (LetterExist(_WordToGuess, word[i]) && !(LetterPositionMatch(_WordToGuess, word[i], i)) && letterCountMatch(_WordToGuess, word[i])== letterCountMatch(word, word[i]))
					{
						textBlock.Background = Brushes.LightSkyBlue;

					}
					
					textBlock.Text = word[i].ToString();
					
					
					Grid.SetColumn(textBlock, _gridColumn); ;
					Grid.SetRow(textBlock, _gridRow);




					_gridColumn++;
					WordGrid.Children.Add(textBlock);
				}
			}
			else
			{
				int[] _letter = letterPosition();




				for (int _index = 0; _index < word.Length; _index++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.VerticalAlignment = VerticalAlignment.Stretch;
					textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
					textBlock.Padding = new Thickness(20, 0, 0, 0);
					textBlock.FontSize = 30;

					//textBlock.Margin = new Thickness(-50, -50,-50,-50);

					if (_index == _letter[0])
					{
						
						textBlock.Text = word[_index].ToString();
						textBlock.Background = Brushes.LightGreen;
						
					}
					if(_index == _letter[1])
					{
						textBlock.Text = word[_index].ToString();
						textBlock.Background = Brushes.LightGreen;

					}
					if(!_letter.Contains(_index))
					{
						textBlock.Text = "";
					}

					Grid.SetColumn(textBlock, _index); ;
					Grid.SetRow(textBlock, _gridRow);
					WordGrid.Children.Add(textBlock);


				}
			}
			nextRow++;


		}
	}
}
