using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Test_Average
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The Average method accepts an int array argument
        // and returns the Average of the values in the array.
        private double Average(int[] iArray)
        {
            int total = 0;   // Accumulator, initialized to 0
            double average;  // To hold the average

            // Step through the array, adding each element to
            // the accumulator. 
            for (int index = 0; index < iArray.Length; index++)
            {
                total += iArray[index];
            }

            // Calculate the average.
            average = (double) total / iArray.Length;

            // Return the average.
            return average;
        }

        // The Highest method accepts an int array argument
        // and returns the highest value in that array.
        private int Highest(int[] iArray)
        {


            // Declare a variable to hold the highest value, and
            // initialize it with the first value in the array.
            //double[] highValue = new double[0];
            int highest = iArray[0];

            // Step through the rest of the array, beginning at
            // element 1. When a value greater than highest is found,
            // assign that value to highest.

            for (int i = 1; i < iArray.Length;  i++)
            {
                if (iArray[i] > highest)
                {
                    highest = iArray[i];
                }
            }



            // Return the highest value.
            return highest;
        }

        // The Lowest method accepts an int array argument
        // and returns the lowest value in that array.
        private int Lowest(int[] iArray)
        {
            int lowest = iArray[0];


            for (int i = 1; i < iArray.Length; i++)
            {
                if (iArray[i] < lowest)
                {
                    lowest = iArray[i];
                }
            }




            return lowest;


        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {

            testScoresListBox.Items.Clear();


            try
            {
                // Local variables
                const int SIZE = 5;            // Number of tests
                int[] score = new int[SIZE];
                int index = 0;                 // Loop counter
                                                // To hold one score 
                int highestScore;              // To hold the highest score
                int lowestScore;               // To hold the lowest score
                double averageScore;           // To hold the average score
                StreamReader inputFile;        // For file input

                // TODO:  declare a variable, scores, that will store 5 integers
                // Open the file and get a StreamReader object.
                inputFile = File.OpenText("TestScores.txt");

                // Read the test scores into the array.
                while (!inputFile.EndOfStream && index < score.Length)
                {




                    // TODO:  read a line from the file, convert to int put in score 
                   score[index] = int.Parse (inputFile.ReadLine());
                    // put score in the array at the current index
                    index++;

                }

                // Close the file.
                inputFile.Close();

                // iterate through the array using a foreach loop

                // and add one item at a time to the listbox

                foreach (int value in score)
                {
                    testScoresListBox.Items.Add(score[index]);
                }

                // Get the highest, lowest, and average scores.
                // Use the methods


                highestScore = Highest(score);
                lowestScore = Lowest(score);
                averageScore = Average(score);



                // Display the values, highest, lowest and average 
                averageScoreLabel.Text = averageScore.ToString("n2");
                lowScoreLabel.Text = lowestScore.ToString();
                highScoreLabel.Text = highestScore.ToString();

            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
