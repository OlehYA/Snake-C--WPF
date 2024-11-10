using Snake_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Snake_WPF.ViewModels
{
    internal class MainWndVM : BindableBase
    {
        private bool _continueGame;

        public bool ContinueGame
        {
            get => _continueGame;
            set
            {
                _continueGame = value;
                RaisePropertyChanged(nameof(ContinueGame));

                if (_continueGame) SnakeGo();
            }
        }

        public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();

        public DelegateCommand StartStopComand { get; }
        private MoveDirection _currentMoveDirection = MoveDirection.Right;

        private int _rowCount = 10;
        private int _columnCount = 10;
        private const int SPEED = 400;
        private int _speed = SPEED;

        private Snake _snake;
        private MainWindow _mainWnd;
        private CellVM _lastFood;


        public MainWndVM(MainWindow mainWnd)
        {
            _mainWnd = mainWnd;
            StartStopComand = new DelegateCommand(() => ContinueGame = !ContinueGame);

            for (int row = 0; row < _rowCount; row++)
            {
                var rowList = new List<CellVM>();
                for (int colum = 0; colum < _columnCount; colum++)
                {
                    var cell = new CellVM(row, colum);
                    rowList.Add(cell);
                }
                AllCells.Add(rowList);
            }

            _snake = new Snake(AllCells, AllCells[_rowCount / 2][_columnCount /2], CreatedFood);
            CreatedFood();

            _mainWnd.KeyDown += UserKeyDown;
        }

        private async Task SnakeGo()
        {
            while (ContinueGame)
            {
                await Task.Delay(_speed);

                try
                {
                    _snake.Move(_currentMoveDirection);
                }
                catch(Exception ex)  
                {
                    ContinueGame= false;
                    MessageBox.Show(ex.Message);
                    _speed=;
                }
            }
        }

        private void UserKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    if(_currentMoveDirection != MoveDirection.Right)
                    _currentMoveDirection = MoveDirection.Left;
                    break;
                case Key.D:
                    if (_currentMoveDirection != MoveDirection.Left)
                        _currentMoveDirection = MoveDirection.Right;
                    break;
                case Key.W:
                    if (_currentMoveDirection != MoveDirection.Down)
                        _currentMoveDirection = MoveDirection.Up;
                    break;
                case Key.S:
                    if (_currentMoveDirection != MoveDirection.Up)
                        _currentMoveDirection = MoveDirection.Down;
                    break;
            }
        }

        private void CreatedFood()
        {
            var random = new Random();

            int row = random.Next(0, _rowCount);
            int colum = random.Next(0, _columnCount);

            _lastFood = AllCells[row][colum];

            if(_snake.SnakeCells.Contains(_lastFood))
            {
                CreatedFood();
            }

            _lastFood.CellType = CellType.Food;
            _speed = (int)(_speed * 0.95);
        }
    }
}
