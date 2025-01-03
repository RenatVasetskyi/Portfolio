using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Audio.Enums;
using Code.Data;
using Code.Gameplay.Candies;
using Code.Gameplay.Candies.Data;
using Code.Gameplay.Candies.Enums;
using Code.Gameplay.Person;
using Code.Gameplay.Swipes;
using Code.Gameplay.Swipes.Enums;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay
{
    public class PlayingField : MonoBehaviour
    {
        private const int MaxCustomers = 3;
        private const float SpawnCustomerDelay = 2.5f;
        
        private readonly List<Customer> _customers = new();
        
        [SerializeField] private List<MarkCell> _localPositions;
        [SerializeField] private List<Cell> _cells;
        
        [SerializeField] private Swiper _swiper;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        [SerializeField] private Transform _spawnCustomerPosition;
        [SerializeField] private Transform _endCustomerPosition;
        [SerializeField] private Transform _customersHolder;
        [SerializeField] private List<WaitPoint> _positionsToWait;
        
        private Camera _camera;
        private IUIFactory _uiFactory;
        private GameData _gameData;
        private ISoundManager _soundManager;

        [Inject]
        public void InjectServices(IUIFactory uiFactory, GameData gameData,
            ISoundManager soundManager)
        {
            _soundManager = soundManager;
            _gameData = gameData;
            _uiFactory = uiFactory;
        }

        public Vector2 GetLocalPositionFromGridPosition(int x, int y)
        {
            return _localPositions.First(cell => cell.PositionInGrid.X == x & cell.PositionInGrid.Y == y).transform.localPosition;
        }

        public CandyData GetUnusedFullCandy()
        {
            for (int i = 0; i < Enum.GetValues(typeof(FullCandyType)).Length; i++)
            {
                FullCandyType type = (FullCandyType)i;

                if (!_cells.Exists(x => x.FullCandyType == type))
                    return _gameData.CandyDatas.First(x => x.FullCandyType == type);
            }

            return _gameData.CandyDatas[Random.Range(0, _gameData.CandyDatas.Length)];
        }

        public HalfCandyData GetUnusedHalfCandy(CandyData data)
        {
            List<HalfOfCandyType> types = new();
            
            foreach (HalfCandyData halfData in data.HalfCandyDatas)
                types.Add(halfData.Type);

            foreach (HalfOfCandyType type in types)
            {
                if (!_cells.Exists(x => x.HalfOfCandyType == type))
                    return data.HalfCandyDatas.First(x => x.Type == type);
            }
            
            return data.HalfCandyDatas[Random.Range(0, data.HalfCandyDatas.Count)];
        }

        private void Awake()
        {
            _camera = Camera.main;

            foreach (Cell cell in _cells)
                cell.Init(this);

            StartCoroutine(SpawnCustomers());
            
            _swiper.OnSwiped += SwapCells;
        }

        private void OnDestroy()
        {
            _swiper.OnSwiped -= SwapCells;
        }

        private void SwapCells(SwipeDirection direction, Vector2 position)
        {
            List<RaycastResult> results = new();
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = _camera.WorldToScreenPoint(position);
            _graphicRaycaster.Raycast(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out Cell cell))
                {
                    PositionInGrid currentPosition = cell.PositionInGrid;
                    PositionInGrid targetCellToSwapWithPositionInGrid = new();
                    
                    switch (direction)
                    {
                        case SwipeDirection.Up:
                            targetCellToSwapWithPositionInGrid = new() { X = currentPosition.X, Y = currentPosition.Y + 1 };
                            break;
                        case SwipeDirection.Down:
                            targetCellToSwapWithPositionInGrid = new() { X = currentPosition.X, Y = currentPosition.Y - 1 };
                            break;
                        case SwipeDirection.Left:
                            targetCellToSwapWithPositionInGrid = new() { X = currentPosition.X - 1, Y = currentPosition.Y };
                            break;
                        case SwipeDirection.Right:
                            targetCellToSwapWithPositionInGrid = new() { X = currentPosition.X + 1, Y = currentPosition.Y };
                            break;
                    }

                    Cell targetCellToSwapWith = _cells.FirstOrDefault(targetCellToSwapWith =>
                        targetCellToSwapWith.PositionInGrid.X == targetCellToSwapWithPositionInGrid.X &
                        targetCellToSwapWith.PositionInGrid.Y == targetCellToSwapWithPositionInGrid.Y);

                    if (targetCellToSwapWith != null)
                        StartCoroutine(Swap(cell, targetCellToSwapWith));

                    return;
                }
            }
        }

        private IEnumerator Swap(Cell cell, Cell targetCellToSwapWith)
        {
            PositionInGrid nextCellPositionInGrid = new PositionInGrid { X = targetCellToSwapWith.PositionInGrid.X, Y = targetCellToSwapWith.PositionInGrid.Y };
            PositionInGrid nextTargetCellToSwapWithPositionInGrid = new PositionInGrid { X = cell.PositionInGrid.X, Y = cell.PositionInGrid.Y };
            
            _swiper.Disable();
            
            cell.SetPositionInGrid(nextCellPositionInGrid);
            targetCellToSwapWith.SetPositionInGrid(nextTargetCellToSwapWithPositionInGrid);

            yield return new WaitForSeconds(cell.SwapDuration);

            CheckMatching();
            _swiper.Enable();
        }

        private void CheckMatching()
        {
            foreach (Cell cell in _cells)
            {
                //left
                PositionInGrid leftCellPosition = new PositionInGrid() { X = cell.PositionInGrid.X - 1, Y = cell.PositionInGrid.Y };
                if (CheckMatching(cell, leftCellPosition))
                    return;
                
                //right
                PositionInGrid rightCellPosition = new PositionInGrid() { X = cell.PositionInGrid.X + 1, Y = cell.PositionInGrid.Y };
                if (CheckMatching(cell, rightCellPosition))
                    return;
                
                //up
                PositionInGrid upCellPosition = new PositionInGrid() { X = cell.PositionInGrid.X, Y = cell.PositionInGrid.Y + 1 };
                if (CheckMatching(cell, upCellPosition))
                    return;
                
                //down
                PositionInGrid downCellPosition = new PositionInGrid() { X = cell.PositionInGrid.X, Y = cell.PositionInGrid.Y - 1 };
                if (CheckMatching(cell, downCellPosition))
                    return;
            }
        }

        private bool CheckMatching(Cell cell, PositionInGrid positionInGrid)
        {
            Cell leftCell = _cells.FirstOrDefault(x => x.PositionInGrid.X == positionInGrid.X & x.PositionInGrid.Y == positionInGrid.Y);

            if (leftCell != null)
            {
                if (leftCell.FullCandyType == cell.FullCandyType && cell.HalfOfCandyType != leftCell.HalfOfCandyType)
                {
                    _soundManager.PlaySfx(SfxTypeEnum.Swap);
                    cell.Match();
                    leftCell.Match();
                    StartCoroutine(CreateFullCandy(cell.FullCandyType));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator CreateFullCandy(FullCandyType type)
        {
            yield return new WaitForSeconds(0.4f);
            DoneCandy candy = _uiFactory.CreateFullCandy(type, _uiFactory.GameUI.transform);
            candy.Init(type);
            
            foreach (Customer customer in _customers)
            {
                if (customer.Order != null &&  customer.Order.FullCandyType == type)
                {
                    _soundManager.PlaySfx(SfxTypeEnum.CompleteOrder);
                    candy.Move(customer, _endCustomerPosition);
                    _customers.Remove(customer);
                    yield break; 
                }
            }

            Destroy(candy.gameObject);
        }

        private void SpawnCustomer()
        {
            WaitPoint emptyPoint = _positionsToWait.FirstOrDefault(x => x.IsEmpty());

            if (emptyPoint == null) return;
            
            Customer customer = _uiFactory.CreateRandomCustomer(_spawnCustomerPosition.localPosition, _customersHolder);
            emptyPoint.SetCustomer(customer.transform);
            _customers.Add(customer);
            customer.MoveAndOrder(emptyPoint.transform);
        }

        private IEnumerator SpawnCustomers()
        {
            while (true)
            {
                if (_customers.Count < MaxCustomers)
                    SpawnCustomer();
                
                yield return new WaitForSeconds(SpawnCustomerDelay);
            }
        }
    }
}