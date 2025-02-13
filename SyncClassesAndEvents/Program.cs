using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SyncClassesAndEvents
{
    internal static class Program
    {
        private static Semaphore semaphore = new Semaphore(3, 3);
        [STAThread]
        static void Main()
        {
      if (!semaphore.WaitOne(0))
            {
                // Если не удалось захватить семафор, значит, уже запущено 3 экземпляра
                MessageBox.Show("Приложение уже запущено в трех экземплярах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

    Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormForCopying());

            // Освобождаем семафор при закрытии приложения
            semaphore.Release();

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormForCopying());

      
        }
    }
}
