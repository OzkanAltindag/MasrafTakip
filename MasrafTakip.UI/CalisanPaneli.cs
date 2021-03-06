﻿using MasrafTakip.BLL;
using MasrafTakip.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasrafTakip.UI
{
    public partial class CalisanPaneli : Form
    {
        public CalisanPaneli()
        {
            InitializeComponent();
        }
        MasrafRepository mr = new MasrafRepository();
        KullaniciRepository kr = new KullaniciRepository();
        Masraflar sec;

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

        void verilerigetir()
        {
            int personelID = KullaniciGirisi.k_id;

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



        private void button5_Click(object sender, EventArgs e)//çıkış
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)//oturumu kapat
        {
            if (MessageBox.Show("Oturumu Sonlandırmak İstediğinizden Emin misiniz?", "Oturumu Kapat?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KullaniciGirisi kg = new KullaniciGirisi();
                this.Hide();
                kg.Show();
            }
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
                m.OnayDurumu = "Onaylanmadı";
                mr.Insert(m);
                verilerigetir();
                temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            int personelID = (int)comboBox1.SelectedValue;
            if (comboBox1.SelectedIndex == 2)
            {
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
            else
            {
                MessageBox.Show("Bu kullanıcının kayıtlarını görmeye yetkiniz yok!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                comboBox1.SelectedIndex = 2;
            }

        }

        private void CalisanPaneli_Load(object sender, EventArgs e)
        {
            MasrafKayitEntities mk = new MasrafKayitEntities();
            label5.Text = KullaniciGirisi.k_rol + " " + KullaniciGirisi.isim + " " + KullaniciGirisi.soyisim + " " + "KullanıcıID=" + KullaniciGirisi.k_id.ToString();
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
                comboBox1.SelectedIndex = KullaniciGirisi.k_id - 1;
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            try
            {
                int personelID = KullaniciGirisi.k_id;

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

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)//sil 
        {
            if (MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Sil?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int secili = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    mr.Delete(secili);
                    verilerigetir();
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

        private void button3_Click(object sender, EventArgs e)//güncelle
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Alanlar Boş Bırakılamaz");
                }
                else
                {
                    sec.Baslik = textBox1.Text;
                    sec.Aciklama = textBox2.Text;
                    mr.Update(sec);
                    verilerigetir();
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
                
                sec = (Masraflar)dataGridView1.CurrentRow.DataBoundItem;
                textBox1.Text = sec.Baslik;
                textBox2.Text = sec.Aciklama;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
