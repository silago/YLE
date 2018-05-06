using UnityEngine;
using YLE.MVC.Controller;

namespace YLE.MVC
{
    public class YLEContainer : MonoBehaviour
    {
        private YLEController _controller;
        [SerializeField] private YLEView _view;


        private void Awake()
        {
            _controller = YLEController.Init(_view, this);
        }
    }
}