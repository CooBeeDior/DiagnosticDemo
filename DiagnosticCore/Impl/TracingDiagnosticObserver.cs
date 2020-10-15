using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DiagnosticCore
{
    public class TracingDiagnosticObserver : IObserver<DiagnosticListener>
    {
        private IEnumerable<ITracingDiagnosticProcessor> _traceDiagnostics;

        public TracingDiagnosticObserver(IEnumerable<ITracingDiagnosticProcessor> traceDiagnostics)
        {
            _traceDiagnostics = traceDiagnostics;
        }

        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception error)
        {
        }

        public virtual void OnNext(DiagnosticListener value)
        {  
            var traceDiagnostic = _traceDiagnostics.FirstOrDefault(i => i.ListenerName == value.Name);
            if (traceDiagnostic != null)
            { 
                //适配订阅
                value.SubscribeWithAdapter(traceDiagnostic);
            }
        }
    }
}
