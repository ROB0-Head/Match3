using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Match3.Core;
using Settings;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Match3
{
    public sealed class Board : MonoBehaviour
    {
        [SerializeField] private List<Row> _rows;
        
        [SerializeField] private LevelManager _levelManager;

        [SerializeField] private float _tweenDuration;

        [SerializeField] private Transform _swappingOverlay;

        [SerializeField] private bool _ensureNoStartingMatches;

        private readonly List<Tile> _selection = new List<Tile>();
        private List<TileTypeData> _tileTypes;
        private bool _isSwapping;
        private bool _isMatching;
        private bool _isShuffling;

        public event Action<TileTypeData, int> OnMatch;

        private TileData[,] Matrix
        {
            get
            {
                var width = _rows.Max(row => row.Tiles.Count);
                var height = _rows.Count;

                var data = new TileData[width, height];

                for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    data[x, y] = GetTile(x, y).Data;

                return data;
            }
        }

        private void Awake()
        {
            _tileTypes = SettingsProvider.Get<TileTypeDatas>().TileDatas;
        }

        private void Start()
        {
            for (var y = 0; y < _rows.Count; y++)
            {
                for (var x = 0; x < _rows.Max(row => row.Tiles.Count); x++)
                {
                    var tile = GetTile(x, y);

                    tile.X = x;
                    tile.Y = y;

                    tile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];
                    tile.Button.onClick.AddListener(() => Select(tile));
                }
            }

            if (_ensureNoStartingMatches)
                StartCoroutine(EnsureNoStartingMatches());
            OnMatch += (type, count) => Debug.Log($"Matched {count}x {type.name}.");
            OnMatch += (type, count) => _levelManager.UpdateDestroyedTilesCount(type, count);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bestMove = TileDataMatrixUtility.FindBestMove(Matrix);

                if (bestMove != null)
                {
                    Select(GetTile(bestMove.X1, bestMove.Y1));
                    Select(GetTile(bestMove.X2, bestMove.Y2));
                }
            }
        }

        private IEnumerator EnsureNoStartingMatches()
        {
            var wait = new WaitForEndOfFrame();

            while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
            {
                Shuffle();

                yield return wait;
            }
        }

        private Tile GetTile(int x, int y) => _rows[y].Tiles[x];

        private Tile[] GetTiles(IList<TileData> tileData)
        {
            var length = tileData.Count;

            var tiles = new Tile[length];

            for (var i = 0; i < length; i++)
                tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

            return tiles;
        }

        private async void Select(Tile tile)
        {
            if (_isSwapping || _isMatching || _isShuffling)
                return;

            if (!_selection.Contains(tile))
            {
                if (_selection.Count > 0)
                {
                    if (Math.Abs(tile.X - _selection[0].X) == 1 && Math.Abs(tile.Y - _selection[0].Y) == 0
                        || Math.Abs(tile.Y - _selection[0].Y) == 1 && Math.Abs(tile.X - _selection[0].X) == 0)
                        _selection.Add(tile);
                }
                else
                {
                    _selection.Add(tile);
                }
            }

            if (_selection.Count < 2) return;

            await SwapAsync(_selection[0], _selection[1]);

            
            //to do: доделать обработку комбинацию из 4-х
            /*if (IsFourTileCombination(_selection.ToArray()))
            {
                var swappedTile = _selection[0].X < _selection[1].X ? _selection[0] : _selection[1];
                if (_selection[0].X == _selection[1].X)
                {
                    swappedTile.Type = _tileTypes.Find(tileType => tileType.TileAbility == EAbility.HorizontalLightning);
                }
                else
                {
                    swappedTile.Type = _tileTypes.Find(tileType => tileType.TileAbility == EAbility.VerticalLightning);
                }
            }*/

            if (!await TryMatchAsync())
            {
                
                await SwapAsync(_selection[0], _selection[1]);
            }

            var matrix = Matrix;

            while (TileDataMatrixUtility.FindBestMove(matrix) == null ||
                   TileDataMatrixUtility.FindBestMatch(matrix) != null)
            {
                Shuffle();

                matrix = Matrix;
            }

            _selection.Clear();
        }

        private bool IsFourTileCombination(Tile[] tiles)
        {
            if (tiles.Length != 4)
                return false;

            var firstTileType = tiles[0].Type;
            if (tiles.Any(tile => tile.Type != firstTileType))
                return false;

            var horizontalOrder = tiles.OrderBy(tile => tile.X).ToArray();
            var horizontalDistinctCount = horizontalOrder.Select((tile, index) => tile.X - index).Distinct().Count();
            if (horizontalDistinctCount == 1)
            {
                var swappedTile = _selection[0].X < _selection[1].X ? _selection[0] : _selection[1];
                return tiles.Any(tile => tile == swappedTile);
            }

            var verticalOrder = tiles.OrderBy(tile => tile.Y).ToArray();
            var verticalDistinctCount = verticalOrder.Select((tile, index) => tile.Y - index).Distinct().Count();
            if (verticalDistinctCount == 1)
            {
                var swappedTile = _selection[0].Y < _selection[1].Y ? _selection[0] : _selection[1];
                return tiles.Any(tile => tile == swappedTile);
            }

            return false;
        }


        private async Task SwapAsync(Tile tile1, Tile tile2)
        {
            _isSwapping = true;

            var icon1 = tile1.Icon;
            var icon2 = tile2.Icon;

            var icon1Transform = icon1.transform;
            var icon2Transform = icon2.transform;

            icon1Transform.SetParent(_swappingOverlay);
            icon2Transform.SetParent(_swappingOverlay);

            icon1Transform.SetAsLastSibling();
            icon2Transform.SetAsLastSibling();

            var sequence = DOTween.Sequence();

            sequence.Join(icon1Transform.DOMove(icon2Transform.position, _tweenDuration).SetEase(Ease.OutBack))
                .Join(icon2Transform.DOMove(icon1Transform.position, _tweenDuration).SetEase(Ease.OutBack));

            await sequence.Play().AsyncWaitForCompletion();

            icon1Transform.SetParent(tile2.transform);
            icon2Transform.SetParent(tile1.transform);

            tile1.Icon = icon2;
            tile2.Icon = icon1;

            (tile1.Type, tile2.Type) = (tile2.Type, tile1.Type);
            _levelManager.SetupCurrentMovesText();
            _isSwapping = false;
        }

        private async Task<bool> TryMatchAsync()
        {
            var didMatch = false;

            _isMatching = true;

            var match = TileDataMatrixUtility.FindBestMatch(Matrix);

            while (match != null)
            {
                
                didMatch = true;

                var tiles = GetTiles(match.Tiles);

                var deflateSequence = DOTween.Sequence();

                foreach (var tile in tiles)
                    deflateSequence.Join(tile.Icon.transform.DOScale(Vector3.zero, _tweenDuration)
                        .SetEase(Ease.InBack));
                
                await deflateSequence.Play().AsyncWaitForCompletion();

                var inflateSequence = DOTween.Sequence();

                bool isHorizontal = tiles.All(t => t.Y == tiles[0].Y & t.Y != 0);
                bool isVertical = tiles.All(t => t.X == tiles[0].X);

                if (isVertical)
                {
                    tiles = tiles.OrderBy(t => t.Y).ToArray();
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        var currentTile = GetTile(tiles[i].X, tiles[i].Y);
                        if (tiles[0].Y != 0)
                        {
                            var previousTile = GetTile(tiles[i].X, i);
                            currentTile.Type = previousTile.Type;
                            inflateSequence.Join(currentTile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                                .SetEase(Ease.OutBack));
                            previousTile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];
                            inflateSequence.Join(previousTile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                                .SetEase(Ease.OutBack));
                        }
                        else
                        {
                            currentTile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];
                            inflateSequence.Join(currentTile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                                .SetEase(Ease.OutBack));
                        }
                    }
                }
                else if (isHorizontal)
                {
                    Tile currentTile = null;
                    Tile previousTile = null;
                    for (int j = 0; j < tiles.Length; j++)
                    {
                        for (int i = 0; i < tiles[0].Y; i++)
                        {
                            currentTile = GetTile(tiles[j].X, tiles[j].Y - i);
                            previousTile = GetTile(tiles[j].X, tiles[j].Y - i - 1);
                            currentTile.Type = previousTile.Type;
                            inflateSequence.Join(currentTile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                                .SetEase(Ease.OutBack));
                        }

                        if (currentTile != null)
                        {
                            previousTile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];
                            inflateSequence.Join(previousTile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                                .SetEase(Ease.OutBack));
                        }
                    }
                }
                else
                {
                    foreach (var tile in tiles)
                    {
                        tile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];
                        inflateSequence.Join(tile.Icon.transform.DOScale(Vector3.one, _tweenDuration)
                            .SetEase(Ease.OutBack));
                    }
                }

                await inflateSequence.Play().AsyncWaitForCompletion();

                OnMatch?.Invoke(Array.Find(_tileTypes.ToArray(), tileType => tileType.TileType == match.TypeId), match.Tiles.Length);
                
                match = TileDataMatrixUtility.FindBestMatch(Matrix);
            }

            _isMatching = false;

            return didMatch;
        }

        private void Shuffle()
        {
            _isShuffling = true;

            foreach (var row in _rows)
            foreach (var tile in row.Tiles)
                tile.Type = _tileTypes[Random.Range(0, _tileTypes.Count)];

            _isShuffling = false;
        }
    }
}