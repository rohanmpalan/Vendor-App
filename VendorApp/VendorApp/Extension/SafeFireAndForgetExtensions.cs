using System;
namespace VendorApp.Extension
{
    public static class SafeFireAndForgetExtensions
    {
        static Action<Exception> _onException;
        static bool _shouldAlwaysRethrowException;

        public static void SafeFireAndForget(this Task task, in bool continueOnCapturedContext = false, in Action<Exception> onException = null)
            => HandleSafeFireAndForget(task, continueOnCapturedContext, onException);

        public static void SafeFireAndForget<TException>(this Task task, in bool continueOnCapturedContext = false, in Action<TException> onException = null)
            where TException : Exception => HandleSafeFireAndForget(task, continueOnCapturedContext, onException);

        public static void Initialize(in bool shouldAlwaysRethrowException = false) => _shouldAlwaysRethrowException = shouldAlwaysRethrowException;

        public static void SetDefaultExceptionHandling(in Action<Exception> onException)
        {
            if (onException is null)
                throw new ArgumentNullException(nameof(onException), $"{onException} cannot be null");

            _onException = onException;
        }

        public static void RemoveDefaultExceptionHandling() => _onException = null;

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        static async void HandleSafeFireAndForget<TException>(Task task, bool continueOnCapturedContext, Action<TException> onException)
            where TException : Exception
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (TException ex) when (_onException != null || onException != null)
            {
                _onException?.Invoke(ex);
                onException?.Invoke(ex);

                if (_shouldAlwaysRethrowException)
                    throw;
            }
        }
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void

        static Dictionary<Guid, List<CancellationTokenSource>> _cancellableSequenceOfTasks = new();
        public static CancellationToken CancelExistedTasksBySequenceID(Guid sequenceID)
        {
            var ct = new CancellationTokenSource();
            if (_cancellableSequenceOfTasks.ContainsKey(sequenceID))
            {
                var tasks = _cancellableSequenceOfTasks[sequenceID];
                if (tasks is null)
                    _cancellableSequenceOfTasks[sequenceID] = new List<CancellationTokenSource>() { ct };
                if (tasks.Any())
                {
                    tasks.ForEach(x => x.Cancel());
                    _cancellableSequenceOfTasks[sequenceID] = new List<CancellationTokenSource>() { ct };
                }
            }
            else
                _cancellableSequenceOfTasks.Add(sequenceID, new List<CancellationTokenSource>() { ct });
            return ct.Token;
        }

        public static void ReleaseSemaphore(this SemaphoreSlim semaphoreSlim)
        {
            if (semaphoreSlim?.CurrentCount == 0)
                semaphoreSlim.Release();
        }

        public static bool IsSemaphoreBusy(this SemaphoreSlim semaphoreSlim) => semaphoreSlim?.CurrentCount == 0;

        public static async Task<TResult> ExecuteOperationWithSemaphoreAndIgnoreOthers<TResult>(this SemaphoreSlim semaphoreSlim,
            Func<Task<TResult>> funcTask, bool rethrowException = false)
        {
            if (semaphoreSlim is null)
                throw new ArgumentNullException($"{nameof(semaphoreSlim)} can not be null");

            if (semaphoreSlim.CurrentCount == 0)
                return default;

            await semaphoreSlim.WaitAsync();

            try
            {
                return await funcTask();
            }
            catch (Exception ex)
            {
                if (rethrowException)
                    throw;
            }
            finally
            {
                semaphoreSlim.ReleaseSemaphore();
            }

            return default;
        }

        public static Task ExecuteOperationWithSemaphoreAndIgnoreOthers(this SemaphoreSlim semaphoreSlim, Func<Task> funcTask, bool rethrowException = false)
            => ExecuteOperationWithSemaphoreAndIgnoreOthers<object>(semaphoreSlim, async () =>
            {
                await funcTask();
                return default;
            }, rethrowException);
    }
}

