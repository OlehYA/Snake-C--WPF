using Snake_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }

        public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();

        public DelegateCommand StartStopComand { get; }
        private MoveDirection _currentMoveDirection = MoveDirection.Right;

        private int _rowCount = 10;
        private int _columnCount = 10;


        public MainWndVM()
        {
            StartStopComand = new DelegateCommand(() => ContinueGame = !ContinueGame);

            for (int row = 0; row < _rowCount; row++)
            {
                var rowList = new List<CellVM>();
                for (int colum = 0; colum < _columnCount; colum++)
                {

                }
            }
        }
    }
}
