using UnityEngine;

namespace DefaultNamespace
{
    public interface IGridInit
    {
        public void Init(Vector2Int gridSize ,float cellSize = 1f,float cellSpacing = 0.1f);
    }
}