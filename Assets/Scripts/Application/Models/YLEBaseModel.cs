using System;
using Networking;
using UnityEngine;

namespace YLE.MVC.Model
{
    public abstract class YLEBaseModel
    {
        private YleAPI _api;
        private MonoBehaviour _monoObject;

        public YleAPI Api
        {
            protected get { return _api; }
            set
            {
                if (_api != null) throw new Exception("Could not change value after initialization");

                _api = value;
            }
        }

        public MonoBehaviour MonoObject
        {
            protected get { return _monoObject; }
            set
            {
                if (_monoObject != null) throw new Exception("Could not change value after initialization");

                _monoObject = value;
            }
        }
    }
}