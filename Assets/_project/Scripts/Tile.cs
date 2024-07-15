using UnityEngine;

namespace TrapTheCat.Grid.GridTile
{
	public class Tile : MonoBehaviour
	{
		[SerializeField] private Sprite circleSprite;
		[SerializeField] private Vector2Int myPos;

		private TileStates myState;

		public void SetRowAndColumnNum(int _row, int _column)
		{
			myPos = new Vector2Int(_row, _column);
		}

		public void SetTileState(TileStates setState)
		{
			myState = setState;
			if (myState == TileStates.Blocked)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
			}
		}

		public TileStates GetTileState()
		{
			return myState;
		}

		public Vector2Int GetMyIndex()
		{
			return myPos;
		}
	}
}

