using Mediator.Interfaces;

namespace Mediator
{
    public class MenuViewMediator : IMediator
    {
        private readonly MenuWindow _menuWindow;
        private readonly SettingsWindow _settingsWindow;
        private readonly SelectNameWindow _selectNameWindow;

        public MenuViewMediator(MenuWindow menuWindow, SettingsWindow settingsWindow, 
            SelectNameWindow selectNameWindow)
        {
            _menuWindow = menuWindow;
            _settingsWindow = settingsWindow;
            _selectNameWindow = selectNameWindow;
            
            _menuWindow.SetMediator(this);
            _settingsWindow.SetMediator(this);
            _selectNameWindow.SetMediator(this);
        }
        
        public void Notify(BaseWindow window, MainMenuOperationType operationType)
        {
            switch (operationType)
            {
                case MainMenuOperationType.OpenSettings:
                    _menuWindow.Hide();
                    _settingsWindow.Open();
                    break;
                case MainMenuOperationType.OpenMainMenu:
                    _menuWindow.Open();
                    _settingsWindow.Hide();
                    break;
                case MainMenuOperationType.OpenSelectNameWindow:
                    _menuWindow.Hide();
                    _settingsWindow.Hide();
                    _selectNameWindow.Open();
                    break;
                case MainMenuOperationType.NameSelected:
                    _menuWindow.Update();
                    _selectNameWindow.Hide();
                    _menuWindow.Open();
                    break;
            }
        }
    }
}