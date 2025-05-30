 public partial class SendNotificationForm : Form
    {
        public SendNotificationForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string noidung = txtContent.Text.Trim();
            int level = Convert.ToInt32(cbCap.SelectedValue);
            int compartment = Convert.ToInt32(cbDonVi.SelectedValue);
            int group = Convert.ToInt32(cbCoSo.SelectedValue);

            int label = (level * 1000) + (compartment * 10) + group;

            using (OracleConnection conn = new OracleConnection("User Id=admin;Password=adminpwd;Data Source=orcl;"))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("INSERT INTO THONGBAO (ID, NOIDUNG, LABEL) VALUES (THONGBAO_SEQ.NEXTVAL, :noidung, :label)", conn);
                cmd.Parameters.Add(new OracleParameter("noidung", noidung));
                cmd.Parameters.Add(new OracleParameter("label", label));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã gửi thông báo thành công.");
            }
        }
    }
