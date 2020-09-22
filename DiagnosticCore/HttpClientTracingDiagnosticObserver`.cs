using System;
namespace DiagnosticCore
{
    public class HttpClientTracingDiagnosticObserver<T> : IObserver<T>
    {
        private Action<T> _next;
        public HttpClientTracingDiagnosticObserver(Action<T> next)
        {
            _next = next;
        }

        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception error)
        {
        }

        public virtual void OnNext(T value) => _next(value);
    }
}
