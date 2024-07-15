using UnityEngine;

namespace TrapTheCat
{
    public abstract class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
    {
        private static GenericSingleton<T> instance;
        public static GenericSingleton<T> Instance;

		private void Awake()
		{
			if(instance == null)
			{
				instance = this;
				return;
			}
			Destroy(this);
		}
	}
}
