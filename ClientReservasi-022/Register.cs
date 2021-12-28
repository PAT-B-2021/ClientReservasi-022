using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientReservasi_022
{
    public partial class Register : Form
    {
        ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        public Register()
        {
            InitializeComponent();
            TampilData();
            textBoxID.Visible = false;
            buttonUpdate.Enabled = false;
            buttonHapus.Enabled = false;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string kategori = comboBoxKategori.Text;
            string a = service.Register(username, password, kategori);

            if (textBoxUsername.Text == "" || textBoxPassword.Text == "" ||comboBoxKategori.Text == "")
            {
                MessageBox.Show("Semua data wajib diisi.");
            }
            else
            {
                MessageBox.Show(a);
                Refresh();
                TampilData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string passwrod = textBoxPassword.Text;
            string kategori = comboBoxKategori.Text;
            int id = Convert.ToInt32(textBoxID.Text);
            string a = service.UpdateRegister(username, passwrod, kategori, id);

            if (textBoxPassword.Text == "" || textBoxPassword.Text == "" || comboBoxKategori.Text == "")
            {
                MessageBox.Show("Semua data wajib diisi.");
            }
            else
            {
                MessageBox.Show(a);
                Clear();
                TampilData();
            }
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;

            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?",
                "Hapus Data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string a = service.DeleteRegister(username);
                MessageBox.Show(a);
                Clear();
                TampilData();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void TampilData()
        {
            var list = service.DataRegist();
            dtRegister.DataSource = list;
        }

        public void Clear()
        {
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            comboBoxKategori.SelectedItem = null;

            buttonSimpan.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonHapus.Enabled = false;
        }

        private void dtRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[0].Value);
            textBoxUsername.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[1].Value);
            textBoxPassword.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[2].Value);
            comboBoxKategori.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[3].Value);

            buttonUpdate.Enabled = true;
            buttonHapus.Enabled = true;
            buttonSimpan.Enabled = true;
        }
    }
}
