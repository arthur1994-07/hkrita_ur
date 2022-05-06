using hkrita_robot.Maths;
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
            public void set()
            {
                lock(this)
                {
                    // mark this observable object as having been changed 
                    Console.WriteLine("notify changes ");
                    // if object has changed, then notify all of its observers
                }
            }
            public IDisposable Subscribe(IObserver<T> observer)
            {
                throw new NotImplementedException();
            }
        }

        IObservable<T> mObservable = new AutoChangeObservable();
        private ReaderWriterLock mLock = new ReaderWriterLock();
        private T mObject;
        private TypeInfo mType;
        public ReadAttribute(TypeInfo type,T genericObject)
        {
            mType = type;
            mObject = genericObject;
        }
        public T Get()
        {
             lock (mLock)
            {
                try
                {
                    //if (mObject == null) return default;
                    MethodInfo[] methods = mType.GetMethods();

                    MethodInfo method = mType.GetDeclaredMethod("Clone");


                    return (T) method.Invoke(mObject, null);
                }
                catch (MethodAccessException ex) 
                {
                    return mObject;
                }
                catch (Exception ex) 
                {
                    return default;
                }
            }
        }


    }
}
