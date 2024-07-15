using System.Collections.Generic;
using TrapTheCat.Grid.GridTile;
using UnityEngine;

namespace TrapTheCat.Grid
{
	public class GridManager : MonoBehaviour
	{
		[Header("Grid")]
		[SerializeField] private int rowCount = 5;
		[SerializeField] private int columnCount = 7;
		[SerializeField] private Transform tilesMap;

		[Space]
		[Header("Tiles")]
		[SerializeField] private GameObject squareSprites;
		[SerializeField] private int maxRandomPreblockedBlocks = 10;
		[SerializeField] private float offset;

		private Vector3 columnTileOffset;
		private Vector3 rowTileOffset;
		private Vector3 currentPosition;

		private Tile[,] grid;
		private int num;

		public int GetRowCount => rowCount;

		public int GetColumnCount => columnCount;

		public Tile[,] GetGrid => grid;

		public void Init()
		{
			currentPosition = transform.position;

			grid = new Tile[rowCount, columnCount];

			columnTileOffset = Vector3.right * offset;
			rowTileOffset = Vector3.up * offset;

			GridCreation(rowCount, columnCount);
		}

		private void GridCreation(int _rowCount, int _columnCount)
		{
			CreateGrid(_rowCount, _columnCount);
		}

		private void CreateGrid(int _rowCount, int _columnCount)
		{
			for (int i = 0; i < _rowCount; i++)
			{
				for (int j = 0; j < _columnCount; j++)
				{
					CreateTile(i, j);
					currentPosition = currentPosition + columnTileOffset;
				}
				currentPosition = transform.position;
				currentPosition = currentPosition - (rowTileOffset * (i + 1));
			}
			PreBlockedTiles();
		}

		private void CreateTile(int _currentRow, int _currentColumn)
		{
			var Tile = Instantiate(squareSprites, currentPosition, transform.rotation, tilesMap);
			Tile.name = $"cell {num++}";
			AssignValuesToCell(_currentRow, _currentColumn, Tile);
		}

		private void AssignValuesToCell(int _currentRow, int _currentColumn, GameObject _cell)
		{
			_cell.GetComponent<Tile>().SetRowAndColumnNum(_currentRow, _currentColumn);
			grid[_currentRow, _currentColumn] = _cell.GetComponent<Tile>();
		}

		private void PreBlockedTiles()
		{
			var addBlocks = new List<Tile>();
			maxRandomPreblockedBlocks = 10;
			while (maxRandomPreblockedBlocks > 0)
			{
				var blocked = grid[(Random.Range(0, rowCount)), (Random.Range(0, columnCount))];

				if(!addBlocks.Contains(blocked))
				{
					blocked.SetTileState(TileStates.Blocked);
					maxRandomPreblockedBlocks--;
					addBlocks.Add(blocked);
				}
			}
		}
	}
}


