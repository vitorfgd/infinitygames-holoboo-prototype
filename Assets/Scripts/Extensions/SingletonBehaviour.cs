using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// Base class for <see cref="MonoBehaviour"/>s that must be unique-loaded and
    /// globally accessible.
    /// </summary>
    /// <example>
    /// To inherit this class, use the Curiously Recurring Generic Pattern:
    /// <code><![CDATA[
    /// public class MySingleton : SingletonBehaviour<MySingleton> {
    ///		public void Foo() { }
    /// }
    /// ]]></code>
    /// Then, add the component to the scene, and you will be able to access it
    /// with the static property <see cref="Instance"/>.
    /// <code><![CDATA[
    /// MySingleton.Instance.Foo();
    /// ]]></code>
    /// </example>
    /// <remarks>
    /// For more information, see https://medium.com/@kroltan/auto-tipos-gen%C3%A9ricos-repetitivos-em-c-83b2886c0d0f
    /// </remarks>
    /// <typeparam name="T">the same type inheriting this class</typeparam>
    public class SingletonBehaviour<T> : MonoBehaviour
    where T : SingletonBehaviour<T>
    {
        private static bool _hasInstance;
        private static T _instance;

        /// <summary>
        /// Accesses the current instance of this singleton, or throws
        /// <see cref="InvalidOperationException"/> in case it does not
        /// exist in the currently loaded scenes.
        /// </summary>
        [NotNull]
        public static T Instance
        {
            get
            {
                if (!_hasInstance)
                {
                    throw new InvalidOperationException($"The singleton '{typeof(T).FullName}' is not present in the game.");
                }

                return _instance;
            }
            private set
            {
                if (value == null)
                {
                    _hasInstance = false;
                    _instance = null;
                }
                else
                {
                    _hasInstance = true;
                    _instance = value;
                }
            }
        }

        public static bool HasInstance => _hasInstance;

        [UsedImplicitly]
        protected virtual void Awake()
        {
            if (_hasInstance)
            {
                Destroy(gameObject);
                throw new InvalidOperationException(
                    $"There must be only one instance of {typeof(T).FullName} loaded!\n"
                    + $"Attempted to load {name}, but {_instance.name} was already loaded.\n"
                    + $"{name} has been destroyed."
                );
            }

            DevelopmentCheck();

            Instance = (T)this;
        }

        [UsedImplicitly]
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                Instance = null;
            }
        }

        [Conditional("UNITY_EDITOR")]
        private void DevelopmentCheck() {
            if (typeof(T) != GetType()) {
                throw new InvalidOperationException(
                    $"The type {GetType().FullName} is not identical to {typeof(T).FullName}, use CRTP!"
                );
            }
        }
    }
}