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
    public partial class Win_UserAcsses : Form
    {
        public Win_UserAcsses()
        {
            InitializeComponent();
        }
        public int GetUserID { set; get; }
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
            lbl_UserName.Parent = pictureBox1;
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

            var query = from SPF in database.VW_SystemPart
                        where SPF.SystemPartLevel == TagInt
                        select SPF;
            var users = query.ToList();

            if (users.Count > 0)
            {
                int get_sysPartFormId;

                for (int I = 0; I < users.Count; I++)
                {
                    TreeNode M = new TreeNode();
                    M.Tag = users[I].SystemPartID;
                    M.Text = M.Tag + " - " + users[I].SystemPartName;
                    M.ToolTipText = "" + M.Tag;
                    get_sysPartFormId = Convert.ToInt32(M.Tag);
                    var query1 = from UA in database.UserAccesses
                                 where UA.UserID == this.GetUserID
                                 where UA.SystemPartID == get_sysPartFormId
                                 select UA;
                    var users1 = query1.ToList();
                    if (users1.Count > 0)
                        M.Checked = true;

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


        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewChild(e.Node,e.Node.Checked);
        }

        private void CheckTreeViewChild(TreeNode Node,Boolean IsCheked)
        {
            foreach (TreeNode item in Node.Nodes) {

                item.Checked = IsCheked;
                if (item.Nodes.Count > 0) {
                    this.CheckTreeViewChild(item, IsCheked);

                }
            }
        }

        private void Btn_Add_Access(object sender, EventArgs e)
        {
            GetArrayofCheckedNodes();
            MessageBox.Show("دسترسی ها با موفقیت اعمال گردید","دسترسی",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private string GetArrayofCheckedNodes()
        {
            return string.Join(" , ", GetCheckedNodes(treeView1.Nodes));
        }

        public List<string> GetCheckedNodes(TreeNodeCollection nodes)
        {
            List<string> nodeList = new List<string>();
            if (nodes == null)
            {
                return nodeList;
            }

            foreach (TreeNode childNode in nodes)
            {
                if (childNode.Checked)
                {

                    UserAccess UserAccess = new UserAccess();
                    UserAccess.UserID = this.GetUserID;
                    UserAccess.SystemPartID = Convert.ToInt32(childNode.Tag);


                    var query = from SPF in database.UserAccesses
                                where SPF.UserID == UserAccess.UserID
                                where SPF.SystemPartID == UserAccess.SystemPartID
                                select SPF;
                    var users = query.ToList();

                    if (users.Count == 0)
                    {
                        database.UserAccesses.Add(UserAccess);
                        database.SaveChanges();
                    }
                }
                else if (childNode.Checked == false && (childNode.Tag) != "1")
                {
                    try
                    {
                        database.SP_Del_UsserAccess(this.GetUserID, Convert.ToInt32(childNode.Tag));
                        database.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("در ثبت دسترس?ها مشکل? بوجود آمده است");
                    }
                }
                nodeList.AddRange(GetCheckedNodes(childNode.Nodes));
            }
            return nodeList;
        }
    }
}
