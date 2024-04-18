using System;
using TMPro;
using UnityEngine;

namespace Codebase.View
{
    public class ScoreView : ElementView
    {
        [SerializeField] private TMP_Text _label;

        private void OnValidate()
        {
            if(_label == null)
                throw new ArgumentNullException(nameof(_label));
        }

        public void Refresh(int score) => 
            _label.text = score.ToString("D2");
    }
}
