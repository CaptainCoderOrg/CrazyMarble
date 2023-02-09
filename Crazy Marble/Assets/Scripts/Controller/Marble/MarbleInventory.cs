using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace CrazyMarble
{
    public class MarbleInventory : MonoBehaviour
    {
        [SerializeField]
        private List<MarbleItem> _items = new ();
        public List<MarbleItem> Items => _items.ToList();
        public void Add(MarbleItem item) => _items.Add(item);
        public void Clear() => _items.Clear();
    }
}