using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.UR.accessor
{
    public class ReadAttribute<T>
    {
        private class AutoChangeObservable : IObservable<T>
        {
            public IDisposable Subscribe(IObserver<T> observer)
            {
                throw new NotImplementedException();
            }
        }

        IObservable<T> mObservable = new AutoChangeObservable();
        private ReaderWriterLock mLock = new ReaderWriterLock();
        private T mObject;
        private TypeInfo mType;

        public T get()
        {
            lock (mLock)
            {
                try
                {
                    if (mObject == null) return default;

                }
                catch (Exception ex) { }
            }
            return mObject;
                    
        }
    }
}
