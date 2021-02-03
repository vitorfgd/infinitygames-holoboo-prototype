using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// Created by https://github.com/kroltan
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        public static Task AsyncCoroutine(
            this MonoBehaviour self,
            IEnumerator coroutine
        )
        {
            return AsyncCoroutine(self, coroutine, CancellationToken.None);
        }

        private static Task AsyncCoroutine(
            this MonoBehaviour self,
            IEnumerator coroutine,
            CancellationToken cancellationToken
        )
        {
            var source = new TaskCompletionSource<object>(
                TaskCreationOptions.RunContinuationsAsynchronously | TaskCreationOptions.AttachedToParent
            );

            var yield = coroutine == null
                ? null
                : self.StartCoroutine(coroutine);

            var completer = self.StartCoroutine(CompleteAfter(source, yield));

            cancellationToken.Register(
                () =>
                {
                    if (source.Task.IsCompleted)
                    {
                        return;
                    }

                    if (self != null)
                    {
                        if (yield != null)
                        {
                            self.StopCoroutine(yield);
                        }

                        self.StopCoroutine(completer);
                    }

                    source.SetCanceled();
                }
            );

            return source.Task;
        }

        private static IEnumerator CompleteAfter(
            TaskCompletionSource<object> source,
            YieldInstruction instruction
        )
        {
            yield return instruction;
            source.SetResult(null);
        }
    }
}
