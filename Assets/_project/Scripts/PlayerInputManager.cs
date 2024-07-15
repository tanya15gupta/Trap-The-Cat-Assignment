using TrapTheCat.Grid.GridTile;
using UnityEngine;

namespace TrapTheCat.PlayerInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        private Camera camera;
        private RaycastHit2D hit;

        private void Awake()
        {
            camera = Camera.main;
        }

        public bool PlayerMove(Vector2Int _catPos)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
				
            if (hit.rigidbody == null)
            {
                return false;
            }

            if (hit.rigidbody.TryGetComponent<Tile>(out Tile cell))
            {
                return IsValidMove(cell, _catPos);
            }
            return false;
        }

        private bool IsValidMove(Tile cell, Vector2Int _catPos)
        {
            if(cell.GetMyIndex() == _catPos)
            {
                return false;
            }
            cell.SetTileState(TileStates.Blocked);
            return true;
        }
    }
}