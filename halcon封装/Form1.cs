using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace halcon封装
{
    public partial class Form1 : Form
    {
        HDevelopExport hd = new HDevelopExport();
        HObject ho_image;
        // 图片
        string address;
        int Addindex = 0;
        int a;
        string[] addressArray = { "../../../../image/", ".bmp.tif" };
        int[] addressIndex = { 0, 1, 2, 3, 4 };
        private static HWindow hwin; //全局窗口变量
        private void getAddress()
        {
            address = addressArray[0] + addressIndex[Addindex] + addressArray[1];
        }
        // 图片缩放
        private void my_MouseWheel(object sender, MouseEventArgs e)
        {
            System.Drawing.Point pt = this.Location;
            int leftBorder = hSmartWindowControl1.Location.X;
            int rightBorder = hSmartWindowControl1.Location.X + hSmartWindowControl1.Size.Width;
            int topBorder = hSmartWindowControl1.Location.Y;
            int bottomBorder = hSmartWindowControl1.Location.Y + hSmartWindowControl1.Size.Height;
            if (e.X > leftBorder && e.X < rightBorder && e.Y > topBorder && e.Y < bottomBorder)
            {
                MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks,
                                                     e.X - pt.X, e.Y - pt.Y, e.Delta);
                hSmartWindowControl1.HSmartWindowControl_MouseWheel(sender, newe);
            }
        }



        public Form1()
        {
            InitializeComponent();
            getAddress();
            hwin = hSmartWindowControl1.HalconWindow;
            hd.InitHalcon(hwin);
            // 图片缩放订阅监听
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.my_MouseWheel);
        }
        // 读取图片
        private void button1_Click(object sender, EventArgs e)
        {

            hd.readImage(out ho_image, address);
        }
        // 处理图片
        private void button2_Click(object sender, EventArgs e)
        {
            if(ho_image == null)
            {
                MessageBox.Show("当前未打开图片");
                return;
            }
            hd.dealImage(ho_image);

        }
        // 下一张
        private void button3_Click(object sender, EventArgs e)
        {
            if (Addindex == addressIndex.Length - 1)
            {
                Addindex = 0;
            }
            else Addindex++;
            getAddress();
            hd.readImage(out ho_image, address);
        }
        // 读取任意图片
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            address = ofd.FileName;
            hd.readImage(out ho_image, address);
        }
        // 拟合圆
        private void button5_Click(object sender, EventArgs e)
        {
            if (ho_image == null)
            {
                MessageBox.Show("当前未打开图片");
                return;
            }
            hd.getCircle(ho_image);
        }
        // 中心点
        private void button6_Click(object sender, EventArgs e)
        {
            if (ho_image == null)
            {
                MessageBox.Show("当前未打开图片");
                return;
            }
            hd.getCenterPoint(ho_image);
        }
    }
}
