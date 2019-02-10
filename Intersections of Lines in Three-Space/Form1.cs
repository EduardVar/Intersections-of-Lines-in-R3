using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersections_of_Lines_in_Three_Space
{
    public partial class MainForm : Form
    {
        double[] vectorA0 = new double[3];
        double[] vectorAM = new double[3];

        double[] vectorB0 = new double[3];
        double[] vectorBM = new double[3];

        double kConstant;

        bool[] wasSuccessful = new bool[12];
        
        double sValue;
        double tValue;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //Draw Arrows
            Graphics g = e.Graphics;
            Pen p = new Pen(Brushes.Black,4);
            p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //(EndX, EndY, BegX, BegY)
            g.DrawLine(p, 380, 350, 145, 390);
            g.DrawLine(p, 380, 430, 145, 400);
            g.DrawLine(p, 710, 430, 535, 430);
            g.DrawLine(p, 710, 360, 535, 420);
        }

        private void btnLinesParallel_Click(object sender, EventArgs e)
        {
            txtBoxR3ax0.BackColor = Color.White;
            txtBoxR3ay0.BackColor = Color.White;
            txtBoxR3az0.BackColor = Color.White;
            txtBoxR3axM.BackColor = Color.White;
            txtBoxR3ayM.BackColor = Color.White;
            txtBoxR3azM.BackColor = Color.White;
            txtBoxR3bx0.BackColor = Color.White;
            txtBoxR3by0.BackColor = Color.White;
            txtBoxR3bz0.BackColor = Color.White;
            txtBoxR3bxM.BackColor = Color.White;
            txtBoxR3byM.BackColor = Color.White;
            txtBoxR3bzM.BackColor = Color.White;

            wasSuccessful[0] = Double.TryParse(txtBoxR3ax0.Text, out vectorA0[0]);
            wasSuccessful[1] = Double.TryParse(txtBoxR3ay0.Text, out vectorA0[1]);
            wasSuccessful[2] = Double.TryParse(txtBoxR3az0.Text, out vectorA0[2]);

            wasSuccessful[3] = Double.TryParse(txtBoxR3axM.Text, out vectorAM[0]);
            wasSuccessful[4] = Double.TryParse(txtBoxR3ayM.Text, out vectorAM[1]);
            wasSuccessful[5] = Double.TryParse(txtBoxR3azM.Text, out vectorAM[2]);

            wasSuccessful[6] = Double.TryParse(txtBoxR3bx0.Text, out vectorB0[0]);
            wasSuccessful[7] = Double.TryParse(txtBoxR3by0.Text, out vectorB0[1]);
            wasSuccessful[8] = Double.TryParse(txtBoxR3bz0.Text, out vectorB0[2]);

            wasSuccessful[9] = Double.TryParse(txtBoxR3bxM.Text, out vectorBM[0]);
            wasSuccessful[10] = Double.TryParse(txtBoxR3byM.Text, out vectorBM[1]);
            wasSuccessful[11] = Double.TryParse(txtBoxR3bzM.Text, out vectorBM[2]);

            if (!wasSuccessful[0]) { txtBoxR3ax0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[1]) { txtBoxR3ay0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[2]) { txtBoxR3az0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[3]) { txtBoxR3axM.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[4]) { txtBoxR3ayM.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[5]) { txtBoxR3azM.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[6]) { txtBoxR3bx0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[7]) { txtBoxR3by0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[8]) { txtBoxR3bz0.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[9]) { txtBoxR3bxM.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[10]) { txtBoxR3byM.BackColor = Color.LightGoldenrodYellow; }
            if (!wasSuccessful[11]) { txtBoxR3bzM.BackColor = Color.LightGoldenrodYellow; }

            bool allSuccessful = true;

            for (int i = 0; i < wasSuccessful.Length; i++)
            {
                if (!wasSuccessful[i])
                {
                    allSuccessful = false;
                }
            }

            if (allSuccessful)
            {
                txtBoxR3ax0.Enabled = false;
                txtBoxR3ay0.Enabled = false;
                txtBoxR3az0.Enabled = false;
                txtBoxR3axM.Enabled = false;
                txtBoxR3ayM.Enabled = false;
                txtBoxR3azM.Enabled = false;  
                txtBoxR3bx0.Enabled = false;
                txtBoxR3by0.Enabled = false;
                txtBoxR3bz0.Enabled = false;
                txtBoxR3bxM.Enabled = false;
                txtBoxR3byM.Enabled = false;
                txtBoxR3bzM.Enabled = false;

                string output = "??????ARE THE LINES PARALLEL??????\n";
                output += "The vector equations provided are\n\n" + "[" + vectorA0[0] + ", " + vectorA0[1] + ", " + vectorA0[2] + "] + t[" + vectorAM[0] + ", " + vectorAM[1] + ", " + vectorAM[2] + "]";
                output += "\n[" + vectorB0[0] + ", " + vectorB0[1] + ", " + vectorB0[2] + " ] + s[" + vectorBM[0] + ", " + vectorBM[1] + ", " + vectorBM[2] + "]";

                output += "\n\nNow, we compare the direction vectors (m) of the two equations\nm1 = [" + vectorAM[0] + ", " + vectorAM[1] + ", " + vectorAM[2] + "]";
                output += "\nm2 = [" + vectorBM[0] + ", " + vectorBM[1] + ", " + vectorBM[2] + " ]";

                if (vectorBM[0] / vectorAM[0] == vectorBM[1] / vectorAM[1] && vectorBM[0] / vectorAM[0] == vectorBM[2] / vectorAM[2])
                {
                    //Parallel
                    btnCoincidentOrDistinct.Enabled = true;

                    kConstant = vectorBM[0] / vectorAM[0];
                    output += "\n\nDividing each 'm' component by the other shows that\n" + kConstant + "(m1) = m2\n\nTHEREFORE THEY ARE PARALLEL!"; 
                }
                else
                {
                    //Not parallel
                    btnIntersectOrSkewed.Enabled = true;

                    output += "\n\nDiving each 'm' component by the other doesn't provide us with a 'k' constant equal to each components\n\nTHEREFORE THEY ARE NOT PARALLEL!";
                }

                MessageBox.Show(output);
            }
        }

        private void btnCoincidentOrDistinct_Click(object sender, EventArgs e)
        {
            string output1 = "??????ARE THE LINES COINCIDENT OR DISTINCT??????\nFirst, split up the second line into its X, Y, and Z components!\n";
            output1 += "\nx = " + vectorB0[0] + " + " + vectorBM[0] + "t";
            output1 += "\ny = " + vectorB0[1] + " + " + vectorBM[1] + "t";
            output1 += "\nz = " + vectorB0[2] + " + " + vectorBM[2] + "t";

            output1 += "\n\nSub the P0 from line 1 into line 2 components!";
            output1 += "\n" + vectorA0[0] + " = " + vectorB0[0] + " + " + vectorBM[0] + "t";
            output1 += "\n" + vectorA0[1] + " = " + vectorB0[1] + " + " + vectorBM[1] + "t";
            output1 += "\n" + vectorA0[2] + " = " + vectorB0[2] + " + " + vectorBM[2] + "t";

            output1 += "\n\nSolve for t!";
            MessageBox.Show(output1);

            string output2 = "";
            output2 += "" + vectorA0[0] + " = " + vectorB0[0] + " + " + vectorBM[0] + "t";
            output2 += "\n" + vectorA0[1] + " = " + vectorB0[1] + " + " + vectorBM[1] + "t";
            output2 += "\n" + vectorA0[2] + " = " + vectorB0[2] + " + " + vectorBM[2] + "t";

            output2 += "\n\n" + (vectorA0[0] - vectorB0[0]) + " = " + vectorBM[0] + "t";
            output2 += "\n" + (vectorA0[1] - vectorB0[1]) + " = " + vectorBM[1] + "t";
            output2 += "\n" + (vectorA0[2] - vectorB0[2]) + " = " + vectorBM[2] + "t";

            double t1 = (vectorA0[0] - vectorB0[0]) / vectorBM[0];
            double t2 = (vectorA0[1] - vectorB0[1]) / vectorBM[1];
            double t3 = (vectorA0[2] - vectorB0[2]) / vectorBM[2];

            output2 += "\n\nt = " + t1;
            output2 += "\nt = " + t2;
            output2 += "\nt = " + t3 + "\n\n";

            if (t1 == t2 && t1 == t3)
            {
                output2 += "All values for t are the same!\n\nTHEREFORE THE LINES ARE COINCIDENT!";
            }
            else
            {
                output2 += "All values for t are NOT the same!\n\nTHEREFORE THE LINES ARE DISTINCT!";
            }

            MessageBox.Show(output2);
        }

        private void btnIntersectOrSkewed_Click(object sender, EventArgs e)
        {
            string output1 = "??????ARE THE LINES COINCIDENT OR DISTINCT??????\nFirst, split both lines up into its x, y, and z components\n";
            output1 += "\nx = " + vectorA0[0] + " + " + vectorAM[0] + "t";
            output1 += "\ny = " + vectorA0[1] + " + " + vectorAM[1] + "t";
            output1 += "\nz = " + vectorA0[2] + " + " + vectorAM[2] + "t\n";

            output1 += "\nx = " + vectorB0[0] + " + " + vectorBM[0] + "s";
            output1 += "\ny = " + vectorB0[1] + " + " + vectorBM[1] + "s";
            output1 += "\nz = " + vectorB0[2] + " + " + vectorBM[2] + "s";

            output1 += "\n\nNext, equal the x, y, and z equations to each other and re-arrange!";
            output1 += "\n" + vectorA0[0] + " + " + vectorAM[0] + "t = " + vectorB0[0] + " + " + vectorBM[0] + "s";
            output1 += "\n1) " + vectorAM[0] + "t - (" + vectorBM[0] + ")s = " + (vectorB0[0] - vectorA0[0]);

            output1 += "\n\n" + vectorA0[1] + " + " + vectorAM[1] + "t = " + vectorB0[1] + " + " + vectorBM[1] + "s";
            output1 += "\n2) " + vectorAM[1] + "t - (" + vectorBM[1] + ")s = " + (vectorB0[1] - vectorA0[1]);

            output1 += "\n\n" + vectorA0[2] + " + " + vectorAM[2] + "t = " + vectorB0[2] + " + " + vectorBM[2] + "s";
            output1 += "\n3) " + vectorAM[2] + "t - (" + vectorBM[2] + ")s = " + (vectorB0[2] - vectorA0[2]);

            MessageBox.Show(output1);

            string output2 = "Next, solve equations 2 and 3 for 's' and 't'\nFirst, solve for s\n\n";

            output2 += "\n2) " + vectorAM[1] + "t - (" + vectorBM[1] + ")s = " + (vectorB0[1] - vectorA0[1]);
            output2 += "\n3) " + vectorAM[2] + "t - (" + vectorBM[2] + ")s = " + (vectorB0[2] - vectorA0[2]) + "\n";

            double multiplier = vectorAM[2] / vectorAM[1];
            output2 += "Multiply equation 2) by " + multiplier;
            output2 += "\n\n\t" + (vectorAM[1] * multiplier) + "t - (" + (vectorBM[1] * multiplier) + ")s = " + ((vectorB0[1] - vectorA0[1]) * multiplier);
            output2 += "\n              -\t" + vectorAM[2] + "t - (" + vectorBM[2] + ")s = " + (vectorB0[2] - vectorA0[2]) + "\n\t---------------------";

            output2 += "\n\t" + (-(vectorBM[1] * multiplier) + vectorBM[2]) + "s = " + (((vectorB0[1] - vectorA0[1]) * multiplier) - (vectorB0[2] - vectorA0[2]));
            sValue = (((vectorB0[1] - vectorA0[1]) * multiplier) - (vectorB0[2] - vectorA0[2])) / (-(vectorBM[1] * multiplier) + vectorBM[2]);
            output2 += "\n\t s = " + sValue;

            output2 += "\n\nSubstitue s in 2) to get t value!";
            output2 += "\n" + vectorAM[1] + "t - (" + vectorBM[1] + ")(" + sValue + ") = " + (vectorB0[1] - vectorA0[1]);
            output2 += "\n" + vectorAM[1] + "t = " + ((vectorB0[1] - vectorA0[1]) + (vectorBM[1] * sValue));
            tValue = ((vectorB0[1] - vectorA0[1]) + (vectorBM[1] * sValue)) / vectorAM[1];
            output2 += "\nt = " + tValue;

            MessageBox.Show(output2);

            string output3 = "t = " + tValue + " and s = " + sValue;
            output3 += "\nCheck to see that s = −3 and t = −2 satisfy equation 1)";

            double RS = (vectorB0[0] - vectorA0[0]);
            output3 += "\n\n Left Side (L.S.)\t| Right Side (R.S.)";
            output3 += "\n    --------------------|---------------------   ";
            output3 += "\n " + vectorAM[0] + "t - (" + vectorBM[0] + ")s\t\t| " + RS;
            output3 += "\n =" + vectorAM[0] + "(" + tValue + ") - (" + vectorBM[0] + ")(" + sValue + ")";
            output3 += "\n =" + (vectorAM[0] * tValue) + " - (" + (vectorBM[0] * sValue) + "";
            double LS = (vectorAM[0] * tValue) - (vectorBM[0] * sValue);
            output3 += "\n =" + LS;

            if (LS == RS)
            {
                output3 += "\n\nLS = RS\nTherefore, the system will have a unique solution!";
                btnPointOfIntersection.Enabled = true;
            }
            else
            {
                output3 += "\n\nLS =/= RS\nTherefore, the lines are askew!";
                btnFindDistance.Enabled = true;
            }

            MessageBox.Show(output3);
        }

        private void btnPointOfIntersection_Click(object sender, EventArgs e)
        {
            string output1 = "Substitute t = −3 into line 1 (or s = −2 into line 2):";
            output1 += "\n\nl1: [x, y, z] = [" + vectorA0[0] + ", " + vectorA0[1] + ", " + vectorA0[2] + "] + (" + tValue + ")[" + vectorAM[0] + ", " + vectorAM[1] + ", " + vectorAM[2] + "]";
            output1 += "\n     [x, y, z] = [" + (vectorA0[0] + (tValue * vectorAM[0])) + ", " + (vectorA0[1] + (tValue * vectorAM[1])) + ", " + (vectorA0[2] + (tValue * vectorAM[2])) + "]";
            output1 += "\n\nTherefore, this system has a unique solution at (" + (vectorA0[0] + (tValue * vectorAM[0])) + ", " + (vectorA0[1] + (tValue * vectorAM[1])) + ", " +
                (vectorA0[2] + (tValue * vectorAM[2])) + ").";

            MessageBox.Show(output1);
        }

        private void btnFindDistance_Click(object sender, EventArgs e)
        {
            string output1 = "Two point vectors are provided, therefore it is possible to determine the shortest distance between the two.";
            output1 += "\n\n|projnP1P2| = |P1P2 ⋅ n| / |n|";

            output1 += "\n\nUse position vectors to determine P1P2 :";
            output1 += "\nP1P2 = [" + vectorB0[0] + ", " + vectorB0[1] + ", " + vectorB0[2] + "] - [" + vectorA0[0] + ", " + vectorA0[1] + ", " + vectorA0[2] + "]";

            double P1P2x = vectorB0[0] - vectorA0[0];
            double P1P2y = vectorB0[1] - vectorA0[1];
            double P1P2z = vectorB0[2] - vectorA0[2];
            output1 += "\nP1P2 = [" + P1P2x + ", " + P1P2y + ", " + P1P2z + "]";

            output1 += "\n\nUse the direction vectors for both lines to calculate the normal vector to the lines.";
            output1 += "\nn = m1 × m2";
            output1 += "\n   = [(" + vectorAM[1] + ")(" + vectorBM[2] + ") - (" + vectorBM[1] + ")(" + vectorAM[2] + "), (" + vectorAM[2] + ")(" + vectorBM[0] + ") - (" + vectorBM[2] + ")(" + 
                vectorAM[0] + "), (" + vectorAM[0] + ")(" + vectorBM[1] + ") - (" + vectorBM[0] + ")(" + vectorAM[1] + ")]";
            output1 += "\n   =[" + (vectorAM[1] * vectorBM[2]) + " - (" + (vectorBM[1] * vectorAM[2]) + "), " + (vectorAM[2] * vectorBM[0]) + " - (" + (vectorBM[2] * vectorAM[0]) + "), " +
                (vectorAM[0] * vectorBM[1]) + " - (" + (vectorBM[0] * vectorAM[1]) + ")]";

            double nx = (vectorAM[1] * vectorBM[2]) - (vectorBM[1] * vectorAM[2]);
            double ny = (vectorAM[2] * vectorBM[0]) - (vectorBM[2] * vectorAM[0]);
            double nz = (vectorAM[0] * vectorBM[1]) - (vectorBM[0] * vectorAM[1]);
            output1 += "\n   =[" + nx + ", " + ny + ", " + nz + "]";

            MessageBox.Show(output1);

            string output2 = "Therefore, a vector normal to both lines is n = [" + nx + ", " + ny + ", " + nz + "] . So then we have:";
            output2 += "\n\n|AB| = |P1P2 ⋅ n| / |n|";
            output2 += "\n        = |[" + P1P2x + ", " + P1P2y + ", " + P1P2z + "] ⋅ [" + nx + ", " + ny + ", " + nz + "]| / |[" + nx + ", " + ny + ", " + nz + "]";
            output2 += "\n        = |[(" + P1P2x + ")(" + nx + ") + (" + P1P2y + ")(" + ny + ") + (" + P1P2z + ")(" + nz + ")]| / sqrt((" + nx + ")^2 + (" + ny + ")^2 + (" + nz + ")^2)";

            double numerator = (P1P2x * nx) + (P1P2y * ny) + (P1P2z * nz);
            double denomiator = Math.Sqrt(Math.Pow(nx, 2) + Math.Pow(ny, 2) + Math.Pow(nz, 2));

            output2 += "\n        = |" + numerator + "| / " + denomiator;

            double answer = Math.Round(Math.Abs(numerator) / denomiator, 2);

            output2 += "\n        ≈ " + answer;
            output2 += "\n\nTherefore, the perpendicular distance between these two lines is about " + answer + " units.";

            MessageBox.Show(output2);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnCoincidentOrDistinct.Enabled = false;
            btnFindDistance.Enabled = false;
            btnIntersectOrSkewed.Enabled = false;
            btnPointOfIntersection.Enabled = false;

            txtBoxR3ax0.Text = "";
            txtBoxR3axM.Text = "";
            txtBoxR3ay0.Text = "";
            txtBoxR3ayM.Text = "";
            txtBoxR3az0.Text = "";
            txtBoxR3azM.Text = "";
            txtBoxR3bx0.Text = "";
            txtBoxR3bxM.Text = "";
            txtBoxR3by0.Text = "";
            txtBoxR3byM.Text = "";
            txtBoxR3bz0.Text = "";
            txtBoxR3bzM.Text = "";

            txtBoxR3ax0.Enabled = true;
            txtBoxR3ay0.Enabled = true;
            txtBoxR3az0.Enabled = true;
            txtBoxR3axM.Enabled = true;
            txtBoxR3ayM.Enabled = true;
            txtBoxR3azM.Enabled = true;
            txtBoxR3bx0.Enabled = true;
            txtBoxR3by0.Enabled = true;
            txtBoxR3bz0.Enabled = true;
            txtBoxR3bxM.Enabled = true;
            txtBoxR3byM.Enabled = true;
            txtBoxR3bzM.Enabled = true;
        }
    }
}
