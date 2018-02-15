using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MasrafTakip.DAL;
using MasrafTakip.BLL;

namespace MasrafTakip.UI
{
    public partial class YöneticiPaneli : Form
    {
        public YöneticiPaneli()
        {
            InitializeComponent();
        }
        

        MasrafRepository mr = new MasrafRepository();
        KullaniciRepository kr = new KullaniciRepository();
        Masraflar secili;
        void temizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox te = (TextBox)item;
                    te.Clear();
                }


            }
        }

        void verilerigoster()
        {
           dataGridView1.DataSource = mr.selectAll().ToList();
        }


        private void button1_Click(object sender, EventArgs e)//ekle
        {
            try
            {
                Masraflar m = new Masraflar();
                m.Tarih = dateTimePicker1.Value;
                m.Baslik = textBox1.Text;
                m.Tutar = numericUpDown1.Value;
                m.Aciklama = textBox2.Text;
                m.PersonelID = KullaniciGirisi.k_id;
                m.OnayDurumu = "Onaylandı";
                mr.Insert(m);
                verilerigoster();
                temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
            
            
        }

        private void YöneticiPaneli_Load(object sender, EventArgs e)
        {
            MasrafKayitEntities mk = new MasrafKayitEntities();
            label5.Text = KullaniciGirisi.k_rol+" "+KullaniciGirisi.isim+" "+ KullaniciGirisi.soyisim+" "+"KullanıcıID="+ KullaniciGirisi.k_id.ToString();
            label7.Text = "Son Oturum: " + KullaniciGirisi.SonOturum;
            try
            {
                comboBox1.DataSource = kr.selectAll().Select(f => new
                {
                    f.PersonelID,
                    f.KullaniciAdi

                }).ToList();
                comboBox1.DisplayMember = "KullaniciAdi";
                comboBox1.ValueMember = "PersonelID";
            }
             
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
           
            verilerigoster();
            
        }

        private void button2_Click(object sender, EventArgs e)//sil
        {
            if (MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Sil?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {


                    int secili = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    mr.Delete(secili);
                    MessageBox.Show("Kayıt silindi");
                    verilerigoster();
                    temizle();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)//oturumu kapat
        {
           if( MessageBox.Show("Oturumu Sonlandırmak İstediğinizden Emin misiniz?","Oturumu Kapat?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                KullaniciGirisi kg = new KullaniciGirisi();
                this.Hide();
                kg.Show();
                
            }
            else
            {
                return;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int personelID =(int) comboBox1.SelectedValue;
          
            dataGridView1.DataSource = mr.selectbyperid(personelID).Select(m => new
            {

                m.MasrafID,
                m.Baslik,
                m.Tutar,
                m.Tarih,
                m.Aciklama,
                m.OnayDurumu
            }).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            verilerigoster();
        }

        private void button3_Click(object sender, EventArgs e)//guncelle
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Alanlar Boş Bırakılamaz");
                }
                else
                {
                    secili.Baslik = textBox1.Text;
                    secili.Aciklama = textBox2.Text;
                    mr.Update(secili);
                    verilerigoster();
                    temizle();
                }
            }
            catch
            {
                MessageBox.Show("Güncellemek istediğiniz kayda çift tıklayın");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                secili = (Masraflar)dataGridView1.CurrentRow.DataBoundItem;
                textBox1.Text = secili.Baslik;
                textBox2.Text = secili.Aciklama;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
