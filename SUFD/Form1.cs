using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SUFD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int numRow = 0;
        List<XElement> elements = new List<XElement>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            XDocument xdoc = new XDocument();

            ofd.Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //try
                {
                    if ((ofd.OpenFile()) != null)
                    {
                        xdoc = XDocument.Load(ofd.FileName);

                        string num_pp = "", kbk = "", okato = "";

                        foreach (XElement node in xdoc.Descendants())
                            if (node.Name.ToString() == "NOM_LINE")
                                dataGridView1.Rows.Add();

                        foreach (XElement node in xdoc.Descendants())
                        {
                            if (node.Name.ToString() == "NOM_LINE")
                            {
                                dataGridView1.Rows[numRow].Cells[0].Value = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (node.Name.ToString() == "DATE_PAY_DOC")
                            {
                                dataGridView1.Rows[numRow].Cells[1].Value = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (node.Name.ToString() == "DATE_REESTR_PP")
                            {
                                dataGridView1.Rows[numRow].Cells[2].Value = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (node.Name.ToString() == "SUM_REESTR_PP")
                            {
                                dataGridView1.Rows[numRow].Cells[3].Value = node.Value.Substring(0, node.Value.Length - 2) + "," + node.Value.Substring(node.Value.Length - 2);
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (node.Name.ToString() == "NOM_PP")
                            {
                                num_pp = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (numRow != dataGridView1.RowCount - 1)
                                dataGridView1.Rows[numRow].Cells[4].Value = num_pp;

                            if (node.Name.ToString() == "KBK")
                            {
                                kbk = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (numRow != dataGridView1.RowCount - 1)
                                dataGridView1.Rows[numRow].Cells[5].Value = kbk;

                            if (node.Name.ToString() == "PURPOSE")
                            {
                                string[] strNodeArray = node.Value.Split(' ');
                                Array.Clear(strNodeArray, 0, 3);
                                dataGridView1.Rows[numRow].Cells[6].Value = String.Join(" ", strNodeArray);
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                                numRow++;
                            }

                            if (node.Name.ToString() == "OKATO")
                            {
                                okato = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }

                            if (numRow != dataGridView1.RowCount - 1)
                                dataGridView1.Rows[numRow].Cells[7].Value = okato;

                            if (node.Name.ToString() == "FIO_PLAT")
                            {
                                dataGridView1.Rows[numRow].Cells[8].Value = node.Value;
                                elements.Add(new XElement(node.Name.ToString(), node.Value));
                            }
                        }
                    }
                }
                //catch (Exception ex)
                {
                    //MessageBox.Show("Ошибка. Не удалось открыть файл: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement("root");
            root.Add(elements);
            xdoc.Add(root);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                xdoc.Save(sfd.FileName);
            }
        }
    }
}
