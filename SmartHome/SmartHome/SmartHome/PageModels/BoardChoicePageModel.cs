using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHome.PageModels
{
    public class BoardChoicePageModel : BasePageModel
    {
        private IBoardService _boardService;
        private ObservableRangeCollection<Board> _boards = new ObservableRangeCollection<Board>();
        public ObservableRangeCollection<Board> Boards
        {
            get => _boards;
            set
            {
                SetProperty(ref _boards, value);
            }
        }

        private Board _selectedBoard = null;

        public Board SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                SetProperty(ref _selectedBoard, value);
            }
        }

        public ICommand BoardCellOnTapCommand { get; set; }

        public BoardChoicePageModel(IBoardService boardService)
        {
            _boardService = boardService;

            BoardCellOnTapCommand = new FreshAwaitCommand(async (obj) =>
            {
                var selectedBoard = SelectedBoard;
                SelectedBoard = null;
                await CoreMethods.PushPageModel<BoardDevicesPageModel>(selectedBoard);
                obj.SetResult(true);
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            UpdateBoards();
        }

        private async void UpdateBoards()
        {
            var boards = await _boardService.GetBoards();
            Boards.ReplaceRange(boards);
        }
    }
}
