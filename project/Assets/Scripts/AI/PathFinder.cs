using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.Assertions;

namespace AI
{
    public class PathFinder
    {
        private class PathNode
        {
            public Vector2Int Position;
            public int PathLengthFromStart;
            public PathNode CameFrom;
            public int HeuristicEstimatePathLength;

            public int EstimateFullPathLength
            {
                get { return PathLengthFromStart + HeuristicEstimatePathLength; }
            }
        }

        /// <summary>
        /// Рассчитать путь от эмиттера до цели 
        /// </summary>
        /// <returns>Найденный путь.</returns>
        public IEnumerable<Vector2Int> CalcPath(CellModel emitter, CellModel target)
        {
            var fm = GameModel.Instance.FieldModel;
            Assert.IsNotNull(fm);

            var field = new bool[fm.Size.x, fm.Size.y];
            for (var y = 0; y < fm.Size.y; ++y)
            {
                for (var x = 0; x < fm.Size.x; ++x)
                {
                    field[x, y] = true;
                }
            }

            foreach (var cell in fm.Cells.Where(cell =>
                cell.ItemType != ItemType.Emitter && cell.ItemType != ItemType.Target))
            {
                field[cell.Coordinate.x, cell.Coordinate.y] = false;
            }

            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();

            var startNode = new PathNode
            {
                Position = emitter.Coordinate,
                HeuristicEstimatePathLength = GetHeuristicPathLength(emitter.Coordinate, target.Coordinate)
            };

            openSet.Add(startNode);
            while (openSet.Any())
            {
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();

                if (currentNode.Position == target.Coordinate)
                {
                    return GetPathForNode(currentNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode, target.Coordinate, field))
                {
                    if (closedSet.Any(node => node.Position == neighbourNode.Position)) continue;
                    var openNode = openSet.FirstOrDefault(node => node.Position == neighbourNode.Position);
                    if (openNode != null)
                    {
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                    }
                    else
                    {
                        openSet.Add(neighbourNode);
                    }
                }
            }

            return new Vector2Int[0];
        }

        private static int GetHeuristicPathLength(Vector2Int from, Vector2Int to)
        {
            return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
        }

        private static IEnumerable<PathNode> GetNeighbours(PathNode pathNode, Vector2Int target, bool[,] field)
        {
            var neighbourPoints = new Vector2Int[4];
            neighbourPoints[0] = new Vector2Int(pathNode.Position.x + 1, pathNode.Position.y);
            neighbourPoints[1] = new Vector2Int(pathNode.Position.x - 1, pathNode.Position.y);
            neighbourPoints[2] = new Vector2Int(pathNode.Position.x, pathNode.Position.y + 1);
            neighbourPoints[3] = new Vector2Int(pathNode.Position.x, pathNode.Position.y - 1);

            return (from point in neighbourPoints
                where point.x >= 0 && point.x < field.GetLength(0)
                where point.y >= 0 && point.y < field.GetLength(1)
                where (field[point.x, point.y])
                select new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart =
                        pathNode.PathLengthFromStart + GetDistanceBetweenNeighbours(pathNode.Position, point),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, target)
                }).ToList();
        }

        private static int GetDistanceBetweenNeighbours(Vector2Int p1, Vector2 p2)
        {
            return 1;
        }

        private static IEnumerable<Vector2Int> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Vector2Int>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }

            result.Reverse();
            return result;
        }
    }
}