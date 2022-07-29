using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Class1
    {
        public void LogWrite(string s)
        {
            StreamWriter sw = null;
            try
            {
                string path = "D://ErrorLog.txt";
                if (System.IO.File.Exists(path))
                {
                    sw = File.AppendText(path);
                    sw.WriteLine(DateTime.Now.ToString() + " " + s);
                    MessageBox.Show("Go to file path D://ErrorLog.txt to view errors.");
                }
                else
                {
                    sw = File.CreateText("D://ErrorLog.txt");
                    sw.WriteLine(DateTime.Now.ToString() + " " + s);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                sw.Close();
            }
        }
    }
}