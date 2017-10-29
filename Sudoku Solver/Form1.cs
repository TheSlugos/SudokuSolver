using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Sudoku_Solver
{
    public partial class Form1 : Form
    {
        Label[,] _labels;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _labels = new Label[9, 9];

            // create the Labels
            for (int rows = 0; rows < 9; rows++)
            {
                for (int cols = 0; cols < 9; cols++)
                {
                    Label b = new Label();
                    b.AutoSize = false;
                    b.BorderStyle = BorderStyle.FixedSingle;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Size = new Size(30, 30);
                    b.Location = new Point(cols * 30, rows * 30);
                    b.Parent = this;
                    b.Text = String.Empty;
                    b.Name = String.Format("{0}{1}", cols, rows);
                    b.Click += Label_Click;
                    _labels[cols, rows] = b;
                }

                // create valid label at end of row
                Label rowValid = new Label();
                rowValid.Location = new Point(300, rows * 30);
                rowValid.AutoSize = false;
                rowValid.Size = new Size(30, 30);
                rowValid.TextAlign = ContentAlignment.MiddleCenter;
                rowValid.Name = "R" + rows.ToString();
                rowValid.Text = rowValid.Name;//"Invalid";
                rowValid.Parent = this;
            }

            // create the valid column labels
            for (int cols = 0; cols < 9; cols++)
            {
                Label colValid = new Label();
                colValid.Location = new Point(cols * 30, 300);
                colValid.AutoSize = false;
                colValid.Size = new Size(30, 30);
                colValid.TextAlign = ContentAlignment.MiddleCenter;
                colValid.Name = "C" + cols.ToString();
                colValid.Text = colValid.Name;
                colValid.Parent = this;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            if (me.Button == MouseButtons.Left)
            {
                HandleLeftClick((Label)sender);
            }
            else if (me.Button == MouseButtons.Right)
            {
                HandleRightClick((Label)sender);
            }
        }

        private void HandleRightClick(Label label)
        {
            Trace.WriteLine(label.Name);

            if (label.Text == String.Empty)
            {
                label.Text = "9";
            }
            else if (label.Text == "1")
            {
                label.Text = String.Empty;
            }
            else
            {
                int n = Convert.ToInt32(label.Text);
                n--;
                label.Text = n.ToString();
            }
        }

        private void HandleLeftClick(Label label)
        {
            Trace.WriteLine(label.Name);
            Trace.WriteLine(label.BackColor.ToString());

            if (label.Text == String.Empty)
            {
                label.Text = "1";
            }
            else if (label.Text == "9")
            {
                label.Text = String.Empty;
            }
            else
            {
                int n = Convert.ToInt32(label.Text);
                n++;
                label.Text = n.ToString();
            }
        }

        private void btnRows_Click(object sender, EventArgs e)
        {
            // go through each row of labels
            for (int rows = 0; rows < 9; rows++)
            {
                // sum for whole row
                int sum = 0;
                List<int> row = new List<int>();
                bool rowIsValid = true;

                // go through each cell in each row and put "value" in a list
                for (int cols = 0; cols < 9; cols++)
                {
                    int[] values = new int[10];
                    if (_labels[cols, rows].Text != String.Empty)
                    {
                        int n = Convert.ToInt32(_labels[cols, rows].Text);
                        row.Add(n);
                        sum += n;
                    }
                }

                CheckForDuplicates(row, rows);
                // check for 9 elements in list
                if ( row.Count != 9)
                {
                    rowIsValid = false;
                }
                // check sum for this row
                if (sum != 45)
                {
                    rowIsValid = false;
                }
                else
                {
                    // must be zero values or duplicates
                    // set all backgrounds to normal

                }

                string labelName = "R" + rows.ToString();
                Label thisLabel = (Label)this.Controls[labelName];
                if (rowIsValid)
                {
                    thisLabel.ForeColor = Color.Black;
                    
                }
                else
                {
                    thisLabel.ForeColor = Color.Red;
                    thisLabel.BackColor = Color.Red;
                }
            }
        }

        private void CheckForDuplicates(List<int> row, int rows)
        {
            // basically go through each cell in the row and
            // check it against each other cell, if the values
            // are the same set each cell background to red
            for (int i = 0; i < 9; i++)
            {
                _labels[i, rows].BackColor = SystemColors.Control;
            }

            for (int i = 0; i < 9; i++)
            {
                // check if this cell is already invalid
                if (_labels[i,rows].BackColor != Color.Red)
                {
                    // check for no value
                    if (_labels[i,rows].Text == String.Empty)
                    {
                        _labels[i, rows].BackColor = Color.Red;
                    }
                    else
                    {
                        // go through all other cells in this row and compare
                        for ( int j = i + 1; j < 9; j++)
                        {
                            if ( _labels[i,rows].Text == _labels[j,rows].Text)
                            {
                                _labels[i, rows].BackColor = Color.Red;
                                _labels[j, rows].BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
        }
    }
}
