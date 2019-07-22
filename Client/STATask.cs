using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public static class STATask
    {
        /// <summary>
        /// Этот класс позволяет запускать потоки в контексте STAT потоков. Это нужно нам для использования
        /// класса Clipboard, который находится в пространстве имён System.Windows.Forms, так как этот класс нельзя
        /// использовать из пула потоков.
        /// </summary>
        /// <typeparam name="TResult">Возвращаемый тип значения</typeparam>
        /// <param name="function">Действие(делегат) для исполнения асинхронно</param>
        /// <returns></returns>

        public static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            var thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }

                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }

        public static Task Run(Action action)
        {
            var tcs = new TaskCompletionSource<object>(); 

            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null); 
            }

                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }
    }
}
