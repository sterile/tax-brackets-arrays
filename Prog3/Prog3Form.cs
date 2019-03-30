/*
 * Grading ID: M5477
 * Program: 3
 * Due Date: Mar 30 2019
 * Course Section: 01
 */

// This application calculates the marginal tax rate and
// tax due for filers in 2019 tax year.

// Version 3
// Listens to radio buttons to apply income thresholds
// Calculates tax due through running total with each
// tier's portion of the tax.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prog3
{
    public partial class Prog3Form : Form
    {
        // Tax Year 2019 Data
        // Taxable income thresholds for each status
        // Single Filers
        private const int SINGLE1 = 9700;   // 1st single threshold (LOWEST)
        private const int SINGLE2 = 39475;  // 2nd single threshold
        private const int SINGLE3 = 84200;  // 3rd single threshold
        private const int SINGLE4 = 160725; // 4th single threshold
        private const int SINGLE5 = 204100; // 5th single threshold
        private const int SINGLE6 = 510300; // 6th single threshold (HIGHEST)

        //Married Filing Separately
        private const int SEPARATELY1 = 9700;   // 1st married-separately threshold (LOWEST)
        private const int SEPARATELY2 = 39475;  // 2nd married-separately threshold
        private const int SEPARATELY3 = 84200;  // 3rd married-separately threshold
        private const int SEPARATELY4 = 160725; // 4th married-separately threshold
        private const int SEPARATELY5 = 204100; // 5th married-separately threshold
        private const int SEPARATELY6 = 306175; // 6th married-separately threshold (HIGHEST)

        // Married Filing Jointly
        private const int JOINTLY1 = 19400;  // 1st married-jointly threshold (LOWEST)
        private const int JOINTLY2 = 78950;  // 2nd married-jointly threshold
        private const int JOINTLY3 = 168400; // 3rd married-jointly threshold
        private const int JOINTLY4 = 321450; // 4th married-jointly threshold
        private const int JOINTLY5 = 408200; // 5th married-jointly threshold
        private const int JOINTLY6 = 612350; // 6th married-jointly threshold (HIGHEST)

        // Head of Household
        private const int HOH1 = 13850;  // 1st head of household threshold (LOWEST)
        private const int HOH2 = 52850;  // 2nd head of household threshold
        private const int HOH3 = 84200;  // 3rd head of household threshold
        private const int HOH4 = 160700; // 4th head of household threshold
        private const int HOH5 = 204100; // 5th head of household threshold
        private const int HOH6 = 510300; // 6th head of household threshold (HIGHEST)

        // Income threshold values that apply to this filer
        private int threshold1; // 1st income threshold
        private int threshold2; // 2nd income threshold
        private int threshold3; // 3rd income threshold
        private int threshold4; // 4th income threshold
        private int threshold5; // 5th income threshold
        private int threshold6; // 6th income threshold

        public Prog3Form()
        {
            InitializeComponent();
        }

        // User has clicked the Calculate Tax button
        // Will calculate and display their marginal tax rate and tax due
        private void calcTaxBtn_Click(object sender, EventArgs e)
        {
            // Tax Year 2019 Data
            // The marginal tax rates
            const decimal RATE1 = .10m; // 1st tax rate (LOWEST)
            const decimal RATE2 = .12m; // 2nd tax rate
            const decimal RATE3 = .22m; // 3rd tax rate
            const decimal RATE4 = .24m; // 4th tax rate
            const decimal RATE5 = .32m; // 5th tax rate
            const decimal RATE6 = .35m; // 6th tax rate
            const decimal RATE7 = .37m; // 7th tax rate (HIGHEST)

            decimal[] rates = {RATE2, RATE3, RATE4, RATE5, RATE6, RATE7 }; // An array of tax rates (except RATE1 since it isn't used in the loop)

            int income; // Filer's taxable income (input)

            decimal marginalRate; // Filer's calculated marginal tax rate
            decimal tax;          // Filer's calculated income tax due
            decimal currentTax;   // Filer's tax for current tier

            if (int.TryParse(incomeTxt.Text, out income) && income >= 0)
            {
                // Calculate income tax due and find their marginal rate
                // Uses running total approach
                // Math.Min returns the smaller of two values

                int[] thresholds = { threshold1, threshold2, threshold3, threshold4, threshold5, threshold6 }; // Array of thresholds set by 

                marginalRate = RATE1; // Assume lowest rate
                tax = Math.Min(income - 0, thresholds[0] - 0) * RATE1; // Always some from Tier 1

                for (int x=0; x < thresholds.Length; ++x)
                {
                    if (income > thresholds[x])
                    {
                        if (x != thresholds.Length - 1)
                        {
                            marginalRate = rates[x];
                            currentTax = Math.Min((income - thresholds[x]), (thresholds[x + 1] - thresholds[x])) * rates[x];
                            tax += currentTax;
                        }
                        else
                        {
                            marginalRate = rates[x];
                            currentTax = (income - thresholds[x]) * rates[x];
                            tax += currentTax;
                        }
                    }
                }

                // Output results
                marginalRateOutLbl.Text = $"{marginalRate:P0}";
                taxOutLbl.Text = $"{tax:C}";
            }
            else // Invalid input
                MessageBox.Show("Enter valid income!");
        }

        // Form is loading
        // Sets default filing status as Single
        private void Prog2Form_Load(object sender, EventArgs e)
        {
            singleRdoBtn.Checked = true; // Choose single by default
                                         // Will raise CheckedChanged event
        }

        // User has checked/unchecked Single radio button
        // Updates income thresholds
        private void singleRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (singleRdoBtn.Checked) // Single?
            {
                threshold1 = SINGLE1;
                threshold2 = SINGLE2;
                threshold3 = SINGLE3;
                threshold4 = SINGLE4;
                threshold5 = SINGLE5;
                threshold6 = SINGLE6;
            }
        }

        // User has checked/unchecked Married Filing Separately radio button
        // Updates income thresholds
        private void separatelyRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (separatelyRdoBtn.Checked) // Married Filing Separately?
            {
                threshold1 = SEPARATELY1;
                threshold2 = SEPARATELY2;
                threshold3 = SEPARATELY3;
                threshold4 = SEPARATELY4;
                threshold5 = SEPARATELY5;
                threshold6 = SEPARATELY6;
            }
        }

        // User has checked/unchecked Married Filing Jointly radio button
        // Updates income thresholds
        private void jointlyRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (jointlyRdoBtn.Checked) // Married Filing Jointly?
            {
                threshold1 = JOINTLY1;
                threshold2 = JOINTLY2;
                threshold3 = JOINTLY3;
                threshold4 = JOINTLY4;
                threshold5 = JOINTLY5;
                threshold6 = JOINTLY6;
            }
        }

        // User has checked/unchecked Head of Household radio button
        // Updates income thresholds
        private void headOfHouseRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (headOfHouseRdoBtn.Checked) // Head of Household?
            {
                threshold1 = HOH1;
                threshold2 = HOH2;
                threshold3 = HOH3;
                threshold4 = HOH4;
                threshold5 = HOH5;
                threshold6 = HOH6;
            }
        }
    }
}
