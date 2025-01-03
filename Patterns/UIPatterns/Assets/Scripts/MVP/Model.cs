using UnityEngine;

namespace MVP
{
    public class Model
    {
        private readonly View _view;
        
        private readonly Color[] _colors = new Color[5]
        {
            Color.blue, Color.cyan, Color.black, Color.magenta, Color.green
        };
        
        public Model(View view) => _view = view;

        public void ChangeText(string text)
        {
            Color randomColor = _colors[Random.Range(0, _colors.Length)];

            _view.UpdateView(randomColor, text);
        }
    }
}