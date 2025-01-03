using Mediator.Interfaces;
using UnityEngine;

namespace Mediator
{
    public class MediatorTest : MonoBehaviour
    {
        private void Awake()
        {
            Test();
        }

        private void Test()
        {
            MenuWindow menuWindow = new MenuWindow();
            SettingsWindow settingsWindow = new SettingsWindow();
            SelectNameWindow selectNameWindow = new SelectNameWindow();
            
            IMediator mediator = new MenuViewMediator(menuWindow, settingsWindow, selectNameWindow);

            //Test1
            settingsWindow.SetSettings();
            
            //Test2
            selectNameWindow.Select();
            
            //Test3
            menuWindow.Update();
        }
    }
}