using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Base64Encoding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        byte[] filedata;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_encode_Click(object sender, RoutedEventArgs e)
        {
            tbx_result.Text = Convert.ToBase64String(filedata);
        }

        private void btn_decode_Click(object sender, RoutedEventArgs e)
        {
            filedata = Convert.FromBase64String(tbx_result.Text);
            string item;string strdata = null;
            for (int i = 0; i < filedata.Length; i++)
            {
                item = Convert.ToString(filedata[i], 16).ToUpper();
                if (item.Length == 1) item = '0' + item;
                strdata += item + ' ';
            }
            if (strdata != null) tbx_context.Text = strdata.Trim();
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.Filter = "所有文件 (*.*)|*.*";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbx_path.Text = ofd.FileName;
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    string strdata = null; string item; int bt;
                    filedata = new byte[fs.Length];
                    for (int i = 0; i < fs.Length; i++)
                    {
                        bt = fs.ReadByte();
                        filedata[i] = (byte)bt;
                        item = Convert.ToString(bt, 16).ToUpper();
                        if (item.Length == 1) item = '0' + item;
                        strdata += item + ' ';
                    }
                    fs.Close();
                    if (strdata != null) tbx_context.Text = strdata.Trim();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title);
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "所有文件 (*.*)|*.*";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                    for (int i = 0; i < filedata.Length; i++)
                    {
                        fs.WriteByte(filedata[i]);
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title);
            }
        }

        private void btn_savetext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(tbx_result.Text);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title);
            }
        }
    }
}
