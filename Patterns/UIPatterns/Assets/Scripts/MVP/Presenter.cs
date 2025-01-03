namespace MVP
{
    public class Presenter
    {
        private readonly Model _model;

        public Presenter(View view) => _model = new Model(view);

        public void OnChangeTextClicked(string text) =>
            _model.ChangeText(text);
    }
}