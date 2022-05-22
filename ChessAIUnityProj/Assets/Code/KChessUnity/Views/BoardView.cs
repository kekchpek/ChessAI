﻿using KChessUnity.ViewModels.Board;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.Views
{
    public class BoardView : ViewBehaviour<IBoardViewModel>
    {
        [SerializeField] private Transform _bottomLeftCorner;
        [SerializeField] private Transform _topRightCorner;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.SetCornerPoints(_bottomLeftCorner.position, _topRightCorner.position);
        }
    }
}