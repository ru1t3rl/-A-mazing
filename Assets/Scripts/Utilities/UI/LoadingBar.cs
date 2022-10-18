using UnityEngine;
using UnityEngine.Events;
using Ru1t3rl.Events;

namespace Ru1t3rl.Utilities.UI
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private Transform foreground;
        public UnityEvent onFinishLoading;

        private void OnEnable()
        {
            EventManager.Instance.AddListener("onLoadingUpdate", OnLoadingUpdate);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener("onLoadingUpdate", OnLoadingUpdate);
        }

        private void OnLoadingUpdate(System.EventArgs args)
        {
            LoadingEventArgs loadingArgs = args as LoadingEventArgs;
            foreground.localScale = new Vector3(loadingArgs.Progress, foreground.localScale.y, foreground.localScale.z);

            if (loadingArgs.Progress >= 1f)
            {
                onFinishLoading.Invoke();
            }
        }
    }
}