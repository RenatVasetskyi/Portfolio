using Code.Infrastructure.Interfaces;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneController : ISceneController
    {
        public void Load(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}