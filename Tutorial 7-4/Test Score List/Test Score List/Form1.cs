using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Test_Score_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        // The ReadScores method reads the scores from the
        // TestScores.txt file into the scoresList parameter.
        private void ReadScores(List<int> scoresList)
        {
            try
            {
                // Open the TestScores.txt file.
                StreamReader inputFile = File.OpenText("TestScores.txt");

                // Read the scores into the list.
                while (!inputFile.EndOfStream)
                {
                   int score = int.Parse(inputFile.ReadLine());
                    scoresList.Add(score);

                }

                // Close the file.
                inputFile.Close();
            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }
        }

        // The DisplayScores method displays the contents of the
        // scoresList parameter in the ListBox control.
        private void DisplayScores(List<int> scoreList2, ListBox lBox)
        {
            // TODO:  iterate through the list
            lBox.Items.Clear();

            foreach (int score in scoreList2)
            {
                lBox.Items.Add(score);
            }




            // add each item from the list to the list box
        }

        // The Average method returns the average of the values
        // in the scoresList parameter.
       private double Average(List <int> scoresList)
        {
            int total = 0;   // Accumulator, initialized to 0
            double average;  // To hold the average

            // Step through the list, adding each element to
            // the accumulator. 

            foreach (int score in scoresList)
            {
                total += score;
            }
           

            // Calculate the average.
           average = (double)total / 

            // Return the average.
            return average;
        }

        // The AboveAverage method returns the number of
        // above average scores in scoresList.
        private int AboveAverage(List<int> scoresList, double average)
        {
            int numAbove = 0;       // Accumulator

            // TODO:  Get the average score.
            Average(scoresList);


            // TODO:  Count the number of above average scores.


            // Return the number of above average scores.
            return numAbove;
        }

        // The BelowAverage method returns the number of
        // below average scores in scoresList.
        private int BelowAverage(List <int> scoresList, double average)
        {
            int numBelow = 0;       // Accumulator

            // TODO:  Get the average score.
            Average(scoresList);

            // TODO:  Count the number of below average scores.

            // Return the number of below average scores.
            return numBelow;
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            List<int> scoresList2 = new List<int>();
            ReadScores(scoresList2);
            DisplayScores(scoresList2, testScoresListBox);


        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
