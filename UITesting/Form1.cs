using System;
using System.Drawing;
using System.Windows.Forms;
using SudokuSolver;
using System.Threading.Tasks;
using System.IO;

namespace UITesting
{
	public partial class Form1 : Form
	{
		public delegate void ChangeRowLabelText(int value, int valid, int attempt, long time);
		public ChangeRowLabelText rowLabelDelegate;

		Label selectedLabel;
		Label[,] cellLabels;

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
			cellLabels = new Label[ Settings.BOARD_SIZE, Settings.BOARD_SIZE ];

			for( int j = 0; j < Settings.BOARD_SIZE; j++ )
			{
				for( int i = 0; i < Settings.BOARD_SIZE; i++ )
				{
					cellLabels[ i, j ] = new Label();
					cellLabels[ i, j ].Size = new Size( 23, 23 );
					cellLabels[ i, j ].Location = new Point( 10 + i * 22, 30 + j * 22 );
					cellLabels[ i, j ].AutoSize = false;
					cellLabels[ i, j ].BorderStyle = BorderStyle.FixedSingle;
					cellLabels[ i, j ].Click += CellSelected;
					cellLabels[ i, j ].TextAlign = ContentAlignment.MiddleCenter;
					this.Controls.Add( cellLabels[ i, j ] );
				}
			}

			ResetCells();
			selectedLabel = default( Label );
			rowLabelDelegate = new ChangeRowLabelText(ChangeRowText);
			
		}

		private void ResetCells()
		{
			for ( int j = 0; j < Settings.BOARD_SIZE; j++)
				for ( int i =0; i < Settings.BOARD_SIZE; i++)
				{
					cellLabels[ i, j ].Text = string.Empty;
				}

			//cellLabels[ 0, 0 ].Text = "9";
			//cellLabels[ 1, 0 ].Text = "";
			//cellLabels[ 2, 0 ].Text = "";
			//cellLabels[ 3, 0 ].Text = "8";
			//cellLabels[ 4, 0 ].Text = "";
			//cellLabels[ 5, 0 ].Text = "";
			//cellLabels[ 6, 0 ].Text = "3";
			//cellLabels[ 7, 0 ].Text = "7";
			//cellLabels[ 8, 0 ].Text = "";

			//cellLabels[ 0, 1 ].Text = "";
			//cellLabels[ 1, 1 ].Text = "";
			//cellLabels[ 2, 1 ].Text = "5";
			//cellLabels[ 3, 1 ].Text = "7";
			//cellLabels[ 4, 1 ].Text = "2";
			//cellLabels[ 5, 1 ].Text = "";
			//cellLabels[ 6, 1 ].Text = "";
			//cellLabels[ 7, 1 ].Text = "8";
			//cellLabels[ 8, 1 ].Text = "";

			//cellLabels[ 0, 2 ].Text = "";
			//cellLabels[ 1, 2 ].Text = "7";
			//cellLabels[ 2, 2 ].Text = "";
			//cellLabels[ 3, 2 ].Text = "";
			//cellLabels[ 4, 2 ].Text = "";
			//cellLabels[ 5, 2 ].Text = "";
			//cellLabels[ 6, 2 ].Text = "";
			//cellLabels[ 7, 2 ].Text = "";
			//cellLabels[ 8, 2 ].Text = "6";

			//cellLabels[ 0, 3 ].Text = "";
			//cellLabels[ 1, 3 ].Text = "";
			//cellLabels[ 2, 3 ].Text = "2";
			//cellLabels[ 3, 3 ].Text = "";
			//cellLabels[ 4, 3 ].Text = "9";
			//cellLabels[ 5, 3 ].Text = "";
			//cellLabels[ 6, 3 ].Text = "";
			//cellLabels[ 7, 3 ].Text = "1";
			//cellLabels[ 8, 3 ].Text = "";

			//cellLabels[ 0, 4 ].Text = "";
			//cellLabels[ 1, 4 ].Text = "";
			//cellLabels[ 2, 4 ].Text = "1";
			//cellLabels[ 3, 4 ].Text = "";
			//cellLabels[ 4, 4 ].Text = "";
			//cellLabels[ 5, 4 ].Text = "";
			//cellLabels[ 6, 4 ].Text = "8";
			//cellLabels[ 7, 4 ].Text = "";
			//cellLabels[ 8, 4 ].Text = "";
		
			//cellLabels[ 0, 5 ].Text = "";
			//cellLabels[ 1, 5 ].Text = "9";
			//cellLabels[ 2, 5 ].Text = "";
			//cellLabels[ 3, 5 ].Text = "";
			//cellLabels[ 4, 5 ].Text = "3";
			//cellLabels[ 5, 5 ].Text = "";
			//cellLabels[ 6, 5 ].Text = "5";
			//cellLabels[ 7, 5 ].Text = "";
			//cellLabels[ 8, 5 ].Text = "";

			//cellLabels[ 0, 6 ].Text = "7";
			//cellLabels[ 1, 6 ].Text = "";
			//cellLabels[ 2, 6 ].Text = "";
			//cellLabels[ 3, 6 ].Text = "";
			//cellLabels[ 4, 6 ].Text = "";
			//cellLabels[ 5, 6 ].Text = "";
			//cellLabels[ 6, 6 ].Text = "";
			//cellLabels[ 7, 6 ].Text = "2";
			//cellLabels[ 8, 6 ].Text = "";

			//cellLabels[ 0, 7 ].Text = "";
			//cellLabels[ 1, 7 ].Text = "4";
			//cellLabels[ 2, 7 ].Text = "";
			//cellLabels[ 3, 7 ].Text = "";
			//cellLabels[ 4, 7 ].Text = "7";
			//cellLabels[ 5, 7 ].Text = "5";
			//cellLabels[ 6, 7 ].Text = "1";
			//cellLabels[ 7, 7 ].Text = "";
			//cellLabels[ 8, 7 ].Text = "";

			//cellLabels[ 0, 8 ].Text = "";
			//cellLabels[ 1, 8 ].Text = "1";
			//cellLabels[ 2, 8 ].Text = "9";
			//cellLabels[ 3, 8 ].Text = "";
			//cellLabels[ 4, 8 ].Text = "";
			//cellLabels[ 5, 8 ].Text = "3";
			//cellLabels[ 6, 8 ].Text = "";
			//cellLabels[ 7, 8 ].Text = "";
			//cellLabels[ 8, 8 ].Text = "5";
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

			l.BackColor = Color.Yellow;
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
				else if( e.KeyChar == 'c' || e.KeyChar == 'C' )
				{
					selectedLabel.Text = string.Empty;
				}

				//MessageBox.Show( e.KeyChar.ToString() );
			}
		}

		private void button10_Click( object sender, EventArgs e )
		{
			ResetCells();

			labelRow.Text = string.Empty;
			labelTime.Text = string.Empty;
			labelAttempts.Text = string.Empty;
			labelValid.Text = string.Empty;

			ResetLabelColor();
		}

		private async void button11_Click( object sender, EventArgs e )
		{
			bool labelValue = false;
			// go through all labels and if there are no label values
			// then cannot attempt solution
			// got some value so attempt to solve
			int[,] board = new int[ Settings.BOARD_SIZE, Settings.BOARD_SIZE ];
				
			for ( int j = 0; j < Settings.BOARD_SIZE; j++)
			{
				for ( int i = 0; i < Settings.BOARD_SIZE; i++)
				{
					if( cellLabels[ i, j ].Text == string.Empty )
					{
						board[ i, j ] = 0;
					}
					else
					{
						labelValue = true;
						board[ i, j ] = Convert.ToInt32( cellLabels[ i, j ].Text );
					}
				}
			}

			if( labelValue )
			{
				SudokuBoard sb = new SudokuBoard( board );
				//MessageBox.Show( sb.Board.ToString() );

				// set buttons to disabled
				button11.Enabled = false;
				this.Cursor = Cursors.WaitCursor;

				bool result = false;
				if( rdoBrute.Checked )
					result = await Task.Run( () => sb.Solve( this, chkSingle.Checked ) );
				else
					result = await Task.Run( () => sb.TargettedSolve( this, chkSingle.Checked ) );

				button11.Enabled = true;
				this.Cursor = Cursors.Default;
				
				if( result )
				{
					for ( int j = 0; j < Settings.BOARD_SIZE; j++)
					{
						for ( int i = 0; i < Settings.BOARD_SIZE; i++)
						{
							cellLabels[ i, j ].Text = String.Format( "{0}", sb.Solution.Board[ i, j ] );
						}
					}
				}
				else
					MessageBox.Show( "No Solution Found" );

				labelValid.Text = sb.Valid.ToString();
				labelAttempts.Text = sb.Attempts.ToString();
				labelTime.Text = sb.TimeTaken.ToString();
			}
		}

		private void menuFileExit_Click( object sender, EventArgs e )
		{
			Close();
		}

		private void menuFileOpen_Click( object sender, EventArgs e )
		{
			// temp board
			int[,] tmpBoard = new int[ Settings.BOARD_SIZE, Settings.BOARD_SIZE ];
			int lineCount = 0;
			bool result = true;

			// open an existing sudoku file
			openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
			if( openFileDialog1.ShowDialog() == DialogResult.OK )
			{
				using( System.IO.StreamReader file =
					new System.IO.StreamReader( openFileDialog1.FileName ) )
				{
					string line;
					while( result && ( line = file.ReadLine() ) != null )
					{
						string[] values = line.Split( ',' );
						if( values.Length != Settings.BOARD_SIZE )
						{
							result = false;
							break;
						}

						for ( int n = 0; n < values.Length; n++ )
						{
							if ( values[n].Length != 1 )
							{
								result = false;
								break;
							}

							int num;
							if ( Int32.TryParse(values[n], out num))
							{
								tmpBoard[ n, lineCount ] = num;
							}
						}

						lineCount++;
					} // end while
				}

				if ( lineCount != Settings.BOARD_SIZE)
				{
					result = false;
				}

				// if valid file write board to screen
				if ( result )
				{
					ResetCells();

					// write the board to the screen
					for( int j = 0; j < Settings.BOARD_SIZE; j++ )
					{
						for( int i = 0; i < Settings.BOARD_SIZE; i++ )
						{
							if( tmpBoard[ i, j ] != 0 )
								cellLabels[ i, j ].Text = String.Format( "{0}", tmpBoard[ i, j ] );
						}
					}
				}
				else
				{
					MessageBox.Show( "Not a valid sudoku file." );
				}
			}
		}

		private void mnuFileSave_Click( object sender, EventArgs e )
		{
			if( BoardIsEmpty() )
			{
				MessageBox.Show( "The board is empty. Cannot save." );
			}
			else if( BoardIsFull() )
			{
				MessageBox.Show( "The board is solved. No point saving." );
			}
			else
			{
				// save to file
				saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

				if( saveFileDialog1.ShowDialog() == DialogResult.OK )
				{
					//MessageBox.Show( saveFileDialog1.FileName );
					using( System.IO.StreamWriter file =
						new System.IO.StreamWriter( saveFileDialog1.FileName ) )
					{
						for( int j = 0; j < Settings.BOARD_SIZE; j++ )
						{
							for( int i = 0; i < Settings.BOARD_SIZE; i++ )
							{
								if( cellLabels[ i, j ].Text == string.Empty )
									file.Write( "0" );
								else
									file.Write( cellLabels[ i, j ].Text );
								if( i != Settings.BOARD_SIZE - 1 )
									file.Write( "," );
							}

							file.WriteLine();
						}
					}
				}
			}
		}

		private bool BoardIsEmpty()
		{
			bool result = true;

			for( int j = 0; j < Settings.BOARD_SIZE; j++ )
			{
				for( int i = 0; i < Settings.BOARD_SIZE; i++ )
				{
					if( cellLabels[ i, j ].Text != String.Empty )
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		private bool BoardIsFull()
		{
			bool result = true;

			for( int j = 0; j < Settings.BOARD_SIZE; j++ )
			{
				for( int i = 0; i < Settings.BOARD_SIZE; i++ )
				{
					if( cellLabels[ i, j ].Text == String.Empty )
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		protected override bool ProcessCmdKey( ref Message msg, Keys keyData )
		{
			int currentX, currentY, nextX, nextY;
			bool handled = false;
			currentX = currentY = nextX = nextY = 0;

			if ( selectedLabel != default(Label))
			{
				// find selected label
				for( int j = 0; j < Settings.BOARD_SIZE; j++ )
				{
					for( int i = 0; i < Settings.BOARD_SIZE; i++ )
					{
						if( cellLabels[ i, j ] == selectedLabel )
						{
							currentX = i; currentY = j;
							break;
						}
					}
				}

				nextX = currentX;
				nextY = currentY;

				switch (keyData)
				{
					case Keys.Left:
						nextX--;
						if( nextX < 0 )
							nextX = Settings.BOARD_SIZE - 1;
						handled = true;
						break;

					case Keys.Right:
						nextX++;
						if( nextX == Settings.BOARD_SIZE )
							nextX = 0;
						handled = true;
						break;

					case Keys.Up:
						nextY--;
						if( nextY < 0 )
							nextY = Settings.BOARD_SIZE - 1;
						handled = true;
						break;

					case Keys.Down:
						nextY++;
						if( nextY == Settings.BOARD_SIZE )
							nextY = 0;
						handled = true;
						break;
				}

				if ( (nextX >= 0 && nextX < Settings.BOARD_SIZE) &&
					( nextY >= 0 && nextY < Settings.BOARD_SIZE))
				{
					cellLabels[ currentX, currentY ].BackColor = SystemColors.Control;
					cellLabels[ nextX, nextY ].BackColor = Color.Yellow;
					selectedLabel = cellLabels[ nextX, nextY ];
				}
				
			}

			if( !handled )
				return base.ProcessCmdKey( ref msg, keyData );
			else
				return handled;
		}
	}
}
