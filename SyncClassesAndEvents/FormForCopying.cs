using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncClassesAndEvents
{
    public partial class FormForCopying : Form
    {
      
        private static Semaphore smph = new Semaphore(2, 3); //counting the main one. AND STATIC TO MAKE IT ONLY ONE FOR EACH FORM 
        public FormForCopying()
        {
            InitializeComponent();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(CopyApp, smph);
        }

        void CopyApp(object a)
        {
            Semaphore sem = a as Semaphore;
            if (!sem.WaitOne(0)) 
            {
                MessageBox.Show("Too much copies.We're closing", "message", MessageBoxButtons.OKCancel);
                Application.Exit();
            }
            else
            {
                FormForCopying newform = new FormForCopying();
                newform.ShowDialog();
                sem.Release();
            }

        }
    }
}
