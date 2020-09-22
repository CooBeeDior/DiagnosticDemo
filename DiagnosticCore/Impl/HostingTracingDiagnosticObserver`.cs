using System;
namespace DiagnosticCore
{
    public class HostingTracingDiagnosticObserver<T> : IObserver<T>
    {
        private Action<T> _next;
        public HostingTracingDiagnosticObserver(Action<T> next)
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
