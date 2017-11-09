using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UITesting
{
	public partial class Form1 : Form
	{
		Label selectedLabel;

		public Form1()
		{
			InitializeComponent();
		}

		private void NumberClicked( object sender, EventArgs e )
		{
			Button b = ( Button )sender;

			switch( selectedLabel )
			{
				case default( Label ):
				{
						MessageBox.Show( "No cell selected", "Error" );
				}
				break;

				default:
				{
						if( b.Text == "C" )
							selectedLabel.Text = String.Empty;
						else
							selectedLabel.Text = b.Text;
				}
				break;
			}
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			selectedLabel = default( Label );
		}

		private void CellSelected( object sender, EventArgs e )
		{
			Label l = ( Label )sender;

			//MessageBox.Show( l.BackColor.ToString() );

			ResetLabelColor();

			l.BackColor = Color.Blue;
			selectedLabel = l;
		}

		private void ResetLabelColor()
		{
			foreach( object o in Controls )
			{
				if( o is Label )
				{
					( ( Label )o ).BackColor = SystemColors.Control;
				}
			}

			selectedLabel = default( Label );
		}

		private void Form1_MouseClick( object sender, MouseEventArgs e )
		{
			ResetLabelColor();
		}

		private void Form1_KeyPress( object sender, KeyPressEventArgs e )
		{
			if( selectedLabel != default( Label ) )
			{
				if( Char.IsDigit( e.KeyChar ) )
				{
					selectedLabel.Text = e.KeyChar.ToString();
				}
			}
		}

		private void button10_Click( object sender, EventArgs e )
		{
			foreach (Control c in Controls)
			{
				if ( c is Label )
				{
					( ( Label )c ).Text = String.Empty;
				}
			}

			ResetLabelColor();
		}
	}
}
