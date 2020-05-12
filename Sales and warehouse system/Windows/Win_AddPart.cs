using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelLayer;

namespace Sales_and_warehouse_system.Windows
{
    public partial class Win_AddPart : Form
    {
        public Win_AddPart()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Win_AddPart_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;
            treeView1.Parent = pictureBox1;
            button1.Parent = pictureBox1;
            button2.Parent = pictureBox1;
        }

        private void Win_AddPart_Activated(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode n = new TreeNode("حقوق دسترسی سیستم");
            n.ForeColor = Color.Red;
            n.Tag = "1";
            treeView1.Nodes.Add(n);

            LoadTreeView(n);
        }

        private void LoadTreeView(TreeNode TN)
        {

            int TagInt;
            TagInt = Convert.ToInt32(TN.Tag);
            /////////////////////////////////////////////////////////////////////////
            var query = from SPF in database.VW_SystemPart
                        where SPF.SystemPartLevel == TagInt
                        select SPF;
            var users = query.ToList();

            if (users.Count > 0)
            {
                for (int I = 0; I < users.Count; I++)
                {
                    TreeNode M = new TreeNode();
                    M.Tag = users[I].SystemPartID;
                    M.Text = M.Tag + " - " + users[I].SystemPartName;
                    M.ToolTipText = "" + M.Tag;

                    if (TN.Level < this.imageList.Images.Count - 1)
                    {
                        M.ImageIndex = TN.Level + 1;
                        M.SelectedImageIndex = TN.Level + 1;
                    }
                    else
                    {
                        M.ImageIndex = this.imageList.Images.Count - 1;
                        M.SelectedImageIndex = this.imageList.Images.Count - 1;
                    }
                    TN.Nodes.Add(M);
                    int CH = Convert.ToInt32(users[I].childecount);
                    if (CH > 0)
                    {
                        LoadTreeView(M);
                    }
                    M = null;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) {
                MessageBox.Show("لطفا بخشی را انتخاب نمایید", "راهنمایی",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            Win_AddSystemPart wn = new Win_AddSystemPart();
            int gettag = Convert.ToInt32(treeView1.SelectedNode.Tag);
            wn.Get_SystePartID = gettag;
            wn.ShowDialog();
            
        }
    }
}
