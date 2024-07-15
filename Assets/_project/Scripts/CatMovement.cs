using UnityEngine;
using System.Collections.Generic;
using TrapTheCat.Grid;
using TrapTheCat.Grid.GridTile;
using System.Collections;

namespace TrapTheCat.Cat
{
    public class CatMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 currentPoss;
        [SerializeField] private List<Vector2> targetRoute;

        private Coroutine gridRoutine;
        
        private GridManager gridManager;
        private Tile[,] grid;

        private float yOffset = 0.18f;
        private float waitTime = 4f;

        private int currentX;
        private int currentY;

        public bool pathAvailabe	{ get; private set; }
        public bool catWon			{ get; private set; }

        public void Init(GridManager gridManager)
        {
            if(gridRoutine == null)
            {
                gridRoutine = StartCoroutine(WaitForGridRoutine());
            }
            this.gridManager = gridManager;
            grid = gridManager.GetGrid;
            pathAvailabe = true;
        }

        public void MoveCat()
        {
            if (currentX == 0 || currentX == gridManager.GetRowCount - 1 || currentY == 0 || currentY == gridManager.GetColumnCount)
            {
                catWon = true;
                return;
            }
            Vector2Int targetPosition = BFSFindPath();

            if (targetPosition != Vector2Int.zero)
            {
                currentX = targetPosition.x;
                currentY = targetPosition.y;
                Vector2.Lerp(transform.position, grid[currentX, currentY].transform.position, 5);
                transform.position = grid[currentX, currentY].transform.position;
            }
        }

        public Vector2Int CurrentPosOfCat()
        {
            return new Vector2Int(currentX, currentY);
        }

        private Vector2Int BFSFindPath()
        {
            int[,] directions = new int[4, 2] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            Queue<List<Vector2Int>> queue = new Queue<List<Vector2Int>>();
            queue.Enqueue(new List<Vector2Int> { new Vector2Int(currentX, currentY) });

            while (queue.Count > 0)
            {
                List<Vector2Int> path = queue.Dequeue();
                Vector2Int current = path[path.Count - 1];

                if (current.x == 0 ||  current.x == gridManager.GetRowCount - 1 ||  current.y == 0 || current.y == gridManager.GetColumnCount - 1)
                {
                    return path[1];
                }


                for (int i = 0; i < 4; i++)
                {
                    int newX = current.x + directions[i, 0];
                    int newY = current.y + directions[i, 1];

                    if (newX >= 0 && newX < gridManager.GetRowCount && newY >= 0 && newY < gridManager.GetColumnCount)
                    {
                        if ((grid[newX, newY].GetTileState() == TileStates.Unblocked) && !path.Contains(new Vector2Int(newX, newY)))
                        {
                            List<Vector2Int> newPath = new List<Vector2Int>(path) { new Vector2Int(newX, newY) };
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }
            pathAvailabe = false;
            return Vector2Int.zero;
        }

        private IEnumerator WaitForGridRoutine()
        {
            yield return new WaitForSeconds(waitTime);
            if (grid == null || gridManager == null)
            {
                Debug.LogError("Grid or grid manager was null.");
                yield return null;
            }
            RepositionCat();
        }

        private void RepositionCat()
        {
            var tileForCatToSpawn = grid[gridManager.GetRowCount / 2, gridManager.GetColumnCount / 2];
            currentX = (gridManager.GetRowCount / 2);
            currentY = (gridManager.GetColumnCount / 2);
            while (tileForCatToSpawn.GetTileState() == TileStates.Blocked)
            {
                int i = 1;
                currentX = currentX + i;
                tileForCatToSpawn = grid[currentX, currentY];
                i++;
            }
            var currentPos = tileForCatToSpawn.transform.position;
            transform.position = currentPos + new Vector3(0, yOffset, 0);
        }
    }
}