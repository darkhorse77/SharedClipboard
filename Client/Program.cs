using System;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        public static string CurrentClipboadValue { get; set; }
        private static string ServerPath { get; set; }

        [STAThread]
        public static void Main(string[] args)
        {
            ServerPath = "https://localhost:44312/api/values/";

            Console.WriteLine("Нажмите любую кнопку, чтобы начать выполнение програмы.");
            Console.ReadKey();
            Console.WriteLine();

            //ThreadPool.QueueUserWorkItem(CheckClipboard);
            //ThreadPool.QueueUserWorkItem(SetClipboard);     

            STATask.Run(() => CheckClipboard(null)).Wait();
            STATask.Run(() => SetClipboard(null)).Wait();
        }

        public async static void CheckClipboard(object state)
        {
            while (true)
            {
                string remoteClipboardValue = await ApiEngine.CallGetAsync(ServerPath);
                if (remoteClipboardValue != CurrentClipboadValue && remoteClipboardValue != null && remoteClipboardValue != "")
                {
                    STATask.Run(() => Clipboard.SetText(remoteClipboardValue)).Wait();
                    CurrentClipboadValue = remoteClipboardValue;

                    Console.WriteLine("Clipboard updated - " + remoteClipboardValue); // для отладки
                }
                Thread.Sleep(100);
            }
        }

        public static void SetClipboard(object state)
        {
            while (true)
            {
                if (Clipboard.GetText() != CurrentClipboadValue)
                {
                    string localClipboardValue = Clipboard.GetText();
                    ApiEngine.CallPutAsync(ServerPath + localClipboardValue).Wait();
                    CurrentClipboadValue = localClipboardValue;

                    Console.WriteLine("Remote updated - " + localClipboardValue); // инофрмация для отладки
                }
            }
        }
    }
}
