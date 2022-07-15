using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace StudentDataProgram
{
    public partial class Form1 : Form
    {
        private const string FolderName = "Students";

        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
            dataGridView1.Update();


        }

        public void btnAdd_Click(object sender, EventArgs e)

        {

            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("AD", typeof(string));
            table.Columns.Add("SOYAD", typeof(string));
            table.Columns.Add("ÜNİVERSİTE", typeof(string));
            table.Columns.Add("BÖLÜM", typeof(string));
            table.Columns.Add("SINIF", typeof(string));




            dataGridView1.DataSource = table;



            string OGRNO = Convert.ToString(txtID.Text);
            string studentFolderPath = $@"{FolderName}\{OGRNO}";

            if (Directory.Exists(studentFolderPath))
            {
                MessageBox.Show("Bu öğrenci daha önce kaydedilmiş", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // ogrenci kaydetmeden  once yapilacak olan validasyonlar, ogrenci ve resim kaydi


                Student student = new Student
                {
                    studentNumber = Convert.ToInt32(txtID.Text),
                    grade = Convert.ToString(comboBox3.SelectedItem),
                    name = txtName.Text,
                    lastName = txtSurname.Text,
                    univercity = Convert.ToString(comboBox1.Text),
                    department = Convert.ToString(comboBox2.Text)
                };


                StudentValid valid = new StudentValid();
                ValidationResult result = valid.Validate(student);
                IList<ValidationFailure> failures = result.Errors;
                if (!result.IsValid)
                {
                    foreach (ValidationFailure failure in failures)
                    {
                        MessageBox.Show(failure.ErrorMessage, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {





                    //Öğrenci ekleme

                    Directory.CreateDirectory($@"{FolderName}\{OGRNO}");

                    using (StreamWriter sw = new StreamWriter($@"{studentFolderPath}\{OGRNO}.txt", true))
                    {


                        openFileDialog1.ShowDialog();
                        pictureBox1.ImageLocation = openFileDialog1.FileName;
                        string studentPhotoPath = $@"{FolderName}\{OGRNO}\{OGRNO}";

                        string sourcePath = pictureBox1.ImageLocation;

                        string destPath = studentPhotoPath;


                        File.Move(sourcePath, destPath + Guid.NewGuid().ToString() + ".jpeg");

                        string uni = Convert.ToString(comboBox1.SelectedItem);
                        string dep = Convert.ToString(comboBox2.SelectedItem);
                        string grade = Convert.ToString(comboBox3.SelectedItem);


                        sw.WriteLine(Convert.ToString(txtID.Text) + "/" + txtName.Text + "/" + txtSurname.Text + "/" + uni + "/" + dep + "/" + grade);
                        sw.Close();
                        string[] lines = File.ReadAllLines($@"{FolderName}\{OGRNO}\{OGRNO}.txt");

                        string[] values;
                        for (int i = 0; i < lines.Length; i++)
                        {
                            values = lines[i].ToString().Split('/');
                            string[] row = new string[values.Length];
                            for (int j = 0; j < values.Length; j++)
                            {
                                row[j] = values[j].Trim();
                            }
                            table.Rows.Add(row);
                        }

                    }

                    MessageBox.Show("Öğrenci başarıyla sisteme eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Update();

                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}