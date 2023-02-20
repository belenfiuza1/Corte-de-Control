using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corte_de_Control
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool _Agrega1 = false;
                bool _Agrega2 = false;
                decimal _TotalGeneral = 0;
                int _TotalCantidadGeneral = 0;
                decimal _TotalVendedor = 0;
                int _TotalCantidad = 0;
                decimal _TotalProducto = 0;
                int _CantidadProducto = 0;
                
                if (File.Exists("Ventas.txt"))
                {
                    
                    using (StreamReader sr = new StreamReader("Ventas.txt"))
                    {
                        string s = sr.ReadLine();
                        string[] registro = s.Split(new char[] { ',' });

                        while (s != null)
                        {

                            string vendedor = registro[0];
                            dataGridView1.Rows.Add(new string[] { registro[0] });
                            while (vendedor == registro[0] && s != null) 
                            {
                                string producto = registro[1]; 
                                if (_Agrega1)
                                    dataGridView1.Rows.Add(new string[] { "", registro[1] });
                                else
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[1].Value = registro[1];
                                    _Agrega1 = true;
                                }


                                while (vendedor == registro[0] && producto == registro[1] && s != null)
                                {
                                    _TotalProducto += decimal.Parse(registro[3]);
                                    _CantidadProducto += int.Parse(registro[2]);
                                    _TotalCantidad += int.Parse(registro[2]);
                                    _TotalCantidadGeneral += int.Parse(registro[2]);
                                    _TotalVendedor += decimal.Parse(registro[3]);
                                    _TotalGeneral += decimal.Parse(registro[3]);
                                    if (_Agrega2)
                                        dataGridView1.Rows.Add(new string[] { "", "", registro[2], registro[3] });
                                    else
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[2].Value = registro[2];
                                        dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[3].Value = registro[3];
                                        _Agrega2 = true;
                                    }
                                    s = sr.ReadLine();
                                    if (s != null) { registro = s.Split(new char[] { ',' }); } 

                                }
                                dataGridView1.Rows.Add(new string[] { "", "Sub Tot =>", _CantidadProducto.ToString(), _TotalProducto.ToString() });
                                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].DefaultCellStyle.BackColor = Color.AliceBlue;
                                dataGridView1.Rows.Add(1); 
                                _TotalProducto = 0; 
                                _CantidadProducto = 0;
                                _Agrega2 = false;
                            }
                            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1); 
                            dataGridView1.Rows.Add(new string[] { "", "Cant Vendida =>", _TotalCantidad.ToString(), "Tot Vend =>", _TotalVendedor.ToString() });
                            dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].DefaultCellStyle.BackColor = Color.Aquamarine;
                            dataGridView1.Rows.Add(1);
                            _TotalVendedor = 0;
                            _TotalCantidad = 0;
                            _Agrega1 = false;
                        }

                        dataGridView1.Rows.Add(new string[] { "", "", _TotalCantidadGeneral.ToString(), "Tot Vendido =>", _TotalGeneral.ToString() });
                        dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].DefaultCellStyle.BackColor = Color.Cyan;

                        _TotalGeneral = 0;
                        _TotalCantidadGeneral = 0;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
