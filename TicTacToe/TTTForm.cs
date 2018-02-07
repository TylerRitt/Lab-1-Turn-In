using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TicTacToe
{
    public partial class TTTForm : Form
    {
        public TTTForm()
        {
            InitializeComponent();
        }

        const string USER_SYMBOL = "X";
        const string COMPUTER_SYMBOL = "O";
        const string EMPTY = "";

        const int SIZE = 5;

        // constants for the 2 diagonals
        const int TOP_LEFT_TO_BOTTOM_RIGHT = 1;
        const int TOP_RIGHT_TO_BOTTOM_LEFT = 2;

        // constants for IsWinner
        const int NONE = -1;
        const int ROW = 1;
        const int COLUMN = 2;
        const int DIAGONAL = 3;

        string[,] board = new string[SIZE, SIZE];

        public void FillBoard()
        {
            for (int row =0; row<SIZE; row++)
            {
                for (int col = 0; col <SIZE; col++)
                {
                    board[row, col] = EMPTY;
                }
            }
            EnableAllSquares();
        }

        // This method takes a row and column as parameters and 
        // returns a reference to a label on the form in that position
        private Label GetSquare(int row, int column)
        {
            int labelNumber = row * SIZE + column + 1;
            return (Label)(this.Controls["label" + labelNumber.ToString()]);
        }

        // This method does the "reverse" process of GetSquare
        // It takes a label on the form as it's parameter and
        // returns the row and column of that square as output parameters
        private void GetRowAndColumn(Label l, out int row, out int column)
        {
            int position = int.Parse(l.Name.Substring(5));
            row = (position - 1) / SIZE;
            column = (position - 1) % SIZE;
        }

        // This method takes a row (in the range of 0 - 4) and returns true if 
        // the row on the form contains 5 Xs or 5 Os.
        // Use it as a model for writing IsColumnWinner
        private bool IsRowWinner(int row)
        {
            //change for array

            // Label square = GetSquare(row, 0);
            
            string symbol = board[row,0];
            for (int col = 0; col < SIZE; col++)
            {
                //square = GetSquare(row, col);
                if (board[row, col] == EMPTY || board[row, col] != symbol)
                {
                    return false;
                }
            }
            HighlightRow(row);
            return true;
        }

        //* TODO:  finish all of these that return true
        private bool IsAnyRowWinner()
        {
            bool winner = false;
            for (int row =0; row <SIZE; row++)
            {
                if (IsRowWinner(row))
                    winner = true;
            }


            return winner;
        }

        private bool IsColumnWinner(int col)
        {
            //change for array
            /* if (GetSquare(0,col).Text == GetSquare(1, col).Text && GetSquare(0,col).Text == GetSquare(2,col).Text && GetSquare(0, col).Text == GetSquare(3,0).Text && GetSquare(0,col).Text == GetSquare(4,0).Text && GetSquare(0,col).Text == GetSquare(5,0).Text && GetSquare(0,col).Text != "")
             {
                 HighlightColumn(col);
                 return true;
             }*/


            string symbol = board[0, col];
            for (int row = 0; row < SIZE; row++)
            {
                // square = GetSquare(row, col);
                if (board[row,col] == EMPTY || board[row, col] != symbol)
                    return false;
            }
            HighlightColumn(col);
            return true;

           /* else
            {
                return false;
            }
            */
            
        }

        private bool IsAnyColumnWinner()
        {
            bool winner = false;
            for (int colu = 0; colu < SIZE; colu++)
            {
                if (IsColumnWinner(colu))
                    winner = true;
            }


            return winner;
        }

        private bool IsDiagonal1Winner()
        {
            // change for array
            // Label square = GetSquare(0, (SIZE - 1));
            
            
            string symbol = board[0, 0];
            //string symbol = square.Text;
            for (int row = 0, col = 0; row < SIZE; row++, col++)
            {
                //square = GetSquare(row, col);
                if (board[row,col] == EMPTY || board[row, col] != symbol)
                {
                    return false;
                }
            }
            HighlightDiagonal1();
            return true;
        }




        private bool IsDiagonal2Winner()
        {
            //change for array
            //Label square = GetSquare(0, (SIZE - 1));
            //string symbol = square.Text;

            string symbol = board[SIZE - 1, 0]; // if we change board size
           
            for (int row = SIZE - 1,  col = 0; col < SIZE; row--, col++)
            {
                //square = GetSquare(row, col);
                if (board[row,col] == EMPTY || board[row, col] != symbol)
                    return false;
            }
            HighlightDiagonal2();
            return true;
        }

        private bool IsAnyDiagonalWinner()
        {

            //change for array
            if (IsDiagonal1Winner())
            {
                HighlightDiagonal(1);
                return true;
            }
            else if (IsDiagonal2Winner())
            {
                HighlightDiagonal(2);
                return true;
            }
            else return false;

        }

        private bool IsFull()
        {
            bool full = true;
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label lab = GetSquare(row, col);
                    if (lab.Text == "")

                    {
                        full = false;
                    }


                }

            }

            return full;
        }

        // This method determines if any row, column or diagonal on the board is a winner.
        // It returns true or false and the output parameters will contain appropriate values
        // when the method returns true.  See constant definitions at top of form.
        private bool IsWinner(out int whichDimension, out int whichOne)
        {
            // rows
            for (int row = 0; row < SIZE; row++)
            {
                if (IsRowWinner(row))
                {
                    whichDimension = ROW;
                    whichOne = row;
                   
                    return true;
                }
            }
            
            
            // columns
            for (int column = 0; column < SIZE; column++)
            {
                if (IsColumnWinner(column))
                {
                    whichDimension = COLUMN;
                    whichOne = column;
                    
                    return true;
                }
            }
            
            // diagonals
            if (IsDiagonal1Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_LEFT_TO_BOTTOM_RIGHT;
                
                return true;
            }
            if (IsDiagonal2Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_RIGHT_TO_BOTTOM_LEFT;
                
                return true;
            }
            whichDimension = NONE;
            whichOne = NONE;
            return false;
        }

        // I wrote this method to show you how to call IsWinner
        private bool IsTie()
        {
            int winningDimension, winningValue;
            return (IsFull() && !IsWinner(out winningDimension, out winningValue));
        }

        // This method takes an integer in the range 0 - 4 that represents a column
        // as it's parameter and changes the font color of that cell to red.
        private void HighlightColumn(int col)
        {
            
            for (int row = 0; row < SIZE; row++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method changes the font color of the top right to bottom left diagonal to red
        // I did this diagonal because it's harder than the other one
        private void HighlightDiagonal2()
        {
            for (int row = 0, col = SIZE - 1; row < SIZE; row++, col--)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method will highlight either diagonal, depending on the parameter that you pass
        private void HighlightDiagonal(int whichDiagonal)
        {
            if (whichDiagonal == TOP_LEFT_TO_BOTTOM_RIGHT)
                HighlightDiagonal1();
            else
                HighlightDiagonal2();

        }

        //* TODO:  finish these 2
        private void HighlightRow(int row)
        {
            for (int col =0; col <SIZE; col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;

                square.ForeColor = Color.Red;
                GetSquare(row, col).ForeColor = Color.Red;

            }
        }

        private void HighlightDiagonal1()
        {
            for (int row = 0, col = 0; row < SIZE; row++, col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }



        }

        //* TODO:  finish this
        private void HighlightWinner(string player, int winningDimension, int winningValue)
        {
            switch (winningDimension)
            {
                case ROW:

                    break;
                case COLUMN:

                    break;
                case DIAGONAL:
                    HighlightDiagonal(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
            }
        }

        //* TODO:  finish these 2
        private void ResetSquares()
        {

            for(int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    board[row, col] = EMPTY;
                    GetSquare(row, col).ForeColor = Color.Black;
                }
            }
            
            SyncArrayAndUi();
        }

        private void MakeComputerMove()
        {

            //change for array
            // just to make the computer move, everything else needs to go into the event handler as a call to method
            bool yes = true;
            int col;
            int row;
            Random rnd = new Random();
            while (yes == true)
            {
                col = rnd.Next(0, 4);
                row = rnd.Next(0, 4);
                if (board[row, col] == EMPTY)
                {
                    //GetSquare(row, col).Enabled = false;
                    board[row, col] = COMPUTER_SYMBOL;
                    yes = false;
                    SyncArrayAndUi();
                    //GetSquare(row, col).Text = COMPUTER_SYMBOL.ToString();
                }
                else if (IsFull())
                {
                    yes = false;
                }
            }





        }

        // Setting the enabled property changes the look and feel of the cell.
        // Instead, this code removes the event handler from each square.
        // Use it when someone wins or the board is full to prevent clicking a square.
        private void DisableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                   
                    DisableSquare(square);
                }
            }
        }

        // Inside the click event handler you have a reference to the label that was clicked
        // Use this method (and pass that label as a parameter) to disable just that one square
        private void DisableSquare(Label square)
        {
            square.Click -= new System.EventHandler(this.label_Click);
        }

        // You'll need this method to allow the user to start a new game
        private void EnableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Click += new System.EventHandler(this.label_Click);
                }
            }
        }


        public void SyncArrayAndUi()
        {
            for (int row = 0; row <board.GetLength(0)/*same as SIZE*/; row++)
            {
                for (int col = 0; col <board.GetLength(1); col++)
                {
                    GetSquare(row, col).Text = board[row, col];
                    if (GetSquare(row, col).Text != EMPTY) DisableSquare(GetSquare(row,col));
                }
            }
        }




        //* TODO:  finish the event handlers
        private void label_Click(object sender, EventArgs e)
        {
            int winningDimension = NONE;
            int winningValue = NONE;
            int row, col;

            Label clickedLabel = (Label)sender;
           GetRowAndColumn(clickedLabel, out row, out col);
           // board[row, col] = USER_SYMBOL;
           // SyncArrayAndUi();



            // if (clickedLabel.Text == "")
            if (board[row, col] == EMPTY)
            {
                
               //GetRowAndColumn(clickedLabel, out row, out col);
               board[row, col] = USER_SYMBOL;
               // clickedLabel.Text = USER_SYMBOL.ToString();
                //clickedLabel.Enabled = false;
                SyncArrayAndUi();
                

                if (IsWinner(out int whichDimension, out int whichOne))
                {
                    MessageBox.Show("You Win!");
                }
                else if (IsFull())
                {
                    MessageBox.Show("It's a tie");
                }
                else
                    MakeComputerMove();
                    SyncArrayAndUi();
            }

        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            ResetSquares();
            EnableAllSquares();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
