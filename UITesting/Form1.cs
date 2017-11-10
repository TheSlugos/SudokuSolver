using System;
using System.Drawing;
using System.Windows.Forms;
using SudokuSolver;
using System.Threading.Tasks;

namespace UITesting
{
	public partial class Form1 : Form
	{
		public delegate void ChangeRowLabelText(int value, int valid, int attempt, long time);
		public ChangeRowLabelText rowLabelDelegate;

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
			rowLabelDelegate = new ChangeRowLabelText(ChangeRowText);
			
		}

		private void ChangeRowText( int value, int valid, int attempt, long time )
		{
			labelRow.Text = value.ToString();
			labelAttempts.Text = attempt.ToString();
			labelValid.Text = valid.ToString();
			labelTime.Text = time.ToString();
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

		private async void button11_Click( object sender, EventArgs e )
		{
			bool labelValue = false;
			// go through all labels and if there are no label values
			// then cannot attempt solution
			foreach(Control c in Controls)
			{
				if ( c is Label )
				{
					if (((Label)c).Text != string.Empty)
					{
						labelValue = true;
						break;
					}
				}
			}

			if ( labelValue )
			{
				// got some value so attempt to solve
				int[,] board = new int[ Settings.BOARD_SIZE, Settings.BOARD_SIZE ];
				
				foreach( Control c in Controls)
				{
					if ( c is Label)
					{
						string tagvalue = (string)c.Tag;
						int tag = Convert.ToInt32( tagvalue );

						if ( tagvalue != default(object))
						{
							int col = tag / 10;
							int row = tag % 10;

							if( ( ( Label )c ).Text == string.Empty )
							{
								board[ col, row ] = 0;
							}
							else
							{
								board[ col, row ] = Convert.ToInt32( ( ( Label )c ).Text );
							}
						}
					}
				}

				SudokuBoard sb = new SudokuBoard( board );
				MessageBox.Show( sb.Board.ToString() );

				// set buttons to disabled
				button11.Enabled = false;
				this.Cursor = Cursors.WaitCursor;
				bool result = await Task.Run( () => sb.Solve( this ) );

				button11.Enabled = true;
				this.Cursor = Cursors.Default;
				
				if( result )
				{
					foreach( Control c in Controls )
					{
						if( c is Label )
						{
							string tagvalue = ( string )c.Tag;
							int tag = Convert.ToInt32( tagvalue );

							if( tagvalue != default( object ) )
							{
								int col = tag / 10;
								int row = tag % 10;

								( ( Label )c ).Text = String.Format( "{0}", sb.Solution.Board[ col, row ] );
							}
						}
					}

					MessageBox.Show( sb.Solution.ToString() );
				}
				else
					MessageBox.Show( "No Solution Found" );

				labelValid.Text = sb.Valid.ToString();
				labelAttempts.Text = sb.Attempts.ToString();
				labelTime.Text = sb.TimeTaken.ToString();
			}
		}
	}
}
