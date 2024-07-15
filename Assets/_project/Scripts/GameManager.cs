using TrapTheCat.Grid;
using TrapTheCat.Cat;
using TrapTheCat.Ui;
using UnityEngine;
using TrapTheCat.PlayerInput;

namespace TrapTheCat.Game
{
	public class GameManager : MonoBehaviour
	{
		[Header("Grid")]
		[SerializeField] private GridManager gridManager;

		[Space]
		[Header("Cat")]
		[SerializeField] private CatMovement cat;

		[Space]
		[Header("Player")]
		[SerializeField] private PlayerManager playerInputManager;

		[Space]
		[Header("UI")]
		[SerializeField] private UiManager uiManager;


		private void Awake()
		{
			SetupGame();
		}

		private void SetupGame()
		{
			gridManager.Init();
			uiManager.Init();
			cat = Instantiate(cat);
			cat.Init(gridManager);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var playerMoved = playerInputManager.PlayerMove(cat.CurrentPosOfCat());
				if (playerMoved)
				{
					cat.MoveCat();
				}
				IsGameOver();
			}
		}

		private void IsGameOver()
		{
			if(cat.catWon)
			{
				uiManager.ActivateCatWin();
			}
			else if(!cat.pathAvailabe)
			{
				uiManager.ActivatePlayerWin();
			}
		}
	}
}
