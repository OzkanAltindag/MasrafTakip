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

namespace MasrafTakip.UI
{
    public partial class KullaniciGirisi : Form

    {
        public KullaniciGirisi()
        {
            InitializeComponent();
        }
        public static string isim;
        public static string soyisim;
        public static int k_id;
        public static string k_rol;
        public static string SonOturum;
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

      


        public bool Login(string username, string password,string rol1,string rol2,string rol3)
        {
            MasrafKayitEntities db = new MasrafKayitEntities();

           



            var admin = (from p in db.Personeller
                        where p.KullaniciAdi == username &&
                              p.Sifre == password&&
                              p.Rol == rol1


                        select p).FirstOrDefault();
            var birimadmin = (from p in db.Personeller
                         where p.KullaniciAdi == username &&
                               p.Sifre == password &&
                               p.Rol == rol2

                         select p).FirstOrDefault();
            var user = (from p in db.Personeller
                         where p.KullaniciAdi == username &&
                               p.Sifre == password &&
                               p.Rol == rol3

                         select p).FirstOrDefault();




            if (user!=null)//eğer oturum açma başarılıysa
            {
                user.SonOturum = DateTime.Now;
                db.SaveChanges();
                MessageBox.Show("Çalışan olarak giriş yapıldı", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CalisanPaneli cp = new CalisanPaneli();
               
                cp.Show();

                this.Hide();

                return true;
               
            }
            else if (admin!=null)
            {
                admin.SonOturum = DateTime.Now;
                db.SaveChanges();
                MessageBox.Show("Yönetici olarak giriş yapıldı","Giriş Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                YöneticiPaneli frm = new YöneticiPaneli();

                this.Hide();
                frm.Show();
                return true;
            }
            else if (birimadmin!=null)
            {
                birimadmin.SonOturum = DateTime.Now;
                db.SaveChanges();
                MessageBox.Show("Birim Yöneticisi olarak giriş yapıldı","Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BirimYöneticiPaneli byp = new BirimYöneticiPaneli();

                this.Hide();
                byp.Show();
                
                return true;


            }
            else
            {
                //MessageBox.Show("başarısız");
                return false;
            }
            


        }
    
        private void isimsoyisim()//kullanıcı adı ve şifreye göre giriş yapan kişinin ismini yazar
        {

            MasrafKayitEntities context = new MasrafKayitEntities();
            var errormessage = "";
            var iserror = false;

            if (string.IsNullOrEmpty(textBox1.Text))//kullanıcı adı şifre boşsa
            {
                errormessage += "Kullanacı adını boş geçemezsiniz\r\n";
                iserror = true;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errormessage += "Şifreyi boş geçemezsiniz\r\n";
                iserror = true;
            }
            if (iserror)
            {
                MessageBox.Show(errormessage, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                isim = (from p in context.Personeller
                        where p.KullaniciAdi == textBox1.Text &&
    p.Sifre == textBox2.Text
                        select p.Ad).FirstOrDefault().ToUpper().ToString();
                soyisim = (from p in context.Personeller
                           where p.KullaniciAdi == textBox1.Text &&
                           p.Sifre == textBox2.Text
                           select p.Soyad).FirstOrDefault().ToUpper().ToString();
                k_id = (from p in context.Personeller
                        where p.KullaniciAdi == textBox1.Text &&
                        p.Sifre == textBox2.Text
                        select p.PersonelID).FirstOrDefault();
                k_rol = (from p in context.Personeller
                         where p.KullaniciAdi == textBox1.Text &&
                                         p.Sifre == textBox2.Text
                         select p.Rol).FirstOrDefault().ToUpper();
                SonOturum = (from p in context.Personeller
                             where p.KullaniciAdi == textBox1.Text &&
                                         p.Sifre == textBox2.Text
                             select p.SonOturum).FirstOrDefault().ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Yanlış kullanıcı adı veya parola girdiniz!\r\n Lüften bilgilerinizi kontrol ediniz.","GİRİŞ BAŞARISIZ",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            isimsoyisim();
            var username = textBox1.Text;
            var password = textBox2.Text;
            string rol1 = "yönetici";
            string rol2 = "birim yöneticisi";
            string rol3 = "çalışan";



            var LoginState = Login(username, password, rol1, rol2, rol3);
            



          
            temizle();
            
        }

        private void KullaniciGirisi_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
